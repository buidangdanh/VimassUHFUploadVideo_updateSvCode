﻿using Newtonsoft.Json;
using OpenQA.Selenium.DevTools.V108.DOM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VimassUHFUploadVideo.Ultil;
using VimassUHFUploadVideo.Vpass.Object;
using VimassUHFUploadVideo;

namespace VimassUHFUploadVideo.Vpass.GiaoDien
{
    public partial class ThongKe : UserControl
    {
        public ThongKe()
        {
            InitializeComponent();

        }

        private void ThongKe_Load(object sender, EventArgs e)
        {    
            //Thiet lap cac thuoc tinh cua trang
            label1.Text = "Từ";
            label2.Text = "Đến";
            button1.Text = "Tra cứu";
            comboBox1.Items.Add("Tất cả");
            comboBox2.Items.Add("Tất cả thiết bị");
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            dataGridView1.Columns.Add("Column1", "STT");
            dataGridView1.Columns.Add("Column2", "Họ tên");
            dataGridView1.Columns.Add("Column3", "Ngày");
            dataGridView1.Columns.Add("Column4", "TG đầu");
            dataGridView1.Columns.Add("Column5", "TG cuối");
            dataGridView1.Columns.Add("Column6", "Tổng giờ");
            dataGridView1.Columns.Add("Column7", "Thời gian khác");
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 13); // Thay đổi kiểu chữ và cỡ chữ
            dataGridView1.RowTemplate.Height = 30; // Thay đổi chiều cao của hàng thành 30 pixel
            dataGridView1.Columns["Column7"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.ReadOnly = true;
          
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }

            DateTime utcDateTime = DateTime.UtcNow;
            string vnTimeZoneKey = "SE Asia Standard Time";
            TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
            DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
            long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();

            try
            {
                ObjectGoiDichVuMini o = new ObjectGoiDichVuMini();
                o.funcId = 120;
                o.device = 2;
                o.currentime = yourDateTimeMilliseconds;

                //Mặc định là hôm nay
                ObjectThongKeRequest OTKR = new ObjectThongKeRequest();
                OTKR.from = yourDateTimeMilliseconds;
                OTKR.to = yourDateTimeMilliseconds +1;
          /*      OTKR.from = 1700499600000;
                OTKR.to = 1700586000000;*/
                OTKR.offset = 0;
                OTKR.limit = 50;
                OTKR.textSearch = "";
                OTKR.typeXacThuc = 2;

                o.data = JsonConvert.SerializeObject(OTKR);

                String url = FunCGeneral.ipMayChuDonVi;
                var json = JsonConvert.SerializeObject(o);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);

                if (response != null&& response.msgCode == 1)
                {

                    ResultResponeMini value = JsonConvert.DeserializeObject<ResultResponeMini>(response.result.ToString());
                    String valueTraVe = FunctionGeneral.DecodeBase64String(value.value);
                    var arrL = JsonConvert.DeserializeObject<List<ObjectThongKeTraVeChiTiet>>(valueTraVe);
                    int i = 0;
                    foreach (ObjectThongKeTraVeChiTiet arr in arrL)
                    {
                        i = i + 1;
                        dataGridView1.Rows.Add(i, arr.accName.Trim() + " - " + arr.phone + arr.vID, FunctionGeneral.chuyenLongSangNgayThangNam(arr.thoiGianDen), FunctionGeneral.chuyenLongSangGioPhut(arr.thoiGianDen, "HH:mm"), FunctionGeneral.chuyenLongSangGioPhut(arr.thoiGianVe, "HH:mm"), arr.tongThoiGian, layHisOnDayTime(arr.hisOnDay));
                        dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }


            }
            catch (Exception ex)
            {
                Logger.LogServices("ThongKe_Load: " + ex.Message);

            }





        }
        private void flowLayoutPanel1_Layout(object sender, LayoutEventArgs e)
        {
            FlowLayoutPanel panel = sender as FlowLayoutPanel;

            // Đối với mỗi control trong panel
            foreach (Control control in panel.Controls)
            {
                // Tính khoảng trống trên và dưới control
                int spaceAbove = (panel.Height - control.Height) / 2;

                // Cài đặt margin cho control để nó được căn giữa theo chiều dọc
                control.Margin = new Padding(control.Margin.Left, spaceAbove, control.Margin.Right, spaceAbove);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_Resize(object sender, EventArgs e)
        {
            int totalWidth = dataGridView1.Width - SystemInformation.VerticalScrollBarWidth;

            // Thiết lập tỉ lệ cho từng cột
            dataGridView1.Columns["Column1"].Width = (int)(totalWidth * 0.05); // 50% chiều rộng STT
            dataGridView1.Columns["Column2"].Width = (int)(totalWidth * 0.2); // 30% chiều rộng Tên
            dataGridView1.Columns["Column3"].Width = (int)(totalWidth * 0.1); // 30% chiều rộng Ngày
            dataGridView1.Columns["Column4"].Width = (int)(totalWidth * 0.1); // 20% chiều rộng Time đến
            dataGridView1.Columns["Column5"].Width = (int)(totalWidth * 0.1); // 20% chiều rộng Time Về
            dataGridView1.Columns["Column6"].Width = (int)(totalWidth * 0.1); // 20% chiều rộng Tổng thời gian
            dataGridView1.Columns["Column7"].Width = (int)(totalWidth * 0.35); // 20% chiều rộng Time khác


        }
        public static String layHisOnDayTime(List<ObjectHisOnday> list)
        {
            String kq = "";
            try
            {
                if (list != null && list.Count > 0)
                {
                    foreach (ObjectHisOnday o in list)
                    {
                        kq += FunctionGeneral.chuyenLongSangGioPhut(o.thoiGianGhiNhan, "HH:mm") + " ";
                    }
                }

            }
            catch (Exception e)
            {

            }
            return kq;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                DateTime utcDateTime = DateTime.UtcNow;
                string vnTimeZoneKey = "SE Asia Standard Time";
                TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
                DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
                long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();

                ObjectGoiDichVuMini o = new ObjectGoiDichVuMini();
                o.funcId = 120;
                o.device = 2;
                o.currentime = yourDateTimeMilliseconds;

                //Mặc định là hôm nay
                ObjectThongKeRequest OTKR = new ObjectThongKeRequest();
                //OTKR.from = yourDateTimeMilliseconds;
                //OTKR.to = yourDateTimeMilliseconds +1;
                OTKR.from = chuyenTimeStringSangLong(dateTimePicker1);
                OTKR.to = chuyenTimeStringSangLong(dateTimePicker2); ;
                OTKR.offset = 0;
                OTKR.limit = 50;
                OTKR.textSearch = textBox1.Text;
                OTKR.typeXacThuc = 2;

                o.data = JsonConvert.SerializeObject(OTKR);





                String url =FunCGeneral.ipMayChuDonVi;
                var json = JsonConvert.SerializeObject(o);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);

                if (response.msgCode == 1)
                {

                    ResultResponeMini value = JsonConvert.DeserializeObject<ResultResponeMini>(response.result.ToString());
                    String valueTraVe = FunctionGeneral.DecodeBase64String(value.value);
                    var arrL = JsonConvert.DeserializeObject<List<ObjectThongKeTraVeChiTiet>>(valueTraVe);
                    int i = 0;
                    foreach (ObjectThongKeTraVeChiTiet arr in arrL)
                    {
                        i = i + 1;
                        dataGridView1.Rows.Add(i, arr.accName.Trim() + " - " + arr.phone + arr.vID, FunctionGeneral.chuyenLongSangNgayThangNam(arr.thoiGianDen), FunctionGeneral.chuyenLongSangGioPhut(arr.thoiGianDen, "HH:mm"), FunctionGeneral.chuyenLongSangGioPhut(arr.thoiGianVe, "HH:mm"), arr.tongThoiGian, layHisOnDayTime(arr.hisOnDay));
                        dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
        public static long chuyenTimeStringSangLong(DateTimePicker dTP)
        {
            long kq = 0;
            try
            {
                // Giả sử dateTimePicker là tên của control DateTimePicker của bạn
                DateTime selectedDate = dTP.Value;

                // Tạo một DateTimeOffset với múi giờ UTC+7
                DateTimeOffset dateOffset = new DateTimeOffset(selectedDate, TimeSpan.FromHours(7));

                // Chuyển đổi thành Unix timestamp (giây)
                long unixTimestampSeconds = dateOffset.ToUnixTimeSeconds();

                // Hoặc chuyển đổi thành Unix timestamp (miligiây)
                kq = dateOffset.ToUnixTimeMilliseconds();
                Debug.WriteLine("Danh: " + kq);
            }
            catch (Exception e)
            {

            }
            return kq;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
