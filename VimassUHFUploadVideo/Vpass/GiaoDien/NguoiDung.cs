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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using VimassUHFUploadVideo.Ultil;
using System.Diagnostics;
using TextBox = System.Windows.Forms.TextBox;
using Button = System.Windows.Forms.Button;
using VimassUHFUploadVideo;
using com.sun.org.apache.bcel.@internal.generic;
using System.Threading;
using VimassUHFUploadVideo.Vpass.Object.ObjectNguoiDung;
using VimassUHFUploadVideo.Vpass.GiaoDien.NhomForm;
using System.Globalization;

namespace VimassUHFUploadVideo.Vpass.GiaoDien
{
    public partial class NguoiDung : UserControl
    {
        public NguoiDung()
        {
            InitializeComponent();
        }
        private TextBox textBox;
        public static List<ObjectGoup> listGroup = new List<ObjectGoup>();
        private List<ObjListPer> arr = new List<ObjListPer>();
        public static List<ObjectGoup> listGroupThayDoi = new List<ObjectGoup>();
        public static Dictionary<String, ObjectGoup> hashNhomCache = new Dictionary<String, ObjectGoup>();
        public static String sdtOrVidCache = "";
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void NguoiDung_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(166, 166, 166);
            //Thiet lap cac thuoc tinh cua dataGridView1
            //Tạo cột ẩn
            DataGridViewColumn columnID1 = new DataGridViewTextBoxColumn();
            columnID1.HeaderText = "ID";
            columnID1.Name = "ID";
            columnID1.Visible = false; // Làm cho cột không hiển thị
            dataGridView1.Columns.Add(columnID1);
            dataGridView1.Columns.Add("Column1", "STT");
            dataGridView1.Columns.Add("Column2", "Tên nhóm");
            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            {
                imgCol.Name = "Column3";
                imgCol.HeaderText = "";
                imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom; // Thiết lập layout để icon được zoom và căn giữa
            }
            dataGridView1.Columns.Add(imgCol);



            //Thiết lập font màu backcolor
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 13); // Thay đổi kiểu chữ và cỡ chữ
            dataGridView1.RowTemplate.Height = 30; // Thay đổi chiều cao của hàng thành 30 pixel
            dataGridView1.BackgroundColor = Color.FromArgb(166, 166, 166);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(242, 249, 255); // Chọn màu bạn muốn
            dataGridView1.EnableHeadersVisualStyles = false; // Cần thiết để màu tùy chỉnh có hiệu lực
            dataGridView1.ReadOnly = true;
            // Căn chỉnh icon ở giữa
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }
            //Thiet lap cac thuoc tinh cua trang
            DataGridViewColumn columnID2 = new DataGridViewTextBoxColumn();
            columnID2.HeaderText = "ID";
            columnID2.Name = "ID";
            columnID2.Visible = false; // Làm cho cột không hiển thị
            dataGridView2.Columns.Add(columnID2);
            dataGridView2.Columns.Add("Column1", "STT");
            dataGridView2.Columns.Add("Column2", "Số điện thoại/số thẻ");
            dataGridView2.Columns.Add("Column3", "Họ tên");
            dataGridView2.Columns.Add("Column4", "Chức danh");
            dataGridView2.Columns.Add("Column5", "Nhóm");
            DataGridViewImageColumn imgColData2 = new DataGridViewImageColumn();
            {
                imgColData2.Name = "Column6";
                imgColData2.HeaderText = "";
                imgColData2.ImageLayout = DataGridViewImageCellLayout.Zoom; // Thiết lập layout để icon được zoom và căn giữa
            }
            dataGridView2.Columns.Add(imgColData2);



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

            // panelMain là tên của Panel trên form của bạn
            // Thêm UserControl vào panel
            // panelContainer.Controls.Add(textBox);
            HienNhomCoMau();
            panelContainer2 = this.flowLayoutPanel2;
            //Phần tìm kiếm
            textBox = new TextBox();
            textBox.Multiline = true;
            textBox.Size = new Size(300, 29);
            panelContainer2 = this.flowLayoutPanel2;
            panelContainer2.FlowDirection = FlowDirection.RightToLeft; // Cài đặt hướng
            panelContainer2.Controls.Add(textBox);
            //Phần bấm tìm
            Button btnTim = new Button();
            btnTim.Text = "Tìm kiếm";
            btnTim.Size = new Size(150, 29);
            btnTim.TextAlign = ContentAlignment.MiddleCenter;
            //Phần nút thêm người
            Button btnThem = new Button();
            btnThem.Size = new Size(30, 30);
            btnThem.ImageAlign = ContentAlignment.MiddleCenter;
            btnThem.Image = Properties.Resources.them;
            panelContainer2.Controls.Add(btnThem);
            panelContainer2.Controls.Add(btnTim);
            panelContainer2.Controls.Add(textBox);
            //thêm sự kiện cho nút thêm
            //btnThem.Click += new EventHandler(ButtonClick);
        }


        private void ButtonClick2(object sender, EventArgs e)
        {
            HienNhomCoMau();
        }
        private void ButtonClick(object sender, EventArgs e)
        {
            //Khởi tạo lại column 2
            dataGridView2.Rows.Clear();
            ObjectGoiDichVuMini o = new ObjectGoiDichVuMini();
            o.funcId = 10602;
            o.device = 2;
            o.currentime = FunCGeneral.timeNow();

            ObjectGetInfoVID oLDSGR = new ObjectGetInfoVID();
            oLDSGR.mcID = FunCGeneral.mcID;
            oLDSGR.offset = 0;
            oLDSGR.limit = 100;
            oLDSGR.typeFace = 1;
            // Truy cập TextBox từ form
            oLDSGR.textSearch = textBox.Text;
            o.data = JsonConvert.SerializeObject(oLDSGR);

            String url = FunCGeneral.ipMayChuDonVi;
            //String url = "http://113.190.248.142:58080/autobank/services/vimassTool/dpDanh";
            var json = JsonConvert.SerializeObject(o);
            String res = Service.SendWebrequest_POST_Method(json, url);
            Response response = JsonConvert.DeserializeObject<Response>(res);

            if (response != null && response.msgCode == 1)
            {

                ResultResponeMini value = JsonConvert.DeserializeObject<ResultResponeMini>(response.result.ToString());
                String valueTraVe = FunctionGeneral.DecodeBase64String(value.value);
                var arrL = JsonConvert.DeserializeObject<List<ObjectInfoVid>>(valueTraVe);
                int i = 0;
                foreach (ObjectInfoVid arr in arrL)
                {
                    i = i + 1;
                    if (arr.faceData != null && !arr.faceData.Equals(""))
                    {
                        if (!arr.dienThoai.Equals(""))
                        {
                            dataGridView2.Rows.Add(+i, arr.dienThoai, arr.hoTen, "x");
                            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        else
                        {
                            dataGridView2.Rows.Add(+i, arr.idVid, arr.hoTen, "x");
                            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }
                    else
                    {
                        if (!arr.dienThoai.Equals(""))
                        {
                            dataGridView2.Rows.Add(+i, arr.dienThoai, arr.hoTen, "");
                            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        else
                        {
                            dataGridView2.Rows.Add(+i, arr.idVid, arr.hoTen, "");
                            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }


                }
            }
        }



        private void bunifuButton1_Click(object sender, EventArgs e)
        {

        }

        public static List<ObjectGoup> layNhomLocal()
        {
            try
            {
                ObjectGoiDichVuMini o = new ObjectGoiDichVuMini();
                o.funcId = 11801;
                o.device = 2;
                o.currentime = FunCGeneral.timeNow();

                layDanhSachGroupRequest oLDSGR = new layDanhSachGroupRequest();
                oLDSGR.mcID = FunCGeneral.mcID;


                o.data = JsonConvert.SerializeObject(oLDSGR);

                String url = FunCGeneral.ipMayChuDonVi;
                //String url = "http://193.169.1.44:58080/autobank/services/vimassTool/dieuPhoi";
                var json = JsonConvert.SerializeObject(o);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);

                if (response != null && response.msgCode == 1)
                {

                    ResultResponeMini value = JsonConvert.DeserializeObject<ResultResponeMini>(response.result.ToString());
                    String valueTraVe = FunctionGeneral.DecodeBase64String(value.value);
                    listGroup = JsonConvert.DeserializeObject<List<ObjectGoup>>(valueTraVe);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return listGroup;
        }

        public void capNhatNhomServer()
        {
            try
            {
                dataGridView1.Rows.Clear();
                FunCGeneral.hashNhom.Clear();
                khoiTaoDataGridView1();
                thayDoiKichThuoc1();
                listGroup = layNhomTuSerVer();


                for (int i = 0; i < listGroup.Count(); i++)
                {

                    FunCGeneral.hashNhom.Add(listGroup[i].groupName, listGroup[i]);
                    //hashNhomCache.Add(listGroup[i].groupName, listGroup[i]);
                    Debug.WriteLine(listGroup[i].groupName);
                    int stt = i + 1;
                    dataGridView1.Rows.Add(stt, listGroup[i].groupName);
                    dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    HienThiNhom(listGroup[0].groupName);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void HienNhomCoMau()
        {
            try
            {
                dataGridView1.Rows.Clear();
                FunCGeneral.hashNhom.Clear();
                khoiTaoDataGridView1();
                thayDoiKichThuoc1();
                listGroup = layNhomTuSerVer();

                for (int i = 0; i < listGroup.Count(); i++)
                {
                    FunCGeneral.hashNhom.Add(listGroup[i].groupName, listGroup[i]);
                    //hashNhomCache.Add(listGroup[i].groupName, listGroup[i]);
                    Debug.WriteLine(listGroup[i].groupName);
                    int stt = i + 1;

                    DataGridViewRow newRow = new DataGridViewRow();
                    newRow.CreateCells(dataGridView1, listGroup[i].id, stt, listGroup[i].groupName);
                    newRow.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    newRow.Height = 30;
                    // Thay đổi màu dựa trên grouplevel
                    if (listGroup[i].groupLevel == 1)
                    {
                        newRow.DefaultCellStyle.ForeColor = Color.Black; // Màu chữ trắng cho tương phản
                    }
                    else if (listGroup[i].groupLevel == 2)
                    {
                        newRow.DefaultCellStyle.ForeColor = Color.FromArgb(255, 108, 55); // Màu chữ trắng cho tương phản
                    }

                    dataGridView1.Rows.Add(newRow);
                    HienThiNhom(listGroup[0].groupName);
                }
                // Đặt chế độ chọn của DataGridView là FullRowSelect để khi bạn chọn một ô, cả hàng sẽ được chọn.


                // Bây giờ, chọn hàng đầu tiên.
                dataGridView1.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(0, 120, 215); // Màu khi hàng được chọn


                dataGridView2.CurrentCell = null;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /*    private void LoadMembersToDataGridView2(List<ObjListPer> members)
            {
                dataGridView2.Rows.Clear();
                if (members!= null&&members.Count>0) {
                    int i = 1;
                    for (int i2 = 0; i2 < members.Count(); i2++)
                    {

                        if (members[i2].sdt != null && !members[i2].sdt.Equals(""))
                        {
                            dataGridView2.Rows.Add(i, members[i2].sdt, members[i2].name.Trim().Replace("  ", "").Replace("\r", "").Replace("\n", "").Replace("\t", ""), members[i2].chucDanh);
                            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        else
                        {
                            dataGridView2.Rows.Add(i, members[i2].vID, members[i2].name.Trim().Replace("  ", "").Replace("\r", "").Replace("\n", "").Replace("\t", ""), members[i2].chucDanh);
                            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        i++;
                    }
                }
            }*/

        public void layDanhSachNguoiMat()
        {
            try
            {
                ObjectGoiDichVuMini o = new ObjectGoiDichVuMini();
                o.funcId = 10602;
                o.device = 2;
                o.currentime = FunCGeneral.timeNow();

                ObjectGetInfoVID oLDSGR = new ObjectGetInfoVID();
                oLDSGR.mcID = FunCGeneral.mcID;
                oLDSGR.offset = 0;
                oLDSGR.limit = 100;
                oLDSGR.typeFace = 1;
                // Truy cập TextBox từ form
                oLDSGR.textSearch = textBox.Text;
                o.data = JsonConvert.SerializeObject(oLDSGR);

                String url = FunCGeneral.ipMayChuDonVi;
                //String url = "http://113.190.248.142:58080/autobank/services/vimassTool/dpDanh";
                var json = JsonConvert.SerializeObject(o);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);

                if (response != null && response.msgCode == 1)
                {

                    ResultResponeMini value = JsonConvert.DeserializeObject<ResultResponeMini>(response.result.ToString());
                    String valueTraVe = FunctionGeneral.DecodeBase64String(value.value);
                    var arrL = JsonConvert.DeserializeObject<List<ObjectInfoVid>>(valueTraVe);
                    int i = 0;
                    foreach (ObjectInfoVid arr in arrL)
                    {
                        i = i + 1;
                        if (arr.faceData != null && !arr.faceData.Equals(""))
                        {
                            dataGridView2.Rows.Add(+i, arr.hoTen, arr.idVid, arr.dienThoai, "x");
                            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        else
                        {
                            dataGridView2.Rows.Add(+i, arr.hoTen, arr.idVid, arr.dienThoai, "");
                            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }


                    }
                }
            }
            catch
            {

            }
        }
        public async void HienThiCoPhanTrang(int offSet, List<ObjListPer> itemHash, String groupName)
        {
            try
            {
                dataGridView2.Rows.Clear();
                int i = 1;
                for (int i2 = 0; i2 < itemHash.Count(); i2++)
                {

                    if (true)
                    {
                        dataGridView2.Rows.Add(itemHash[i2].id, i, itemHash[i2].vID, itemHash[i2].sdt, itemHash[i2].name.Trim().Replace("  ", "").Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("\u0000", ""),"", itemHash[i2].chucDanh, groupName);
                        dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                  /*  else
                    {
                        dataGridView2.Rows.Add(itemHash[i2].id, i, itemHash[i2].vID, itemHash[i2].name.Trim().Replace("  ", "").Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("\u0000", ""),"", itemHash[i2].chucDanh, groupName);
                        dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }*/
                    i++;
                }
            }
            catch (Exception ex)
            {
                Logger.LogServices("HienThiCoPhanTrang Exception: " + ex.Message);

            }

        }
        public async void HienThiCoPhanTrang2(int offSet, List<ObjListPer> itemHash, String groupName)
        {
            try
            {
                dataGridView2.Rows.Clear();
                int i = 1;
                for (int i2 = 0; i2 < itemHash.Count(); i2++)
                {

                    if (itemHash[i2].sdt != null && !itemHash[i2].sdt.Equals(""))
                    {
                        dataGridView2.Rows.Add(i, itemHash[i2].sdt, itemHash[i2].name.Trim().Replace("  ", "").Replace("\r", "").Replace("\n", "").Replace("\t", ""), itemHash[i2].chucDanh, groupName, "");
                        dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    else
                    {
                        if (await checkMatThread((itemHash[i2].vID)))
                        {
                            dataGridView2.Rows.Add(i, itemHash[i2].vID, itemHash[i2].name.Trim().Replace("  ", "").Replace("\r", "").Replace("\n", "").Replace("\t", ""), itemHash[i2].chucDanh, groupName, "v");
                            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        else
                        {
                            dataGridView2.Rows.Add(i, itemHash[i2].vID, itemHash[i2].name.Trim().Replace("  ", "").Replace("\r", "").Replace("\n", "").Replace("\t", ""), itemHash[i2].chucDanh, groupName, "");
                            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }

                    }
                    i++;
                }
            }
            catch (Exception ex)
            {
                Logger.LogServices("HienThiCoPhanTrang Exception: " + ex.Message);

            }

        }

        FlowLayoutPanel panelContainer1;
        FlowLayoutPanel panelContainer2;
        public void themPhanTrangVaoPanel(int SoTrang)
        {
            try
            {

                int soTrang = FunCGeneral.TinhSoTrang(SoTrang);

                // Tạo một panel hoặc label trống để đẩy các nút sang phải
                Control spacer = new Panel();
                spacer.AutoSize = true;
                panelContainer1.Controls.Add(spacer);
                if (SoTrang < 10)
                {
                    for (int i = 1; i <= soTrang; i++)
                    {
                        Button btnTrang = new Button();
                        btnTrang.Size = new Size(30, 30);
                        btnTrang.Text = i.ToString();
                        btnTrang.Name = i.ToString();
                        btnTrang.Click += new EventHandler(btnTrang_Click);
                        panelContainer1.Controls.Add(btnTrang);

                    }
                }
                else
                {
                    for (int i = 1; i <= soTrang; i++)
                    {
                        //Thêm 3 trang đầu
                        if (i > 0 && i < 5)
                        {
                            Button btnTrang = new Button();
                            btnTrang.Size = new Size(30, 30);
                            btnTrang.Text = i.ToString();
                            btnTrang.Name = i.ToString();
                            btnTrang.Click += new EventHandler(btnTrang_Click);
                            panelContainer1.Controls.Add(btnTrang);
                        }
                        if (i > 4 && i < 6)
                        {
                            TextBox txtTrang = new TextBox();
                            txtTrang.Multiline = true;
                            txtTrang.Height = 30;

                            panelContainer1.Controls.Add(txtTrang);
                            txtTrang.TextChanged += new EventHandler(txtTrang_TextChanged);
                        }


                        if (i > soTrang - 4 && i <= soTrang)
                        {
                            Button btnTrang = new Button();
                            btnTrang.Size = new Size(30, 30);
                            btnTrang.Text = i.ToString();
                            btnTrang.Name = i.ToString();
                            btnTrang.Click += new EventHandler(btnTrang_Click);
                            panelContainer1.Controls.Add(btnTrang);
                        }

                    }
                }


                // Điều chỉnh chiều rộng của spacer dựa trên số lượng nút
                spacer.Width = panelContainer1.Width - (soTrang * 50); // 50 là chiều rộng ước lượng của mỗi nút
            }
            catch (Exception e)
            {
                // Xử lý lỗi ở đây
            }
        }

        private void btnTrang_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("dđ");
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                // Lấy giá trị từ nút được click
                string buttonValue = clickedButton.Text;

                HienThiCoPhanTrang(int.Parse(buttonValue) * 100, arr, "");
                // Tìm TextBox trong FlowLayoutPanel

            }
        }
        private void txtTrang_TextChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("dđ");
            TextBox txt = sender as TextBox;
            if (txt != null)
            {
                // Lấy giá trị từ nút được click
                string sd = txt.Text;
                if (sd != null)
                {
                    String sdf = sd.Trim();
                    if (!sdf.Equals(""))
                    {
                        HienThiCoPhanTrang(int.Parse(sd) * 100, arr, "");
                        // Tìm TextBox trong FlowLayoutPanel
                    }
                }



            }

        }


        public void HienThiNhom(String tenNhom)
        {
            try
            {
                panelContainer1 = this.flowLayoutPanel1;
                panelContainer1.FlowDirection = FlowDirection.RightToLeft; // Cài đặt hướng
                panelContainer1.Controls.Clear();
                Button btnThem = new Button();
                btnThem.Size = new Size(30, 30);
                btnThem.ImageAlign = ContentAlignment.MiddleCenter;
                btnThem.Image = Properties.Resources.them;
                panelContainer1.Controls.Add(btnThem);
                //thêm sự kiện cho nút thêm
                btnThem.Click += new EventHandler(ButtonClick1);

                Button btnCapNhat = new Button();
                btnCapNhat.Size = new Size(30, 30);
                btnCapNhat.ImageAlign = ContentAlignment.MiddleCenter;
                btnCapNhat.Image = Properties.Resources.capnhat;
                panelContainer1.Controls.Add(btnCapNhat);
                btnCapNhat.Click += new EventHandler(ButtonClick2);
                dataGridView2.Rows.Clear();
                int i = 1; // Khởi tạo biến đếm bên ngoài vòng lặp

                foreach (KeyValuePair<String, ObjectGoup> itemHash in FunCGeneral.hashNhom)
                {
                    if (itemHash.Key.Equals(tenNhom))
                    {
                        if (itemHash.Value.listPer != null && itemHash.Value.listPer.Count() > 0)
                        {
                            khoiTaoDataGridView2Cap1();
                            thayDoiKichThuoc2();
                            if (true)
                            {
                                arr = itemHash.Value.listPer;
                                //themPhanTrangVaoPanel(itemHash.Value.listPer.Count());
                                HienThiCoPhanTrang(0, itemHash.Value.listPer, itemHash.Key);
                                //dataGridView1.Sort(dataGridView1.Columns["Column2"], ListSortDirection.Ascending);
                            }
                            goiLayKhuonMat(itemHash.Value.listPer);
                            /* else
                             {
                                 for (int i2 = 0; i2 < 100; i2++)
                                 {

                                     if (itemHash.Value.listPer[i2].sdt != null && !itemHash.Value.listPer[i2].sdt.Equals(""))
                                     {
                                         dataGridView2.Rows.Add(i2 + 1, itemHash.Value.listPer[i2].sdt, itemHash.Value.listPer[i2].name.Trim().Replace("  ", "").Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("\u0000", ""), itemHash.Value.listPer[i2].chucDanh, itemHash.Key);
                                         dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                     }
                                     else
                                     {
                                         dataGridView2.Rows.Add(i2 + 1, itemHash.Value.listPer[i2].vID, itemHash.Value.listPer[i2].name.Trim().Replace("  ", "").Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("\u0000", ""), itemHash.Value.listPer[i2].chucDanh, itemHash.Key);
                                         dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                     }
                                 }
                             }*/
                        }
                        else if (itemHash.Value.listGr != null && itemHash.Value.listGr.Count() > 0)
                        {
                            khoiTaoDataGridView2Cap2();
                            thayDoiKichThuoc2Cap2();
                            int i3 = 1;
                            foreach (ObjectGoup objectGoup in itemHash.Value.listGr)
                            {
                                foreach (KeyValuePair<String, ObjectGoup> itemHash2 in FunCGeneral.hashNhom)
                                {
                                    if (itemHash2.Key.Equals(objectGoup.groupName))
                                    {
                                        dataGridView2.Rows.Add(objectGoup.id, i3, objectGoup.groupName, itemHash2.Value.listPer.Count());
                                        dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                        i3++;
                                    }
                                }
                            }

                        }
                    }
                }

            }
            catch (Exception e)
            {
                Logger.LogServices("HienThiNhom Exception: " + e.Message);
            }
        }
        // Khai báo ở mức độ lớp
        private CancellationTokenSource cancellationTokenSource;
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
                            result = checkMat10603(listPer[index]); // Thay đổi ở đây
                        }

                        if (!IsDisposed && IsHandleCreated)
                        {
                            Invoke(new Action(() =>
                            {
                                if (dataGridView2.Rows.Count > index)
                                {
                                    dataGridView2.Rows[index].Cells["Column7"].Value = result != 0 ? result.ToString() : ""; // Cập nhật ở đây
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
        private void goiLayKhuonMat2(List<ObjListPer> listPer)
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
                            result = checkMat10602(listPer[index].vID);
                        }

                        if (!IsDisposed && IsHandleCreated)
                        {
                            Invoke(new Action(() =>
                            {
                                if (dataGridView2.Rows.Count > index)
                                {
                                    dataGridView2.Rows[index].Cells["Column7"].Value = result ? "v" : "";
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
        // Gọi phương thức này khi bạn chuyển sang tab khác
        public void CancelTasks()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
            }
        }



        private void ButtonClick1(object sender, EventArgs e)
        {
            new ThemNhomMoi().Show();
        }

        public void khoiTaoPanel()
        {
            panelContainer1 = this.flowLayoutPanel1;
            panelContainer1.FlowDirection = FlowDirection.RightToLeft; // Cài đặt hướng
            panelContainer1.Controls.Clear();

            panelContainer2 = this.flowLayoutPanel2;
            panelContainer2.FlowDirection = FlowDirection.RightToLeft; // Cài đặt hướng
            panelContainer2.Controls.Clear();
        }



        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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




        private void dataGridView2_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            /*            if (e.ColumnIndex == dataGridView2.Columns["Column6"].Index && e.RowIndex >= 0)
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
                        }*/
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Đặt lại màu nền cho tất cả các hàng
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White; // Màu mặc định
            }
            // Xác định và tô màu hàng được nhấn
            int rowIndex = e.RowIndex;

            dataGridView2.Rows.Clear();
            panelContainer1 = this.flowLayoutPanel1;
            panelContainer1.Controls.Clear();
            // Check if the click is on a valid row (not on the header)
            if (e.RowIndex >= 0)
            {
                if (rowIndex >= 0)
                {
                    dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(0, 120, 215); // Màu khi hàng được chọn

                }
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string STT = "";
                string tenNhom = "";

                // Retrieve values from the row
                if (row.Cells["Column1"].Value != null && row.Cells["Column2"].Value != null)
                {
                    STT = row.Cells["Column1"].Value.ToString();
                    tenNhom = row.Cells["Column2"].Value.ToString();
                }

                // You can replace "ColumnName1", "ColumnName2", etc. with your actual column names or indices
                HienThiNhom(tenNhom);
                dataGridView2.CurrentCell = null;
            }
        }
        public static List<ObjectGoup> layNhomTuSerVer()
        {
            try
            {
                ObjectDanhSachGroupTuSerVer obj = new ObjectDanhSachGroupTuSerVer();
                obj.user = "0966074236";
                obj.mcID = FunCGeneral.mcID;
                obj.currentTime = FunCGeneral.timeNow();
                obj.deviceID = 3;
                obj.cks = FunctionGeneral.Md5(obj.user + obj.deviceID + "ZgVCHxqMd$aNCm54X2YHD" + obj.currentTime + obj.mcID).ToLower();
                String url = "http://210.245.8.7:12318/vimass/services/VUHF/dsNhomRV";
                var json = JsonConvert.SerializeObject(obj);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);
                if (response != null && response.msgCode == 1)
                {
                    listGroup = JsonConvert.DeserializeObject<List<ObjectGoup>>(response.result.ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.LogServices("layNhomTuSerVer Exception: " + ex.Message);
            }
            return listGroup;

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
                        string key = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();

                        // Xóa từ hashListPers
                        if (FunCGeneral.hashNhom.TryGetValue(key, out var value))
                        {
                            value.type = 3;
                            ObjectGroupRQServer obj = new ObjectGroupRQServer();
                            obj.user = "0966074236";
                            obj.currentTime = FunCGeneral.timeNow();
                            obj.deviceID = 3;
                            obj.mcID = FunCGeneral.mcID;
                            obj.listGr = new List<ObjectGoup>();
                            obj.listGr.Add(value);
                            obj.cks = FunctionGeneral.Md5(obj.user + obj.deviceID + "ZgVCHxqMd$aN11ggg2YHD" + obj.currentTime + obj.mcID).ToLower();
                            String url = "http://210.245.8.7:12318/vimass/services/VUHF/nhomRV";
                            var json = JsonConvert.SerializeObject(obj);
                            String res = Service.SendWebrequest_POST_Method(json, url);
                            Response response = JsonConvert.DeserializeObject<Response>(res);

                            if (response != null && response.msgCode == 1)
                            {
                                MessageBox.Show("Xóa nhóm " + key + " thành công");
                                HienNhomCoMau();


                            }
                        }
                        else
                        {
                            // Không tìm thấy key trong Dictionary
                        }

                        // Xóa dòng từ DataGridView
                        dataGridView1.Rows.RemoveAt(e.RowIndex);

                        // Cập nhật lại STT sau khi xóa

                    }
                }


            }
            catch (Exception ex)
            {
                Logger.LogServices("dataGridView1_CellContentClick Exception: " + ex.Message);
            }
        }

        private int layLaiHamCache(string key)
        {
            int kq = 0;
            try
            {
                hashNhomCache.Clear();
                foreach (KeyValuePair<String, ObjectGoup> item in FunCGeneral.hashNhom)
                {
                    if (item.Key.Equals(key))
                    {
                        hashNhomCache.Add(key, item.Value);
                    }
                }
                if (NguoiDung.hashNhomCache.First().Value.groupLevel == 1)
                {
                    kq = 1;
                }
                else
                {
                    kq = 2;
                }

            }
            catch (Exception ex)
            {
                Logger.LogServices("layLaiHamCache Exception: " + ex.Message);

            }
            return kq;
        }

        private void thayDoiKichThuoc2()
        {
            try
            {
                try
                {
                    int totalWidth = dataGridView2.Width - SystemInformation.VerticalScrollBarWidth;

                    // Thiết lập tỉ lệ cho từng cột
                    dataGridView2.Columns["Column1"].Width = (int)(totalWidth * 0.05); // 50% chiều rộng STT
                    dataGridView2.Columns["Column2"].Width = (int)(totalWidth * 0.15); // 50% chiều rộng sdt
                    dataGridView2.Columns["Column3"].Width = (int)(totalWidth * 0.2); // 50% chiều rộng ht
                    dataGridView2.Columns["Column4"].Width = (int)(totalWidth * 0.2); // 50% chiều rộng cd
                    dataGridView2.Columns["Column5"].Width = (int)(totalWidth * 0.2); // 50% chiều rộng nh
                    dataGridView2.Columns["Column7"].Width = (int)(totalWidth * 0.1); // 50% chiều rộng nh
                    dataGridView2.Columns["Column8"].Width = (int)(totalWidth * 0.15); // 50% chiều rộng nh
                    //dataGridView2.Columns["Column6"].Width = (int)(totalWidth * 0.05); // 50% chiều rộng STT
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
        private void thayDoiKichThuoc1()
        {
            try
            {
                try
                {
                    int totalWidth = dataGridView1.Width - SystemInformation.VerticalScrollBarWidth;

                    // Thiết lập tỉ lệ cho từng cột
                    dataGridView1.Columns["Column1"].Width = (int)(totalWidth * 0.3); // 50% chiều rộng STT
                    dataGridView1.Columns["Column2"].Width = (int)(totalWidth * 0.6); // 30% chiều rộng Tên
                    dataGridView1.Columns["Column3"].Width = (int)(totalWidth * 0.1); // 30% chiều rộng Tên
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

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                /*   if (e.RowIndex >= 0)
                   {
                       DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                       // Thay thế 'ColumnName' với tên thực tế của cột bạn muốn lấy giá trị
                       string value = row.Cells["Column2"].Value.ToString();

                       // Hiển thị giá trị hoặc làm gì đó với nó
                       sdtOrVidCache = value.Replace("V", "").Replace("v", "");

                       new NhomForm.KhuonMat().Show();
                   }*/
            }
            catch (Exception ex)
            {

            }
        }
        private void khoiTaoDataGridView1()
        {
            try
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                //Thêm cột id
                DataGridViewColumn columnID1 = new DataGridViewTextBoxColumn();
                columnID1.HeaderText = "ID";
                columnID1.Name = "ID";
                columnID1.Visible = false; // Làm cho cột không hiển thị
                dataGridView1.Columns.Add(columnID1);
                dataGridView1.Columns.Add("Column1", "STT");
                dataGridView1.Columns.Add("Column2", "Tên nhóm");
                DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
                {
                    imgCol.Name = "Column3";
                    imgCol.HeaderText = "";
                    imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom; // Thiết lập layout để icon được zoom và căn giữa
                }
                dataGridView1.Columns.Add(imgCol);


                //Thiết lập font màu backcolor
                dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 13); // Thay đổi kiểu chữ và cỡ chữ
                dataGridView1.RowTemplate.Height = 30; // Thay đổi chiều cao của hàng thành 30 pixel
                dataGridView1.BackgroundColor = Color.FromArgb(166, 166, 166);
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(242, 249, 255); // Chọn màu bạn muốn
                dataGridView1.EnableHeadersVisualStyles = false; // Cần thiết để màu tùy chỉnh có hiệu lực
                dataGridView1.ReadOnly = true;
                // Căn chỉnh icon ở giữa
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
        private void khoiTaoDataGridView2Cap1()
        {
            try
            {
                dataGridView2.Rows.Clear();
                dataGridView2.Columns.Clear();
                //tạo cột ẩn
                DataGridViewColumn columnID2 = new DataGridViewTextBoxColumn();
                columnID2.HeaderText = "ID";
                columnID2.Name = "ID";
                columnID2.Visible = false; // Làm cho cột không hiển thị
                dataGridView2.Columns.Add(columnID2);

                dataGridView2.Columns.Add("Column1", "STT");
                dataGridView2.Columns.Add("Column2", "Số thẻ");
                dataGridView2.Columns.Add("Column8", "Số điện thoại");
                dataGridView2.Columns.Add("Column3", "Họ tên");
                dataGridView2.Columns.Add("Column7", "Khuôn mặt");
                dataGridView2.Columns.Add("Column4", "Chức danh");
                dataGridView2.Columns.Add("Column5", "Nhóm");
              
                /*                DataGridViewImageColumn imgColData2 = new DataGridViewImageColumn();
                                {
                                    imgColData2.Name = "Column6";
                                    imgColData2.HeaderText = "";
                                    imgColData2.ImageLayout = DataGridViewImageCellLayout.Zoom; // Thiết lập layout để icon được zoom và căn giữa
                                }
                                dataGridView2.Columns.Add(imgColData2);*/
                dataGridView2.DefaultCellStyle.Font = new Font("Segoe UI", 13); // Thay đổi kiểu chữ và cỡ chữ
                dataGridView2.RowTemplate.Height = 30; // Thay đổi chiều cao của hàng thành 30 pixel
                dataGridView2.BackgroundColor = Color.White;
                //Thiet lap màu của tên cột
                dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(242, 249, 255); // Chọn màu bạn muốn
                dataGridView2.EnableHeadersVisualStyles = false; // Cần thiết để màu tùy chỉnh có hiệu lực
                dataGridView2.ReadOnly = true;
                dataGridView2.CurrentCell = null;

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
        private void khoiTaoDataGridView2Cap2()
        {
            try
            {
                dataGridView2.Rows.Clear();
                dataGridView2.Columns.Clear();

                //Thêm cột ID
                DataGridViewColumn columnID2 = new DataGridViewTextBoxColumn();
                columnID2.HeaderText = "ID";
                columnID2.Name = "ID";
                columnID2.Visible = false; // Làm cho cột không hiển thị
                dataGridView2.Columns.Add(columnID2);

                dataGridView2.Columns.Add("Column1", "STT");
                dataGridView2.Columns.Add("Column2", "Nhóm cấp 1");
                dataGridView2.Columns.Add("Column3", "Số lượng");
                /*                DataGridViewImageColumn imgColData2 = new DataGridViewImageColumn();
                                {
                                    imgColData2.Name = "Column6";
                                    imgColData2.HeaderText = "";
                                    imgColData2.ImageLayout = DataGridViewImageCellLayout.Zoom; // Thiết lập layout để icon được zoom và căn giữa
                                }
                                dataGridView2.Columns.Add(imgColData2);*/
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
        private void thayDoiKichThuoc2Cap2()
        {
            try
            {
                int totalWidth = dataGridView2.Width - SystemInformation.VerticalScrollBarWidth;

                // Thiết lập tỉ lệ cho từng cột
                dataGridView2.Columns["Column1"].Width = (int)(totalWidth * 0.05); // 50% chiều rộng STT
                dataGridView2.Columns["Column2"].Width = (int)(totalWidth * 0.65); // 30% chiều rộng Tên
                dataGridView2.Columns["Column3"].Width = (int)(totalWidth * 0.25); // 30% chiều rộng Tên
                //dataGridView2.Columns["Column6"].Width = (int)(totalWidth * 0.05); // 30% chiều rộng Tên
            }
            catch (Exception ex)
            {
                Logger.LogServices("thayDoiKichThuoc Exception: " + ex.Message);
            }


        }
        public static int checkMat10603(ObjListPer obj)
        {

            int kq = 0;
            try
            {
                ObjectGoiDichVuMini o = new ObjectGoiDichVuMini();
                o.funcId = 10603;
                o.device = 2;
                o.currentime = FunCGeneral.timeNow();

                checkMatRequest oLDSGR = new checkMatRequest();


                // Truy cập TextBox từ form
                oLDSGR.vID = obj.vID.Replace("v", "").Replace("V", "");
                oLDSGR.textSearch = RemoveDiacritics( obj.name);
                o.data = JsonConvert.SerializeObject(oLDSGR);

                String url = FunCGeneral.ipMayChuDonVi;
                // String url = "http://193.169.1.11:58080/autobank/services/vimassTool/dieuPhoi";
                var json = JsonConvert.SerializeObject(o);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);

                if (response != null && response.msgCode == 1)
                {

                    ResultResponeMini value = JsonConvert.DeserializeObject<ResultResponeMini>(response.result.ToString());
                    String valueTraVe = FunctionGeneral.DecodeBase64String(value.value);
                    ObjectInfoVid arrL = JsonConvert.DeserializeObject<ObjectInfoVid>(valueTraVe);
                    if (arrL != null && arrL.faceData != null && !arrL.faceData.Equals("") && arrL.faceData.Length>5)
                    {
                        kq = arrL.personPosition;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogServices("checkMat Exception: " + ex.Message);

            }
            return kq;
        }
        public static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
        public static bool checkMat10602(String vID)
        {

            bool kq = false;
            try
            {
                ObjectGoiDichVuMini o = new ObjectGoiDichVuMini();
                o.funcId = 10602;
                o.device = 2;
                o.currentime = FunCGeneral.timeNow();

                ObjectGetInfoVID oLDSGR = new ObjectGetInfoVID();
                oLDSGR.mcID = FunCGeneral.mcID;
                oLDSGR.offset = 0;
                oLDSGR.limit = 100;
                oLDSGR.typeFace = 1;
                // Truy cập TextBox từ form
                oLDSGR.textSearch = vID.Replace("v", "").Replace("V", "");
                o.data = JsonConvert.SerializeObject(oLDSGR);

                String url = FunCGeneral.ipMayChuDonVi;
                //String url = "http://113.190.248.142:58080/autobank/services/vimassTool/dpDanh";
                var json = JsonConvert.SerializeObject(o);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);

                if (response != null && response.msgCode == 1)
                {

                    ResultResponeMini value = JsonConvert.DeserializeObject<ResultResponeMini>(response.result.ToString());
                    String valueTraVe = FunctionGeneral.DecodeBase64String(value.value);
                    var arrL = JsonConvert.DeserializeObject<List<ObjectInfoVid>>(valueTraVe);
                    if (arrL != null && arrL.Count > 0 && arrL[0].faceData != null && !arrL[0].faceData.Equals(""))
                    {
                        kq = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogServices("checkMat Exception: " + ex.Message);

            }
            return kq;
        }
        public static async Task<bool> checkMatThread(String vID)
        {
            bool kq = false;
            try
            {
                ObjectGoiDichVuMini o = new ObjectGoiDichVuMini();
                o.funcId = 10602;
                o.device = 2;
                o.currentime = FunCGeneral.timeNow();

                ObjectGetInfoVID oLDSGR = new ObjectGetInfoVID();
                oLDSGR.mcID = FunCGeneral.mcID;
                oLDSGR.offset = 0;
                oLDSGR.limit = 100;
                oLDSGR.typeFace = 1;
                // Truy cập TextBox từ form
                oLDSGR.textSearch = vID.Replace("v", "").Replace("V", "");
                o.data = JsonConvert.SerializeObject(oLDSGR);

                String url = FunCGeneral.ipMayChuDonVi;
                //String url = "http://113.190.248.142:58080/autobank/services/vimassTool/dpDanh";
                var json = JsonConvert.SerializeObject(o);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);

                if (response != null && response.msgCode == 1)
                {

                    ResultResponeMini value = JsonConvert.DeserializeObject<ResultResponeMini>(response.result.ToString());
                    String valueTraVe = FunctionGeneral.DecodeBase64String(value.value);
                    var arrL = JsonConvert.DeserializeObject<List<ObjectInfoVid>>(valueTraVe);
                    if (arrL != null && arrL.Count > 0 && arrL[0].faceData != null && !arrL[0].faceData.Equals(""))
                    {
                        kq = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }

            return kq;
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow.Index != -1)
                {
                    Debug.WriteLine("Sửa nhóm");
                    // Lấy hàng hiện tại
                    DataGridViewRow row = dataGridView1.CurrentRow;

                    // Lấy giá trị từ cột cụ thể, ví dụ cột đầu tiên
                    string value = row.Cells[2].Value.ToString();

                    // Thực hiện hành động với giá trị 'key'
                    if (layLaiHamCache(value) == 1)
                    {

                        new Sua().Show();

                    }
                    else if (layLaiHamCache(value) == 2)
                    {

                        new SuaNhomCap2().Show();

                    }

                }
            }
            catch (Exception ex)
            {
                Logger.LogServices("dataGridView1_DoubleClick Exception: " + ex.Message);
            }
        }
        private bool IsFormOpen(System.Type formType)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.GetType() == formType)
                {
                    openForm.Activate();
                    return true;
                }
            }
            return false;
        }




        /*  private void dataGridView1_SelectionChanged(object sender, EventArgs e)
          {
              if (dataGridView1.CurrentRow != null)
              {
                  var selectedGroup = listGroup[dataGridView1.CurrentRow.Index];
                  LoadMembersToDataGridView2(selectedGroup.listPer);
              }
          }*/
    }
}
