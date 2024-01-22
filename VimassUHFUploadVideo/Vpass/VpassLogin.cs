using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VimassUHFUploadVideo.Vpass.Object;
using VimassUHFUploadVideo.Ultil;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Diagnostics;
using VimassUHFUploadVideo.Vpass.GiaoDien;
using VimassUHFUploadVideo.Vpass.Object;

namespace VimassUHFUploadVideo.Vpass
{
    public partial class VpassLogin : Form
    {
        public VpassLogin()
        {
            InitializeComponent();
        }
        public static Boolean kq = true;
        private void VpassLogin_Load(object sender, EventArgs e)
        {
            this.Text = "Đăng nhập";
            this.MaximizeBox = false; //tắt mở to nhỏ
            this.checkBox1.Text = "Hiển thị mật khẩu";
            radioButton1.Text = "Điện thoại";
            radioButton2.Text = "Thẻ";
            button1.Text = "Đăng nhập";
            label2.Text = "";
            this.button1.Location = new System.Drawing.Point((this.ClientSize.Width - button1.Width) / 2, button1.Location.Y);
            button1.Cursor = Cursors.Hand;
            //richTextBox
            this.richTextBox3.Visible = false;
            this.richTextBox4.Visible = false;

        }


        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime utcDateTime = DateTime.UtcNow;
            string vnTimeZoneKey = "SE Asia Standard Time";
            TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
            DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
            long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();
            try
            {
                Logger.LogServices("Dang nhap");
                if (kq)
                {
                    //Đăng nhập bằng số điện thoại
                    if (this.richTextBox1.Text.Trim() != null && !this.richTextBox1.Text.Trim().Equals(""))
                    {
                        ObjectLoginSDT oSDT = new ObjectLoginSDT();
                        oSDT.type = 1;
                        oSDT.VimassMH = 0;
                        oSDT.appId = 5;
                        oSDT.user = this.richTextBox1.Text.Trim();
                        oSDT.pass = actualTextMatKhauSDT.Trim();
                        string url = "https://vimass.vn/vmbank/services/account/login1";
                        var json = JsonConvert.SerializeObject(oSDT);
                        String res = Service.SendWebrequest_POST_Method(json, url);
                        Response response = JsonConvert.DeserializeObject<Response>(res);

                        if (response.msgCode == 1)
                        {
                            LoginSDTKetQua lSKQ = JsonConvert.DeserializeObject<LoginSDTKetQua>(response.result.ToString());
                            if(lSKQ != null&&!lSKQ.phone.Equals("") && !lSKQ.acc_name.Equals(""))
                            {
                                FunCGeneral.lGinKQ.phone = lSKQ.phone;
                                FunCGeneral.lGinKQ.hoTen = lSKQ.acc_name;
                                Logger.LogServices("Dang nhap sdt thanh cong: " + FunCGeneral.lGinKQ.phone);
                                this.Hide();
                                FunCGeneral.checkQuyen();
                                VPassMain vPass = new VPassMain();
                                vPass.Show();
                            }
                   
                        }
                        else
                        {
                            MessageBox.Show(response.msgContent);
                        }
                    }

                }
                else
                {  
                    //Đăng nhập bằng số thẻ
                    ObjectLoginThe oLT = new ObjectLoginThe();
                    oLT.vID = this.richTextBox1.Text.Trim();
                    oLT.timeRequest = yourDateTimeMilliseconds;
                    oLT.uID = "";
                    oLT.cksSer = "";
                    oLT.cksCVV = FunctionGeneral.Md5(oLT.vID + "YolyoZgav46545fsj98cNeYatlld54X2" + "" + actualTextCVV + oLT.timeRequest).ToLower();
                    oLT.dataCheckF = FunctionGeneral.Md5("Y4SkpSofFAgsR27fMBM02SNoV9iPI2CBo0ZCImM6EgnfXikl3en3VfWyTwfLLTC883yJC" + oLT.vID + oLT.VMApp + oLT.userLTV + oLT.timeRequest + actualTextMatKhauThe + oLT.perNumber + "" + oLT.TBTTId).ToLower();
                    string url = "http://124.158.4.173:8080/vmbank/services/vIdService/requestComand";
                    var json = JsonConvert.SerializeObject(oLT);
                    String res = Service.SendWebrequest_POST_Method(json, url);
                    Response response = JsonConvert.DeserializeObject<Response>(res);

                    if (response.msgCode == 1)
                    {
                        LoginTheKetQua lSKQ = JsonConvert.DeserializeObject<LoginTheKetQua>(response.result.ToString());
                        if (lSKQ != null && !lSKQ.dienThoai.Equals("") && !lSKQ.hoTen.Equals(""))
                        {
                            FunCGeneral.lGinKQ.phone = lSKQ.dienThoai;
                            FunCGeneral.lGinKQ.soThe = lSKQ.idVid;
                            FunCGeneral.lGinKQ.hoTen = lSKQ.hoTen;
                            Logger.LogServices("Dang nhap the thanh cong: " + FunCGeneral.lGinKQ.soThe);
                            this.Hide();
                            FunCGeneral.checkQuyen();
                            VPassMain vPass = new VPassMain();
                            vPass.Show();
                        }
                       
                    }
                    else
                    {
                        MessageBox.Show(response.msgContent);
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.LogServices("button1_Click Exception: " + ex.Message);

            }
        }
        //Nhấn nút chọn thẻ
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "";
            kq = false;
            this.richTextBox2.Visible = false;
            this.richTextBox3.Visible = true;
            this.richTextBox4.Visible = true;
            this.label3.Text = "Số thẻ";
            this.label1.Text = "Mật khẩu/CVV";

        }
        //Nhấn nút chọn sdt
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "";
            kq = true;
            this.richTextBox2.Visible = true;
            this.richTextBox3.Visible = false;
            this.richTextBox4.Visible = false;
            this.label3.Text = "Số điện thoại";
            this.label1.Text = "Mật khẩu";
        }

        private string actualTextMatKhauSDT = "";
        private string actualTextMatKhauThe = "";
        private string actualTextCVV = "";
        private void richTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar))
            {
                // Thêm ký tự mới vào mật khẩu thực tế
                actualTextMatKhauSDT += e.KeyChar;
                e.Handled = true; // Ngăn không cho ký tự được hiển thị
            }
            else if (e.KeyChar == (char)Keys.Back && actualTextMatKhauSDT.Length > 0)
            {
                // Xóa ký tự cuối cùng khi nhấn phím xóa (backspace)
                actualTextMatKhauSDT = actualTextMatKhauSDT.Substring(0, actualTextMatKhauSDT.Length - 1);
                e.Handled = true;
            }

            // Cập nhật hiển thị của RichTextBox
            richTextBox2.Text = new string('*', actualTextMatKhauSDT.Length);
            richTextBox2.Select(richTextBox2.Text.Length, 0); // Di chuyển con trỏ đến cuối
        }

        private void richTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar))
            {
                // Thêm ký tự mới vào mật khẩu thực tế
                actualTextMatKhauThe += e.KeyChar;
                e.Handled = true; // Ngăn không cho ký tự được hiển thị
            }
            else if (e.KeyChar == (char)Keys.Back && actualTextMatKhauThe.Length > 0)
            {
                // Xóa ký tự cuối cùng khi nhấn phím xóa (backspace)
                actualTextMatKhauThe = actualTextMatKhauThe.Substring(0, actualTextMatKhauThe.Length - 1);
                e.Handled = true;
            }

            // Cập nhật hiển thị của RichTextBox
            richTextBox3.Text = new string('*', actualTextMatKhauThe.Length);
            richTextBox3.Select(richTextBox3.Text.Length, 0); // Di chuyển con trỏ đến cuối

        }

        private void richTextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar))
            {
                // Thêm ký tự mới vào mật khẩu thực tế
                actualTextCVV += e.KeyChar;
                e.Handled = true; // Ngăn không cho ký tự được hiển thị
            }
            else if (e.KeyChar == (char)Keys.Back && actualTextCVV.Length > 0)
            {
                // Xóa ký tự cuối cùng khi nhấn phím xóa (backspace)
                actualTextCVV = actualTextCVV.Substring(0, actualTextCVV.Length - 1);
                e.Handled = true;
            }

            // Cập nhật hiển thị của RichTextBox
            richTextBox4.Text = new string('*', actualTextCVV.Length);
            richTextBox4.Select(richTextBox4.Text.Length, 0); // Di chuyển con trỏ đến cuối

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                richTextBox2.Text = actualTextMatKhauSDT;
                richTextBox3.Text = actualTextMatKhauThe;
                richTextBox4.Text = actualTextCVV;
            }
            else
            {
                richTextBox2.Text = new string('*', actualTextMatKhauSDT.Length);
                richTextBox3.Text = new string('*', actualTextMatKhauThe.Length);
                richTextBox4.Text = new string('*', actualTextCVV.Length);
            }

        }
    }
}
