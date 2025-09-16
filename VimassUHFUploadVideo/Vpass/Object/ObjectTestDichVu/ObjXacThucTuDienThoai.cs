using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VimassUHFUploadVideo.Vpass.Object.ObjectVanTay;

namespace VimassUHFUploadVideo.Vpass.Object.ObjectTestDichVu
{
    public class ObjXacThucTuDienThoai
    {
        public int type { get; set; } // 1: xac thuc , 2: chua nghi ra
        public String idFP { get; set; }
        public FingerData fingerData { get; set; }
        public ObjectFPRequest thongTinNguoi { get; set; }
    }
}
