using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VimassUHFUploadVideo.Ultil;

namespace VimassUHFUploadVideo.Vpass.Object.ObjectVanTay
{
    public class ObjFPSua
    {
        public String id { get; set; }
        public String mcID { get; set; }

        public String idDonVi { get; set; }
        public String nameF { get; set; }
        public int totalF { get; set; } // 1k, 3k
        public int currentF { get; set; } // so luong van tay hien co

        public List<String> listIDDiem { get; set; } // id cua QR diem ra vao
        public String IdDeviceVManager { get; set; } // id thiet bi V|I|A. luu y khong phai id cua deviceID

        public int type { get; set; } // 1 them, 2 sua, 3 xoa // k luu db. trang thai de cap nhat khi client gọi dv
    }
}
