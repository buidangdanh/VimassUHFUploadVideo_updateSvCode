using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VimassUHFUploadVideo.Vpass.Object;

namespace VimassUHFUploadVideo.Vpass.GiaoDien.NhomForm
{
    public partial class Sua : Form
    {
        public Sua()
        {
            InitializeComponent();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Sua_Load(object sender, EventArgs e)
        {
            try
            {
                khoiTaoDataGrid();
                thayDoiKichThuoc2();
                foreach (KeyValuePair<String, ObjectGoup> item in NguoiDung.hashNhomCache)
                {
                    textBox7.Text= item.Key;
                    HienThiThanhVien(item.Value.listPer, item.Key);
                    goiLayKhuonMat(item.Value.listPer);
                }
            }
            catch (Exception ex)
            {
                Logger.LogServices("Sua Sua_Load: " + ex.Message);

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
                dataGridView1.Columns.Add("Column2", "Số điện thoại/số thẻ");
                dataGridView1.Columns.Add("Column3", "Họ tên");
                dataGridView1.Columns.Add("Column4", "Chức danh");
                dataGridView1.Columns.Add("Column5", "Nhóm");
                dataGridView1.Columns.Add("Column7", "Khuôn mặt");
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
                    dataGridView1.Columns["Column1"].Width = (int)(totalWidth * 0.05); // 50% chiều rộng STT
                    dataGridView1.Columns["Column2"].Width = (int)(totalWidth * 0.2); // 50% chiều rộng sdt
                    dataGridView1.Columns["Column3"].Width = (int)(totalWidth * 0.25); // 50% chiều rộng ht
                    dataGridView1.Columns["Column4"].Width = (int)(totalWidth * 0.2); // 50% chiều rộng cd
                    dataGridView1.Columns["Column5"].Width = (int)(totalWidth * 0.25); // 50% chiều rộng nh
                    dataGridView1.Columns["Column7"].Width = (int)(totalWidth * 0.1); // 50% chiều rộng nh
                    dataGridView1.Columns["Column6"].Width = (int)(totalWidth * 0.05); // 50% chiều rộng STT
                }
                catch (Exception ex)
                {
                    Logger.LogServices("dataGridView2_Resize Exception: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                Logger.LogServices("thayDoiKichThuoc Exception: " + ex.Message);
            }


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
        public async void HienThiThanhVien(List<ObjListPer> itemHash, String groupName)
        {
            try
            {
                dataGridView1.Rows.Clear();
                int i = 1;
                for (int i2 = 0; i2 < itemHash.Count(); i2++)
                {

                    if (itemHash[i2].sdt != null && !itemHash[i2].sdt.Equals(""))
                    {
                        dataGridView1.Rows.Add(itemHash[i2].id, i, itemHash[i2].sdt, itemHash[i2].name.Trim().Replace("  ", "").Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("\u0000", ""), itemHash[i2].chucDanh, groupName, "");
                        dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    else
                    {
                        dataGridView1.Rows.Add(itemHash[i2].id, i, itemHash[i2].vID, itemHash[i2].name.Trim().Replace("  ", "").Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("\u0000", ""), itemHash[i2].chucDanh, groupName, "");
                        dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    i++;
                }
            }
            catch (Exception ex)
            {
                Logger.LogServices("HienThiThanhVien Exception: " + ex.Message);

            }

        }
        private CancellationTokenSource cancellationTokenSource;
        public void CancelTasks()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
            }
        }
        private void goiLayKhuonMat(List<ObjListPer> listPer)
        {
            CancelTasks();
            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            try
            {
                List<Task> tasks = new List<Task>();

                for (int i = 0; i < listPer.Count(); i++)
                {
                    int index = i;
                    Task task = Task.Run(() =>
                    {
                        if (token.IsCancellationRequested)
                        {
                            // Nếu task đã bị hủy, thoát khỏi task
                            return;
                        }

                        bool result = false;
                        if (listPer[index].vID != null && !listPer[index].vID.Equals(""))
                        {
                            result = NguoiDung.checkMat10603(listPer[index]);
                        }

                        if (!IsDisposed && IsHandleCreated)
                        {
                            Invoke(new Action(() =>
                            {
                                if (dataGridView1.Rows.Count > index)
                                {
                                    dataGridView1.Rows[index].Cells["Column7"].Value = result ? "v" : "";
                                }
                            }));
                        }

                    }, token);

                    tasks.Add(task);
                }

                Task.WhenAll(tasks).ContinueWith(t =>
                {
                    // Các thao tác sau khi tất cả các tasks hoàn thành, nếu cần
                }, token);
            }
            catch (Exception ex)
            {
                Logger.LogServices("goiLayKhuonMat Exception: " + ex.Message);

            }
        }
    }
}
