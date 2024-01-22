using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Ultil
{
    internal class EPCClone
    {
        public String dataEPCClone;
        public String dataTID;

        public long currentTime;
        public String cks; // md5 : currentTime + "YFF87L7LdLcks&5"  + sdt + mcID // chữ in thường
        public String sdt;

        public String mcID;
    }
}
