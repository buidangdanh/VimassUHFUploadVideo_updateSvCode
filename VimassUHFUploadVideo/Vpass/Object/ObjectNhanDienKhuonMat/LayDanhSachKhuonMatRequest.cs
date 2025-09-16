using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Vpass.Object.ObjectNhanDienKhuonMat
{
    public class LayDanhSachKhuonMatRequest
    {
        public string idQRgreat { get; set; }
        public int offset { get; set; }
        public int limit { get; set; }
        public string textSearch { get; set; }
    }
}
