using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Ultil
{
    internal class CreatEPCNewClass
    {
        public String L;   // trong file word d gui
        public String X; // tùy thuộc vào giá trị trường L: 	Biển số xe || Số điện thoại (bỏ 0 ở đầu) || Hoặc là dãy số do đơn vị tự định nghĩa

        public String TID;
        public long currentTime;
        public String cks; // chu in thuong // md5 : X + currentTime + Pa87L7LdLwocks&5 + TID + sdt 
        public String sdt;
        public String mcID;

    }
}
