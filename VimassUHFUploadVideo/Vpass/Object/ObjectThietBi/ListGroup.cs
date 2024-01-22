using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Vpass.Object.ObjectThietBi
{
    public class ListGroup
    {
        public string id { get; set; }
        public string groupName { get; set; }
        public string mcID { get; set; }
        public long timeTao { get; set; }
        public string userTao { get; set; }
        public long timeSua { get; set; }
        public string userSua { get; set; }
        public string mess { get; set; }
        public int groupLevel { get; set; }
        public List<ListGroup> listGr { get; set; }
        public int type { get; set; }
    }
}
