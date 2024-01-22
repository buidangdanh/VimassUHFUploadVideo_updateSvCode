using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Vpass.Object
{
    public class ObjectThongKeTraVeChiTiet
    {
        public String maGD { get; set; }
        public String vID { get; set; }
        public String loiRa { get; set; }
        public String diaChi { get; set; }
        public String phone { get; set; }
        public String accName { get; set; }
        public String idQR { get; set; }
        public String tongThoiGian { get; set; }
        public long thoiGianDen { get; set; }
        public long thoiGianVe { get; set; }
        public int typeXacThuc { get; set; }
        public List<ObjectHisOnday> hisOnDay { get; set; }
    }
}
