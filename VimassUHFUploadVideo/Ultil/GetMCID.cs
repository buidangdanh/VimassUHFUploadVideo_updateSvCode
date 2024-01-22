using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Ultil
{
    internal class GetMCID
    {
        public String sdt;
        public long currentTime;

        public String cks; // md5: currentTime + "99CkMsx2&Ata8mBE" + sdt

        public int limmit;
        public int offset;

        public int typeDevice; 			// Tat ca: 100 // active: 1 // chua active = 0
    }
}
