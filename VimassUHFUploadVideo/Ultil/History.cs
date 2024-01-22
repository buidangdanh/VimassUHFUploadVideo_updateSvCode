using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Ultil
{
    internal class History
    {
        public long thoiGianGhiNhan;
        public string idthe;
        public string name;
        public string sothe;
        public string imgBase64;
        public string uCodeID;
        public string uID;
        public int deviceID;        // Android:1, ios:2

        public string idThietBi;
        public int typeDataAuThen;  // FACE = 1	 /	VOICE = 2 /	  FINGER_PRINT = 3 
        public string sdt;

        public string cks;      // MD5: "77a+6Mx2&Ata8mBE" + deviceID + thoiGianGhiNhan + uID + typeDataAuThen;

    }
}
