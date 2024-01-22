using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VimassUHFUploadVideo.Ultil;

namespace VimassUHFUploadVideo
{
    public partial class LayChamCong : Form
    {
        public LayChamCong()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            layThoiGianChamCong(textBox1.Text);


        }
        public static void layThoiGianChamCong(string vid)
        {
            DateTime utcDateTime = DateTime.UtcNow;
            string vnTimeZoneKey = "SE Asia Standard Time";
            TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
            DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
            long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();
            ParamLayChamCong u = new ParamLayChamCong();
            u.vid = vid;
            u.personNumber = 0;
            u.status = 1;
            u.fromDate = 1690822800000;
            u.toDate = 1693501140000;
            u.curentTime = yourDateTimeMilliseconds;
            u.limmit = 100;
            u.typeXacThuc = 2;
            u.offset = 0;
            u.sdt = "0353465132";
            u.cks = FunctionGeneral.Md5("SafFPMPKCjauZ%Ma" + u.sdt + u.curentTime).ToLower();
            string url = "http://210.245.8.7:12318/vimass/services/VUHF/layDanhSachYeuCauXacThuc";
            var json = JsonConvert.SerializeObject(u);
            String res = Service.SendWebrequest_POST_Method(json, url);
            Response response = JsonConvert.DeserializeObject<Response>(res);

            Debug.WriteLine(res);
        }
    }
}
