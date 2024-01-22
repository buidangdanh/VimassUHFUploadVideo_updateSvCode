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
using static System.Windows.Forms.LinkLabel;
using VimassUHFUploadVideo.Ultil;
using VimassUHFUploadVideo.Vpass.GiaoDien.NhomForm;
using System.Diagnostics;

namespace VimassUHFUploadVideo.Vpass.GiaoDien
{
    public partial class ThemNhomMoi : Form
    {
        public ThemNhomMoi()
        {
            InitializeComponent();
        }
        public static ObjectGoup objectGoup = new ObjectGoup();
        public static List<ObjListPer> ListPers = new List<ObjListPer>();
        public static List<ObjectGoup> ListGroup = new List<ObjectGoup>();
        public static Dictionary<String, ObjListPer> hashListPers = new Dictionary<String, ObjListPer>();
        public static Dictionary<String, ObjectGoup> hashListGroupCap1 = new Dictionary<String, ObjectGoup>();
        public static Dictionary<String, ObjectGoup> hashListGroup = new Dictionary<String, ObjectGoup>();
        private void panel1_Paint(object sender, PaintEventArgs e)
        {


        }

        private void ThemNhomMoi_Load(object sender, EventArgs e)
        {
            radioButton1.Text = "Nhóm cấp 1";
            radioButton2.Text = "Nhóm cấp 2";
            button1.Text = "Thêm thành viên";
            button2.Text = "Lưu";
            radioButton1.Checked = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

            button1.Text = "Thêm thành viên";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            button1.Text = "Thêm nhóm";
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (textBox7.Text != null && !textBox7.Text.Equals(""))
            {
                objectGoup.groupName = textBox7.Text;
                objectGoup.type = 1;
                objectGoup.userTao = "0966074236";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (radioButton1.Checked)
            {

                if (textBox7.Text != null && !textBox7.Text.Equals(""))
                {
                    objectGoup.groupLevel = 1;

                    new ThemThanhVien().Show();
                }
                else
                {
                    MessageBox.Show("Chưa nhập tên nhóm");
                }

            }
            else
            {
                if (textBox7.Text != null && !textBox7.Text.Equals(""))
                {
                    objectGoup.groupLevel = 2;

                    new ThemNhomCapHai().Show();
                }
                else
                {
                    MessageBox.Show("Chưa nhập tên nhóm");
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ObjectGroupRQServer obj = new ObjectGroupRQServer();
                obj.user = "0966074236";
                obj.currentTime = FunCGeneral.timeNow();
                obj.deviceID = 3;
                obj.mcID = FunCGeneral.mcID;
                obj.listGr = new List<ObjectGoup>();
                objectGoup.listPer = ListPers;
                objectGoup.listGr = ListGroup;
                obj.listGr.Add(objectGoup);
                obj.cks = FunctionGeneral.Md5(obj.user + obj.deviceID + "ZgVCHxqMd$aN11ggg2YHD" + obj.currentTime + obj.mcID).ToLower();
                String url = "http://210.245.8.7:12318/vimass/services/VUHF/nhomRV";
                var json = JsonConvert.SerializeObject(obj);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);

                if (response != null && response.msgCode == 1)
                {
                    MessageBox.Show("Thêm nhóm " + objectGoup.groupName+" thành công");
                    hashListPers.Clear();
                    hashListGroup.Clear();
                    hashListGroupCap1.Clear();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Debug.Write(ex.ToString());
            }
        }
    }
}
