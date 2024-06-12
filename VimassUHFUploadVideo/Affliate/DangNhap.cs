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
using System.IO;
using sun.swing;

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
                if (richTextBox4.Text != null && !richTextBox4.Text.Equals(""))
                {
                    oLDSGR.diaChi = richTextBox4.Text;
                }

                if (oLDSGR.sdt != null && oLDSGR.name != null && !oLDSGR.sdt.Equals("") && !oLDSGR.name.Equals(""))
                {
                    // Truy cập TextBox từ form
                    o.data = JsonConvert.SerializeObject(oLDSGR);

                    String url = "http://113.190.248.142:58080/autobank/services/vimassTool/dpDanhVer3";
                    var json = JsonConvert.SerializeObject(o);
                    String res = Service.SendWebrequest_POST_Method(json, url);
                    Response response = JsonConvert.DeserializeObject<Response>(res);

                    if (response != null)
                    {
                        String valueTraVe = FunctionGeneral.DecodeBase64String(response.msgContent);
                        MessageBox.Show(valueTraVe);
                        if (response.msgCode == 1)
                        {
                            luuCacheDangNhap(o.data.Trim().Replace("\a",""));
                            this.Hide();
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

        private void luuCacheDangNhap(String dataLogin)
        {
            try
            {
                string directoryPath = FunctionGeneral.rootPath + "\\TikTokAuto";
                DirectoryInfo directory = new DirectoryInfo(directoryPath);
                directory.Create();
                directory.Attributes = FileAttributes.Hidden;

                string data_MaHoa = FunctionGeneral.maHoa(dataLogin);
                File.WriteAllText(FunctionGeneral.pathLogin, data_MaHoa);

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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DangNhap_Load(object sender, EventArgs e)
        {
            try
            {
                String data_Login = docDuLieuLogin(FunctionGeneral.pathLogin).Trim().Replace("\a", "").Replace("\b","");
                if (data_Login != null && !data_Login.Equals(""))
                {
                    DangNhapRequest objDataLogin = JsonConvert.DeserializeObject<DangNhapRequest>(data_Login);
                    if(objDataLogin.token != null && !objDataLogin.token.Equals(""))
                    {
                        richTextBox1.Text = objDataLogin.token;
                    }
                    if (objDataLogin.sdt != null && !objDataLogin.sdt.Equals(""))
                    {
                        richTextBox2.Text = objDataLogin.sdt;
                    }
                    if (objDataLogin.name != null && !objDataLogin.name.Equals(""))
                    {
                        richTextBox3.Text = objDataLogin.name;
                    }
                    if (objDataLogin.diaChi != null && !objDataLogin.diaChi.Equals(""))
                    {
                        richTextBox4.Text = objDataLogin.diaChi;
                    }
                }


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("DangNhap_Load " + ex.Message);
            }
        }

        private string docDuLieuLogin(string pathLogin)
        {
            String data_Login = "";
            try
            {
                if (File.Exists(pathLogin))
                {
                    // Đọc toàn bộ nội dung file và lưu vào một biến string
                    data_Login = FunctionGeneral.giaiMa(File.ReadAllText(pathLogin));

                    // In nội dung file ra màn hình console
                    Console.WriteLine(data_Login);
                }
                else
                {
                    // In thông báo nếu file không tồn tại
                    Console.WriteLine("File không tồn tại.");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("docDuLieuLogin " + ex.Message);
            }
            return data_Login;
        }
    }
}
