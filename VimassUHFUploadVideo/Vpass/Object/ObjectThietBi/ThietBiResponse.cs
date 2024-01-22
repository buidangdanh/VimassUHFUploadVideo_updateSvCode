using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Vpass.Object.ObjectThietBi
{
    public class ThietBiResponse
    {
        public string ip { get; set; }
        public string tenMayChu { get; set; }
        public List<Device> devices { get; set; }
    }
}
