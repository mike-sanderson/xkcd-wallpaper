using System;
using System.Windows.Forms;

namespace XKCD.Random.Wallpaper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            using (var icon = new WallpaperIcon())
            {
                icon.Display();
                Application.Run();
            }
        }
    }
}
