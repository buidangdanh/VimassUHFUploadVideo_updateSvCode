using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Vpass.Object.ObjectDiemDinhDanh
{
    public class ObjectDsDiemTheoToaDo
    {
        public long timeRequest { get; set; }
        public int VMApp { get; set; }
        public String checkSum { get; set; }

        public String ip { get; set; }
        public double lng { get; set; }
        public double lat { get; set; }
        public double km { get; set; }
        public String catId { get; set; }

        public int funcId { get; set; }

        public String userLTV { get; set; }
        public int trangThai { get; set; }

        public int limit { get; set; }

        public String keySearch { get; set; }

        public int offset { get; set; }

        public String user { get; set; }
    }
}
