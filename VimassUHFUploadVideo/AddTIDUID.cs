using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;
using VimassUHFUploadVideo.Ultil;

namespace VimassUHFUploadVideo
{
    public partial class AddTIDUID : Form
    {
        static DateTime utcDateTime = DateTime.UtcNow;
        static string vnTimeZoneKey = "SE Asia Standard Time";
        static TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
        static DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
        long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();
        string timenow = ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
        public AddTIDUID()
        {
            InitializeComponent();
        }

        private void AddTIDUID_Load(object sender, EventArgs e)
        {
            textBox5.Hide();
            textBox6.Hide();
            button7.Hide();
            button10.Hide();
            if (FunctionGeneral.soVi.Equals("0966074236"))
            {
                textBox5.Show();
                textBox6.Show();
                button10.Show();
            }
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
                comboBox2.Items.Add(port);
                comboBox3.Items.Add(port);
            }
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
            label7.Text = "";
            label8.Text = "";
            label9.Text = "";
            label10.Text = "";
            label11.Text = "";
            label14.Text = "";
            textBox7.Font = new Font(textBox7.Font.FontFamily, 20);

            if (FunctionGeneral.check == 0)
            {
                label12.Text = "Đang format thẻ. Người thực hiện "+FunctionGeneral.tenDN;
                label12.ForeColor = Color.Red;
            }
            else
            {
                label12.Text = "Đang format tem dán. Người thực hiện " + FunctionGeneral.tenDN;
                label12.ForeColor = Color.Red;
            }
           
            /*AutoSend();*/


        }

        private void button1_Click(object sender, EventArgs e)
        {
            detectUID();
        }
        public static void SetTimeout(Action action, int timeout)
        {
            var timer = new Timer();
            timer.Interval = timeout;
            timer.Tick += (s, e) =>
            {
                action();
                timer.Stop();
            };
            timer.Start();
        }
        
        private void detectUID()
        {
            try
            {
                DateTime utcDateTime = DateTime.UtcNow;
                string vnTimeZoneKey = "SE Asia Standard Time";
                TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
                DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
                string timenow2 = ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
                String strPort = comboBox1.Text;
                SerialPort port2 = new SerialPort(strPort, 115200, Parity.None, 8, StopBits.One);
                port2.Open();
                // string vuid = readingVuid();
                // FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", timenow + "_HHTIN_" + vuid);
                string str4 = "";
                if (true)
                {
                    var action = new Action(() =>
                    {
                        string vUidFull = port2.ReadExisting();
                        FunctionGeneral.writeFile(@"D:\VimassTIDLog.txt", timenow2 + "_UID_1_" + vUidFull);
                        if (vUidFull.Length < 5)
                        {
                            var action4 = new Action(() =>
                            {
                                detectUID();
                            });
                            SetTimeout(action4, 2000);
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
                        FunctionGeneral.writeFile(@"D:\VimassTIDLog.txt", timenow2 + "__2_" + ListIDTrung);
                        foreach (string vuIDIn in ListIDTrung)
                        {
                            FunctionGeneral.writeFile(@"D:\VimassTIDLog.txt", timenow2 + "_UID_3_" + vuIDIn);
                            if (vuIDIn.Length > 5)
                            {
                                string vuIDFinal = vuIDIn.Substring(0, vuIDIn.Length - 1);
                                if (vuIDFinal.Length >= 14) {
                                    textBox1.Text = vuIDFinal.Substring(vuIDFinal.Length - 14);
                                }
                                else
                                {
                                    textBox1.Text = vuIDFinal;
                                }
                               
       

                                var actionReRead = new Action(() =>
                                {
                                    detectUID();

                                });
                                SetTimeout(actionReRead, 1000);
                                label4.Text = "Đã nhận UID: " + vuIDIn + "_" + ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
                            }
                        }

                    });
                    SetTimeout(action, 1000);
                    //   Debug.WriteLine(vuid);
                }
            }
            catch (Exception e3)
            {
                FunctionGeneral.writeFile(@"D:\VimassTIDLog.txt", timenow+  "_UID_Die"+ e3);
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
                            SetTimeout(action4, 2000);
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
                                string vuIDFinal = "";
                                if (vuIDIn.Length > 24)
                                {
                                    vuIDFinal = vuIDIn;
                                    if (vuIDFinal.Length >= 24) {
                                        textBox2.Text = vuIDFinal.Substring(vuIDFinal.Length - 24);
                                    }
                                    else
                                    {
                                        textBox2.Text = vuIDFinal;
                                    }
                             


                                var actionReRead = new Action(() =>
                                {
                                    detectTID();

                                });
                                SetTimeout(actionReRead, 1000);
                                label5.Text = "Đã nhận TID: " + vuIDIn + "_" + ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
                            }
                        }

                        }
                    });
                    SetTimeout(action, 2000);
                    //   Debug.WriteLine(vuid);
                }
            }
            catch (Exception e3)
            {
                FunctionGeneral.writeFile(@"D:\VimassTIDLog.txt", timenow + "__TID__Die" + e3);

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
                            SetTimeout(action4, 2000);
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
                                //textBox3.Text = vuIDFinal.Substring(vuIDFinal.Length - 24);
                                if (vuIDFinal.Length >= 24)
                                {
                                    textBox3.Text = vuIDFinal.Substring(vuIDFinal.Length - 24);
                                }
                                else
                                {
                                    textBox3.Text = vuIDFinal;
                                }
                
                                var actionReRead = new Action(() =>
                                {
                                    detectEPC();

                                });
                                SetTimeout(actionReRead, 1000);
                                label7.Text = "Đã nhận EPC: " + vuIDIn + "_" + ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
                            }
                        }

                    });
                    SetTimeout(action, 2000);
                    //   Debug.WriteLine(vuid);
                }
            }
            catch (Exception e3)
            {
                FunctionGeneral.writeFile(@"D:\VimassTIDLog.txt", timenow + "__EPC__Die" + e3);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            detectTID();
        }
        private void AutoSend()
        {
            textBox1.ForeColor = Color.Red; textBox2.ForeColor = Color.Green; textBox3.ForeColor = Color.Blue;
            FunctionGeneral.UID = textBox1.Text;
            FunctionGeneral.TID = textBox2.Text;
            FunctionGeneral.EPC = textBox3.Text;
            if (FunctionGeneral.check == 0)
            {
                label11.Text = "UID : " + FunctionGeneral.UID;
                label11.ForeColor = Color.Red;
            }
            if(FunctionGeneral.TID.Length> 12&&FunctionGeneral.EPC.Length>12&&FunctionGeneral.TID!=FunctionGeneral.checkTC) {
                string xTID = FunctionGeneral.TID.Substring(FunctionGeneral.TID.Length - 12, 12);
                string xEPC = FunctionGeneral.EPC.Substring(FunctionGeneral.EPC.Length - 12, 12);
                if (xTID == xEPC)
                {
                    UidMapTid u = new UidMapTid();
                    DateTime utcDateTime = DateTime.UtcNow;
                    string vnTimeZoneKey = "SE Asia Standard Time";
                    TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
                    DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
                    long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();
                    string timenow2 = ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");

                    u.uID = "";
                    u.TID = FunctionGeneral.TID;
                    u.EPC = FunctionGeneral.EPC;
                    u.timeRequest = yourDateTimeMilliseconds;
                    u.cks = FunctionGeneral.Md5("ifafioeqfo" + u.uID + u.idVid + u.TID + "1" + u.EPC + u.timeRequest).ToLower();
                    string url = "https://vimass.vn/vmbank/services/TDNService/requestComand";
                    var json = JsonConvert.SerializeObject(u);
                    String res = Service.SendWebrequest_POST_Method(json, url);
                    Response response = JsonConvert.DeserializeObject<Response>(res);
                    FunctionGeneral.writeFile(@"D:\VimassJonLog.txt", timenow2 + "__" + json);
                    FunctionGeneral.writeFile(@"D:\VimassTIDLog.txt", timenow2 + "__EPC__Reponse" + response.ToString());
                    if (response.msgCode == 1)
                    {
                        label8.Text = "Ghép thành công_"+ timenow2 +"_"+ FunctionGeneral.TID ;
                        FunctionGeneral.checkTC = FunctionGeneral.TID;
                        label8.ForeColor = Color.Green;
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        FunctionGeneral.writeFile(@"D:\VimassTIDLogSuccess.txt", timenow2 + "_" + FunctionGeneral.TID);
                        label9.Text = "TID : " + FunctionGeneral.TID;
                        label9.ForeColor = Color.Green;
                        label10.Text = "EPC: " + FunctionGeneral.EPC;
                        FunctionGeneral.TID = "";
                        FunctionGeneral.EPC = "";

                        label14.ForeColor = Color.Red;
                        label14.Text = xTID;
                        
                    }
                    else
                    {
                       /* label8.Text = response.msgContent + "_" + timenow2;*/
                        label8.ForeColor = Color.Red;
                        FunctionGeneral.TID = "";
                        FunctionGeneral.EPC = "";
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                    }
                }
            }
            var actionReRead = new Action(() =>
            {
                AutoSend();

            });
            SetTimeout(actionReRead, 500);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Red; textBox2.ForeColor = Color.Green; textBox3.ForeColor = Color.Blue; textBox7.ForeColor = Color.Violet;
            FunctionGeneral.UID = textBox1.Text;
            FunctionGeneral.TID = textBox2.Text; 
            FunctionGeneral.EPC = textBox3.Text;
            FunctionGeneral.SoCuaTem = textBox7.Text;

            if (FunctionGeneral.check == 0)
            {
                label11.Text = "UID : " + FunctionGeneral.UID;
                label11.ForeColor = Color.Red;
            }
           
            label9.Text = "TID : "+ FunctionGeneral.TID;
            label9.ForeColor= Color.Green;
            label10.Text = "EPC: " + FunctionGeneral.EPC;
            label15.Text = "Số của TEM: " + FunctionGeneral.SoCuaTem;
            label15.ForeColor = Color.Violet;
            if (FunctionGeneral.TID!=""&& FunctionGeneral.EPC!= "")
            {
                textBox4.Show();
                button8.Show(); 
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                detectEPC();
            }
            catch(Exception e4)
            {   
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string uid = FunctionGeneral.UID;
            string tid = FunctionGeneral.TID;
            string epc = FunctionGeneral.EPC;
            string socuatem = FunctionGeneral.SoCuaTem;
            DateTime utcDateTime = DateTime.UtcNow;
            string vnTimeZoneKey = "SE Asia Standard Time";
            TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
            DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
            long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();
            string timenow2 = ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
            try
            {
                UidMapTid u = new UidMapTid();
                if (uid != "")
                {
                    u.uID = uid;
                }
                else
                {
                    u.uID = "";
                }
                if (tid != "")
                {
                    u.TID = tid;
                }
                else
                {
                    label8.Text = "Chưa có TID";
                    label8.ForeColor = Color.Red;
                }
                if (epc != "")
                {
                    u.EPC = epc;
                }
                else
                {
                    label8.Text = "Chưa có EPC";
                    label8.ForeColor = Color.Red;
                }
                if (socuatem != "")
                {
                    u.soThe = socuatem;
                }
                else
                {
                    label8.Text = "Chưa nhập số của TEM";
                    label8.ForeColor = Color.Red;
                }
                if (FunctionGeneral.check==1) 
                {
                    if (u.EPC != "" && u.TID != "" && u.soThe != "" && u.EPC != null&& u.TID != null && u.soThe != null)
                    {
                        u.timeRequest = yourDateTimeMilliseconds;
                        u.cks = FunctionGeneral.Md5("ifafioeqfo" + u.uID + u.idVid + u.TID + "1" + u.EPC + u.timeRequest).ToLower();
                        string url = "https://vimass.vn/vmbank/services/TDNService/requestComand";
                        var json = JsonConvert.SerializeObject(u);
                        String res = Service.SendWebrequest_POST_Method(json, url);
                        Response response = JsonConvert.DeserializeObject<Response>(res);
                        FunctionGeneral.writeFile(@"D:\VimassJonLog.txt", timenow2 + "__" + json);
                        FunctionGeneral.writeFile(@"D:\VimassTIDLog.txt", timenow2 + "__EPC__Reponse" + response.ToString());
                        if (response.msgCode == 1)
                        {
                            label8.Text = "Ghép thành công_" + timenow2;
                            label8.ForeColor = Color.Green;
                            textBox1.Text = "";
                            textBox2.Text = "";
                            textBox3.Text = "";
                            FunctionGeneral.writeFile(@"D:\VimassTIDLogSuccess.txt",timenow2+"_"+ tid);
                            FunctionGeneral.TID = "";
                            FunctionGeneral.EPC = "";
                        }
                        else
                        {
                            label8.Text = response.msgContent + "_" + timenow2;
                            label8.ForeColor = Color.Red;
                            FunctionGeneral.TID = "";
                            FunctionGeneral.EPC = "";
                        }
                    }
                    else
                    {
                        label8.Text = "Vui lòng bấm xem lại thông tin";

                    }
                }
                else
                {
                    if (u.EPC != "" && u.TID != "" && u.uID != "" && u.uID.Length>5 && u.TID.Length > 5 && u.EPC.Length > 5)
                    {
                        u.timeRequest = yourDateTimeMilliseconds;
                        u.cks = FunctionGeneral.Md5("ifafioeqfo" + u.uID + u.idVid + u.TID + "1" + u.EPC + u.timeRequest).ToLower();
                        string url = "https://vimass.vn/vmbank/services/TDNService/requestComand";
                        var json = JsonConvert.SerializeObject(u);
                        String res = Service.SendWebrequest_POST_Method(json, url);
                        Response response = JsonConvert.DeserializeObject<Response>(res);
                        FunctionGeneral.writeFile(@"D:\VimassJonLog.txt", timenow2 + "__" + json);
                        FunctionGeneral.writeFile(@"D:\VimassTIDLog.txt", timenow2 + "__EPC__Reponse" + response.ToString());
                        if (response.msgCode == 1)
                        {
                            label8.Text = "Ghép thành công_" + timenow2;
                            label8.ForeColor = Color.Green;
                            textBox1.Text = "";
                            textBox2.Text = "";
                            textBox3.Text = "";
                            FunctionGeneral.writeFile(@"D:\VimassUIDLogSuccess.txt",timenow2+"_"+ uid +"_"+ tid);
                            FunctionGeneral.UID = "";
                            FunctionGeneral.TID = "";
                            FunctionGeneral.EPC = "";
                        }
                        else
                        {
                            label8.Text = response.msgContent + "_" + timenow2;
                            label8.ForeColor = Color.Red;
                            FunctionGeneral.UID = "";
                            FunctionGeneral.TID = "";
                            FunctionGeneral.EPC = "";
                        }
                    }
                    else
                    {
                        label8.Text = "Vui lòng bấm xem lại thông tin";

                    }
                }
               
           
            }
            catch (Exception e4)
            {

            }
         

        }

        private void button6_Click(object sender, EventArgs e)
        {
            new Form2().Show();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

     

        private void button8_Click(object sender, EventArgs e)
        {
            FunctionGeneral.VuID = textBox4.Text;
            new Search().Show();    
        }

/*        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime utcDateTime = DateTime.UtcNow;
                string vnTimeZoneKey = "SE Asia Standard Time";
                TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
                DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
                long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();
                string timenow2 = ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");

                UidMapTid u = new UidMapTid();
                u.sdt = "0966074236";
                u.TID = FunctionGeneral.TID;
                u.EPC = FunctionGeneral.EPC;
                u.curentTime = yourDateTimeMilliseconds;
                u.cks = FunctionGeneral.Md5(u.TID + "99v+6Myt&Tta8mBE" + u.curentTime + u.EPC).ToLower();
                string url = "http://210.245.8.7:12318/vimass/services/VUHF/kiemTraTIDvsEPC";
                var json = JsonConvert.SerializeObject(u);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);
                if (response.msgCode == 1)
                {
                    MessageBox.Show("Thông tin map đúng");
                }
                else
                {
                    MessageBox.Show("Sai rồi");
                }

            }
            catch (Exception exx)
            {

            }
        }*/

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                string[] ports = SerialPort.GetPortNames();
                foreach (string port in ports)
                {
                    comboBox1.Items.Add(port);
                    comboBox2.Items.Add(port);
                    comboBox3.Items.Add(port);
                }
            }
            catch 
            { 
            
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string nameJson = FunctionGeneral.pathJson + textBox5.Text + @".txt";
            String DulieuUHF = "";
            string path = nameJson;
            DulieuUHF = CreatEPCNew.docThongTin(path);
            DateTime utcDateTime = DateTime.UtcNow;
            string vnTimeZoneKey = "SE Asia Standard Time";
            TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
            DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
            long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();

            if (DulieuUHF != "")
            {
                var datauhf = JsonConvert.DeserializeObject<List<UidMapTid>>(DulieuUHF);
                for (int i3 = 0; i3 < datauhf.Count; i3++)
                {
                    try
                    {
                        UidMapTid u = new UidMapTid();

                        u.uID = datauhf[i3].uID;

                        u.TID = datauhf[i3].TID;

                        u.EPC = datauhf[i3].EPC;

                        u.timeRequest = yourDateTimeMilliseconds;
                        u.cks = FunctionGeneral.Md5("ifafioeqfo" + u.uID + u.idVid + u.TID + "1" + u.EPC + u.timeRequest).ToLower();
                        string url = "https://vimass.vn/vmbank/services/TDNService/requestComand";
                        var json = JsonConvert.SerializeObject(u);
                        String res = Service.SendWebrequest_POST_Method(json, url);
                        Response response = JsonConvert.DeserializeObject<Response>(res);

                        if (response.msgCode == 1)
                        {
                            textBox6.Text += u.TID + "%" + u.EPC + "\r\n";

                        }

                    }
                    catch (Exception e4)
                    {

                    }
                }
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra xem ký tự nhập vào có phải là số hoặc ký tự điều khiển (ví dụ: backspace) hay không
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;  // Ngăn chặn việc nhập ký tự này vào TextBox
            }
        }
    }
}
