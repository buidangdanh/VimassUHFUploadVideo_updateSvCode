using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VimassUHFUploadVideo.Ultil;

namespace VimassUHFUploadVideo.Vpass.Object
{
    public class ObjectGroupRQServer
    {
        public String user { get; set; }

        public String cks { get; set; } // md5 :user + deviceID + "ZgVCHxqMd$aN11ggg2YHD" + currentTime + mcID;
        public long currentTime { get; set; }
        public int deviceID { get; set; }

        public String mcID { get; set; }
        public List<ObjectGoup> listGr { get; set; }
    }
}
