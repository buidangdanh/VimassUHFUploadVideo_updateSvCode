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

namespace VimassUHFUploadVideo.Vpass.GiaoDien.NhomForm
{
    public partial class ThemNhomCapHai : Form
    {
        public ThemNhomCapHai()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ThemNhomCapHai_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = "Thêm nhóm cấp 1";
                dataGridView1.Columns.Add("Column1", "STT");
                dataGridView1.Columns.Add("Column2", "Nhóm cấp 1");
                DataGridViewImageColumn column3 = new DataGridViewImageColumn();
                {
                    column3.Name = "Column3";
                    column3.HeaderText = "";
                    column3.ImageLayout = DataGridViewImageCellLayout.Zoom; // Thiết lập layout để icon được zoom và căn giữa
                }
                dataGridView1.Columns.Add(column3);
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
                foreach (ObjectGoup arr in NguoiDung.listGroup)
                {
                    if(arr != null && arr.groupLevel == 1)
                    {
                        ThemNhomMoi.hashListGroupCap1.Add(arr.groupName, arr);
                    }
                }
                foreach (KeyValuePair<String, ObjectGoup> item in ThemNhomMoi.hashListGroupCap1)
                {
                    comboBox1.Items.Add(item.Key);
                }
                comboBox1.SelectedIndex = 0;
                themVaoBangCache();

            }
            catch(Exception ex)
            {
                Logger.LogServices("ThemNhomCapHai_Load Exception: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (KeyValuePair<String, ObjectGoup> item in ThemNhomMoi.hashListGroupCap1)
                {
                    if (comboBox1.Text.Equals(item.Key))
                    {
                        item.Value.type = 1;
                        ThemNhomMoi.hashListGroup.Add(item.Key, item.Value);
                    }
                }

                themVaoBangCache();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Đã tồn tại nhóm này");
                Logger.LogServices("button2_Click Exception: " + ex.Message);
            }
        }
        private void thayDoiKichThuoc(DataGridView dataGridView)
        {
            try
            {
                int totalWidth = dataGridView.Width - SystemInformation.VerticalScrollBarWidth;

                // Thiết lập tỉ lệ cho từng cột
                dataGridView.Columns["Column1"].Width = (int)(totalWidth * 0.2); // 50% chiều rộng STT
                dataGridView.Columns["Column2"].Width = (int)(totalWidth * 0.7); // 30% chiều rộng SDT/the
                dataGridView.Columns["Column3"].Width = (int)(totalWidth * 0.1); // 30% chiều rộng Tên
            }
            catch (Exception ex)
            {
                Logger.LogServices("thayDoiKichThuoc Exception: " + ex.Message);
            }
          

        }
        private void themVaoBangCache()
        {
            try
            {
                int Stt = 1;
                dataGridView1.Rows.Clear();
                if (ThemNhomMoi.hashListGroup != null && ThemNhomMoi.hashListGroup.Count() > 0)
                {
                    foreach (KeyValuePair<String, ObjectGoup> item in ThemNhomMoi.hashListGroup)
                    {
                        dataGridView1.Rows.Add(Stt, item.Key);
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

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridView1.Columns["Column3"].Index && e.RowIndex >= 0)
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
            catch (Exception ex)
            {
                Logger.LogServices("dataGridView1_CellPainting Exception: " + ex.Message);
            }
          
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridView1.Columns["Column3"].Index && e.RowIndex >= 0)
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn xóa dòng này không?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        // Lấy key từ dòng được chọn (giả sử là ở cột 1)
                        string key = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

                        // Xóa từ hashListPers
                        if (ThemNhomMoi.hashListGroup.ContainsKey(key))
                        {
                            ThemNhomMoi.hashListGroup.Remove(key);
                        }

                        // Xóa dòng từ DataGridView
                        dataGridView1.Rows.RemoveAt(e.RowIndex);

                        // Cập nhật lại STT sau khi xóa
                        themVaoBangCache();
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.LogServices("dataGridView1_CellContentClick Exception: " + ex.Message);
            }
     
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ThemNhomMoi.ListGroup.Clear();
                foreach (KeyValuePair<String, ObjectGoup> item in ThemNhomMoi.hashListGroup)
                {
                    ThemNhomMoi.ListGroup.Add(item.Value);
                }
                this.Hide();

            }
            catch(Exception ex)
            {
                Logger.LogServices("button1_Click Exception: " + ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
