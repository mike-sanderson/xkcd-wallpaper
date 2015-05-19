using System.Windows.Forms;

namespace XKCD.Random.Wallpaper
{
    internal sealed class XKCDContextMenu
    {
        public ContextMenuStrip Create()
        {
            var menu = new ContextMenuStrip();
            ToolStripMenuItem item;
            ToolStripSeparator separator;

            item = new ToolStripMenuItem();
            item.Text = "Next Comic";
            item.Click += (s, e) => WallpaperManager.NextRandomComic();
            menu.Items.Add(item);

            item = new ToolStripMenuItem();
            item.Text = "View in Browser";
            item.Click += (s, e) => WallpaperManager.OpenCurrentInBrowser();
            menu.Items.Add(item);

            separator = new ToolStripSeparator();
            menu.Items.Add(separator);

            item = new ToolStripMenuItem();
            item.Text = "Exit";
            item.Click += (s, e) => Application.Exit();
            menu.Items.Add(item);

            return menu;
        }
    }
}
