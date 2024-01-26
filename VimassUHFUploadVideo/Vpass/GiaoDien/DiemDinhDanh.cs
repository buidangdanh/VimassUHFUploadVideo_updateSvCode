using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VimassUHFUploadVideo.Vpass.Object.ObjectThietBi;
using VimassUHFUploadVideo.Vpass.Object;
using VimassUHFUploadVideo.Ultil;
using VimassUHFUploadVideo.Vpass.Object.ObjectDiemDinhDanh;
using System.Diagnostics;
using OpenQA.Selenium.DevTools;
using VimassUHFUploadVideo;
using System.Security.Policy;
using System.Globalization;

namespace VimassUHFUploadVideo.Vpass.GiaoDien
{
    public partial class DiemDinhDanh : UserControl
    {
        public DiemDinhDanh()
        {
            InitializeComponent();
        }

        private void DiemDinhDanh_Load(object sender, EventArgs e)
        {
            try
            {
                hashDiemDinhDanh.Clear();
                flowLayoutPanel1.BackColor = Color.FromArgb(166, 166, 166);
                khoiTaoNutCapNhat();
                goiDichVuLayDiem();
                khoiTaoDiemDinhDanh1();
                thayDoiKichThuoc1();
                hienThiDiemDinhDanh();
                dataGridView1.CurrentCell = null;
            }
            catch(Exception ex) { 
            
            }
        }
        public static List<RootDiemDinhDanh> listDiemDinhDanh = new List<RootDiemDinhDanh>();
        public static Dictionary<String, RootDiemDinhDanh> hashDiemDinhDanh = new Dictionary<String, RootDiemDinhDanh>();
        FlowLayoutPanel panelContainerCenter;
        private void goiDichVuLayDiem()
        {
            try
            {
                ObjectDsDiemTheoToaDo obj = new ObjectDsDiemTheoToaDo();
                obj.timeRequest = FunCGeneral.timeNow();
                obj.VMApp = 5;
                obj.ip = "193.169.1.11";
                obj.lat = 21.0074402;
                obj.lng = 105.8055788;

                obj.km = 0.3;
                obj.catId = "1020";
                obj.funcId = 59;
                obj.userLTV = "0981455707";
                obj.trangThai = 1;
                obj.limit = 500;
                obj.keySearch = "";
                obj.offset = 0;
                obj.user = "";
                obj.checkSum = FunctionGeneral.Md5("Y4SkpSofFAgsR27fMBM02SNoV9iPI2CBo0ZCImM6EgnfXikl3en3VfWyTwfLLTC883yJC" + obj.funcId + obj.km.ToString(CultureInfo.InvariantCulture) + obj.lat.ToString(CultureInfo.InvariantCulture) + obj.lng.ToString(CultureInfo.InvariantCulture) + obj.limit + obj.offset + obj.timeRequest + obj.user + obj.ip + obj.userLTV + obj.trangThai).ToLower();
                String url = "http://103.21.150.8:62168/toaDoVimass/services/toaDoVimass/requestCommand";
                var json = JsonConvert.SerializeObject(obj);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);
                String k1 = FunctionGeneral.Md5("1" + "Y4SkpSofFAgsR27fMBM02SNoV9iPI2CBo0ZCImM6EgnfXikl3en3VfWyTwfLLTC883yJC" + obj.timeRequest + 5).ToLower();
                String k2 = FunctionGeneral.Md5("2" + "Y4SkpSofFAgsR27fMBM02SNoV9iPI2CBo0ZCImM6EgnfXikl3en3VfWyTwfLLTC883yJC" + obj.timeRequest + 5).ToLower();
                String k3 = FunctionGeneral.Md5("3" + "Y4SkpSofFAgsR27fMBM02SNoV9iPI2CBo0ZCImM6EgnfXikl3en3VfWyTwfLLTC883yJC" + obj.timeRequest + 5).ToLower();
                if (response != null && response.msgCode == 1)
                {
                    String resul = FunctionGeneral.DecryptTripleDES(k1, k2, k3, response.result.ToString());
                    listDiemDinhDanh = JsonConvert.DeserializeObject<List<RootDiemDinhDanh>>(resul);
                    foreach(RootDiemDinhDanh arr in listDiemDinhDanh)
                    {
                        hashDiemDinhDanh.Add(arr.id,arr);
                    }
                }

               

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Logger.LogServices("capNhatWifiTuServer Exception: " + ex.Message);
            }
        }
        private void khoiTaoNutCapNhat()
        {
            try
            {
                panelContainerCenter = this.flowLayoutPanel1;
                panelContainerCenter.Controls.Clear();
                Button btnCapNhat = new Button();
                btnCapNhat.Size = new Size(30, 30);
                btnCapNhat.Margin = new Padding(left: 0, top: 0, right: 2, bottom: 0);
                btnCapNhat.ImageAlign = ContentAlignment.MiddleCenter;
                btnCapNhat.Image = Properties.Resources.capnhat;
                btnCapNhat.Click += new EventHandler(btnCapNhatClick);
                panelContainerCenter.Controls.Add(btnCapNhat);
            }
            catch (Exception ex)
            {
                Logger.LogServices("khoiTaoNutCapNhat Exception: " + ex.Message);

            }
        }

        private void btnCapNhatClick(object sender, EventArgs e)
        {
            try
            {
         
                hashDiemDinhDanh.Clear();
                goiDichVuLayDiem();
                khoiTaoDiemDinhDanh1();
                thayDoiKichThuoc1();
                hienThiDiemDinhDanh();


            }
            catch (Exception ex)
            {
                Logger.LogServices("btnCapNhatClick Exception: " + ex.Message);

            }
        }
        private void hienThiDiemDinhDanh()
        {
            try
            {
                dataGridView1.Rows.Clear();
                int i = 1;
                foreach (KeyValuePair<String, RootDiemDinhDanh> item in hashDiemDinhDanh)
                {
                    if(item.Value.maMcId!=null&&item.Value.maMcId.Equals(FunCGeneral.mcID))
                    {
                        dataGridView1.Rows.Add(i, item.Value.tenCuaHang, item.Value.diaChi);
                        dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        i++;
                    }
              
                }

            }
            catch (Exception ex)
            {
                Logger.LogServices("button8_Click Exception: " + ex.Message);
            }
        }
        private void khoiTaoDiemDinhDanh1()
        {
            try
            {
            
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("Column1", "STT");
                dataGridView1.Columns.Add("Column2", "Tên điểm");
                dataGridView1.Columns.Add("Column3", "Địa chỉ");
                dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 13); // Thay đổi kiểu chữ và cỡ chữ
                dataGridView1.RowTemplate.Height = 30; // Thay đổi chiều cao của hàng thành 30 pixel
                dataGridView1.BackgroundColor = Color.White;
                //Thiet lap màu của tên cột
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(242, 249, 255); // Chọn màu bạn muốn
                dataGridView1.EnableHeadersVisualStyles = false; // Cần thiết để màu tùy chỉnh có hiệu lực
                dataGridView1.ReadOnly = true;

                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                }
            }
            catch (Exception ex)
            {
                Logger.LogServices("khoiTaoDiemDinhDanh1 Exception: " + ex.Message);

            }
        }
        private void thayDoiKichThuoc1()
        {
            try
            {
                int totalWidth = dataGridView1.Width - SystemInformation.VerticalScrollBarWidth;

                // Thiết lập tỉ lệ cho từng cột
                dataGridView1.Columns["Column1"].Width = (int)(totalWidth * 0.05); // 50% chiều rộng STT
                dataGridView1.Columns["Column2"].Width = (int)(totalWidth * 0.35); // 30% chiều rộng Tên
                dataGridView1.Columns["Column3"].Width = (int)(totalWidth * 0.6); // 30% chiều rộng Tên
            }
            catch (Exception ex)
            {
                Logger.LogServices("thayDoiKichThuocKhoa1 Exception: " + ex.Message);
            }
        }
    }
}
