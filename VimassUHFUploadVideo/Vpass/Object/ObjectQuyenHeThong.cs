using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Vpass.Object
{
    public class ObjectQuyenHeThong
    {
        public String user { get; set; } // V123xx || 096652xxxxx // user check quyen
        public long currentTime { get; set; }
        public String cks { get; set; } // md5: "L99JAuGHYaBYYyycsLy26" + currentTime;
    }
}
