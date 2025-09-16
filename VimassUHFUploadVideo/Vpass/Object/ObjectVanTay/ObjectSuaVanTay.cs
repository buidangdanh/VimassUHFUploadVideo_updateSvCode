using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VimassUHFUploadVideo.Ultil;

namespace VimassUHFUploadVideo.Vpass.Object.ObjectVanTay
{
    public class ObjectSuaVanTay
    {
        public String user { get; set; } // sdt || Vxxx
        public int perNum { get; set; } // 1,2,3,4 neu la the V

        public String cks { get; set; } // md5:  user  + "ZgVCHxqMd$aNkk54X2YHD" + currentTime ;
        public long currentTime { get; set; }
        public int deviceID { get; set; } // //Androi 1 | IOS 2 |

        public String mcID { get; set; }

        public List<ObjFPSua> listItem { get; set; }

    }
}
