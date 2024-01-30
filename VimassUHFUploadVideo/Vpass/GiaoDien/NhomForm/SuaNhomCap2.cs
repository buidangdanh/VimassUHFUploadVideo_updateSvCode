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
using System.Diagnostics;

namespace VimassUHFUploadVideo.Vpass.GiaoDien.NhomForm
{
    public partial class SuaNhomCap2 : Form
    {
        public SuaNhomCap2()
        {
            InitializeComponent();
        }
        public static Dictionary<String, ObjectGoup> hashListGroupCap1 = new Dictionary<String, ObjectGoup>();
        private void SuaNhomCap2_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = "Sửa nhóm";
                hashListGroupCap1.Clear();
                khoiTaoDataGrid();
                thayDoiKichThuoc2();
                foreach (ObjectGoup arr in NguoiDung.listGroup)
                {
                    if (arr != null && arr.groupLevel == 1)
                    {
                        hashListGroupCap1.Add(arr.groupName, arr);
                    }
                }
                foreach (KeyValuePair<String, ObjectGoup> item in hashListGroupCap1)
                {
                    comboBox1.Items.Add(item.Key);
                }
                comboBox1.SelectedIndex = 0;
                textBox2.Text = NguoiDung.hashNhomCache.First().Value.groupName;
                NguoiDung.hashNhomCache.First().Value.type = 2;
                hienThiNhomCap1();

            }
            catch (Exception ex)
            {
                Logger.LogServices("SuaNhomCap2_Load Exception: " + ex.Message);

            }

        }

        private void hienThiNhomCap1()
        {
            try
            {
                dataGridView1.Rows.Clear();
                int i3 = 1;
                foreach (ObjectGoup objectGoup in NguoiDung.hashNhomCache.First().Value.listGr)
                {
                    foreach (KeyValuePair<String, ObjectGoup> itemHash2 in FunCGeneral.hashNhom)
                    {
                        if (itemHash2.Key.Equals(objectGoup.groupName))
                        {
                            if (objectGoup.type != 3)
                            {
                                dataGridView1.Rows.Add(objectGoup.id, i3, objectGoup.groupName);
                                dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                i3++;
                            }
                           
                        }
                    }
                 
                }

            }
            catch (Exception ex)
            {
                Logger.LogServices("hienThiNhomCap1 Exception: " + ex.Message);

            }
        }

        private void khoiTaoDataGrid()
        {
            try
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                //tạo cột ẩn
                DataGridViewColumn columnID2 = new DataGridViewTextBoxColumn();
                columnID2.HeaderText = "ID";
                columnID2.Name = "ID";
                columnID2.Visible = false; // Làm cho cột không hiển thị
                dataGridView1.Columns.Add(columnID2);

                dataGridView1.Columns.Add("Column1", "STT");
                dataGridView1.Columns.Add("Column2", "Nhóm cấp 1");
                DataGridViewImageColumn imgColData2 = new DataGridViewImageColumn();
                {
                    imgColData2.Name = "Column6";
                    imgColData2.HeaderText = "";
                    imgColData2.ImageLayout = DataGridViewImageCellLayout.Zoom; // Thiết lập layout để icon được zoom và căn giữa
                }
                dataGridView1.Columns.Add(imgColData2);
                dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 13); // Thay đổi kiểu chữ và cỡ chữ
                dataGridView1.RowTemplate.Height = 30; // Thay đổi chiều cao của hàng thành 30 pixel
                dataGridView1.BackgroundColor = Color.White;
                //Thiet lap màu của tên cột
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(242, 249, 255); // Chọn màu bạn muốn
                dataGridView1.EnableHeadersVisualStyles = false; // Cần thiết để màu tùy chỉnh có hiệu lực
                dataGridView1.ReadOnly = true;
                dataGridView1.CurrentCell = null;

                foreach (DataGridViewColumn column in dataGridView1.Columns)
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
        private void thayDoiKichThuoc2()
        {
            try
            {
                try
                {
                    int totalWidth = dataGridView1.Width - SystemInformation.VerticalScrollBarWidth;

                    // Thiết lập tỉ lệ cho từng cột
                    dataGridView1.Columns["Column1"].Width = (int)(totalWidth * 0.1); // 50% chiều rộng STT
                    dataGridView1.Columns["Column2"].Width = (int)(totalWidth * 0.8); // 50% chiều rộng sdt
                    dataGridView1.Columns["Column6"].Width = (int)(totalWidth * 0.1); // 50% chiều rộng sdt
                }
                catch (Exception ex)
                {
                    Logger.LogServices("thayDoiKichThuoc2 Exception: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                Logger.LogServices("thayDoiKichThuoc2 Exception: " + ex.Message);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (KeyValuePair<String, ObjectGoup> item in hashListGroupCap1)
                {
                    if (comboBox1.Text.Equals(item.Key))
                    {
                        item.Value.type = 0;
                        NguoiDung.hashNhomCache.First().Value.listGr.Add(item.Value);
                    }
                }
                hienThiNhomCap1();

                //themVaoBangCache();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã tồn tại nhóm này");
                Logger.LogServices("button2_Click Exception: " + ex.Message);
            }
        }
        private void themVaoBangCache()
        {
            /* try
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
             }*/
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Column6"].Index && e.RowIndex >= 0)
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ObjectGroupRQServer obj = new ObjectGroupRQServer();
                obj.user = "0966074236";
                obj.currentTime = FunCGeneral.timeNow();
                obj.deviceID = 3;
                obj.mcID = FunCGeneral.mcID;
                obj.listGr = new List<ObjectGoup>();
                obj.listGr.Add(NguoiDung.hashNhomCache.First().Value);
                obj.cks = FunctionGeneral.Md5(obj.user + obj.deviceID + "ZgVCHxqMd$aN11ggg2YHD" + obj.currentTime + obj.mcID).ToLower();
                String url = "http://210.245.8.7:12318/vimass/services/VUHF/nhomRV";
                var json = JsonConvert.SerializeObject(obj);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);

                if (response != null && response.msgCode == 1)
                {
                    MessageBox.Show("Sửa nhóm " + NguoiDung.hashNhomCache.First().Key + " thành công");
                    NguoiDung.hashNhomCache.Clear();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Debug.Write(ex.ToString());
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridView1.Columns["Column6"].Index && e.RowIndex >= 0)
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn xóa dòng này không?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        // Lấy key từ dòng được chọn (giả sử là ở cột 1)
                        if (dataGridView1.Rows[e.RowIndex].Cells[0].Value != null)
                        {
                            string key = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

                            // Xóa từ hashListPers

                            Debug.WriteLine(key);
                            // Xóa dòng từ DataGridView


                            // Cập nhật lại STT sau khi xóa
                            XoaKhoiListGroup(key);
                            hienThiNhomCap1();
                            dataGridView1.Rows.RemoveAt(e.RowIndex);
                        }



                    }
                }


            }
            catch (Exception ex)
            {
                Logger.LogServices("dataGridView1_CellContentClick Exception: " + ex.Message);
            }
        }
        private void thayDoiType(String id)
        {
            try
            {
                foreach (ObjectGoup obj in NguoiDung.hashNhomCache.First().Value.listGr)
                {
                    if (id != null && obj.id.Equals(id))
                    {
                        obj.type = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogServices("thayDoiType Exception: " + ex.Message);

            }
        }
        private void XoaKhoiListGroup(String id)
        {
            try
            {
                foreach (ObjectGoup obj in NguoiDung.hashNhomCache.First().Value.listGr)
                {
                    if (id != null && obj.id.Equals(id))
                    {
                        NguoiDung.hashNhomCache.First().Value.listGr.Remove(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogServices("thayDoiType Exception: " + ex.Message);

            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                NguoiDung.hashNhomCache.First().Value.groupName = textBox2.Text;
            }
            catch (Exception ex)
            {
                Logger.LogServices("textBox2_TextChanged Exception: " + ex.Message);

            }
        }
    }
}
