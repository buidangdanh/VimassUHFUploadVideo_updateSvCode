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
using VimassUHFUploadVideo;
namespace VimassUHFUploadVideo.Vpass.GiaoDien
{
    public partial class GiaoDichDinhDanh : UserControl
    {
        public GiaoDichDinhDanh()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public static Timer actionTimer;
        private void GiaoDichDinhDanh_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Column1", "STT");
            dataGridView1.Columns.Add("Column2", "Họ tên");
            dataGridView1.Columns.Add("Column3", "Thời gian ghi nhận");
            dataGridView1.Columns.Add("Column4", "Điểm");
            dataGridView1.Columns.Add("Column5", "Thời gian xác thực");
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
            
            SetInterval(() => goiDichVuLaySaoKe(), 1000);
            this.Disposed += MyUserControl_Disposed;


        }
        private void MyUserControl_Disposed(object sender, EventArgs e)
        {
            if (actionTimer != null)
            {
                actionTimer.Stop();
            }
        }
        public static void SetInterval(Action action, int interval)
        {
            actionTimer = new Timer();
            actionTimer.Interval = interval;
            actionTimer.Tick += (s, e) =>
            {
                action();
            };
            actionTimer.Start();
        }
        public void goiDichVuLaySaoKe()
        {
            try
            {
                dataGridView1.Rows.Clear();
                ObjectGoiDichVuMini o = new ObjectGoiDichVuMini();
                o.funcId = 122;
                o.device = 2;
                o.currentime = FunCGeneral.timeNow();

                ObjectLichSuQRBanGhiMoiNhat oLDSGR = new ObjectLichSuQRBanGhiMoiNhat();
                oLDSGR.soBanGhi = 20;

                o.data = JsonConvert.SerializeObject(oLDSGR);

                String url = FunCGeneral.ipMayChuDonVi;
                //String url = "http://192.168.6.79:58080/autobank/services/vimassTool/dieuPhoi";
                var json = JsonConvert.SerializeObject(o);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);

                if (response != null && response.msgCode == 1)
                {

                    ResultResponeMini value = JsonConvert.DeserializeObject<ResultResponeMini>(response.result.ToString());
                    String valueTraVe = FunctionGeneral.DecodeBase64String(value.value);
                    var arrL = JsonConvert.DeserializeObject<List<ObjectLichSuRaVaoQuetQr>>(valueTraVe);

                    for (int i = 0; i < arrL.Count(); i++)
                    {
                        if (IsInCurrentDay(arrL[i].thoiGianGhiNhan))
                        {
                            if (arrL[i].typeXacThuc != null && arrL[i].typeXacThuc == 0)
                            {
                                if (arrL[i].vID != null && !arrL[i].vID.Equals(""))
                                {
                                    dataGridView1.Rows.Add(i + 1, arrL[i].accName + " - " + arrL[i].vID, FunctionGeneral.chuyenLongSangGioPhut(arrL[i].thoiGianGhiNhan, "HH:mm:ss"), arrL[i].loiRa + " - " + arrL[i].diaChi, hamDoiSangGiay(arrL[i].timeAuthen) + "s");
                                    dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                }
                                else if (arrL[i].phone != null && !arrL[i].phone.Equals(""))
                                {
                                    dataGridView1.Rows.Add(i + 1, arrL[i].accName + " - " + arrL[i].phone, FunctionGeneral.chuyenLongSangGioPhut(arrL[i].thoiGianGhiNhan, "HH:mm:ss"), arrL[i].loiRa + " - " + arrL[i].diaChi, hamDoiSangGiay(arrL[i].timeAuthen) + "s");
                                    dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                }


                            }
                            else
                            {
                                if (arrL[i].vID != null && !arrL[i].vID.Equals(""))
                                {
                                    dataGridView1.Rows.Add(i + 1, arrL[i].accName + " - " + arrL[i].vID, FunctionGeneral.chuyenLongSangGioPhut(arrL[i].thoiGianGhiNhan, "HH:mm:ss"), arrL[i].loiRa + " - " + arrL[i].diaChi, hamDoiSangGiay(arrL[i].timeAuthen) + "s");
                                    dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                }
                                else if (arrL[i].phone != null && !arrL[i].phone.Equals(""))
                                {
                                    dataGridView1.Rows.Add(i + 1, arrL[i].accName + " - " + arrL[i].phone, FunctionGeneral.chuyenLongSangGioPhut(arrL[i].thoiGianGhiNhan, "HH:mm:ss"), arrL[i].loiRa + " - " + arrL[i].diaChi, hamDoiSangGiay(arrL[i].timeAuthen) + "s");
                                    dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                }

                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogServices("goiDichVuLaySaoKe Exception: " + ex.Message);

            }

        }

        private double hamDoiSangGiay(int timeAuthen)
        {
            return timeAuthen / 1000.0; // Chuyển đổi sang giây
        }

        private void dataGridView1_Resize(object sender, EventArgs e)
        {
            int totalWidth = dataGridView1.Width - SystemInformation.VerticalScrollBarWidth;

            // Thiết lập tỉ lệ cho từng cột
            dataGridView1.Columns["Column1"].Width = (int)(totalWidth * 0.05); // 50% chiều rộng STT
            dataGridView1.Columns["Column2"].Width = (int)(totalWidth * 0.3); // 50% chiều rộng STT
            dataGridView1.Columns["Column3"].Width = (int)(totalWidth * 0.15); // 50% chiều rộng STT
            dataGridView1.Columns["Column4"].Width = (int)(totalWidth * 0.35); // 50% chiều rộng STT
            dataGridView1.Columns["Column5"].Width = (int)(totalWidth * 0.15); // 50% chiều rộng STT



        }
        public static bool IsInCurrentDay(long unixMilliseconds)
        {
            // Chuyển đổi Unix time (milliseconds) thành DateTime
            DateTime dateTime = DateTimeOffset.FromUnixTimeMilliseconds(unixMilliseconds).DateTime;

            // Lấy thời gian hiện tại theo múi giờ của Việt Nam (UTC+7)
            TimeZoneInfo vietnamZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            DateTime nowInVietnam = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamZone);

            // So sánh ngày và trả về kết quả
            return dateTime.Date == nowInVietnam.Date;
        }

    }
}
