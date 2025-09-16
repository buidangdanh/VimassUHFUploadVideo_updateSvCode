using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VimassUHFUploadVideo.Ultil;
using VimassUHFUploadVideo.Vpass.Object.ObjectThietBi;

namespace VimassUHFUploadVideo.Vpass.Object.ObjectVanTay
{
    public class ObjFP
    {
        public String id { get; set; }
        public String mcID { get; set; }
        public String idDonVi { get; set; }
        public String nameF { get; set; }
        public int totalF { get; set; }
        public int currentF { get; set; }
        public long timeTao { get; set; }
        public long timeSua { get; set; }
        public int type { get; set; }
        public List<ListDiem> listDiem { get; set; }
        public Device deviceV { get; set; }
        public String port { get; set; }
    }
}
