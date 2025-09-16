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
using VimassUHFUploadVideo.Vpass.Object.ObjectVanTay;
using VimassUHFUploadVideo.Ultil;
using com.sun.org.apache.xml.@internal.resolver.helpers;
using System.Diagnostics;
using com.sun.source.doctree;
using Utilities.BunifuCheckBox.Transitions;
using VimassUHFUploadVideo.Vpass.Object.ObjectThietBi;
using System.Security.Policy;


namespace VimassUHFUploadVideo.Vpass.GiaoDien
{
    public partial class ConfigVanTay : Form
    {
        public ConfigVanTay()
        {
            InitializeComponent();
        }
        public static List<ObjFP> listVanTay = new List<ObjFP>();
        public static Dictionary<String, ObjFP> hashVanTay = new Dictionary<String, ObjFP>();

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                List<ObjFPSua> listDiem = new List<ObjFPSua>();

                List<String> listiddiem = new List<String>();
                ObjectGoiDichVuMini o = new ObjectGoiDichVuMini();
                o.funcId = 130;
                o.device = 2;
                o.currentime = FunCGeneral.timeNow();

                ObjectSuaVanTay oData = new ObjectSuaVanTay();
                oData.user = "0966074236";

                oData.perNum = 1;
                oData.cks = "";
                oData.currentTime = o.currentime;
                oData.deviceID = 1;
                oData.mcID = FunCGeneral.mcID;






                ObjFPSua objFPSua = new ObjFPSua();
                objFPSua.id = "";
                objFPSua.mcID = FunCGeneral.mcID;
                objFPSua.idDonVi = "";
                objFPSua.nameF = textBox2.Text;
                objFPSua.totalF = 0;
                objFPSua.currentF = 1000;
                listiddiem.Add(textBox3.Text);
                objFPSua.listIDDiem = listiddiem;
                objFPSua.IdDeviceVManager = textBox7.Text;
                objFPSua.type = 1;
                listDiem.Add(objFPSua);
                oData.listItem = listDiem;


                o.data = JsonConvert.SerializeObject(oData);

                //String url = FunCGeneral.ipMayChuDonVi;
                String url = textBox8.Text;
                var json = JsonConvert.SerializeObject(o);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);
                System.Diagnostics.Debug.WriteLine("V reques" + json.ToString());


            }
            catch (Exception ex)
            {
                Logger.LogServices("goiDichVuLayKhuonMat Exception: " + ex.Message);

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            label5.Hide();
            groupBox1.Hide();
            textBox8.Text = "http://192.168.1.254:58080/autobank/services/vimassTool/dieuPhoi";
            try
            {
                LayVanTayTuMayMini();
                comboBox1.Items.Clear();
                foreach (ObjFP arr in listVanTay)
                {
                    comboBox1.Items.Add(new KeyValuePair<string, string>(arr.id, arr.nameF));
                }
                comboBox1.SelectedIndex = 0;
                // Đặt thuộc tính để hiển thị Value (tên) trong ComboBox
                comboBox1.DisplayMember = "Value";
                comboBox1.ValueMember = "Key";
                comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;



            }
            catch (Exception ex)
            {
                Logger.LogServices("Form1_Load Exception: " + ex.Message);

            }


        }

        private void LayVanTayTuMayMini()
        {
            try
            {
                ObjectGoiDichVuMini o = new ObjectGoiDichVuMini();
                o.funcId = 131;
                o.device = 2;
                o.currentime = FunCGeneral.timeNow();

                ObjGetVanTay objGetVanTay = new ObjGetVanTay();
                objGetVanTay.user = "0966074236";
                objGetVanTay.perNum = 10;
                objGetVanTay.mcID = "";
                objGetVanTay.cks = "";
                objGetVanTay.currentTime = 0;
                objGetVanTay.deviceID = 1;

                o.data = JsonConvert.SerializeObject(objGetVanTay);

                String url = textBox8.Text.Trim();
                var json = JsonConvert.SerializeObject(o);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);
                System.Diagnostics.Debug.WriteLine("V reques" + json.ToString());
                if (response != null && response.msgCode == 1)
                {

                    ResultResponeMini value = JsonConvert.DeserializeObject<ResultResponeMini>(response.result.ToString());
                    String valueTraVe = FunctionGeneral.DecodeBase64String(value.value);
                    listVanTay = JsonConvert.DeserializeObject<List<ObjFP>>(valueTraVe);
                    foreach (ObjFP arr in listVanTay)
                    {
                        hashVanTay.Add(arr.id, arr);
                    }
                  

                }
                else
                {

                    label5.Text = FunctionGeneral.DecodeBase64String(response.msgContent);
                    label5.Show();


                }
            }
            catch (Exception ex)
            {
                Logger.LogServices("LayVanTayTuMayMini Exception: " + ex.Message);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label5.Text = "";
            try
            {
                ObjectFPRequest objectFPRequest = new ObjectFPRequest();
                ObjectGoiDichVuMini o = new ObjectGoiDichVuMini();
                o.funcId = 127;
                o.device = 2;
                o.currentime = FunCGeneral.timeNow();

                ThemSuaXoaVanTay oData = new ThemSuaXoaVanTay();
                oData.type = 1;
                objectFPRequest.idVid = textBox5.Text.Trim();
                objectFPRequest.personName = textBox6.Text.Trim();
                oData.thongTinNguoi = objectFPRequest;
                oData.idFP = LayIDVanTay();
                oData.nameFP = textBox4.Text.Trim();
                oData.emptyID = "";

                o.data = JsonConvert.SerializeObject(oData);

                //String url = FunCGeneral.ipMayChuDonVi;
                String url = textBox8.Text.Trim();
                var json = JsonConvert.SerializeObject(o);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);
                System.Diagnostics.Debug.WriteLine("V reques" + json.ToString());
                if (response != null && response.msgCode == 1)
                {

                    label5.Text = "Thành công";
                    label5.Show();

                }
                else
                {

                    label5.Text = FunctionGeneral.DecodeBase64String(response.msgContent);
                    label5.Show();


                }

            }
            catch (Exception ex)
            {
                Logger.LogServices("goiDichVuLayKhuonMat Exception: " + ex.Message);

            }

        }

        private string LayIDVanTay()
        {
            String idVanTay = "";
            try
            {
                if (comboBox1.SelectedItem != null)
                {
                    var selectedItem = (KeyValuePair<string, string>)comboBox1.SelectedItem;
                    idVanTay = selectedItem.Key;
                    string selectedName = selectedItem.Value;


                }


            }
            catch (Exception ex)
            {
                Logger.LogServices("LayIDVanTay Exception: " + ex.Message);
            }
            return idVanTay;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            String textNhapVaoTextBoxt5 = textBox5.Text.Trim();
            try
            {
                if (textNhapVaoTextBoxt5.Equals("Danh"))
                {
                    groupBox1.Show();
                }

            }
            catch (Exception ex)
            {
                Logger.LogServices("textBox5_TextChanged Exception: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                khoiTaoDataGridView1();



            }
            catch (Exception ex)
            {
                Logger.LogServices("button2_Click Exception: " + ex.Message);
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
                dataGridView1.Columns.Add("Column2", "Tên vân tay");
                DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
                {
                    imgCol.Name = "Column3";
                    imgCol.HeaderText = "";
                    imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom; // Thiết lập layout để icon được zoom và căn giữa
                }
                // dataGridView1.Columns.Add(imgCol);


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
    }
}
