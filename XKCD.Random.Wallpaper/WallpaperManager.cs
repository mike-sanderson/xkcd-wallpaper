using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Timers;
using System.Windows;

namespace XKCD.Random.Wallpaper
{
    internal sealed class WallpaperManager : IDisposable
    {
        private const string RandomXkcdComic = @"http://c.xkcd.com/random/comic/";
        private const string XkcdComicUrlFormat = @"http://xkcd.com/{0}/";
        private const string ComicUrlRegex = @"Permanent link to this comic: http://xkcd.com/(?<comicNumber>[\d]+)/";
        private const string ComicUrlRegexCaptureGroupName = "comicNumber";
        private const string ImageUrlRegex = @"http://imgs.xkcd.com/comics/[\w_-]+.(png|jpg)";
        private static double maxHeight = SystemParameters.WorkArea.Height;
        private static double maxWidth = SystemParameters.WorkArea.Width;
        private readonly Timer _timer;
        private static string _comicUrl;

        public WallpaperManager()
        {
            _timer = new Timer(TimeSpan.FromMinutes(15).TotalMilliseconds);
            _timer.Elapsed += (s,e) => NextRandomComic();
            SystemEvents.PowerModeChanged += OnPowerChanged;
            _timer.Start();
            NextRandomComic();
        }

        public static void NextRandomComic()
        {
            bool success = false;
            int i = 0;
            string imageUrl = string.Empty;
            string randomComicHtml = string.Empty;

            while (!success && i++ <= 100)
            {
                randomComicHtml = HtmlUtility.GetHtmlFromUrl(RandomXkcdComic);
                if (!string.IsNullOrEmpty(randomComicHtml))
                {
                    imageUrl = HtmlUtility.RegexHtmlCapture(randomComicHtml, ImageUrlRegex);
                }
                if(!string.IsNullOrEmpty(imageUrl))
                {
                    var image = HtmlUtility.GetImageFromUrl(imageUrl);
                    success = image.Height <= maxHeight && image.Width <= maxWidth;
                }
            }
            if (!success)
            {
                Debug.WriteLine(string.Format("Failed to retrieve image url after {0} attempts.", i));
            }
            else
            {
                Debug.WriteLine(string.Format("Succeeded in retrieving image url {0} after {1} attempts.", imageUrl, i));
                var comicNumber = HtmlUtility.RegexHtmlCapture(randomComicHtml, ComicUrlRegex, ComicUrlRegexCaptureGroupName);
                _comicUrl = string.Format(XkcdComicUrlFormat, comicNumber);
                Wallpaper.Set(new Uri(imageUrl));
            }
        }

        public static void OpenCurrentInBrowser()
        {
            Process.Start(_comicUrl);
        }

        public void Dispose()
        {
            _timer.Stop();
            _timer.Dispose();
            SystemEvents.PowerModeChanged -= OnPowerChanged;
        }

        void OnPowerChanged(object sender, PowerModeChangedEventArgs e)
        {
            switch(e.Mode)
            {
                case PowerModes.Suspend:
                    _timer.Stop();
                    break;
                case PowerModes.Resume:
                    _timer.Start();
                    break;
            }
        }
    }
}
