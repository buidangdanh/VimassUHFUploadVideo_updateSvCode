using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VimassUHFUploadVideo.Vpass.Object;
using VimassUHFUploadVideo.Ultil;
using System.Diagnostics;

namespace VimassUHFUploadVideo.Vpass.GiaoDien.NhomForm
{
    public partial class ThemThanhVien : Form
    {
        public ThemThanhVien()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ThemThanhVien_Load(object sender, EventArgs e)
        {
            try
            {

                radioButton1.Checked = true;
                //Thiet lap cac thuoc tinh cua trang
                dataGridView1.Columns.Add("Column1", "STT");
                dataGridView1.Columns.Add("Column2", "Số điện thoại/số thẻ");
                dataGridView1.Columns.Add("Column3", "Họ tên");
                dataGridView1.Columns.Add("Column4", "Chức danh");
                DataGridViewImageColumn column5 = new DataGridViewImageColumn();
                {
                    column5.Name = "Column5";
                    column5.HeaderText = "";
                    column5.ImageLayout = DataGridViewImageCellLayout.Zoom; // Thiết lập layout để icon được zoom và căn giữa
                }
                dataGridView1.Columns.Add(column5);
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
                comboBox1.Items.Add("Hiệu trưởng");
                comboBox1.Items.Add("Hiệu phó");
                comboBox1.Items.Add("Giáo viên chủ nhiệm");
                comboBox1.Items.Add("Giáo viên bộ môn");
                comboBox1.Items.Add("Cán bộ nhân viên");
                comboBox1.Items.Add("Học sinh");
                comboBox1.SelectedIndex = 0;
                themVaoBangCache();

            }
            catch (Exception ex)
            {
                Logger.LogServices("ThemThanhVien_Load Exception: " + ex.Message);
            }

        }



        private void button1_Click(object sender, EventArgs e)
        {
            ThemNhomMoi.ListPers.Clear();
            foreach (KeyValuePair<String, ObjListPer> item in ThemNhomMoi.hashListPers)
            {
                
                ThemNhomMoi.ListPers.Add(item.Value);
            }
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ObjListPer objListPer = new ObjListPer();
                if (radioButton1.Checked)
                {
                    if (textBox1.Text != null && !textBox1.Text.Equals(""))
                    {
                        objListPer.sdt = textBox7.Text;
                        objListPer.name = textBox1.Text;
                        objListPer.chucDanh = comboBox1.Text;
                        objListPer.idThietBi = "";
                        objListPer.uID = "";
                        objListPer.vID = "";
                        objListPer.avatar = "";
                        objListPer.perNum = 0;
                        objListPer.type = 1;
                        ThemNhomMoi.hashListPers.Add(objListPer.sdt, objListPer);
                        themVaoBangCache();
                        textBox1.Text = "";
                        comboBox1.SelectedIndex = 0;
                        textBox7.Text = "";

                    }
                    else
                    {
                        MessageBox.Show("Vui lòng điền lại số điện thoại");
                    }
                }
                else if (radioButton2.Checked)
                {
                    if (textBox7.Text != null && !textBox7.Text.Equals("") && textBox7.Text.Count() > 6)
                    {
                        ObjListPer objListPerThe = new ObjListPer();
                        objListPerThe.sdt = "";
                        objListPerThe.name = layThongTinThe(textBox7.Text)[0].personName;
                        objListPerThe.chucDanh = comboBox1.Text;
                        objListPerThe.idThietBi = "";
                        objListPerThe.uID = layThongTinThe(textBox7.Text)[0].uID;
                        objListPerThe.vID = textBox7.Text;
                        objListPerThe.avatar = "";
                        objListPerThe.perNum = layThongTinThe(textBox7.Text)[0].personNumber;
                        objListPerThe.type = 1;
                        ThemNhomMoi.hashListPers.Add(objListPerThe.vID, objListPerThe);
                        themVaoBangCache();
                        textBox1.Text = "";
                        comboBox1.SelectedIndex = 0;
                        textBox7.Text = "";

                    }
                    else
                    {
                        MessageBox.Show("Vui lòng điền lại số điện thoại");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã tồn tại số thẻ/số điện thoại");
                Logger.LogServices("button2_Click Exception: " + ex.Message);
                Debug.WriteLine(ex.Message);
            }

        }

        private void themVaoBangCache()
        {
            try
            {
                int Stt = 1;
                dataGridView1.Rows.Clear();
                if (ThemNhomMoi.hashListPers != null && ThemNhomMoi.hashListPers.Count() > 0)
                {
                    foreach (KeyValuePair<String, ObjListPer> item in ThemNhomMoi.hashListPers)
                    {
                        dataGridView1.Rows.Add(Stt, item.Key, item.Value.name, item.Value.chucDanh);
                        dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        thayDoiKichThuoc(dataGridView1);
                        Stt++;
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.LogServices("themVaoBangCache Exception: " + ex.Message);
                Debug.WriteLine(ex.Message);
            }
        }

        private String layName(String idCheck)
        {
            String kq = "";
            try
            {
                if (radioButton1.Checked)
                {
                    layThongTinSDTRequest sDTRequest = new layThongTinSDTRequest();
                    if (textBox7.Text != null && !textBox7.Text.Equals(""))
                    {
                        sDTRequest.param = textBox7.Text.Trim();
                        sDTRequest.pass = "8249539tgsdlka";

                        string url = "http://210.245.8.6:8080/vmNoiBo/services/account/getAvt";
                        string param = "param" + ";" + sDTRequest.param + ";" + "pass" + ";" + sDTRequest.pass;
                        string result2 = Service.SendWebrequest_Get_Method(param, url);
                        ObjectResLayThongTinTuSDT response = JsonConvert.DeserializeObject<ObjectResLayThongTinTuSDT>(result2);

                        if (response != null)
                        {
                            kq = response.acc_name;
                        }


                    }


                }
                else
                {
                    if (textBox7.Text != null && !textBox7.Text.Equals(""))
                    {
                        kq = layThongTinThe(textBox7.Text)[0].personName;
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.LogServices("layName Exception: " + ex.Message);
            }
            return kq;
        }

        private List<ObjectResInfoThe> layThongTinThe(string text)
        {
            List<ObjectResInfoThe> listInfo = new List<ObjectResInfoThe>();
            try
            {
                ObjectTraThongTinThe objectTraThongTin = new ObjectTraThongTinThe();
                objectTraThongTin.vID = textBox7.Text.Trim();
                objectTraThongTin.currentTime = FunCGeneral.timeNow();
                objectTraThongTin.cks = FunctionGeneral.Md5("LJAuGHYaBe8dLy26" + objectTraThongTin.vID + "tenelntvahY04").ToLower();

                string url = "http://210.245.8.7:12318/vimass/services/cardAuthen/traCuuNguoiDungTrenMotThe";
                var json = JsonConvert.SerializeObject(objectTraThongTin);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);

                if (response != null && response.msgCode == 1)
                {
                    listInfo = JsonConvert.DeserializeObject<List<ObjectResInfoThe>>(response.result.ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.LogServices("layThongTinThe Exception: " + ex.Message);
            }
            return listInfo;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked)
                {
                    if (textBox7.Text != null && !textBox7.Text.Equals("") && textBox7.Text.Length > 9)
                    {
                        textBox1.Text = layName(textBox7.Text);
                    }

                }
                else
                {
                    if (textBox7.Text != null && !textBox7.Text.Equals("") && textBox7.Text.Length > 6)
                    {
                        textBox1.Text = layName(textBox7.Text);
                    }

                }

            }
            catch (Exception ex)
            {
                Logger.LogServices("textBox7_TextChanged Exception: " + ex.Message);
            }

        }


        private void thayDoiKichThuoc(DataGridView dataGridView)
        {
            int totalWidth = dataGridView.Width - SystemInformation.VerticalScrollBarWidth;

            // Thiết lập tỉ lệ cho từng cột
            dataGridView.Columns["Column1"].Width = (int)(totalWidth * 0.05); // 50% chiều rộng STT
            dataGridView.Columns["Column2"].Width = (int)(totalWidth * 0.25); // 30% chiều rộng SDT/the
            dataGridView.Columns["Column3"].Width = (int)(totalWidth * 0.3); // 30% chiều rộng Tên
            dataGridView.Columns["Column4"].Width = (int)(totalWidth * 0.3); // 30% chiều rộng ChucDanh
            dataGridView.Columns["Column5"].Width = (int)(totalWidth * 0.1); // 30% chiều rộng ChucDanh
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox7.Text = "";
            textBox1.Text = "";
            textBox7.Text = "";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox7.Text = "";

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Column5"].Index && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                Image myIcon = Properties.Resources.thungrac; // Giả sử đây là icon của bạn
                                                              // Tính toán vị trí để vẽ icon sao cho nó nằm giữa ô
                Size imageSize = myIcon.Size;
                Point iconLocation = new Point(
                    e.CellBounds.X + (e.CellBounds.Width - imageSize.Width) / 2,
                    e.CellBounds.Y + (e.CellBounds.Height - imageSize.Height) / 2);

                e.Graphics.DrawImage(myIcon, iconLocation);
                e.Handled = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Column5"].Index && e.RowIndex >= 0)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa dòng này không?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Lấy key từ dòng được chọn (giả sử là ở cột 1)
                    string key = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

                    // Xóa từ hashListPers
                    if (ThemNhomMoi.hashListPers.ContainsKey(key))
                    {
                        ThemNhomMoi.hashListPers.Remove(key);
                    }

                    // Xóa dòng từ DataGridView
                    dataGridView1.Rows.RemoveAt(e.RowIndex);

                    // Cập nhật lại STT sau khi xóa
                    themVaoBangCache();
                }
            }
        }
    }
}
