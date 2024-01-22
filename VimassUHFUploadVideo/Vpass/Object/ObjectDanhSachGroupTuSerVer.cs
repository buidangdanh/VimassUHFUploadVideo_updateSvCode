using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Vpass.Object
{
    public class ObjectDanhSachGroupTuSerVer
    {
        public String user { get; set; } // sdt || Vxxx
        public int perNum { get; set; } // 1,2,3,4 neu la the V

        public String mcID { get; set; }
        public String cks { get; set; } //md5: user + deviceID + "ZgVCHxqMd$aNCm54X2YHD" + currentTime + mcID
        public long currentTime { get; set; }

        public int deviceID { get; set; } //Androi 1 | IOS 2 |
    }
}
