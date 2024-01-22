using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Vpass.Object.ObjectThietBi
{
    public class RootQR
    {
        public string id { get; set; }
        public string mcID { get; set; }
        public long timeTao { get; set; }
        public string catID { get; set; }
        public string theDaNangLK { get; set; }
        public Infor infor { get; set; }
        public List<ListLockDevice> listLockDevice { get; set; }
        public List<ListGroup> listGroup { get; set; }
    }
}
