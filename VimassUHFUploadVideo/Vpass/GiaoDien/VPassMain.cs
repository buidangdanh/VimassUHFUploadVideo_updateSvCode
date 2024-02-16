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
using VimassUHFUploadVideo.Vpass.Object;
using VimassUHFUploadVideo.Vpass.Object.ObjectThietBi;

namespace VimassUHFUploadVideo.Vpass.GiaoDien
{
    public partial class VPassMain : Form
    {
        public VPassMain()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("vi-VN");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("vi-VN");
            InitializeComponent();
            labelDangNhap.Cursor = Cursors.Hand;

        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            try
            {
                comboBox2.Visible= false;
                this.comboBox2.Items.Add("V1");
                this.comboBox2.Items.Add("V2");
                comboBox2.SelectedIndex = 0;
                comboBox2.Enabled = true;
                foreach (KeyValuePair<String, ObjecthttpMayChuDonVi> itemHash in FunCGeneral.hashMCID)
                {
                    this.comboBox1.Items.Add(itemHash.Key);
                    comboBox1.SelectedIndex = 0;
                    comboBox1.Enabled = true;
                    if (itemHash.Key.Equals(comboBox1.SelectedItem))
                    {
                        FunCGeneral.ipMayChuDonVi = "http://" + itemHash.Value.ip + ":58080/autobank/services/vimassTool/dieuPhoi";
                        FunCGeneral.mcID = itemHash.Value.mcID;
                    }
                }
                FunCGeneral.AutoSizeCombobox(comboBox1);
                this.WindowState = FormWindowState.Maximized;
                if (FunCGeneral.lGinKQ != null && (FunCGeneral.lGinKQ.soThe != null || FunCGeneral.lGinKQ.phone != null))
                {
                    labelDangNhap.Text = FunCGeneral.lGinKQ.hoTen;
                }
                ThietBi.capNhatThietBiTuServer();
                

            }
            catch (Exception ex)
            {
                Logger.LogServices("ThongKe_Load Exception: " + ex.Message);
            }



        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void thốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                activeNut(sender, e);
                // Giả sử bạn có một Panel trên form của bạn để chứa UserControl
                Panel panelContainer = this.panel1; // panelMain là tên của Panel trên form của bạn

                // Xóa bất kỳ controls nào đã được thêm vào panel trước đó
                panelContainer.Controls.Clear();

                // Tạo một instance mới của UserControl
                ThongKe thongKeControl = new ThongKe();

                // Thiết lập kích thước và vị trí cho UserControl nếu cần
                thongKeControl.Dock = DockStyle.Fill; // Điều này sẽ làm cho UserControl phủ kín panelContainer

                // Thêm UserControl vào panel
                panelContainer.Controls.Add(thongKeControl);
                if (GiaoDichDinhDanh.actionTimer != null)
                {
                    GiaoDichDinhDanh.actionTimer.Stop();
                }
            }
            catch(Exception ex)
            {

            }
         
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {

        }

        private void thongKe1_Load(object sender, EventArgs e)
        {

        }

        private void ngườiDùngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                activeNut(sender, e);
                Panel panelContainer = this.panel1; // panelMain là tên của Panel trên form của bạn

                // Xóa bất kỳ controls nào đã được thêm vào panel trước đó
                panelContainer.Controls.Clear();

                // Tạo một instance mới của UserControl
                NguoiDung nguoiDung = new NguoiDung();

                // Thiết lập kích thước và vị trí cho UserControl nếu cần
                nguoiDung.Dock = DockStyle.Fill; // Điều này sẽ làm cho UserControl phủ kín panelContainer

                // Thêm UserControl vào panel
                panelContainer.Controls.Add(nguoiDung);
                if (GiaoDichDinhDanh.actionTimer != null)
                {
                    GiaoDichDinhDanh.actionTimer.Stop();
                }

            }
            catch(Exception ex) { 
            }
     
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (KeyValuePair<String, ObjecthttpMayChuDonVi> itemHash in FunCGeneral.hashMCID)
            {
                if (itemHash.Key.Equals(comboBox1.SelectedItem))
                {
                    FunCGeneral.ipMayChuDonVi = "http://" + itemHash.Value.ip + ":58080/autobank/services/vimassTool/dieuPhoi";
                    FunCGeneral.mcID = itemHash.Value.mcID;
                }
            }
        }

        private void giaoDịchĐịnhDanhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                activeNut(sender, e);
                // Giả sử bạn có một Panel trên form của bạn để chứa UserControl
                Panel panelContainer = this.panel1; // panelMain là tên của Panel trên form của bạn

                // Xóa bất kỳ controls nào đã được thêm vào panel trước đó
                panelContainer.Controls.Clear();

                // Tạo một instance mới của UserControl
                GiaoDichDinhDanh giaoDichDinhDanh = new GiaoDichDinhDanh();

                // Thiết lập kích thước và vị trí cho UserControl nếu cần
                giaoDichDinhDanh.Dock = DockStyle.Fill; // Điều này sẽ làm cho UserControl phủ kín panelContainer

                // Thêm UserControl vào panel
                panelContainer.Controls.Add(giaoDichDinhDanh);

            }
            catch(Exception ex)
            {

            }
          

        }

        private void thiếtBịToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                activeNut(sender, e);
                // Giả sử bạn có một Panel trên form của bạn để chứa UserControl
                Panel panelContainer = this.panel1; // panelMain là tên của Panel trên form của bạn

                // Xóa bất kỳ controls nào đã được thêm vào panel trước đó
                panelContainer.Controls.Clear();

                // Tạo một instance mới của UserControl
                ThietBi thietBi = new ThietBi();

                // Thiết lập kích thước và vị trí cho UserControl nếu cần
                thietBi.Dock = DockStyle.Fill; // Điều này sẽ làm cho UserControl phủ kín panelContainer

                // Thêm UserControl vào panel
                panelContainer.Controls.Add(thietBi);

                if (GiaoDichDinhDanh.actionTimer != null)
                {
                    GiaoDichDinhDanh.actionTimer.Stop();
                }
            }
            catch(Exception ex)
            {

            }
          
        }

        private void điểmThanhToánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            activeNut(sender, e);
            if (GiaoDichDinhDanh.actionTimer != null)
            {
                GiaoDichDinhDanh.actionTimer.Stop();
            }
        }

        private void điểmĐịnhDanhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            activeNut(sender, e);

            try
            {
                activeNut(sender, e);
                // Giả sử bạn có một Panel trên form của bạn để chứa UserControl
                Panel panelContainer = this.panel1; // panelMain là tên của Panel trên form của bạn

                // Xóa bất kỳ controls nào đã được thêm vào panel trước đó
                panelContainer.Controls.Clear();

                // Tạo một instance mới của UserControl
                DiemDinhDanh diemDinhDanh = new DiemDinhDanh();

                // Thiết lập kích thước và vị trí cho UserControl nếu cần
                diemDinhDanh.Dock = DockStyle.Fill; // Điều này sẽ làm cho UserControl phủ kín panelContainer

                // Thêm UserControl vào panel
                panelContainer.Controls.Add(diemDinhDanh);

                if (GiaoDichDinhDanh.actionTimer != null)
                {
                    GiaoDichDinhDanh.actionTimer.Stop();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void giaoDịchThanhToánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            activeNut(sender, e);
            if (GiaoDichDinhDanh.actionTimer != null)
            {
                GiaoDichDinhDanh.actionTimer.Stop();
            }
        }

        private void phânQuyềnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            activeNut(sender, e);
            if (GiaoDichDinhDanh.actionTimer != null)
            {
                GiaoDichDinhDanh.actionTimer.Stop();
            }
        }
        private void SetDefaultColors()
        {
            thiếtBịToolStripMenuItem.BackColor = SystemColors.Control;
            ngườiDùngToolStripMenuItem.BackColor = SystemColors.Control;
            điểmĐịnhDanhToolStripMenuItem.BackColor = SystemColors.Control;
            giaoDịchĐịnhDanhToolStripMenuItem.BackColor = SystemColors.Control;
            điểmThanhToánToolStripMenuItem.BackColor = SystemColors.Control;
            giaoDịchThanhToánToolStripMenuItem.BackColor = SystemColors.Control;
            thốngKêToolStripMenuItem.BackColor = SystemColors.Control;
            phânQuyềnToolStripMenuItem.BackColor = SystemColors.Control;
            // Lặp lại cho các ToolStripMenuItem khác
        }
        private void activeNut(object sender, EventArgs e)
        {
            try
            {
                SetDefaultColors();

                // Thay đổi màu của ToolStripMenuItem được chọn
                ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;
                if (clickedItem != null)
                {
                    clickedItem.BackColor = Color.LightBlue; // Chọn màu bạn thích
                }

            }
            catch(Exception ex)
            {

            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(comboBox2.Text!=null && !comboBox2.Text.Equals(""))
                {
                    if(comboBox2.Text.Equals("V1"))
                    {
                        foreach (KeyValuePair<String, Device> item in ThietBi.hashDevice)
                        {
                            if (item.Value.typeDevice.Equals(1) || item.Value.typeDevice.Equals(2))
                            {

                                if (item.Value.stt == 1)
                                {
                                    FunCGeneral.ipMayChuDonVi = "http://" + item.Value.ip + ":58080/autobank/services/vimassTool/dieuPhoi";
                                }


                            }
                        }
                    }
                    else
                    {
                        foreach (KeyValuePair<String, Device> item in ThietBi.hashDevice)
                        {
                            if (item.Value.typeDevice.Equals(1) || item.Value.typeDevice.Equals(2))
                            {

                                if (item.Value.stt == 2)
                                {
                                    FunCGeneral.ipMayChuDonVi = "http://" + item.Value.ip + ":58080/autobank/services/vimassTool/dieuPhoi";
                                }

                            }
                        }

                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
