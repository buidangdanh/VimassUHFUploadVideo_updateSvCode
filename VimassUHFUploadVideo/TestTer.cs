using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using VimassUHFUploadVideo.Ultil;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VimassUHFUploadVideo
{
    public partial class TestTer : Form
    {
        public TestTer()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            detectTID();
        }
        private void detectTID()
        {
            try
            {
                DateTime utcDateTime = DateTime.UtcNow;
                string vnTimeZoneKey = "SE Asia Standard Time";
                TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
                DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
                String strPort = comboBox2.Text;
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
                        FunctionGeneral.writeFile(@"D:\VimassTIDLog.txt", timenow2 + "_TID_1_" + vUidFull);
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
                        FunctionGeneral.writeFile(@"D:\VimassTIDLog.txt", timenow2 + "_TID_2_" + ListIDTrung);
                        foreach (string vuIDIn in ListIDTrung)
                        {
                            FunctionGeneral.writeFile(@"D:\VimassTIDLog.txt", timenow2 + "_TID_3_" + vuIDIn);
                            if (vuIDIn.Length > 5)
                            {
                                string vuIDFinal = vuIDIn.Substring(1, vuIDIn.Length - 2);
                                textBox2.Text = vuIDFinal;

                                var actionReRead = new Action(() =>
                                {
                                    detectTID();

                                });
                                FunctionGeneral.SetTimeout(actionReRead, 1000);
                                label5.Text = "Đã nhận TID: " + vuIDIn + "_" + ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
                            }
                        }

                    });
                    FunctionGeneral.SetTimeout(action, 2000);
                    //   Debug.WriteLine(vuid);
                }
            }
            catch (Exception e3)
            {
                

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FunctionGeneral.TID = textBox2.Text;
            label7.Text = textBox2.Text;
            label7.ForeColor= Color.Red;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                detectEPC();
            }
            catch (Exception e4)
            {
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
                String strPort = comboBox3.Text;
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
                                textBox3.Text = vuIDFinal;

                                var actionReRead = new Action(() =>
                                {
                                    detectEPC();

                                });
                                FunctionGeneral.SetTimeout(actionReRead, 1000);
                                label1.Text = "Đã nhận EPC: " + vuIDIn + "_" + ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
                            }
                        }

                    });
                    FunctionGeneral.SetTimeout(action, 2000);
                    //   Debug.WriteLine(vuid);
                }
            }
            catch (Exception e3)
            {
               

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FunctionGeneral.EPCNew = textBox3.Text; 
            label8.Text =textBox3.Text;
            label8.ForeColor= Color.Green;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime utcDateTime = DateTime.UtcNow;
                string vnTimeZoneKey = "SE Asia Standard Time";
                TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
                DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
                long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();
                string timenow2 = ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");

                CheckMapsBs u = new CheckMapsBs();
                u.sdt = "0966074236";
                u.TID = FunctionGeneral.TID;
                u.EPC = FunctionGeneral.EPCNew;
                u.curentTime = yourDateTimeMilliseconds;
                u.cks = FunctionGeneral.Md5(u.TID + "99v+6Myt&Tta8mBE" + u.curentTime + u.EPC).ToLower();
                string url = "http://210.245.8.7:12318/vimass/services/VUHF/kiemTraTIDvsEPC";
                var json = JsonConvert.SerializeObject(u);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);
                if (response.msgCode == 1)
                {
                    MessageBox.Show("Đốt đúng rồi");
                }
                else
                {
                    MessageBox.Show("Đốt sai rồi");
                }

            }
            catch (Exception exx)
            {

            }
        }
        public void checkDotToiUu()
        {
            try
            {
                string vnTimeZoneKey = "SE Asia Standard Time";
                TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
                DateTimeOffset localDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vnTimeZone);

                long currentTimeMillis = localDateTime.ToUnixTimeMilliseconds();

                CheckMapsBs user = new CheckMapsBs
                {
                    sdt = "0966074236",
                    TID = FunctionGeneral.TID,
                    EPC = FunctionGeneral.EPCNew,
                    curentTime = currentTimeMillis,
                    cks = FunctionGeneral.Md5(FunctionGeneral.TID + "99v+6Myt&Tta8mBE" + currentTimeMillis + FunctionGeneral.EPCNew).ToLower()
                };

                string url = "http://210.245.8.7:12318/vimass/services/VUHF/kiemTraTIDvsEPC";
                string json = JsonConvert.SerializeObject(user);
                string responseString = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(responseString);

                MessageBox.Show(response.msgCode == 1 ? "Đốt đúng rồi" : "Đốt sai rồi");
            }
            catch (Exception ex)
            {
                // Log or handle the exception
            }

        }

        private void TestTer_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
             
                comboBox2.Items.Add(port);
                comboBox3.Items.Add(port);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label3.Text = textBox2.Text;
            FunctionGeneral.TID1 = label3.Text;
            label3.ForeColor= Color.Red;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            label4.Text = textBox2.Text;
            FunctionGeneral.TID2 = label4.Text;
            label4.ForeColor = Color.Green;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime utcDateTime = DateTime.UtcNow;
                string vnTimeZoneKey = "SE Asia Standard Time";
                TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
                DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
                long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();
                string timenow2 = ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
                var epcNew = JsonConvert.DeserializeObject<List<EpcNewObject>>(FunctionGeneral.dataLuuJson);
                for (int i3 = 0; i3 < epcNew.Count; i3++)
                {
                    if (epcNew[i3].TID.Equals( FunctionGeneral.TID1))
                    {
                        FunctionGeneral.check1 = epcNew[i3].vID;
                        FunctionGeneral.check3 = epcNew[i3].idBienSo;

                    }
                    else if(epcNew[i3].TID.Equals(FunctionGeneral.TID2))
                    {
                        FunctionGeneral.check2 = epcNew[i3].vID;
                        FunctionGeneral.check4 = epcNew[i3].idBienSo;

                    }
                }
                if(FunctionGeneral.check1=="" && FunctionGeneral.check2 == "")
                {
                    MessageBox.Show("Bắt cặp sai");
                }
                else
                {
                    if (FunctionGeneral.check1.Equals(FunctionGeneral.check2)|| FunctionGeneral.check3.Equals(FunctionGeneral.check4))
                    {
                        MessageBox.Show("Bắt cặp đúng");
                        FunctionGeneral.writeFile(FunctionGeneral.pathJson + @"CapTemTheGhepThanhCong.txt", timenow2 + "__EPCOK__" + FunctionGeneral.TID1 + "__" + FunctionGeneral.TID2);
                        FunctionGeneral.check1 = "";
                        FunctionGeneral.check2 = "";
                        FunctionGeneral.check3 = "";
                        FunctionGeneral.check4 = "";
                    }
                    else
                    {
                        MessageBox.Show("Bắt cặp sai");
                        FunctionGeneral.check1 = "";
                        FunctionGeneral.check2 = "";
                        FunctionGeneral.check3 = "";
                        FunctionGeneral.check4 = "";
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
             
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                string[] ports = SerialPort.GetPortNames();
                foreach (string port in ports)
                {
                   
                    comboBox2.Items.Add(port);
                    comboBox3.Items.Add(port);
                }
            }
            catch
            {

            }
        }
    }
}
