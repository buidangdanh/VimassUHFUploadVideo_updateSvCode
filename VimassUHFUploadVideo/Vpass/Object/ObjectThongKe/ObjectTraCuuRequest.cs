using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Vpass.Object.ObjectThongKe
{
    public class ObjectTraCuuRequest
    {
        public long from { get; set; }
        public long to { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public String vID { get; set; }
        public String phone { get; set; }
        public String idThietBi { get; set; }
        public int personNumber { get; set; }
        public String mcID { get; set; }

    }
}
