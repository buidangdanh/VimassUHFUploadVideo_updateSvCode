using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Vpass.Object
{
    public class ObjectGoiDichVuMini
    {
        public int funcId { get; set; }
        public int device { get; set; } // 0-ios, 1-android, 2-pc
        public long currentime { get; set; }
        public String data { get; set; }
    }
}
