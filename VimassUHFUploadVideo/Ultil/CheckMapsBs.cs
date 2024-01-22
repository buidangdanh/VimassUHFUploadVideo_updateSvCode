using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Ultil
{
    internal class CheckMapsBs
    {
        public String sdt;
        public long curentTime;

        public String TID;
        public String EPC;

        public String cks;  // md5: TID + "99v+6Myt&Tta8mBE"  + curentTime + EPC;  // chữ in thường.
    }
}
