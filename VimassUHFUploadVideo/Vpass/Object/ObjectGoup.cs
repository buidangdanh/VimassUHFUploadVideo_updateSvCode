using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VimassUHFUploadVideo.Ultil;

namespace VimassUHFUploadVideo.Vpass.Object
{
    public class ObjectGoup
    {
        public String id { get; set; }
        public String groupName { get; set; }
        public string mcID { get; set; }

        public long timeTao { get; set; }
        public String userTao { get; set; }
        public long timeSua { get; set; }
        public String userSua { get; set; }
        public String mess { get; set; }
        public int groupLevel { get; set; }
        public int type { get; set; }
        public List<ObjectGoup> listGr { get; set; }
        public List<ObjListPer> listPer { get; set; }
       
    }
}
