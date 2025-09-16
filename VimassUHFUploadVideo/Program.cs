using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using VimassUHFUploadVideo;
using VimassUHFUploadVideo.Vpass;
using VimassUHFUploadVideo.Vpass.GiaoDien;

namespace Onvif.IP.Camera.Viewer
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
            Application.Run(new NhanDienTinhMachLongBanTay());
        }
    }
}
