using OpenQA.Selenium.DevTools.V108.DOM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using VimassUHFUploadVideo.Affliate;
using VimassUHFUploadVideo.Vpass;
using VimassUHFUploadVideo.Vpass.GiaoDien;
namespace VimassUHFUploadVideo
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new VpassLogin() );

        }
    }
}
