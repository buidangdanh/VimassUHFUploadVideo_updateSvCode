using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Affliate.Object
{
    public class DangNhapRequest
    {
        public String token { get; set; }
        public String physical { get; set; }
        public long currentTime { get; set; }
        public String sdt { get; set; }
        public String name { get; set; }
    }
}
