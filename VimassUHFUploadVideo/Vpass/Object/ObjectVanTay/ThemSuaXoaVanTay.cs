using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Vpass.Object.ObjectVanTay
{
    public class ThemSuaXoaVanTay
    {
        public int type { get; set; } //0: getData 1: Đăng Ký vân tay 2: Sửa 3: Xóa

        public ObjectFPRequest thongTinNguoi { get; set; }

        public String idFP { get; set; }

        public String nameFP { get; set; } // Tên của vân tay

        public String emptyID { get; set; } // truyền khi type = 2,3
    }
}
