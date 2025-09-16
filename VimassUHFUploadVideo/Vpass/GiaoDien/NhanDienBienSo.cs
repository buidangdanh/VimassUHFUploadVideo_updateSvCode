using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Onvif;
using VisioForge.Core.ONVIF;
using VimassUHFUploadVideo.Vpass.Object.ObjectNhanDienBienSo;
using sun.net.www.http;
using HttpClient = System.Net.Http.HttpClient;

namespace VimassUHFUploadVideo.Vpass.GiaoDien
{
    public partial class NhanDienBienSo : Form
    {
        public NhanDienBienSo()
        {
            InitializeComponent();
        }

        private async void NhanDienBienSo_Load(object sender, EventArgs e)
        {

            try
            {
                textBox4.Text = "192.168.1.198";
                textBox5.Text = "admin";
                textBox6.Text = "Vimass6868";
            }
            catch (Exception ex)
            {
                MessageBox.Show("NhanDienBienSo_Load_Exception_"+ ex.Message);
            }
           

        }
        // Hàm tạo HttpClient với Digest Authentication
        private static HttpClient CreateHttpClient(string ip, string username, string password)
        {
            // Tạo một handler cho HttpClient để cấu hình Digest Authentication
            var handler = new HttpClientHandler()
            {
                Credentials = new NetworkCredential(username, password)
            };

            // Tạo HttpClient với handler được cấu hình
            HttpClient client = new HttpClient(handler);
            client.BaseAddress = new Uri("http://" + ip);
            return client;
        }

        // Hàm gửi yêu cầu GET và nhận phản hồi từ camera
        private async Task<string> SendGetRequest(string ip, string url, HttpClient httpClient)
        {
            try
            {
                // Gửi yêu cầu GET và nhận phản hồi
                HttpResponseMessage response = await httpClient.GetAsync(url);

                // Đảm bảo phản hồi thành công
                response.EnsureSuccessStatusCode();

                // Đọc nội dung trả về
                string responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
            catch (Exception ex)
            {
                // In thông báo lỗi nếu có
                return "Không thể kết nối đến camera: " + ex.Message;
            }
        }

        // Sự kiện Button để gửi yêu cầu và hiển thị kết quả
        private async void btnGetCameraInfo_Click(object sender, EventArgs e)
        {
            string ip = "192.168.1.198"; // IP của camera
            string username = "admin";  // Tên người dùng
            string password = "Vimass6868"; // Mật khẩu của camera
            string url = "/ISAPI/System/deviceInfo";  // Endpoint để lấy thông tin thiết bị

            // Tạo HttpClient với Digest Authentication
            HttpClient httpClient = CreateHttpClient(ip, username, password);

            // Gửi yêu cầu GET và nhận phản hồi
            string response = await SendGetRequest(ip, url, httpClient);

            // Hiển thị kết quả trên form

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string ip = "192.168.1.198"; // IP của camera
            string username = "admin";  // Tên người dùng
            string password = "Vimass6868"; // Mật khẩu của camera
            string url = "/ISAPI/System/deviceInfo";  // Endpoint để lấy thông tin thiết bị

            // Tạo HttpClient với Digest Authentication
            HttpClient httpClient = CreateHttpClient(ip, username, password);

            // Gửi yêu cầu GET và nhận phản hồi
            string response = await SendGetRequest(ip, url, httpClient);

            // Hiển thị kết quả trên form

        }
        public static DangNhapCamera dangNhapCamera;

        private void button3_Click(object sender, EventArgs e)
        {
            dangNhapCamera = new DangNhapCamera();
            if (!string.IsNullOrEmpty(textBox4.Text))
            {
                dangNhapCamera.ip = textBox4.Text.Trim();
            }
            else
            {
                MessageBox.Show("Thiếu thông số IP");
            }

            if (!string.IsNullOrEmpty(textBox5.Text))
            {
                dangNhapCamera.taiKhoan = textBox5.Text.Trim();
            }
            else
            {
                MessageBox.Show("Thiếu thông số tài khoản");
            }

            if (!string.IsNullOrEmpty(textBox6.Text))
            {
                dangNhapCamera.matKhau = textBox6.Text.Trim();
            }
            else
            {
                MessageBox.Show("Thiếu thông số mật khẩu");
            }

            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                dangNhapCamera.key = textBox1.Text.Trim();
            }
            else
            {
                MessageBox.Show("Thiếu thông số key");
            }

            if (!string.IsNullOrEmpty(dangNhapCamera.ip) && !string.IsNullOrEmpty(dangNhapCamera.taiKhoan) && !string.IsNullOrEmpty(dangNhapCamera.matKhau) && !string.IsNullOrEmpty(dangNhapCamera.key))
            {
                new NhanDienBienSoLive().Show();
            }

        }
    }
}
