using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Ultil
{
    internal class CanhBao
    {
        public String idThietBi { get; set; }
        public int maDichVu { get; set; }
        public long curentTime { get; set; }

        public String cks { get; set; }  // MD5: "RAuJyCYRZfL$Ya5S" + curentTime + idThietBi + maDichVu;
    }
}
