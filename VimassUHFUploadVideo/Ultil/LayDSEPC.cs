using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Ultil
{
    internal class LayDSEPC
    {
        public String sdt;
        public String mcID;
        public long currentTime;
        public String cks;

        public int typeS = 1; // md5: mcID + currentTime + "Y+6Mxt&Tta8mBE" // chữ in thường.
    }
}
