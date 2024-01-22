using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Vpass.Object
{
    public class Logger
    {
        private const string LogPath = @"D:\VpassLog\"; // Update with your log directory path

        public static void LogServices(string content)
        {
            // Check if the directory exists, if not, create it
            if (!Directory.Exists(LogPath))
            {
                Directory.CreateDirectory(LogPath);
            }

            DateTime now = DateTime.Now;

            string formattedTime = now.ToString("dd-MM-yyyy HH:mm:ss");
            string fileDate = now.ToString("dd_MM_yyyy");
            string hourSegment = GetHourSegment(now.Hour);

            string fileName = $"{fileDate}{hourSegment}.txt";
            string logContent = $"{formattedTime}\t{content}\n";

            File.AppendAllText(LogPath + "VpassViewLog_" + fileName, logContent);
        }

        private static string GetHourSegment(int hour)
        {
            if (hour <= 5)
                return "_0_5";
            if (hour <= 8)
                return "_6_8";
            if (hour <= 10)
                return "_9_10";
            if (hour <= 12)
                return "_11_12";
            if (hour <= 14)
                return "_13_14";
            if (hour <= 16)
                return "_15_16";
            if (hour <= 18)
                return "_17_18";
            if (hour <= 23)
                return "_19_23";

            return "";
        }
    }
}
