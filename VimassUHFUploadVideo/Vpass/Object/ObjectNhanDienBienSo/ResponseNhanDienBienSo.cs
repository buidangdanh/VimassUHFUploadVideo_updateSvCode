using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Vpass.Object.ObjectNhanDienBienSo
{
    public class ResponseNhanDienBienSo
    {
        public double processing_time { get; set; }
        public List<Result> results { get; set; }
        public string filename { get; set; }
        public int version { get; set; }
        public object camera_id { get; set; }
        public DateTime timestamp { get; set; }
        public int image_width { get; set; }
        public int image_height { get; set; }
    }
}
