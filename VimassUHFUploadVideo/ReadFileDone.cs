using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using VimassUHFUploadVideo.Ultil;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VimassUHFUploadVideo
{
    public partial class ReadFileDone : Form
    {
        public ReadFileDone()
        {
            InitializeComponent();
        }

        private void ReadFileDone_Load(object sender, EventArgs e)
        {
            textBox4.Hide();
            if (FunctionGeneral.soVi.Equals("0966074236"))
            {
                textBox4.Show();
            }
           
            string[] ports = SerialPort.GetPortNames();
            if (comboBox1.Text == "")
            {
                foreach (string port in ports)
                {
                    comboBox1.Items.Add(port);
                    comboBox2.Items.Add(port);
                }
            }
        
            label1.Text = "";
            label2.Text = "";
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
            label9.Text = "";
            label11.Text = "";
            /* */
            String DulieuUHF = "";
            string path = FunctionGeneral.nameFileRead;
            DulieuUHF = FunctionGeneral.docThongTin(path);
            var epcNew = JsonConvert.DeserializeObject<List<EpcNewObject>>(DulieuUHF);
            for (int i3 = 0; i3 < epcNew.Count; i3++)
            {
                textBox4.Text += epcNew[i3].TID + "      "+ epcNew[i3].Giatri + "       "+ epcNew[i3].EPC + "\r\n";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                detectTID();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void detectTID()
        {
            try
            {
                DateTime utcDateTime = DateTime.UtcNow;
                string vnTimeZoneKey = "SE Asia Standard Time";
                TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
                DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
                String strPort = comboBox1.Text;
                SerialPort port2 = new SerialPort(strPort, 9600, Parity.None, 8, StopBits.One);
                string timenow2 = ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
                port2.Open();
                // string vuid = readingVuid();
                // FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", timenow + "_HHTIN_" + vuid);
                string str4 = "";
                if (true)
                {
                    var action = new Action(() =>
                    {
                        string vUidFull = port2.ReadExisting();
                        FunctionGeneral.writeFile(@"D:\VMCreat.txt", timenow2 + "_TID_1_" + vUidFull);
                        if (vUidFull.Length < 5)
                        {
                            var action4 = new Action(() =>
                            {
                                detectTID();
                            });
                            FunctionGeneral.SetTimeout(action4, 2000);
                        }
                        port2.Close();
                        string[] ListID = vUidFull.Split('\n');
                        foreach (string text in ListID)
                        {
                            if (str4.IndexOf(text) < 0)
                            {
                                str4 += text;
                            }
                        }
                        string[] ListIDTrung = str4.Split('');
                        FunctionGeneral.writeFile(@"D:\VMCreat.txt", timenow2 + "_TID_2_" + ListIDTrung);
                        foreach (string vuIDIn in ListIDTrung)
                        {
                            FunctionGeneral.writeFile(@"D:\VMCreat.txt", timenow2 + "_TID_3_" + vuIDIn);
                            if (vuIDIn.Length > 5)
                            {
                                string vuIDFinal = vuIDIn.Substring(1, vuIDIn.Length - 2);
                                //label2.Text = vuIDFinal.Substring(vuIDFinal.Length - 24);
                                label2.Text = vuIDFinal;
                                label2.ForeColor = Color.Red;
                                textBox3.Text = vuIDFinal;
                                var actionReRead = new Action(() =>
                                {
                                    detectTID();

                                });
                                FunctionGeneral.SetTimeout(actionReRead, 1000);
                                label1.Text = "Đã nhận TID: " + vuIDIn + "_" + ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
                            }
                        }

                    });
                    FunctionGeneral.SetTimeout(action, 2000);
                    //   Debug.WriteLine(vuid);
                }
            }
            catch (Exception e3)
            {
                FunctionGeneral.writeFile(@"D:\VMCreat.txt", "__TID__Die" + e3);

            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
           
            try
            {
                Clipboard.SetText("");
                DateTime utcDateTime = DateTime.UtcNow;
                string vnTimeZoneKey = "SE Asia Standard Time";
                TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
                DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
                String strPort = comboBox1.Text;
                SerialPort port2 = new SerialPort(strPort, 9600, Parity.None, 8, StopBits.One);
                string timenow2 = ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
                FunctionGeneral.readTID = textBox3.Text;
                label6.Text = FunctionGeneral.readTID;
                if (FunctionGeneral.readTID != "" && FunctionGeneral.nameFileRead != "")
                {
                    String DulieuUHF = "";
                    string path = FunctionGeneral.nameFileRead;
                    DulieuUHF = FunctionGeneral.docThongTin(path);
                    var epcNew = JsonConvert.DeserializeObject<List<EpcNewObject>>(DulieuUHF);
                    for (int i3 = 0; i3 < epcNew.Count; i3++)
                    {
                        if (epcNew[i3].TID == FunctionGeneral.readTID)
                        {
                            label6.Text = epcNew[i3].TID;
                            label7.Text = epcNew[i3].Giatri;
                            label11.Text = epcNew[i3].Vid;
                            label7.ForeColor = Color.Green;
                            label11.ForeColor = Color.Red;
                            textBox2.Text = epcNew[i3].EPC;
                            Clipboard.SetText(textBox2.Text);
                            FunctionGeneral.readTID = "";
                            FunctionGeneral.writeFile(FunctionGeneral.pathLog+@"EpcConfirm.txt", timenow2 +"_" + epcNew[i3].TID+"_"+ epcNew[i3].EPC);
                            break;
                        }
                        else
                        {
                            label7.Text = "Không có TID trong File";
                            label7.ForeColor = Color.Red;
                        }
                    }
                }
                else
                {
                    label7.Text = "Chưa chốt TID";
                }
           
            }
            catch (Exception ed)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox2.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                detectEPC();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void detectEPC()
        {
            try
            {
                DateTime utcDateTime = DateTime.UtcNow;
                string vnTimeZoneKey = "SE Asia Standard Time";
                TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
                DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
                string timenow2 = ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
                String strPort = comboBox2.Text;
                SerialPort port2 = new SerialPort(strPort, 9600, Parity.None, 8, StopBits.One);
                port2.Open();
                // string vuid = readingVuid();
                // FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", timenow + "_HHTIN_" + vuid);
                string str4 = "";
                if (true)
                {
                    var action = new Action(() =>
                    {
                        string vUidFull = port2.ReadExisting();
                        FunctionGeneral.writeFile(@"D:\VimassTIDLog.txt", timenow2 + "_EPC_1_" + vUidFull);
                        if (vUidFull.Length < 5)
                        {
                            var action4 = new Action(() =>
                            {
                                detectEPC();
                            });
                           FunctionGeneral.SetTimeout(action4, 2000);
                        }
                        port2.Close();
                        string[] ListID = vUidFull.Split('\n');
                        foreach (string text in ListID)
                        {
                            if (str4.IndexOf(text) < 0)
                            {
                                str4 += text;
                            }
                        }
                        string[] ListIDTrung = str4.Split('');
                        FunctionGeneral.writeFile(@"D:\VimassTIDLog.txt", timenow2 + "_EPC_2_" + ListIDTrung);
                        foreach (string vuIDIn in ListIDTrung)
                        {
                            FunctionGeneral.writeFile(@"D:\VimassTIDLog.txt", timenow2 + "_EPC_3_" + vuIDIn);
                            if (vuIDIn.Length > 5)
                            {
                                string vuIDFinal = vuIDIn.Substring(1, vuIDIn.Length - 2);
                                label9.Text = vuIDFinal;

                                var actionReRead = new Action(() =>
                                {
                                    detectEPC();

                                });
                                FunctionGeneral.SetTimeout(actionReRead, 1000);

                            }
                        }

                    });
                    FunctionGeneral.SetTimeout(action, 2000);
                    //   Debug.WriteLine(vuid);
                }
            }
            catch (Exception e3)
            {
                FunctionGeneral.writeFile(@"D:\VimassTIDLog.txt",  "__EPC__Die" + e3);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = Clipboard.GetText();
        }
    }
}
