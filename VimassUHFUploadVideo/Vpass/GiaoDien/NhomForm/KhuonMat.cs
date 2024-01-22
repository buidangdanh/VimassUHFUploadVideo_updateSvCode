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
using VimassUHFUploadVideo.Vpass.GiaoDien;
using VimassUHFUploadVideo.Vpass.Object;
using VimassUHFUploadVideo.Ultil;

namespace VimassUHFUploadVideo.Vpass.GiaoDien.NhomForm
{
    public partial class KhuonMat : Form
    {
        public KhuonMat()
        {
            InitializeComponent();
        }
        FlowLayoutPanel panelContainerCenter;
        private void KhuonMat_Load(object sender, EventArgs e)
        {
            khoiTao1();
            khoiTaoNutCapNhat();
            thayDoiKichThuoc1();
            goiDichVuLayKhuonMat(NguoiDung.sdtOrVidCache);
        }

        private void goiDichVuLayKhuonMat(string sdtOrVidCache)
        {
            try
            {
                if (sdtOrVidCache != null && !sdtOrVidCache.Equals(""))
                {
                    dataGridView1.Rows.Clear();
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
                    oLDSGR.textSearch = sdtOrVidCache;
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
                                    dataGridView1.Rows.Add(+i, arr.dienThoai, arr.hoTen, "v");
                                    dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                }
                                else
                                {
                                    dataGridView1.Rows.Add(+i, arr.idVid, arr.hoTen, "v");
                                    dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                }
                            }
                            else
                            {
                                if (!arr.dienThoai.Equals(""))
                                {
                                    dataGridView1.Rows.Add(+i, arr.dienThoai, arr.hoTen, "");
                                    dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                }
                                else
                                {
                                    dataGridView1.Rows.Add(+i, arr.idVid, arr.hoTen, "");
                                    dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                }
                            }


                        }
                    }
                }
                

            }
            catch(Exception ex)
            {
                Logger.LogServices("goiDichVuLayKhuonMat Exception: " + ex.Message);

            }
        }
        private void khoiTaoNutCapNhat()
        {
            try
            {
                panelContainerCenter = this.flowLayoutPanel1;
                panelContainerCenter.Controls.Clear();
                Button btnCapNhat = new Button();
                btnCapNhat.Size = new Size(30, 30);
                btnCapNhat.Margin = new Padding(left: 0, top: 0, right: 2, bottom: 0);
                btnCapNhat.ImageAlign = ContentAlignment.MiddleCenter;
                btnCapNhat.Image = Properties.Resources.capnhat;
                btnCapNhat.Click += new EventHandler(btnCapNhatClick);
                panelContainerCenter.Controls.Add(btnCapNhat);
            }
            catch (Exception ex)
            {
                Logger.LogServices("khoiTaoNutCapNhat Exception: " + ex.Message);

            }
        }
        private void btnCapNhatClick(object sender, EventArgs e)
        {
            try
            {
                goiDichVuLayKhuonMat(NguoiDung.sdtOrVidCache);

            }
            catch (Exception ex)
            {
                Logger.LogServices("btnCapNhatClick Exception: " + ex.Message);

            }
        }
        private void khoiTao1()
        {
            try
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("Column1", "STT");
                dataGridView1.Columns.Add("Column2", "Số điện thoại/Số thẻ");
                dataGridView1.Columns.Add("Column3", "Họ tên");
                dataGridView1.Columns.Add("Column4", "Dữ liệu khuôn mặt");
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
                Logger.LogServices("khoiTao1 Exception: " + ex.Message);

            }
        }
        private void thayDoiKichThuoc1()
        {
            try
            {
                int totalWidth = dataGridView1.Width - SystemInformation.VerticalScrollBarWidth;

                // Thiết lập tỉ lệ cho từng cột
                dataGridView1.Columns["Column1"].Width = (int)(totalWidth * 0.05); // 50% chiều rộng STT
                dataGridView1.Columns["Column2"].Width = (int)(totalWidth * 0.35); // 30% chiều rộng Tên
                dataGridView1.Columns["Column3"].Width = (int)(totalWidth * 0.4); // 30% chiều rộng Tên
                dataGridView1.Columns["Column4"].Width = (int)(totalWidth * 0.2); // 30% chiều rộng Tên
            }
            catch (Exception ex)
            {
                Logger.LogServices("thayDoiKichThuoc1 Exception: " + ex.Message);
            }
        }
    }
}
