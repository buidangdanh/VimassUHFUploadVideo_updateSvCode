using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Vpass.Object.ObjectThietBi
{
    public class Device
    {
        public string id { get; set; }
        public string mcID { get; set; }
        public string desDevice { get; set; }
        public double storage { get; set; }
        public int typeDevice { get; set; }
        public string portD { get; set; }
        public int function { get; set; }
        public string ip { get; set; }
        public List<ListDiem> listDiem { get; set; }
        public string deviceID { get; set; }
        public int stt { get; set; }
        public int timeTao { get; set; }
        public int timeSua { get; set; }
    }
}
