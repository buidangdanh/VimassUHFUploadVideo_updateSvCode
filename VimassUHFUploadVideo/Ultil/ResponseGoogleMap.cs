using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Ultil
{
    internal class ResponseGoogleMap
    {
        public int msgCode { get; set; }
        public String msgContent { get; set; }

        public String next_page_token { get; set; }
        public Object result { get; set; }
        public double total { get; set; }
        public double totalMoney { get; set; }
        public int total2 { get; set; }
    }
}
