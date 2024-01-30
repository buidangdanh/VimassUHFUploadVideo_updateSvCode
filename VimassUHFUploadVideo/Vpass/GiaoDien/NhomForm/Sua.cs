using Newtonsoft.Json;
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
using VimassUHFUploadVideo.Ultil;
using System.Diagnostics;
using VimassUHFUploadVideo.Vpass.Object.ObjectThietBi;

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
                radioButton1.Checked = true;
                comboBox1.Items.Add("Hiệu trưởng");
                comboBox1.Items.Add("Hiệu phó");
                comboBox1.Items.Add("Giáo viên chủ nhiệm");
                comboBox1.Items.Add("Giáo viên bộ môn");
                comboBox1.Items.Add("Cán bộ nhân viên");
                comboBox1.Items.Add("Học sinh");
                comboBox1.SelectedIndex = 0;
                khoiTaoDataGrid();
                thayDoiKichThuoc2();
                foreach (KeyValuePair<String, ObjectGoup> item in NguoiDung.hashNhomCache)
                {
                    textBox2.Text= item.Key;
                    HienThiThanhVien(item.Value.listPer, item.Key);
                    goiLayKhuonMat(item.Value.listPer);
                }
                NguoiDung.hashNhomCache.First().Value.type = 2;
            }
            catch (Exception ex)
            {
                Logger.LogServices("Sua Sua_Load: " + ex.Message);

            }
        }
        private void hienThiDsNguoi()
        {
            try
            {
                foreach (KeyValuePair<String, ObjectGoup> item in NguoiDung.hashNhomCache)
                {
                    HienThiThanhVien(item.Value.listPer, item.Key);
                    goiLayKhuonMat(item.Value.listPer);
                }
            }
            catch(Exception ex)
            {

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
                    if (itemHash[i2].type != 3)
                    {
                        if (itemHash[i2].sdt != null && !itemHash[i2].sdt.Equals(""))
                        {
                            dataGridView1.Rows.Add(itemHash[i2].id,i, itemHash[i2].sdt, itemHash[i2].name.Trim().Replace("  ", "").Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("\u0000", ""), itemHash[i2].chucDanh, groupName, "");
                            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        else
                        {
                            dataGridView1.Rows.Add(itemHash[i2].id,i, itemHash[i2].vID, itemHash[i2].name.Trim().Replace("  ", "").Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("\u0000", ""), itemHash[i2].chucDanh, groupName, "");
                            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        i++;

                    }
                  
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

                        int result = 0; // Thay đổi ở đây
                        if (listPer[index].vID != null && !listPer[index].vID.Equals(""))
                        {
                            result =NguoiDung.checkMat10603(listPer[index]); // Thay đổi ở đây
                        }

                        if (!IsDisposed && IsHandleCreated)
                        {
                            Invoke(new Action(() =>
                            {
                                if (dataGridView1.Rows.Count > index)
                                {
                                    dataGridView1.Rows[index].Cells["Column7"].Value = result != 0 ? result.ToString() : ""; // Cập nhật ở đây
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
            catch (Exception e)
            {
                // Xử lý ngoại lệ
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox7.Text = "";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox7.Text = "V";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
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
                        kq = layThongTinThe(idCheck)[0].personName;
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
                objectTraThongTin.vID = text;
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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

                        objListPer.id = objListPer.sdt + objListPer.perNum+ NguoiDung.hashNhomCache.First().Value.id;
                        NguoiDung.hashNhomCache.First().Value.listPer.Add(objListPer);
                        hienThiDsNguoi();
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
                        objListPerThe.name = textBox1.Text;
                        objListPerThe.chucDanh = comboBox1.Text;
                        objListPerThe.idThietBi = "";
                        objListPerThe.uID = layThongTinThe(textBox7.Text.Replace("V", ""))[0].uID;
                        objListPerThe.vID = textBox7.Text;
                        objListPerThe.avatar = "";
                        objListPerThe.perNum = 0;
                        // objListPerThe.perNum = layThongTinThe(textBox7.Text.Replace("V", ""))[0].personNumber;
                        objListPerThe.type = 1;
                        objListPerThe.id = FunCGeneral.timeNow().ToString() + FunCGeneral.GenerateRandomCharacters(3)+ NguoiDung.hashNhomCache.First().Value.id;

                        NguoiDung.hashNhomCache.First().Value.listPer.Add(objListPerThe);
                        hienThiDsNguoi();
                        textBox1.Text = "";
                        comboBox1.SelectedIndex = 0;
                        textBox7.Text = "V";

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
                /*else
                {
                    if (textBox7.Text != null && !textBox7.Text.Equals("") && textBox7.Text.Length > 6)
                    {
                        textBox1.Text = layName(textBox7.Text.Replace("V",""));
                    }

                }*/

            }
            catch (Exception ex)
            {
                Logger.LogServices("textBox7_TextChanged Exception: " + ex.Message);
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
                            thayDoiType(key);
                            HienThiThanhVien(NguoiDung.hashNhomCache.First().Value.listPer, NguoiDung.hashNhomCache.First().Key);
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
                foreach (ObjListPer obj in NguoiDung.hashNhomCache.First().Value.listPer)
                {
                    if (id!=null&&obj.id.Equals(id)) {
                        obj.type = 3;
                    }
                }
            }catch(Exception ex)
            {
                Logger.LogServices("thayDoiType Exception: " + ex.Message);

            }
        }
    }
}
