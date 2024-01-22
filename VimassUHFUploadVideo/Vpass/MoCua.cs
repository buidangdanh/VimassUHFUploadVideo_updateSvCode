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
namespace VimassUHFUploadVideo.Vpass
{
    public partial class MoCua : Form
    {
        public MoCua()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                while(true)
                {
                    ObjectGoiDichVuMini o = new ObjectGoiDichVuMini();
                    string url = textBox7.Text.Trim();
                    if (url != null && !url.Equals(""))
                    {
                        o.funcId = 120;
                        var json = JsonConvert.SerializeObject(o);
                        String res = Service.SendWebrequest_POST_Method(json, url);
                        Response response = JsonConvert.DeserializeObject<Response>(res);

                        if (response.msgCode == 1)
                        {
                            FunctionGeneral.SendUdp("192.168.1.151", 8888, "ON");
                        }

                    }
                }
            
              
            }
            catch(Exception) { }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
