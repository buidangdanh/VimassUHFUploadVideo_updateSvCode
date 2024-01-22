using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Vpass.Object
{
    public class ObjectThongKeRequest
    {
        public long from { get; set; }
        public long to { get; set; }
        public int offset { get; set; }
        public int limit { get; set; }
        public String textSearch { get; set; }
        public int typeXacThuc { get; set; }

    }
}
