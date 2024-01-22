using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Vpass.Object
{
    public class ObjectgetMayChuDonVi
    {
        public String user { get; set; }
        public int perNum { get; set; } // 1,2,3,4 neu la the V
        public String mcID { get; set; }
        public long currentTime { get; set; }
        public String cks { get; set; } // md5: "Y99JAuGfmYaBYYyycsLy26" + mcID + currentTime;

        public String deviceID { get; set; } // neu truyen chi lay theo deviceID
        public int typeDevice { get; set; } // neu deviceID = null thi loc theo typeDevice.  truyen 0: ALL 	| 	typeDevice = 1|2 : V1,V2,..    |   typeDevice = 3,4 : I1,I2,...		| typeDevice = 5,6,7,8 : A1,A2,..

    }
}
