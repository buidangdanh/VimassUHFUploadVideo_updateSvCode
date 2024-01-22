using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Ultil
{
    internal class SearchDelete
    {
        public int funcId = 3;//3
        public String ma;
        public String user;
        public String token;
        public String dataCheck;//bamMD5("ifafioeqfo" + funcId + ma + timeRequest + token + user);

        public long timeRequest;
    }
}
