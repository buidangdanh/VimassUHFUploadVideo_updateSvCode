using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VimassUHFUploadVideo.Ultil;
using static System.Net.WebRequestMethods;

namespace VimassUHFUploadVideo
{
    public partial class DayAnhLenSerVer : Form
    {
        public DayAnhLenSerVer()
        {
            InitializeComponent();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files(*.jpg;*.jpeg;*.gif;*.bmp;*.png)|*.jpg;*.jpeg;*.gif;*.bmp;*.png";
                openFileDialog.Multiselect = true; // Cho phép chọn nhiều file

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string imagePath in openFileDialog.FileNames)
                    {
                        // Xử lý từng file ảnh ở đây
                        string fileName = Path.GetFileName(imagePath);
                        Console.WriteLine("Đã chọn file ảnh: " + imagePath);
                        textBox4.Text+= fileName+"__________Link:     "+ "https://web.vimass.vn/VimassMedia/services/VMMedia/getImage?id=" + dayLen(imagePath) + "\r\n";
                        // Ví dụ: Chuyển đổi thành Base64, hiển thị, v.v.
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
        private static String dayLen(String pathsd)
        {
            String kq = "";
            try
            {
                upLoadAnh u = new upLoadAnh();
                u.value = ConvertImageToBase64(pathsd);
                u.idCheck = FunctionGeneral.Md5(u.value);
                string url = "http://103.21.150.10:8080/VimassMedia/services/VMMedia/uploadImg";
                var json = JsonConvert.SerializeObject(u);
                String res = Service.SendWebrequest_POST_Method(json, url);

                Debug.WriteLine(json);

                Response response = JsonConvert.DeserializeObject<Response>(res);
                if (response.msgCode == 1)
                {
                    kq = response.result.ToString();

                }


            }
            catch(Exception ex)
            {       
                Debug.WriteLine(ex.Message);

            }
            return kq;
        }
        private static string ConvertImageToBase64(string imagePath)
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(textBox4.Text);
            }
            catch(Exception ex ) { 
            }
            
        }
    }
}
