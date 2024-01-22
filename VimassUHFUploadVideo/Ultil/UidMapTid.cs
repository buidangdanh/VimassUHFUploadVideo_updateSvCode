using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Ultil
{
    internal class UidMapTid
    {

        public int funcId = 1; //= 1
        public String idVid = ""; // truyền ""
        public String uID;
        public String TID;
        public String EPC;
        public String soThe;

        public String cks; //bamMD5("ifafioeqfo" + uID + idVid + TID + funcId + EPC + timeRequest);
        public long timeRequest;
    }
}
