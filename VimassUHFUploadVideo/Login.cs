using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VimassUHFUploadVideo.Ultil;

namespace VimassUHFUploadVideo
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                label4.Text = "Vui lòng chờ!";
                label4.ForeColor = Color.Red;
                string user = textBox1.Text;
                string pass = textBox2.Text;
                string url = "https://vimass.vn/vmbank/services/account/login1";
                User u = new User();
                u.pass = pass;
                u.VimassMH = 0;
                u.appId = 5;
                u.user = user;
                u.type = 1;
                var json = JsonConvert.SerializeObject(u);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);
                if (response != null && response.msgCode == 1)
                {
                    InfoLogin infoLogin = JsonConvert.DeserializeObject<InfoLogin>(response.result.ToString());
                    FunctionGeneral.pathExcel = FunctionGeneral.CreatFolder(@"VMCreatEPC\Excel")+ @"\";
                    FunctionGeneral.pathJson = FunctionGeneral.CreatFolder(@"VMCreatEPC\Json")+ @"\";
                    FunctionGeneral.pathLog = FunctionGeneral.CreatFolder(@"VMCreatEPC\Log") + @"\";
                    FunctionGeneral.soVi = u.user;
                    FunctionGeneral.tenDN = infoLogin.acc_name;
                    this.Hide();
                    new FormConfig().Show();
                    
                }
                else
                {
                    label4.Text = response.msgContent;
                    label4.ForeColor = Color.Red;
                }
            }
            catch(Exception ex) {
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            label4.Text = "";
            try
            {
                String DulieuUHF = "";
                string path = @"D:\VMCreatEPC\Log\key.txt";
                DulieuUHF = FunctionGeneral.docThongTin(path);
                Key datauhf = JsonConvert.DeserializeObject<Key>(DulieuUHF);
                FunctionGeneral.user = datauhf.user;
                FunctionGeneral.keyCreateEPC = datauhf.keyCreateEPC;
                FunctionGeneral.keyconfirmEPC = datauhf.keyconfirmEPC;
                FunctionGeneral.keyCreateEPCUnkown = datauhf.keyCreateEPCUnkown;
                FunctionGeneral.keyconfirmEPCUnkown = datauhf.keyconfirmEPCUnkown;
            }
            catch
            {

            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text == "0966074234")
            {
                FunctionGeneral.pathExcel = FunctionGeneral.CreatFolder(@"VMCreatEPC\Excel") + @"\";
                FunctionGeneral.pathJson = FunctionGeneral.CreatFolder(@"VMCreatEPC\Json") + @"\";
                FunctionGeneral.pathLog = FunctionGeneral.CreatFolder(@"VMCreatEPC\Log") + @"\";
                this.Hide();
                new FormConfig().Show();
            }
            else if(textBox1.Text == "8")
            {
                FunctionGeneral.pathExcel = FunctionGeneral.CreatFolder(@"VMCreatEPC\Excel") + @"\";
                FunctionGeneral.pathJson = FunctionGeneral.CreatFolder(@"VMCreatEPC\Json") + @"\";
                FunctionGeneral.pathLog = FunctionGeneral.CreatFolder(@"VMCreatEPC\Log") + @"\";
                new GetDataStore().Show();
            }
            else if (textBox1.Text == "7")
            {
                FunctionGeneral.pathExcel = FunctionGeneral.CreatFolder(@"VMCreatEPC\Excel") + @"\";
                FunctionGeneral.pathJson = FunctionGeneral.CreatFolder(@"VMCreatEPC\Json") + @"\";
                FunctionGeneral.pathLog = FunctionGeneral.CreatFolder(@"VMCreatEPC\Log") + @"\";
                new ConvertExcelToJson().Show();
            }
            else if (textBox1.Text == "0377249552")
            {
                FunctionGeneral.pathExcel = FunctionGeneral.CreatFolder(@"VMCreatEPC\Excel") + @"\";
                FunctionGeneral.pathJson = FunctionGeneral.CreatFolder(@"VMCreatEPC\Json") + @"\";
                FunctionGeneral.pathLog = FunctionGeneral.CreatFolder(@"VMCreatEPC\Log") + @"\";
                new FormSearchTheoTemThe().Show();
            }
            else if (textBox1.Text == "04082000")
            {
                FunctionGeneral.pathExcel = FunctionGeneral.CreatFolder(@"VMCreatEPC\Excel") + @"\";
                FunctionGeneral.pathJson = FunctionGeneral.CreatFolder(@"VMCreatEPC\Json") + @"\";
                FunctionGeneral.pathLog = FunctionGeneral.CreatFolder(@"VMCreatEPC\Log") + @"\";
                new LayChamCong().Show();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new GetLatLng().Show();
        }
    }
}
