using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Ultil
{
    internal class ConfimUnknown
    {
        public ArrayList TIDs = new ArrayList();
        public long currentTime;
        public String cks; // md5:  currentTime + "DwJd2UVPalSWXw" + sdt + TIDs.size() // chữ in thường
        public String sdt;
    }
}
