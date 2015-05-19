using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace XKCD.Random.Wallpaper
{
    internal static class HtmlUtility
    {
        internal static string RegexHtmlCapture(string html, string regex)
        {
            var match = Regex.Match(html, regex);
            if (match.Captures.Count > 0)
            {
                return match.Captures[0].Value;
            }
            return string.Empty;
        }

        internal static string RegexHtmlCapture(string html, string regex, string captureGroupName)
        {
            var match = Regex.Match(html, regex);
            if (match.Groups.Count > 1)
            {
                return match.Groups[captureGroupName].ToString();
            }
            return string.Empty;
        }

        internal static string GetHtmlFromUrl(string url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);

            using (var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                if(httpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    if(httpWebResponse.CharacterSet == null)
                    {
                        using (var readStream = new StreamReader(httpWebResponse.GetResponseStream()))
                        {
                            return readStream.ReadToEnd();
                        }
                    }
                    else
                    {
                        using (var readStream = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding(httpWebResponse.CharacterSet)))
                        {
                            return readStream.ReadToEnd();
                        }
                    }
                }
            }
            return string.Empty;
        }

        internal static Image GetImageFromUrl(string url)
        {
            System.IO.Stream s = new System.Net.WebClient().OpenRead(url.ToString());

            System.Drawing.Image img = System.Drawing.Image.FromStream(s);
            //img.ResizeImageToFitDesktop();

            s.Close();
            return img;
        }
    }
}
