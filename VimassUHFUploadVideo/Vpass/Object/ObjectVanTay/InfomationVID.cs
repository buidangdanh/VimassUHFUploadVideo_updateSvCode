using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Vpass.Object.ObjectVanTay
{
    public class InfomationVID
    {
        public String id { get; set; } // = idVid + personPosition
        public String idVid { get; set; }
        public String uID { get; set; }
        public String maSoThue { get; set; }
        public String diaChi { get; set; }
        public String dienThoai { get; set; }
        public String email { get; set; }
        public String gioiTinh { get; set; }
        public String hoTen { get; set; }
        public String ngayCapCCCD { get; set; }
        public String ngayHetHanCCCD { get; set; }
        public String ngaySinh { get; set; }
        public String quocTich { get; set; }
        public String soCanCuoc { get; set; }
        public String soTheBHYT { get; set; }
        public String tk { get; set; }
        public String anhDaiDien { get; set; }
        public String anhCMNDMatTruoc { get; set; }
        public String anhCMNDMatSau { get; set; }
        public string faceData { get; set; }
        public String personName { get; set; }
        public String chucDanh { get; set; }
        public int personPosition { get; set; }
        public String groupID { get; set; }
        public String mcID { get; set; }
        public String face { get; set; }
        public String cksFaceOfVid { get; set; }

        public List<FingerData> fingerData { get; set; }
    }
}
