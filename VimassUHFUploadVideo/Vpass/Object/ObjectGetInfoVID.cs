using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Vpass.Object
{
    public class ObjectGetInfoVID
    {
        public String mcID { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public int typeFace { get; set; }

        public String textSearch { get; set; }
    }
}
