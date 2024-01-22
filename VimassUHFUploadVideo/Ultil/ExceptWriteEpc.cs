using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Ultil
{
    internal class ExceptWriteEpc
    {
        public String EPC;
        public long currentTime;
        public String cks; // chu in thuong // md5:  currentTime + PalSWXwDwJd2U + EPC+ sdt 
        public String sdt;
    }
}
