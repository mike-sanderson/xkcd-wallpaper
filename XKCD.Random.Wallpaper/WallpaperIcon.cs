using System;
using System.Windows.Forms;
using XKCD.Random.Wallpaper.Properties;

namespace XKCD.Random.Wallpaper
{
    internal sealed class WallpaperIcon : IDisposable
    {
        private readonly NotifyIcon _notifyIcon;
        private readonly WallpaperManager _wallpaperManager;

        public WallpaperIcon()
        {
            _notifyIcon = new NotifyIcon();
            _wallpaperManager = new WallpaperManager();
        }

        public void Dispose()
        {
            _notifyIcon.Dispose();
            _wallpaperManager.Dispose();
        }

        public void Display()
        {
            _notifyIcon.MouseClick += Icon_MouseClick;
            _notifyIcon.Icon = Resources.xkcd;
            _notifyIcon.Text = "Random xkcd Desktop Wallpaper";
            _notifyIcon.Visible = true;

            _notifyIcon.ContextMenuStrip = new XKCDContextMenu().Create();
        }

        void Icon_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                WallpaperManager.NextRandomComic();
            }
        }
    }
}
