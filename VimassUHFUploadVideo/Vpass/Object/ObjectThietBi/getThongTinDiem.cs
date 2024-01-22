using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Vpass.Object.ObjectThietBi
{
    public class getThongTinDiem
    {
        public String user { get; set; }
        public int perNum { get; set; } // 1,2,3,4 neu la the V
        public String mcID { get; set; }
        public long currentTime { get; set; }
        public String cks { get; set; } // md5: "Y99JAuGfmYaBYYyycsLy26" + mcID + currentTime;

        public String catID { get; set; } // "" || null lay tat ca
        public bool theLK { get; set; } // loc nhung diem cua don vi co lk voi the da nang
    }
}
