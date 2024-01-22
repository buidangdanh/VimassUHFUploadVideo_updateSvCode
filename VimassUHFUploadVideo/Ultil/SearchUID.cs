using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Ultil
{
    internal class SearchUID
    {
        public int funcId = 2; //= 2

        public String idVid;
        public String uID;
        public String TID;
        public String EPC;
        public String soThe;
        public int offset = 0;
        public int limit = 100;
        public String dataCheck; //bamMD5("ifafioeqfo" + uID + idVid + TID + funcId + EPC + timeRequest + offset + limit);

        public long timeRequest;
    }
}
