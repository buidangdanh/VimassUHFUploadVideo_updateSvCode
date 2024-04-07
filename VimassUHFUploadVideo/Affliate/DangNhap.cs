using com.sun.org.apache.xml.@internal.resolver.helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Windows.Forms;
using VimassUHFUploadVideo.Affliate.Object;
using VimassUHFUploadVideo.Vpass.Object;
using System.Diagnostics;
using VimassUHFUploadVideo.Ultil;

namespace VimassUHFUploadVideo.Affliate
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != null && !richTextBox1.Text.Equals(""))
            {
                DangNhaps();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập token");
            }


        }

        public void DangNhaps()
        {
            try
            {
                ObjectGoiDichVuMini o = new ObjectGoiDichVuMini();
                o.funcId = 133;
                o.device = 2;
                o.currentime = FunCGeneral.timeNow();

                DangNhapRequest oLDSGR = new DangNhapRequest();
                oLDSGR.token = richTextBox1.Text;
                oLDSGR.physical = layPhysicalCuaMay();
                oLDSGR.currentTime = FunCGeneral.timeNow();
                if (richTextBox2.Text != null && !richTextBox2.Text.Equals(""))
                {
                    oLDSGR.sdt = richTextBox2.Text;
                }
                if (richTextBox3.Text != null && !richTextBox3.Text.Equals(""))
                {
                    oLDSGR.name = richTextBox3.Text;
                }

                if (oLDSGR.sdt!=null&&oLDSGR.name!=null&&!oLDSGR.sdt.Equals("") && !oLDSGR.name.Equals("")) {
                    // Truy cập TextBox từ form
                    o.data = JsonConvert.SerializeObject(oLDSGR);

                    String url = "http://192.168.1.199:58080/transfer/services/vimassTool/dieuPhoi";
                    var json = JsonConvert.SerializeObject(o);
                    String res = Service.SendWebrequest_POST_Method(json, url);
                    Response response = JsonConvert.DeserializeObject<Response>(res);

                    if (response != null)
                    {
                        String valueTraVe = FunctionGeneral.DecodeBase64String(response.msgContent);
                        MessageBox.Show(valueTraVe);
                        if (response.msgCode == 1)
                        {
                            new ControllerTikTok().Show();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ số điện thoại và họ tên");
                }
               
            }
            catch (Exception ex)
            {

            }

        }

        public static string layPhysicalCuaMay()
        {
            String kq = "";
            try
            {
                foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet && ni.OperationalStatus == OperationalStatus.Up)
                    {

                        kq = ni.GetPhysicalAddress().ToString();
                        System.Diagnostics.Debug.WriteLine("layPhysicalCuaMay: " + kq);
                        break; // Thoát khỏi vòng lặp sau khi tìm thấy địa chỉ MAC đầu tiên
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return kq;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
