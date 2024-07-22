using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.VPOS.Ultil
{
    public class ResponseBHX
    {
        public int msgCode { get; set; }
        public string msgContent { get; set; }
        public ResultBHX result { get; set; }
        public int total { get; set; }
        public string cksInput { get; set; }
        public int adminLevel { get; set; }
        public int tongDiem { get; set; }
        public int tienQuyDoiDiem { get; set; }
    }
}
