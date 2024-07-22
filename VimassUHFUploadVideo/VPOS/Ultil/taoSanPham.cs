using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.VPOS.Ultil
{
    public class taoSanPham
    {
        public int funcId { get; set; }
        public string sessionLogin { get; set; }
        public string idLogin { get; set; }
        public string token { get; set; }
        public int typeLogin { get; set; }
        public string companyCode { get; set; }
        public long timeRequest { get; set; }
        public double gia { get; set; }
        public double giaGiam { get; set; }
        public string idStore { get; set; }
        public string idCate { get; set; }
        public String productName { get; set; }
        public string image { get; set; }
        public string thumbnail { get; set; }
        public string description { get; set; }
        public string cksInput { get; set; }
    }
}
