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
    public partial class FormConfig : Form
    {
        public FormConfig()
        {
            InitializeComponent();
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {
            label1.Text = "Chọn công cụ cần format: Thẻ = 0 ; Tem dán = 1";
            label2.Text = "Chào " + FunctionGeneral.tenDN;
            textBox2.Hide();
            button5.Hide();
            textBox6.Hide();
            button9.Hide();
            button10.Hide();
            if (FunctionGeneral.soVi.Equals("0966074236"))
            {
                textBox6.Show();
                button9.Show();
                button10.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FunctionGeneral.check = int.Parse(textBox1.Text);
            new AddTIDUID().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new FormCheck().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new CreatEPCNew().Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Show();
            button5.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show(FunctionGeneral.CreatFolder(textBox2.Text));
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new ConvertExcelToJson().Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FunctionGeneral.nameFileRead = FunctionGeneral.pathJson+ textBox3.Text+ @".txt";
            new ReadFileDone().Show();
        }
        public static void ReadFileDone(string nameFile) 
        {
            try
            {
                String DulieuUHF = "";
                string path =nameFile ;
                DulieuUHF =FunctionGeneral.docThongTin(path);
                var epcNew = JsonConvert.DeserializeObject<List<EpcNewObject>>(DulieuUHF);
                for (int i3 = 0; i3 < epcNew.Count; i3++)
                {

                }
            }
            catch (Exception ed)
            {

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FunctionGeneral.nameFileRead = FunctionGeneral.pathJson + textBox3.Text + @".txt";
            FunctionGeneral.dataLuuJson = FunctionGeneral.ConvertCSVtoJson(textBox4.Text);
            new TestTer().Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            File.WriteAllText(FunctionGeneral.pathLog + @"key.txt", textBox6.Text);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            new Search().Show();    
        }
    }
}
