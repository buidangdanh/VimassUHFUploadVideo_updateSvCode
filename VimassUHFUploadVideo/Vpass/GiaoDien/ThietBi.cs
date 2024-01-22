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
using VimassUHFUploadVideo.Vpass.Object;
using VimassUHFUploadVideo.Ultil;
using VimassUHFUploadVideo.Vpass.Object.ObjectThietBi;
using System.Linq.Expressions;
using System.Diagnostics;

namespace VimassUHFUploadVideo.Vpass.GiaoDien
{
    public partial class ThietBi : UserControl
    {
        public ThietBi()
        {
            InitializeComponent();
        }
        public static ThietBiResponse thietBiResponse = new ThietBiResponse();
        public static List<RootQR> listQR = new List<RootQR>();
        public static List<RootQR> listThe = new List<RootQR>();
        public static List<ListLockDevice> listLock = new List<ListLockDevice>();
        public static List<wifi> listWifi = new List<wifi>();

        public static Dictionary<String, Device> hashDevice = new Dictionary<String, Device>();
        public static Dictionary<String, RootQR> hashQR = new Dictionary<String, RootQR>();
        public static Dictionary<String, RootQR> hashThe = new Dictionary<String, RootQR>();
        public static Dictionary<String, ListLockDevice> hashLock = new Dictionary<String, ListLockDevice>();
        public static Dictionary<String, wifi> hashWifi = new Dictionary<String, wifi>();
        public static int loaiThietBi = 0;
        FlowLayoutPanel panelContainerLeft;
        PictureBox pictureBox = new PictureBox();
        private void ThietBi_Load(object sender, EventArgs e)
        {
            try
            {
                button8.Visible= false;
                button9.Visible= false;
                hashDevice.Clear();
                hashQR.Clear();
                hashLock.Clear();
                hashWifi.Clear();
                panel1.BackColor = Color.FromArgb(166, 166, 166);
                capNhatThietBiTuServer();
                capNhatQRTuServer();
                capNhatKhoaTuServer();
                capNhatWifiTuServer();
                capNhatTheTuServer();


            }
            catch (Exception ex)
            {
                Logger.LogServices("ThietBi ThietBi_Load Exception: " + ex.Message);
            }
        }

        private void khoiTaoAnh()
        {
            try
            {             // Tạo và cấu hình PictureBox nếu chưa làm
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage; // Chỉnh kích thước ảnh phù hợp

                // Đặt hình ảnh bạn muốn hiển thị
                pictureBox.Image = Properties.Resources.heThong;  // Thay [YourImage] bằng hình ảnh của bạn

                // Đặt vị trí và kích thước của PictureBox
                pictureBox.Location = new Point(panel1.Location.X, panel1.Location.Y);
                pictureBox.Size = new Size(panel1.Width + panel2.Width, panel1.Height);
                pictureBox.Visible = true;
                // Thêm PictureBox vào Form (hoặc Panel chứa)
                this.Controls.Add(pictureBox);

                // Đưa PictureBox lên phía trên cùng
                pictureBox.BringToFront();
            }
            catch (Exception ex)
            {
                Logger.LogServices("khoiTaoAnh Exception: " + ex.Message);

            }
        }

        private void capNhatWifiTuServer()
        {
            try
            {
                dsWifiDonVi obj = new dsWifiDonVi();
                obj.user = "0966074236";
                obj.perNum = 0;
                obj.mcID = FunCGeneral.mcID;
                obj.currentTime = FunCGeneral.timeNow();
                obj.deviceID = 3;
                obj.cks = FunctionGeneral.Md5(obj.currentTime + obj.user + obj.deviceID + "ZgVCHxWqM$aNiCm54X2fYHD" + obj.mcID).ToLower();

                String url = "http://210.245.8.7:12318/vimass/services/VUHF/dsWifiDonVi";
                var json = JsonConvert.SerializeObject(obj);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);
                if (response != null && response.msgCode == 1)
                {
                    listWifi = JsonConvert.DeserializeObject<List<wifi>>(response.result.ToString());
                    foreach (wifi arr in listWifi)
                    {
                        hashWifi.Add(arr.name, arr);
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.LogServices("capNhatWifiTuServer Exception: " + ex.Message);
            }
        }

        private void capNhatKhoaTuServer()
        {
            try
            {
                dsThietBiKhoa obj = new dsThietBiKhoa();
                obj.user = "0966074236";
                obj.perNum = 0;
                obj.mcID = FunCGeneral.mcID;
                obj.currentTime = FunCGeneral.timeNow();
                obj.typeD = 1;
                obj.cks = FunctionGeneral.Md5(obj.user + obj.deviceID + "ZgVCHxqMd$aNCm54X2YHD" + obj.currentTime + obj.mcID).ToLower();

                String url = "http://210.245.8.7:12318/vimass/services/VUHF/dsThietBiKhoa";
                var json = JsonConvert.SerializeObject(obj);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);
                if (response != null && response.msgCode == 1)
                {
                    listLock = JsonConvert.DeserializeObject<List<ListLockDevice>>(response.result.ToString());
                    foreach (ListLockDevice arr in listLock)
                    {
                        hashLock.Add(arr.nameDevice, arr);
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.LogServices("capNhatKhoaTuServer Exception: " + ex.Message);
            }
        }

        private void btnCapNhatClick(object sender, EventArgs e)
        {
            try
            {
                hashDevice.Clear();
                hashQR.Clear();
                hashLock.Clear();
                hashWifi.Clear();
                hashThe.Clear();
                capNhatThietBiTuServer();
                capNhatQRTuServer();
                capNhatKhoaTuServer();
                capNhatWifiTuServer();
                capNhatTheTuServer();
                switch (loaiThietBi)
                {
                    case 0:
                        // code block to be executed if expression equals value1
                        break;
                    case 1:
                        khoiTaoDataGridViewThe1();
                        khoiTaoDataGridViewThe2();
                        thayDoiKichThuocThe1();
                        thayDoiKichThuocThe2();
                        hienThiThe();
                        break;
                    // You can have any number of case statements.
                    case 2:
                        khoiTaoDataGridKhoa1();
                        khoiTaoDataGridKhoa2();
                        thayDoiKichThuocKhoa1();
                        thayDoiKichThuocKhoa2();
                        hienThiKhoa();
                        break;
                    // You can have any number of case statements.
                    case 3:
                        // code block to be executed if expression equals value2
                        break;
                    // You can have any number of case statements.
                    case 4:
                        // code block to be executed if expression equals value2
                        break;
                    // You can have any number of case statements.
                    case 5:
                        // code block to be executed if expression equals value2
                        break;
                    // You can have any number of case statements.
                    case 6:
                        khoiTaoDataGridViewMayTinhDienThoai1();
                        khoiTaoDataGridViewMayTinhDienThoai2();
                        thayDoiKichThuocMTDT1();
                        thayDoiKichThuocMTDT2();
                        hienThiMayTinh();
                        break;
                    // You can have any number of case statements.
                    case 7:
                        khoiTaoDataGridViewMayTinhDienThoai1();
                        khoiTaoDataGridViewMayTinhDienThoai2();
                        thayDoiKichThuocMTDT1();
                        thayDoiKichThuocMTDT2();
                        hienThiDienThoai();
                        break;
                    // You can have any number of case statements.
                    case 8:
                        khoiTaoDataGridWifi1();
                        khoiTaoDataGridWifi2();
                        thayDoiKichThuocWifi1();
                        thayDoiKichThuocWifi2();
                        hienThiWifi();
                        break;
                    // You can have any number of case statements.
                    case 9:
                        // code block to be executed if expression equals value2
                        break;
                    case 10:
                        khoiTaoDataGridViewQR1();
                        khoiTaoDataGridViewQR2();
                        thayDoiKichThuocQR1();
                        thayDoiKichThuocQR2();
                        hienThiQR();
                        break;
                    case 11:
                        khoiTaoDataGridViewVpass1();
                        khoiTaoDataGridViewVpass2();
                        thayDoiKichThuocVpass1();
                        thayDoiKichThuoVpass2();
                        hienThiVpass();
                        break;
                    default:
                        // code block to be executed if no case matches
                        break;
                }

            }
            catch (Exception ex)
            {

            }

        }

        private void capNhatTheTuServer()
        {
            try
            {
                getThongTinDiem obj = new getThongTinDiem();
                obj.user = "0966074236";
                obj.perNum = 0;
                obj.mcID = FunCGeneral.mcID;
                //obj.mcID = "VM_TEST";
                obj.currentTime = FunCGeneral.timeNow();
                obj.theLK = true;
                obj.cks = FunctionGeneral.Md5("Y99JAuGfmYaBYYyycsLy26" + obj.mcID + obj.currentTime).ToLower();

                String url = "http://210.245.8.7:12318/vimass/services/VUHF/getThongTinDiem";
                var json = JsonConvert.SerializeObject(obj);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);
                if (response != null && response.msgCode == 1)
                {
                    listThe = JsonConvert.DeserializeObject<List<RootQR>>(response.result.ToString());
                    foreach (RootQR arr in listThe)
                    {
                        hashThe.Add(arr.infor.theDaNangLK, arr);
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.LogServices("capNhatThietBiTuServer Exception: " + ex.Message);
            }
        }
        private void capNhatQRTuServer()
        {
            try
            {
                getThongTinDiem obj = new getThongTinDiem();
                obj.user = "0966074236";
                obj.perNum = 0;
                obj.mcID = FunCGeneral.mcID;
                obj.currentTime = FunCGeneral.timeNow();
                obj.theLK = false;
                obj.cks = FunctionGeneral.Md5("Y99JAuGfmYaBYYyycsLy26" + obj.mcID + obj.currentTime).ToLower();

                String url = "http://210.245.8.7:12318/vimass/services/VUHF/getThongTinDiem";
                var json = JsonConvert.SerializeObject(obj);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);
                if (response != null && response.msgCode == 1)
                {
                    listQR = JsonConvert.DeserializeObject<List<RootQR>>(response.result.ToString());
                    foreach (RootQR arr in listQR)
                    {
                        hashQR.Add(arr.infor.tenCuaHang, arr);
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.LogServices("capNhatThietBiTuServer Exception: " + ex.Message);
            }
        }

        private void capNhatThietBiTuServer()
        {
            try
            {
                ObjectgetMayChuDonVi obj = new ObjectgetMayChuDonVi();
                obj.user = "0966074236";
                obj.perNum = 0;
                obj.mcID = FunCGeneral.mcID;
                obj.currentTime = FunCGeneral.timeNow();
                obj.typeDevice = 0;
                obj.cks = FunctionGeneral.Md5("Y99JAuGfmYaBYYyycsLy26" + obj.mcID + obj.currentTime).ToLower();

                String url = "http://210.245.8.7:12318/vimass/services/VUHF/getMayChuDonVi";
                var json = JsonConvert.SerializeObject(obj);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);
                if (response != null && response.msgCode == 1)
                {
                    thietBiResponse = JsonConvert.DeserializeObject<ThietBiResponse>(response.result.ToString());
                    if (thietBiResponse.devices != null && thietBiResponse.devices.Count() > 0)
                    {
                        foreach (Device may in thietBiResponse.devices)
                        {
                            if (may.typeDevice == 1 || may.typeDevice == 2)
                            {
                                hashDevice.Add("V" + may.stt, may);
                                Debug.WriteLine("V" + may.stt);
                            }
                            else if (may.typeDevice == 3 || may.typeDevice == 4)
                            {
                                hashDevice.Add("I" + may.stt, may);
                            }
                            else
                            {
                                hashDevice.Add("A" + may.stt, may);
                            }

                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Logger.LogServices("capNhatThietBiTuServer Exception: " + ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel1.Width = 200;
            pictureBox.Visible = false;
            loaiThietBi = 6;
            khoiTaoNutCapNhat();
            try
            {
                khoiTaoDataGridViewMayTinhDienThoai1();
                khoiTaoDataGridViewMayTinhDienThoai2();
                thayDoiKichThuocMTDT1();
                thayDoiKichThuocMTDT2();
                hienThiMayTinh();


            }
            catch (Exception ex)
            {
                Logger.LogServices("button8_Click Exception: " + ex.Message);
            }

        }

        private void hienThiMayTinh()
        {
            try
            {
                dataGridView1.Rows.Clear();
                for (int i = 0; i < hashDevice.Count(); i++)
                {
                    foreach (KeyValuePair<String, Device> item in hashDevice)
                    {
                        if (item.Value.typeDevice.Equals(1) || item.Value.typeDevice.Equals(2))
                        {

                            if (item.Value.stt == i)
                            {
                                dataGridView1.Rows.Add(i, item.Key);
                                dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            }

                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Logger.LogServices("button8_Click Exception: " + ex.Message);
            }

        }
        private void hienThiDienThoai()
        {
            try
            {
                dataGridView1.Rows.Clear();
                int icache = 0;
                for (int i = 0; i < hashDevice.Count(); i++)
                {
                    foreach (KeyValuePair<String, Device> item in hashDevice)
                    {

                        if (item.Value.typeDevice.Equals(3) || item.Value.typeDevice.Equals(4))
                        {
                            if (item.Value.stt == i)
                            {
                                dataGridView1.Rows.Add(i, item.Key);
                                dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                icache = i + 1;
                            }

                        }


                    }
                }
                for (int i = 0; i < hashDevice.Count(); i++)
                {
                    foreach (KeyValuePair<String, Device> item in hashDevice)
                    {

                        if (item.Value.typeDevice > 4)
                        {
                            if (item.Value.stt == i)
                            {
                                dataGridView1.Rows.Add(icache, item.Key);
                                dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                icache = icache + 1;
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Logger.LogServices("button8_Click Exception: " + ex.Message);
            }

        }
        private void hienThiVpass()
        {
            try
            {
                dataGridView1.Rows.Clear();
                int icache = 0;
                for (int i = 0; i < hashDevice.Count(); i++)
                {
                    foreach (KeyValuePair<String, Device> item in hashDevice)
                    {

                        if (item.Value.typeDevice.Equals(1) || item.Value.typeDevice.Equals(2))
                        {
                            if (item.Value.stt == i)
                            {
                                dataGridView1.Rows.Add(i, item.Key);
                                dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                icache = i + 1;
                            }

                        }


                    }
                }
                for (int i = 0; i < hashDevice.Count(); i++)
                {
                    foreach (KeyValuePair<String, Device> item in hashDevice)
                    {

                        if (item.Value.typeDevice.Equals(3) || item.Value.typeDevice.Equals(4))
                        {
                            if (item.Value.stt == i)
                            {
                                dataGridView1.Rows.Add(icache, item.Key);
                                dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                icache = icache + 1;
                            }

                        }


                    }
                }
                for (int i = 0; i < hashDevice.Count(); i++)
                {
                    foreach (KeyValuePair<String, Device> item in hashDevice)
                    {

                        if (item.Value.typeDevice > 4)
                        {
                            if (item.Value.stt == i)
                            {
                                dataGridView1.Rows.Add(icache, item.Key);
                                dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                icache = icache + 1;
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Logger.LogServices("button8_Click Exception: " + ex.Message);
            }

        }

        private String layDiemDinhDanh(List<ListDiem> listDiems)
        {
            String kq = "";
            List<String> listDiem = new List<String>();
            try
            {
                if (listDiems != null && listDiems.Count > 0)
                {
                    foreach (ListDiem arr in listDiems)
                    {
                        kq += arr.infor.tenCuaHang + "; ";
                    }
                }
                if (kq.Length > 2)
                {
                    kq = kq.Substring(0, kq.Length - 2);
                }
            }
            catch (Exception ex)
            {
                Logger.LogServices("layDiemDinhDanh Exception: " + ex.Message);


            }
            return kq;
        }
        private void khoiTaoDataGridViewMayTinhDienThoai1()
        {
            try
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("Column1", "STT");
                dataGridView1.Columns.Add("Column2", "Thiết bị");
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
                Logger.LogServices("khoiTaoDataGridViewMayTinhDienThoai1 Exception: " + ex.Message);

            }

        }
        private void khoiTaoDataGridViewVpass1()
        {
            try
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("Column1", "STT");
                dataGridView1.Columns.Add("Column2", "Thiết bị");
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
                Logger.LogServices("khoiTaoDataGridViewVpass1 Exception: " + ex.Message);

            }

        }

        private void khoiTaoDataGridViewMayTinhDienThoai2()
        {
            try
            {
                dataGridView2.Rows.Clear();
                dataGridView2.Columns.Clear();
                dataGridView2.Columns.Add("Column1", "STT");
                dataGridView2.Columns.Add("Column2", "Thiết bị");
                dataGridView2.Columns.Add("Column3", "IP / DeviceID");
                dataGridView2.Columns.Add("Column4", "Dung lượng");
                dataGridView2.Columns.Add("Column5", "Điều khiển");
                dataGridView2.DefaultCellStyle.Font = new Font("Segoe UI", 13); // Thay đổi kiểu chữ và cỡ chữ
                dataGridView2.RowTemplate.Height = 30; // Thay đổi chiều cao của hàng thành 30 pixel
                dataGridView2.BackgroundColor = Color.White;
                //Thiet lap màu của tên cột
                dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(242, 249, 255); // Chọn màu bạn muốn
                dataGridView2.EnableHeadersVisualStyles = false; // Cần thiết để màu tùy chỉnh có hiệu lực
                dataGridView2.ReadOnly = true;

                foreach (DataGridViewColumn column in dataGridView2.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                }
            }
            catch (Exception ex)
            {
                Logger.LogServices("khoiTaoDataGridViewMayTinhDienThoai2 Exception: " + ex.Message);

            }

        }
        private void khoiTaoDataGridViewVpass2()
        {
            try
            {
                dataGridView2.Rows.Clear();
                dataGridView2.Columns.Clear();
                dataGridView2.Columns.Add("Column1", "Thiết bị");
                dataGridView2.Columns.Add("Column2", "IP / DeviceID");
                dataGridView2.Columns.Add("Column3", "Dung lượng");
                dataGridView2.Columns.Add("Column4", "Điều khiển");
                dataGridView2.DefaultCellStyle.Font = new Font("Segoe UI", 13); // Thay đổi kiểu chữ và cỡ chữ
                dataGridView2.RowTemplate.Height = 30; // Thay đổi chiều cao của hàng thành 30 pixel
                dataGridView2.BackgroundColor = Color.White;
                //Thiet lap màu của tên cột
                dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(242, 249, 255); // Chọn màu bạn muốn
                dataGridView2.EnableHeadersVisualStyles = false; // Cần thiết để màu tùy chỉnh có hiệu lực
                dataGridView2.ReadOnly = true;

                foreach (DataGridViewColumn column in dataGridView2.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                }
            }
            catch (Exception ex)
            {
                Logger.LogServices("khoiTaoDataGridViewVpass2 Exception: " + ex.Message);

            }

        }

        private void thayDoiKichThuocMTDT1()
        {
            try
            {
                int totalWidth = dataGridView1.Width - SystemInformation.VerticalScrollBarWidth;

                // Thiết lập tỉ lệ cho từng cột
                dataGridView1.Columns["Column1"].Width = (int)(totalWidth * 0.2); // 50% chiều rộng STT
                dataGridView1.Columns["Column2"].Width = (int)(totalWidth * 0.8); // 30% chiều rộng Tên
            }
            catch (Exception ex)
            {
                Logger.LogServices("thayDoiKichThuoc Exception: " + ex.Message);
            }


        }
        private void thayDoiKichThuocVpass1()
        {
            try
            {
                int totalWidth = dataGridView1.Width - SystemInformation.VerticalScrollBarWidth;

                // Thiết lập tỉ lệ cho từng cột
                dataGridView1.Columns["Column1"].Width = (int)(totalWidth * 0.2); // 50% chiều rộng STT
                dataGridView1.Columns["Column2"].Width = (int)(totalWidth * 0.8); // 30% chiều rộng Tên
            }
            catch (Exception ex)
            {
                Logger.LogServices("thayDoiKichThuoc Exception: " + ex.Message);
            }


        }
        private void thayDoiKichThuocMTDT2()
        {
            try
            {
                int totalWidth = dataGridView2.Width - SystemInformation.VerticalScrollBarWidth;

                // Thiết lập tỉ lệ cho từng cột
                dataGridView2.Columns["Column1"].Width = (int)(totalWidth * 0.1); // 50% chiều rộng STT
                dataGridView2.Columns["Column2"].Width = (int)(totalWidth * 0.1); // 30% chiều rộng Tên
                dataGridView2.Columns["Column3"].Width = (int)(totalWidth * 0.3); // 30% chiều rộng Tên
                dataGridView2.Columns["Column4"].Width = (int)(totalWidth * 0.2); // 30% chiều rộng Tên
                dataGridView2.Columns["Column5"].Width = (int)(totalWidth * 0.3); // 30% chiều rộng Tên
            }
            catch (Exception ex)
            {
                Logger.LogServices("thayDoiKichThuoc Exception: " + ex.Message);
            }


        }
        private void thayDoiKichThuoVpass2()
        {
            try
            {
                int totalWidth = dataGridView2.Width - SystemInformation.VerticalScrollBarWidth;

                // Thiết lập tỉ lệ cho từng cột
                dataGridView2.Columns["Column1"].Width = (int)(totalWidth * 0.1); // 50% chiều rộng STT
                dataGridView2.Columns["Column2"].Width = (int)(totalWidth * 0.3); // 30% chiều rộng Tên
                dataGridView2.Columns["Column3"].Width = (int)(totalWidth * 0.1); // 30% chiều rộng Tên
                dataGridView2.Columns["Column4"].Width = (int)(totalWidth * 0.5); // 30% chiều rộng Tên
            }
            catch (Exception ex)
            {
                Logger.LogServices("thayDoiKichThuoc Exception: " + ex.Message);
            }


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Đặt lại màu nền cho tất cả các hàng
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.White; // Màu mặc định
                }
                // Xác định và tô màu hàng được nhấn
                int rowIndex = e.RowIndex;
                dataGridView2.Rows.Clear();
                // Check if the click is on a valid row (not on the header)
                if (e.RowIndex >= 0)
                {
                    if (rowIndex >= 0)
                    {
                        dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(0, 120, 215); // Màu khi hàng được chọn

                    }
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    string STT = "";
                    string tenThietBi = "";

                    // Retrieve values from the row
                    if (row.Cells["Column1"].Value != null && row.Cells["Column2"].Value != null)
                    {
                        STT = row.Cells["Column1"].Value.ToString();
                        tenThietBi = row.Cells["Column2"].Value.ToString();
                    }

                    // You can replace "ColumnName1", "ColumnName2", etc. with your actual column names or indices
                    switch (loaiThietBi)
                    {
                        case 0:
                            // code block to be executed if expression equals value1
                            break;
                        case 1:
                            hienThiChiTietThe(tenThietBi);
                            break;
                        // You can have any number of case statements.
                        case 2:
                            hienThiChiTietKhoa(tenThietBi);
                            break;
                        // You can have any number of case statements.
                        case 3:
                            // code block to be executed if expression equals value2
                            break;
                        // You can have any number of case statements.
                        case 4:
                            // code block to be executed if expression equals value2
                            break;
                        // You can have any number of case statements.
                        case 5:
                            // code block to be executed if expression equals value2
                            break;
                        // You can have any number of case statements.
                        case 6:
                            hienThiChiTiet(tenThietBi);
                            break;
                        // You can have any number of case statements.
                        case 7:
                            hienThiChiTiet(tenThietBi);
                            break;
                        // You can have any number of case statements.
                        case 8:
                            hienThiChiTietWifi(tenThietBi);
                            break;
                        // You can have any number of case statements.
                        case 9:
                            // code block to be executed if expression equals value2
                            break;
                        case 10:
                            hienThiChiTietQR(tenThietBi);
                            break;
                        case 11:
                            hienThiChiTiet(tenThietBi);
                            break;
                        default:
                            // code block to be executed if no case matches
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogServices("dataGridView1_CellClick Exception: " + ex.Message);

            }

        }

        private void hienThiChiTietQR(string tenThietBi)
        {
            try
            {

                foreach (KeyValuePair<String, RootQR> item in hashQR)
                {
                    if (item.Key.Equals(tenThietBi))
                    {
                        if (item.Value.catID.Equals("1020"))
                        {
                            dataGridView2.Rows.Add(1, "Pass QR", layNhom(item.Value.listGroup), item.Value.infor.tenCuaHang + " - " + item.Value.infor.diaChi, layLock(item.Value.listLockDevice));
                            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        else
                        {
                            dataGridView2.Rows.Add(1, "Pay QR", layNhom(item.Value.listGroup), item.Value.infor.tenCuaHang + " - " + item.Value.infor.diaChi, layLock(item.Value.listLockDevice));
                            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                Logger.LogServices("hienThiChiTiet Exception: " + ex.Message);

            }
        }
        private void hienThiChiTietThe(string tenThietBi)
        {
            try
            {
                foreach (KeyValuePair<String, RootQR> item in hashThe)
                {
                    if (item.Key.Equals(tenThietBi))
                    {
                            dataGridView2.Rows.Add(1, item.Value.infor.theDaNangLK, layNhom(item.Value.listGroup), item.Value.infor.tenCuaHang + " - " + item.Value.infor.diaChi, layLock(item.Value.listLockDevice));
                            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.LogServices("hienThiChiTiet Exception: " + ex.Message);

            }
        }
        private void hienThiChiTietKhoa(string tenThietBi)
        {
            try
            {
                foreach (KeyValuePair<String, ListLockDevice> item in hashLock)
                {
                    if (item.Key.Equals(tenThietBi))
                    {
                        dataGridView2.Rows.Add(1, item.Key, item.Value.ip, item.Value.portD);
                        dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    }
                }

            }
            catch (Exception ex)
            {
                Logger.LogServices("hienThiChiTiet Exception: " + ex.Message);

            }
        }
        private void hienThiChiTietWifi(string tenThietBi)
        {
            try
            {
                foreach (KeyValuePair<String, wifi> item in hashWifi)
                {
                    if (item.Key.Equals(tenThietBi))
                    {
                        dataGridView2.Rows.Add(1, item.Key, item.Value.pass);
                        dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    }
                }

            }
            catch (Exception ex)
            {
                Logger.LogServices("hienThiChiTiet Exception: " + ex.Message);

            }
        }

        private object layLock(List<ListLockDevice> listLockDevice)
        {
            String kq = "";
            List<String> listDiem = new List<String>();
            try
            {
                if (listLockDevice != null && listLockDevice.Count > 0)
                {
                    foreach (ListLockDevice arr in listLockDevice)
                    {
                        kq += arr.nameDevice + "; ";
                    }
                }
                if (kq.Length > 2)
                {
                    kq = kq.Substring(0, kq.Length - 2);
                }
            }
            catch (Exception ex)
            {
                Logger.LogServices("layNhom Exception: " + ex.Message);


            }
            return kq;
        }

        private object layNhom(List<ListGroup> listGroup)
        {
            String kq = "";
            List<String> listDiem = new List<String>();
            try
            {
                if (listGroup != null && listGroup.Count > 0)
                {
                    foreach (ListGroup arr in listGroup)
                    {
                        kq += arr.groupName + "; ";
                    }
                }
                if (kq.Length > 2)
                {
                    kq = kq.Substring(0, kq.Length - 2);
                }
            }
            catch (Exception ex)
            {
                Logger.LogServices("layNhom Exception: " + ex.Message);


            }
            return kq;
        }

        private void hienThiChiTiet3(string tenThietBi)
        {
            try
            {
                foreach (KeyValuePair<String, Device> item in hashDevice)
                {
                    if (item.Key.Equals(tenThietBi))
                    {

                        if (item.Value.ip != null && !item.Value.ip.Equals(""))
                        {
                            dataGridView2.Rows.Add(1, item.Key, item.Value.ip, item.Value.storage + " GB", layDiemDinhDanh(item.Value.listDiem));
                            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        else
                        {
                            if (item.Value.deviceID != null && !item.Value.deviceID.Equals(""))
                            {
                                dataGridView2.Rows.Add(1, item.Key, item.Value.deviceID, item.Value.storage + " GB", layDiemDinhDanh(item.Value.listDiem));
                                dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            }
                            else
                            {
                                dataGridView2.Rows.Add(1, item.Key, "", item.Value.storage + " GB", layDiemDinhDanh(item.Value.listDiem));
                                dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            }

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                Logger.LogServices("hienThiChiTiet Exception: " + ex.Message);

            }
        }
        private void hienThiChiTiet(string tenThietBi)
        {
            try
            {

                foreach (KeyValuePair<String, Device> item in hashDevice)
                {
                    if (item.Key.Equals(tenThietBi))
                    {
                        if (item.Value.typeDevice < 3)
                        {
                            if (item.Value.ip != null && !item.Value.ip.Equals(""))
                            {
                                dataGridView2.Rows.Add("Mini",item.Value.ip, item.Value.storage + " GB", layDiemDinhDanh(item.Value.listDiem));
                                dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            }
                        }
                        else
                        {
                            if (item.Value.typeDevice == 3|| item.Value.typeDevice == 4)
                            {
                                if (item.Value.deviceID != null && !item.Value.deviceID.Equals(""))
                                {
                                    dataGridView2.Rows.Add("IPhone", item.Value.deviceID, item.Value.storage + " GB", layDiemDinhDanh(item.Value.listDiem));
                                    dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                }
                                else
                                {
                                    dataGridView2.Rows.Add("IPhone", "", item.Value.storage + " GB", layDiemDinhDanh(item.Value.listDiem));
                                    dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                }
                            }
                            else
                            {

                                if (item.Value.deviceID != null && !item.Value.deviceID.Equals(""))
                                {
                                    dataGridView2.Rows.Add("Android", item.Value.deviceID, item.Value.storage + " GB", layDiemDinhDanh(item.Value.listDiem));
                                    dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                }
                                else
                                {
                                    dataGridView2.Rows.Add("Android", "", item.Value.storage + " GB", layDiemDinhDanh(item.Value.listDiem));
                                    dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                }
                            }
                      

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                Logger.LogServices("hienThiChiTiet Exception: " + ex.Message);

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {

            panel1.Width = 200;
            pictureBox.Visible = false;
            loaiThietBi = 7;
            khoiTaoNutCapNhat();
            try
            {
                khoiTaoDataGridViewMayTinhDienThoai1();
                khoiTaoDataGridViewMayTinhDienThoai2();
                thayDoiKichThuocMTDT1();
                thayDoiKichThuocMTDT2();
                hienThiDienThoai();


            }
            catch (Exception ex)
            {
                Logger.LogServices("button8_Click Exception: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Width = 400;
            pictureBox.Visible = false;
            khoiTaoNutCapNhat();
            loaiThietBi = 10;
            try
            {
                khoiTaoDataGridViewQR1();
                khoiTaoDataGridViewQR2();
                thayDoiKichThuocQR1();
                thayDoiKichThuocQR2();
                hienThiQR();

            }
            catch (Exception ex)
            {
                Logger.LogServices("button2_Click Exception: " + ex.Message);

            }
        }

        private void khoiTaoNutCapNhat()
        {
            try
            {
                panelContainerLeft = this.flowLayoutPanel2;
                panelContainerLeft.Controls.Clear();
                Button btnCapNhat = new Button();
                btnCapNhat.Size = new Size(30, 30);
                btnCapNhat.Margin = new Padding(left: 0, top: 0, right: 2, bottom: 0);
                btnCapNhat.ImageAlign = ContentAlignment.MiddleCenter;
                btnCapNhat.Image = Properties.Resources.capnhat;
                btnCapNhat.Click += new EventHandler(btnCapNhatClick);
                panelContainerLeft.Controls.Add(btnCapNhat);
            }
            catch (Exception ex)
            {
                Logger.LogServices("khoiTaoNutCapNhat Exception: " + ex.Message);

            }
        }

        private void hienThiQR()
        {
            try
            {
                dataGridView1.Rows.Clear();
                int i = 1;
                foreach (KeyValuePair<String, RootQR> item in hashQR)
                {
                    dataGridView1.Rows.Add(i, item.Key);
                    dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    i++;
                }

            }
            catch (Exception ex)
            {
                Logger.LogServices("button8_Click Exception: " + ex.Message);
            }
        }
        private void hienThiThe()
        {
            try
            {
                dataGridView1.Rows.Clear();
                int i = 1;
                foreach (KeyValuePair<String, RootQR> item in hashThe)
                {
                    dataGridView1.Rows.Add(i, item.Key);
                    dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    i++;
                }

            }
            catch (Exception ex)
            {
                Logger.LogServices("button8_Click Exception: " + ex.Message);
            }
        }

        private void hienThiKhoa()
        {
            try
            {
                dataGridView1.Rows.Clear();
                int i = 1;
                foreach (KeyValuePair<String, ListLockDevice> item in hashLock)
                {
                    dataGridView1.Rows.Add(i, item.Key);
                    dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    i++;
                }

            }
            catch (Exception ex)
            {
                Logger.LogServices("hienThiKhoa Exception: " + ex.Message);
            }
        }

        private void thayDoiKichThuocQR2()
        {
            try
            {
                int totalWidth = dataGridView2.Width - SystemInformation.VerticalScrollBarWidth;

                // Thiết lập tỉ lệ cho từng cột
                dataGridView2.Columns["Column1"].Width = (int)(totalWidth * 0.05); // 50% chiều rộng STT
                dataGridView2.Columns["Column2"].Width = (int)(totalWidth * 0.1); // 30% chiều rộng Tên
                dataGridView2.Columns["Column3"].Width = (int)(totalWidth * 0.4); // 30% chiều rộng Tên
                dataGridView2.Columns["Column4"].Width = (int)(totalWidth * 0.25); // 30% chiều rộng Tên
                dataGridView2.Columns["Column5"].Width = (int)(totalWidth * 0.2); // 30% chiều rộng Tên
            }
            catch (Exception ex)
            {
                Logger.LogServices("thayDoiKichThuocQR2 Exception: " + ex.Message);
            }
        }
        private void thayDoiKichThuocThe2()
        {
            try
            {
                int totalWidth = dataGridView2.Width - SystemInformation.VerticalScrollBarWidth;

                // Thiết lập tỉ lệ cho từng cột
                dataGridView2.Columns["Column1"].Width = (int)(totalWidth * 0.05); // 50% chiều rộng STT
                dataGridView2.Columns["Column2"].Width = (int)(totalWidth * 0.15); // 30% chiều rộng Tên
                dataGridView2.Columns["Column3"].Width = (int)(totalWidth * 0.35); // 30% chiều rộng Tên
                dataGridView2.Columns["Column4"].Width = (int)(totalWidth * 0.25); // 30% chiều rộng Tên
                dataGridView2.Columns["Column5"].Width = (int)(totalWidth * 0.2); // 30% chiều rộng Tên
            }
            catch (Exception ex)
            {
                Logger.LogServices("thayDoiKichThuocQR2 Exception: " + ex.Message);
            }
        }

        private void thayDoiKichThuocQR1()
        {
            try
            {
                int totalWidth = dataGridView1.Width - SystemInformation.VerticalScrollBarWidth;

                // Thiết lập tỉ lệ cho từng cột
                dataGridView1.Columns["Column1"].Width = (int)(totalWidth * 0.2); // 50% chiều rộng STT
                dataGridView1.Columns["Column2"].Width = (int)(totalWidth * 0.8); // 30% chiều rộng Tên
            }
            catch (Exception ex)
            {
                Logger.LogServices("thayDoiKichThuocQR1 Exception: " + ex.Message);
            }
        }
        private void thayDoiKichThuocThe1()
        {
            try
            {
                int totalWidth = dataGridView1.Width - SystemInformation.VerticalScrollBarWidth;

                // Thiết lập tỉ lệ cho từng cột
                dataGridView1.Columns["Column1"].Width = (int)(totalWidth * 0.2); // 50% chiều rộng STT
                dataGridView1.Columns["Column2"].Width = (int)(totalWidth * 0.8); // 30% chiều rộng Tên
            }
            catch (Exception ex)
            {
                Logger.LogServices("thayDoiKichThuocQR1 Exception: " + ex.Message);
            }
        }
        private void thayDoiKichThuocKhoa1()
        {
            try
            {
                int totalWidth = dataGridView1.Width - SystemInformation.VerticalScrollBarWidth;

                // Thiết lập tỉ lệ cho từng cột
                dataGridView1.Columns["Column1"].Width = (int)(totalWidth * 0.2); // 50% chiều rộng STT
                dataGridView1.Columns["Column2"].Width = (int)(totalWidth * 0.8); // 30% chiều rộng Tên
            }
            catch (Exception ex)
            {
                Logger.LogServices("thayDoiKichThuocKhoa1 Exception: " + ex.Message);
            }
        }
        private void thayDoiKichThuocKhoa2()
        {
            try
            {
                int totalWidth = dataGridView2.Width - SystemInformation.VerticalScrollBarWidth;

                // Thiết lập tỉ lệ cho từng cột
                dataGridView2.Columns["Column1"].Width = (int)(totalWidth * 0.1); // 50% chiều rộng STT
                dataGridView2.Columns["Column2"].Width = (int)(totalWidth * 0.3); // 30% chiều rộng Tên
                dataGridView2.Columns["Column3"].Width = (int)(totalWidth * 0.3); // 30% chiều rộng Tên
                dataGridView2.Columns["Column4"].Width = (int)(totalWidth * 0.3); // 30% chiều rộng Tên
            }
            catch (Exception ex)
            {
                Logger.LogServices("thayDoiKichThuocKhoa2 Exception: " + ex.Message);
            }
        }

        private void khoiTaoDataGridViewQR2()
        {
            try
            {
                dataGridView2.Rows.Clear();
                dataGridView2.Columns.Clear();
                dataGridView2.Columns.Add("Column1", "STT");
                dataGridView2.Columns.Add("Column2", "Chức năng");
                dataGridView2.Columns.Add("Column3", "Nhóm");
                dataGridView2.Columns.Add("Column4", "Hiển thị");
                dataGridView2.Columns.Add("Column5", "Điều khiển");
                dataGridView2.DefaultCellStyle.Font = new Font("Segoe UI", 13); // Thay đổi kiểu chữ và cỡ chữ
                dataGridView2.RowTemplate.Height = 30; // Thay đổi chiều cao của hàng thành 30 pixel
                dataGridView2.BackgroundColor = Color.White;
                //Thiet lap màu của tên cột
                dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(242, 249, 255); // Chọn màu bạn muốn
                dataGridView2.EnableHeadersVisualStyles = false; // Cần thiết để màu tùy chỉnh có hiệu lực
                dataGridView2.ReadOnly = true;

                foreach (DataGridViewColumn column in dataGridView2.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                }
            }
            catch (Exception ex)
            {
                Logger.LogServices("khoiTaoDataGridViewQR2 Exception: " + ex.Message);

            }
        }

        private void khoiTaoDataGridViewQR1()
        {
            try
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("Column1", "STT");
                dataGridView1.Columns.Add("Column2", "QR");
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
                Logger.LogServices("khoiTaoDataGridViewQR1 Exception: " + ex.Message);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Width = 400;
            loaiThietBi = 1;
            pictureBox.Visible = false;
            khoiTaoNutCapNhat();
            try
            {
                khoiTaoDataGridViewThe1();
                khoiTaoDataGridViewThe2();
                thayDoiKichThuocThe1();
                thayDoiKichThuocThe2();
                hienThiThe();

            }
            catch (Exception ex)
            {
                Logger.LogServices("button2_Click Exception: " + ex.Message);

            }
        }

        private void khoiTaoDataGridViewThe2()
        {
            try
            {
                dataGridView2.Rows.Clear();
                dataGridView2.Columns.Clear();
                dataGridView2.Columns.Add("Column1", "STT");
                dataGridView2.Columns.Add("Column2", "Thẻ");
                dataGridView2.Columns.Add("Column3", "Nhóm");
                dataGridView2.Columns.Add("Column4", "Hiển thị");
                dataGridView2.Columns.Add("Column5", "Điều khiển");
                dataGridView2.DefaultCellStyle.Font = new Font("Segoe UI", 13); // Thay đổi kiểu chữ và cỡ chữ
                dataGridView2.RowTemplate.Height = 30; // Thay đổi chiều cao của hàng thành 30 pixel
                dataGridView2.BackgroundColor = Color.White;
                //Thiet lap màu của tên cột
                dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(242, 249, 255); // Chọn màu bạn muốn
                dataGridView2.EnableHeadersVisualStyles = false; // Cần thiết để màu tùy chỉnh có hiệu lực
                dataGridView2.ReadOnly = true;

                foreach (DataGridViewColumn column in dataGridView2.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                }
            }
            catch (Exception ex)
            {
                Logger.LogServices("khoiTaoDataGridViewQR2 Exception: " + ex.Message);

            }
        }

        private void khoiTaoDataGridViewThe1()
        {
            try
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("Column1", "STT");
                dataGridView1.Columns.Add("Column2", "Thẻ");
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
                Logger.LogServices("khoiTaoDataGridViewQR1 Exception: " + ex.Message);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Width = 400;
            pictureBox.Visible = false;
            loaiThietBi = 2;
            khoiTaoNutCapNhat();
            try
            {
                khoiTaoDataGridKhoa1();
                khoiTaoDataGridKhoa2();
                thayDoiKichThuocKhoa1();
                thayDoiKichThuocKhoa2();
                hienThiKhoa();

            }
            catch (Exception ex)
            {
                Logger.LogServices("button2_Click Exception: " + ex.Message);

            }

        }

        private void khoiTaoDataGridKhoa2()
        {
            try
            {
                dataGridView2.Rows.Clear();
                dataGridView2.Columns.Clear();
                dataGridView2.Columns.Add("Column1", "STT");
                dataGridView2.Columns.Add("Column2", "Chức năng");
                dataGridView2.Columns.Add("Column3", "IP");
                dataGridView2.Columns.Add("Column4", "Port");
                dataGridView2.DefaultCellStyle.Font = new Font("Segoe UI", 13); // Thay đổi kiểu chữ và cỡ chữ
                dataGridView2.RowTemplate.Height = 30; // Thay đổi chiều cao của hàng thành 30 pixel
                dataGridView2.BackgroundColor = Color.White;
                //Thiet lap màu của tên cột
                dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(242, 249, 255); // Chọn màu bạn muốn
                dataGridView2.EnableHeadersVisualStyles = false; // Cần thiết để màu tùy chỉnh có hiệu lực
                dataGridView2.ReadOnly = true;

                foreach (DataGridViewColumn column in dataGridView2.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                }
            }
            catch (Exception ex)
            {
                Logger.LogServices("khoiTaoDataGridKhoa2 Exception: " + ex.Message);

            }
        }

        private void khoiTaoDataGridKhoa1()
        {
            try
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("Column1", "STT");
                dataGridView1.Columns.Add("Column2", "QR");
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
                Logger.LogServices("khoiTaoDataGridKhoa Exception: " + ex.Message);

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Width = 400;
            loaiThietBi = 3;
            pictureBox.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Width = 400;
            loaiThietBi = 4;
            pictureBox.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel1.Width = 400;
            loaiThietBi = 5;
            pictureBox.Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel1.Width = 300;
            pictureBox.Visible = false;
            loaiThietBi = 8;
            khoiTaoNutCapNhat();
            try
            {
                khoiTaoDataGridWifi1();
                khoiTaoDataGridWifi2();
                thayDoiKichThuocWifi1();
                thayDoiKichThuocWifi2();
                hienThiWifi();

            }
            catch (Exception ex)
            {
                Logger.LogServices("button2_Click Exception: " + ex.Message);

            }
        }

        private void hienThiWifi()
        {
            try
            {
                dataGridView1.Rows.Clear();
                int i = 1;
                foreach (KeyValuePair<String, wifi> item in hashWifi)
                {
                    dataGridView1.Rows.Add(i, item.Key);
                    dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    i++;
                }

            }
            catch (Exception ex)
            {
                Logger.LogServices("hienThiWifi Exception: " + ex.Message);
            }
        }

        private void thayDoiKichThuocWifi2()
        {
            try
            {
                int totalWidth = dataGridView2.Width - SystemInformation.VerticalScrollBarWidth;

                // Thiết lập tỉ lệ cho từng cột
                dataGridView2.Columns["Column1"].Width = (int)(totalWidth * 0.1); // 50% chiều rộng STT
                dataGridView2.Columns["Column2"].Width = (int)(totalWidth * 0.45); // 30% chiều rộng Tên
                dataGridView2.Columns["Column3"].Width = (int)(totalWidth * 0.45); // 30% chiều rộng Tên

            }
            catch (Exception ex)
            {
                Logger.LogServices("thayDoiKichThuocWifi2 Exception: " + ex.Message);
            }
        }

        private void thayDoiKichThuocWifi1()
        {

            try
            {
                int totalWidth = dataGridView1.Width - SystemInformation.VerticalScrollBarWidth;

                // Thiết lập tỉ lệ cho từng cột
                dataGridView1.Columns["Column1"].Width = (int)(totalWidth * 0.2); // 50% chiều rộng STT
                dataGridView1.Columns["Column2"].Width = (int)(totalWidth * 0.8); // 30% chiều rộng Tên
            }
            catch (Exception ex)
            {
                Logger.LogServices("thayDoiKichThuocWifi1 Exception: " + ex.Message);
            }
        }

        private void khoiTaoDataGridWifi2()
        {
            try
            {
                dataGridView2.Rows.Clear();
                dataGridView2.Columns.Clear();
                dataGridView2.Columns.Add("Column1", "STT");
                dataGridView2.Columns.Add("Column2", "Tên");
                dataGridView2.Columns.Add("Column3", "Mật khẩu");
                dataGridView2.DefaultCellStyle.Font = new Font("Segoe UI", 13); // Thay đổi kiểu chữ và cỡ chữ
                dataGridView2.RowTemplate.Height = 30; // Thay đổi chiều cao của hàng thành 30 pixel
                dataGridView2.BackgroundColor = Color.White;
                //Thiet lap màu của tên cột
                dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(242, 249, 255); // Chọn màu bạn muốn
                dataGridView2.EnableHeadersVisualStyles = false; // Cần thiết để màu tùy chỉnh có hiệu lực
                dataGridView2.ReadOnly = true;

                foreach (DataGridViewColumn column in dataGridView2.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                }
            }
            catch (Exception ex)
            {
                Logger.LogServices("khoiTaoDataGridWifi2 Exception: " + ex.Message);

            }
        }

        private void khoiTaoDataGridWifi1()
        {
            try
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("Column1", "STT");
                dataGridView1.Columns.Add("Column2", "Wifi");
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
                Logger.LogServices("khoiTaoDataGridWifi1 Exception: " + ex.Message);

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            panel1.Width = 300;
            pictureBox.Visible = false;
            loaiThietBi = 9;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                loaiThietBi = 0;
                khoiTaoAnh();

            }
            catch (Exception ex)
            {
                Logger.LogServices("button1_Click Exception: " + ex.Message);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel1.Width= 200;
            pictureBox.Visible = false;
            loaiThietBi = 11;
            khoiTaoNutCapNhat();
            try
            {
                khoiTaoDataGridViewVpass1();
                khoiTaoDataGridViewVpass2();
                thayDoiKichThuocVpass1();
                thayDoiKichThuoVpass2();
                hienThiVpass();


            }
            catch (Exception ex)
            {
                Logger.LogServices("button8_Click Exception: " + ex.Message);
            }
        }
    }
}
