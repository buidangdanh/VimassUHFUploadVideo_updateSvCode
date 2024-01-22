using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using VimassUHFUploadVideo.Ultil;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections;

namespace VimassUHFUploadVideo
{
    public partial class CreatEPCNew : Form
    {
        public CreatEPCNew()
        {
            InitializeComponent();
        }

        private void CreatEPCNew_Load(object sender, EventArgs e)
        {
            try
            {   
                label21.Text = "";
                label19.Hide();
                label18.Hide();
                textBox9.Hide();
                textBox6.Hide();
                button17.Hide();
                button14.Hide();

                textBox4.Hide();
                string[] ports = SerialPort.GetPortNames();
                if (comboBox1.Text == ""|| comboBox2.Text == "")
                {
                    foreach (string port in ports)
                    {
                        comboBox1.Items.Add(port);
                        comboBox2.Items.Add(port);
                    }
                }
                GetMCID u = new GetMCID();
                DateTime utcDateTime = DateTime.UtcNow;
                string vnTimeZoneKey = "SE Asia Standard Time";
                TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
                DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
                string timenow2 = ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
                long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();

                u.sdt = "0966074236";
                u.currentTime= yourDateTimeMilliseconds;
                u.typeDevice= 1;
                u.cks = FunctionGeneral.Md5(u.currentTime + "99CkMsx2&Ata8mBE" + u.sdt).ToLower();
                string url = "http://210.245.8.7:12318/vimass/services/VUHF/listVUHFDevice";
                var json = JsonConvert.SerializeObject(u);
                Console.WriteLine(json);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);
                FunctionGeneral.writeFile(@"D:\VMCreat.txt", timenow2 + "__" + json);
                FunctionGeneral.writeFile(@"D:\VMCreat.txt", timenow2 + "__EPC__Reponse" + response.ToString());
                if (response.msgCode == 1)
                {
                    var datamcID = JsonConvert.DeserializeObject<List<MCIDObj>>(response.result.ToString().Replace("//", ""));
                    FunctionGeneral.writeFile(@"D:\VMCreat.txt", timenow2 + "__" + datamcID);
                    if (datamcID != null)
                    {
                        for (int i2 = 0; i2 < datamcID.Count; i2++)
                        {
                            FunctionGeneral.DataMcID.Add(datamcID[i2].mcName, datamcID[i2].mcID);
                            comboBox3.Items.Add(datamcID[i2].mcName);
                            Console.WriteLine(datamcID[i2].mcID);
                        }
                    }
                }
                else
                {

                }
                label1.Text = "Tạo EPC theo chuẩn Vimass";
                label1.ForeColor = Color.Blue;
                label5.Text = "";
                label6.Text = "";
                label7.Text = "";
                label9.Text = "";
                label10.Text = "";
                label11.Text = "";
                label13.Text = "";
                label15.Text = "";


            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);    
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                detectTID();
            }
            catch(Exception ex)
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
                                textBox2.Text = vuIDFinal;

                                var actionReRead = new Action(() =>
                                {
                                    detectTID();

                                });
                                FunctionGeneral.SetTimeout(actionReRead, 1000);
                                label9.Text = "Đã nhận TID: " + vuIDIn + "_" + ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
                            }
                        }

                    });
                    FunctionGeneral.SetTimeout(action, 2000);
                    //   Debug.WriteLine(vuid);
                }
            }
            catch (Exception e3)
            {
                FunctionGeneral.writeFile(@"D:\VMCreat.txt",  "__TID__Die" + e3);

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                label10.Text = textBox2.Text;
                label10.ForeColor = Color.Red;
                FunctionGeneral.TIDCreat = textBox2.Text.Trim();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);    
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                FunctionGeneral.LoaiXe = "1";
                label5.Text = "Đã chọn Xe Đạp";
                label5.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                FunctionGeneral.LoaiXe = "2";
                label5.Text = "Đã chọn Xe Máy";
                label5.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                FunctionGeneral.LoaiXe = "3";
                label5.Text = "Đã chọn Ô Tô";
                label5.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                FunctionGeneral.DinhDanh = "4";
                label6.Text = "Đã chọn Biển Số";
                label6.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                FunctionGeneral.DinhDanh = "5";
                label6.Text = "Đã chọn Số Điện Thoại";
                label6.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
       
            try
            {
                FunctionGeneral.DinhDanh = "6";
                label6.Text = "Đã chọn MST";
                label6.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FunctionGeneral.GiaTri = textBox1.Text;
            label7.Text = textBox1.Text;
            label7.ForeColor = Color.Red;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime utcDateTime = DateTime.UtcNow;
                string vnTimeZoneKey = "SE Asia Standard Time";
                TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
                DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
                string timenow2 = ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
                long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();

                CreatEPCNewClass u = new CreatEPCNewClass();
                if (FunctionGeneral.LoaiXe != "" && FunctionGeneral.DinhDanh != "" &&FunctionGeneral.McName!=""&& FunctionGeneral.TIDCreat != "")
                {
                    if(FunctionGeneral.DinhDanh == "8")
                    {
                        Unknown u3 = new Unknown();
                        if (FunctionGeneral.TIDCreat != "")
                        {
                            u3.TID.Add(FunctionGeneral.TIDCreat);
                        }
                        else
                        {
                            string nameExcel = textBox5.Text;
                            string nameSheet = textBox11.Text;
                            int soCot = Int32.Parse(textBox12.Text);
                            string nameJson = FunctionGeneral.pathJson + textBox5.Text + @".txt";
                            File.WriteAllText(nameJson, FunctionGeneral.ExcelToJson(nameExcel, nameSheet, soCot));
                            String DulieuUHF = "";
                            DulieuUHF = docThongTin(nameJson);
                            if (DulieuUHF != "")
                            {
                                var datauhf = JsonConvert.DeserializeObject<List<DataUHF>>(DulieuUHF);

                                for (int i3 = 0; i3 < datauhf.Count; i3++)
                                {
                                    u3.TID.Add(datauhf[i3].TID);
                                }
                            }
                        }
                        foreach (KeyValuePair<string, string> mcID in FunctionGeneral.DataMcID)
                        {
                            if (mcID.Key == FunctionGeneral.McName)
                            {
                                u3.mcID = mcID.Value;
                            }
                        }

                        if (FunctionGeneral.LoaiDot == "1")
                        {
                            if (FunctionGeneral.LoaiXe == "1")
                            {
                                u3.L = "{";
                            }
                            else if (FunctionGeneral.LoaiXe == "2")
                            {
                                u3.L = "|";
                            }
                            else if (FunctionGeneral.LoaiXe == "3")
                            {
                                u3.L = "}";
                            }
                        }
                        else if (FunctionGeneral.LoaiDot == "2")
                        {
                            if (FunctionGeneral.LoaiXe == "1")
                            {
                                u3.L = "!";
                            }
                            else if (FunctionGeneral.LoaiXe == "2")
                            {
                                u3.L = @"\";
                            }
                            else if (FunctionGeneral.LoaiXe == "3")
                            {
                                u3.L = "#";
                            }
                        }

                        u3.currentTime = yourDateTimeMilliseconds;
                        u3.sdt = FunctionGeneral.soVi;
                        u3.cks = FunctionGeneral.Md5(u3.currentTime + FunctionGeneral.keyCreateEPCUnkown + u3.sdt + u3.mcID).ToLower();

                        string url = "http://210.245.8.7:12318/vimass/services/VUHF/createEPCidUnKnown";
                        var json = JsonConvert.SerializeObject(u3);
                        String res = Service.SendWebrequest_POST_Method(json, url);
                        Response response = JsonConvert.DeserializeObject<Response>(res);
                        if (response.msgCode == 1)
                        {
                            label11.Text = response.result.ToString();
                            label11.ForeColor = Color.Red;
                            textBox3.Text = response.result.ToString();
                            FunctionGeneral.checkTC = u.TID;
                            FunctionGeneral.EPCNew = response.result.ToString();
                            FunctionGeneral.writeFile(@"D:\VMCreat.txt", timenow2 + "__EPCCreat__" + FunctionGeneral.EPCNew);
                        }
                        else
                        {
                            label11.Text = response.msgContent;
                        }


                    }
                    if(FunctionGeneral.DinhDanh=="7")
                    {
                        EPCClone u2 = new EPCClone();
                        foreach (KeyValuePair<string, string> mcID in FunctionGeneral.DataMcID)
                        {
                            if (mcID.Key == FunctionGeneral.McName)
                            {
                                u2.mcID = mcID.Value;
                            }
                        }
                        u2.dataEPCClone = textBox1.Text;
                        u2.dataTID = FunctionGeneral.TIDCreat;
                        u2.currentTime = yourDateTimeMilliseconds;
                        u2.sdt = FunctionGeneral.soVi;
                        u2.cks = FunctionGeneral.Md5(u2.currentTime + FunctionGeneral.keyCreateEPCUnkown + u2.sdt + u2.mcID).ToLower();
                        string url2 = "http://210.245.8.7:12318/vimass/services/VUHF/createEPCidUnKnown";
                        var json2 = JsonConvert.SerializeObject(u2);
                        String res2 = Service.SendWebrequest_POST_Method(json2, url2);
                        Response response2 = JsonConvert.DeserializeObject<Response>(res2);
                        if (response2.msgCode == 1)
                        {
                            label11.Text = response2.result.ToString();
                            label11.ForeColor = Color.Red;
                            textBox3.Text = response2.result.ToString();
                            FunctionGeneral.checkTC = u2.dataTID;
                            FunctionGeneral.EPCNew = response2.result.ToString();
                            FunctionGeneral.writeFile(@"D:\VMCreat.txt", timenow2 + "__EPCCreat__" + FunctionGeneral.EPCNew);
                        }



                    }
                    else if(FunctionGeneral.DinhDanh != "7"&& FunctionGeneral.DinhDanh != "8")
                    {
                        foreach (KeyValuePair<string, string> mcID in FunctionGeneral.DataMcID)
                        {
                            if (mcID.Key == FunctionGeneral.McName)
                            {
                                u.mcID = mcID.Value;
                            }
                        }
                        if (FunctionGeneral.LoaiDot == "1")
                        {
                            if (FunctionGeneral.LoaiXe == "1")
                            {
                                if (FunctionGeneral.DinhDanh == "4")
                                {
                                    u.L = "1";
                                }
                                else if (FunctionGeneral.DinhDanh == "5")
                                {
                                    u.L = "4";
                                }
                                else if (FunctionGeneral.DinhDanh == "6")
                                {
                                    u.L = "7";
                                }
                            }
                            else if (FunctionGeneral.LoaiXe == "2")
                            {
                                if (FunctionGeneral.DinhDanh == "4")
                                {
                                    u.L = "2";
                                }
                                else if (FunctionGeneral.DinhDanh == "5")
                                {
                                    u.L = "5";
                                }
                                else if (FunctionGeneral.DinhDanh == "6")
                                {
                                    u.L = "8";
                                }
                            }
                            else if (FunctionGeneral.LoaiXe == "3")
                            {
                                if (FunctionGeneral.DinhDanh == "4")
                                {
                                    u.L = "3";
                                }
                                else if (FunctionGeneral.DinhDanh == "5")
                                {
                                    u.L = "6";
                                }
                                else if (FunctionGeneral.DinhDanh == "6")
                                {
                                    u.L = "9";
                                }
                            }
                        }
                        else if (FunctionGeneral.LoaiDot == "2")
                        {
                            if (FunctionGeneral.LoaiXe == "1")
                            {
                                if (FunctionGeneral.DinhDanh == "4")
                                {
                                    u.L = ":";
                                }
                                else if (FunctionGeneral.DinhDanh == "5")
                                {
                                    u.L = "=";
                                }
                                else if (FunctionGeneral.DinhDanh == "6")
                                {
                                    u.L = "@";
                                }
                            }
                            else if (FunctionGeneral.LoaiXe == "2")
                            {
                                if (FunctionGeneral.DinhDanh == "4")
                                {
                                    u.L = ";";
                                }
                                else if (FunctionGeneral.DinhDanh == "5")
                                {
                                    u.L = ">";
                                }
                                else if (FunctionGeneral.DinhDanh == "6")
                                {
                                    u.L = "A";
                                }
                            }
                            else if (FunctionGeneral.LoaiXe == "3")
                            {
                                if (FunctionGeneral.DinhDanh == "4")
                                {
                                    u.L = "<";
                                }
                                else if (FunctionGeneral.DinhDanh == "5")
                                {
                                    u.L = "?";
                                }
                                else if (FunctionGeneral.DinhDanh == "6")
                                {
                                    u.L = "B";
                                }
                            }
                        }
                        if (FunctionGeneral.DinhDanh == "5")
                        {
                            u.X = FunctionGeneral.GiaTri.Substring(1, FunctionGeneral.GiaTri.Length - 1);
                        }
                        else
                        {
                            if (FunctionGeneral.GiaTri.IndexOf("-") > -1)
                            {
                                u.X = FunctionGeneral.GiaTri.ToUpper();
                            }
                            else
                            {
                                u.X = FunctionGeneral.GiaTri.ToUpper().Trim().Replace(" ", "");
                            }
                           
                        }

                        if (FunctionGeneral.TIDCreat == FunctionGeneral.checkTC)
                        {
                            label11.Text = "Đã tạo EPC cho TID này rồi!";
                            label11.ForeColor = Color.Red;
                        }
                        else
                        {
                            u.TID = FunctionGeneral.TIDCreat;
                        }
                        u.sdt = FunctionGeneral.soVi;
                        u.currentTime = yourDateTimeMilliseconds;
                        u.cks = FunctionGeneral.Md5(u.X + u.currentTime + FunctionGeneral.keyCreateEPC + u.TID + u.sdt).ToLower();
                        FunctionGeneral.writeFile(@"D:\VMCreat.txt", "__MD5__" + u.X + u.currentTime + "Pa87L7LdLwocks&5" + u.TID + u.sdt);
                        
                        string url = "http://210.245.8.7:12318/vimass/services/VUHF/createEPCid";
                        var json = JsonConvert.SerializeObject(u);
                        String res = Service.SendWebrequest_POST_Method(json, url);
                        Response response = JsonConvert.DeserializeObject<Response>(res);
                        if (response.msgCode == 1)
                        {
                            label11.Text = response.result.ToString();
                            label11.ForeColor = Color.Red;
                            textBox3.Text = response.result.ToString();
                            FunctionGeneral.checkTC = u.TID;
                            FunctionGeneral.EPCNew = response.result.ToString();
                            FunctionGeneral.writeFile(@"D:\VMCreat.txt", timenow2 + "__EPCCreat__" + FunctionGeneral.EPCNew);
                        }
                        else
                        {
                            label11.Text = response.msgContent;
                        }
                    }
                    
                }
                else
                {
                    label11.Text = "Thiếu thông tin vui lòng xem lại!";
                    label11.ForeColor = Color.Red;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
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
                        FunctionGeneral.writeFile(@"D:\VMCreat.txt", timenow2 + "_EPC_1_" + vUidFull);
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
                        FunctionGeneral.writeFile(@"D:\VMCreat.txt", timenow2 + "_EPC_2_" + ListIDTrung);
                        foreach (string vuIDIn in ListIDTrung)
                        {
                            FunctionGeneral.writeFile(@"D:\VMCreat.txt", timenow2 + "_EPC_3_" + vuIDIn);
                            if (vuIDIn.Length > 5)
                            {
                                string vuIDFinal = vuIDIn.Substring(1, vuIDIn.Length - 2);
                                label13.Text = vuIDFinal;

                                var actionReRead = new Action(() =>
                                {
                                    detectEPC();

                                });
                                FunctionGeneral.SetTimeout(actionReRead, 1000);
                                label22.Text = "Đã nhận EPC: " + vuIDFinal + "_" + ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
                                textBox10.Text= vuIDFinal;
                            }
                        }

                    });
                    FunctionGeneral.SetTimeout(action, 2000);
                    //   Debug.WriteLine(vuid);
                }
            }
            catch (Exception e3)
            {
                FunctionGeneral.writeFile(@"D:\VMCreat.txt",   "__EPC__Die" + e3);

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                if (FunctionGeneral.EPCNew != "")
                {
                    DateTime utcDateTime = DateTime.UtcNow;
                    string vnTimeZoneKey = "SE Asia Standard Time";
                    TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
                    DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
                    string timenow2 = ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
                    long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();

                    if (FunctionGeneral.DinhDanh == "8"|| FunctionGeneral.DinhDanh == "7")
                    {
                        ConfimUnknown u2 = new ConfimUnknown();
                        u2.TIDs.Add(FunctionGeneral.TIDCreat);
                        u2.currentTime = yourDateTimeMilliseconds;
                        u2.sdt = FunctionGeneral.soVi;
                        u2.cks = FunctionGeneral.Md5(u2.currentTime + FunctionGeneral.keyconfirmEPCUnkown + u2.sdt + 1).ToLower();
                        string url2 = "http://210.245.8.7:12318/vimass/services/VUHF/confirmWriteEPCidUnKnown";
                        var json2 = JsonConvert.SerializeObject(u2);
                        String res2 = Service.SendWebrequest_POST_Method(json2, url2);
                        Response response2 = JsonConvert.DeserializeObject<Response>(res2);
                        if (response2.msgCode == 1)
                        {
                            label15.Text = "OK";
                            label15.ForeColor = Color.Green;
                            label5.Text = "";
                            label6.Text = "";
                            label7.Text = "";
                            label10.Text = "";
                            label11.Text = "";
                            textBox1.Text = "";
                            FunctionGeneral.writeFile(FunctionGeneral.pathJson + @"EPCTaoThanhCong.txt", timenow2 + "__EPCOK__" + FunctionGeneral.EPCNew + "__" + FunctionGeneral.TIDCreat);
                        }
                        else
                        {
                            label15.Text = response2.msgContent;
                        }
                    }
                    else {

                        ExceptWriteEpc u = new ExceptWriteEpc();
                        u.EPC = FunctionGeneral.EPCNew;
                        u.currentTime = yourDateTimeMilliseconds;
                        u.sdt = FunctionGeneral.soVi;
                        u.cks = FunctionGeneral.Md5(u.currentTime + FunctionGeneral.keyconfirmEPC + u.EPC + u.sdt).ToLower();
                        string url = "http://210.245.8.7:12318/vimass/services/VUHF/confirmWriteEPCid";
                        var json = JsonConvert.SerializeObject(u);
                        String res = Service.SendWebrequest_POST_Method(json, url);
                        Response response = JsonConvert.DeserializeObject<Response>(res);
                        if (response.msgCode == 1)
                        {
                            label15.Text = "OK";
                            label15.ForeColor = Color.Green;
                            label5.Text = "";
                            label6.Text = "";
                            label7.Text = "";
                            label10.Text = "";
                            label11.Text = "";
                            textBox1.Text = "";
                            FunctionGeneral.writeFile(FunctionGeneral.pathJson + @"EPCTaoThanhCong.txt", timenow2 + "__EPCOK__" + FunctionGeneral.EPCNew + "__" + FunctionGeneral.TIDCreat);
                        }
                        else
                        {
                            label15.Text = response.msgContent;
                        }
                    }

                }
                else
                {
                    label15.Text = "";
                }
                if (FunctionGeneral.DinhDanh == "7")
                {
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            FunctionGeneral.McName = comboBox3.Text;
        }
        public static string docThongTin(string path)
        {
            string temp = File.ReadAllText(path);
            return temp;
        }
        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                string nameExcel = textBox5.Text;
                string nameSheet = textBox11.Text;
                int soCot = Int32.Parse(textBox12.Text);
                string nameJson = FunctionGeneral.pathJson + textBox5.Text + @".txt";
                File.WriteAllText(nameJson, FunctionGeneral.ExcelToJson(nameExcel, nameSheet, soCot));

                String DulieuUHF = "";
                string path = nameJson;
                DulieuUHF = docThongTin(path);
                if (DulieuUHF != "")
                {
                    var datauhf = JsonConvert.DeserializeObject<List<DataUHF>>(DulieuUHF);
                    for (int i3 = 0; i3 < datauhf.Count; i3++)
                    {
                        CreatEPCNewClass u = new CreatEPCNewClass();
                        DateTime utcDateTime = DateTime.UtcNow;
                        string vnTimeZoneKey = "SE Asia Standard Time";
                        TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
                        DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
                        string timenow2 = ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
                        long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();
                        if (datauhf[i3].TID==(null)){
                            break;
                        }
                        u.TID = datauhf[i3].TID.Trim();
                        FunctionGeneral.GiaTri = datauhf[i3].Giatri;


                        if (FunctionGeneral.LoaiXe != "" && FunctionGeneral.DinhDanh != "" && FunctionGeneral.GiaTri != "" && FunctionGeneral.LoaiXe != null && FunctionGeneral.DinhDanh != null && FunctionGeneral.GiaTri != null)
                        {
                            foreach (KeyValuePair<string, string> mcID in FunctionGeneral.DataMcID)
                            {
                                if (mcID.Key == FunctionGeneral.McName)
                                {
                                    u.mcID = mcID.Value;
                                }
                            }

                            if (FunctionGeneral.LoaiDot == "1")
                            {
                                if (FunctionGeneral.LoaiXe == "1")
                                {
                                    if (FunctionGeneral.DinhDanh == "4")
                                    {
                                        u.L = "1";
                                    }
                                    else if (FunctionGeneral.DinhDanh == "5")
                                    {
                                        u.L = "4";
                                    }
                                    else if (FunctionGeneral.DinhDanh == "6")
                                    {
                                        u.L = "7";
                                    }
                                }
                                else if (FunctionGeneral.LoaiXe == "2")
                                {
                                    if (FunctionGeneral.DinhDanh == "4")
                                    {
                                        u.L = "2";
                                    }
                                    else if (FunctionGeneral.DinhDanh == "5")
                                    {
                                        u.L = "5";
                                    }
                                    else if (FunctionGeneral.DinhDanh == "6")
                                    {
                                        u.L = "8";
                                    }
                                }
                                else if (FunctionGeneral.LoaiXe == "3")
                                {
                                    if (FunctionGeneral.DinhDanh == "4")
                                    {
                                        u.L = "3";
                                    }
                                    else if (FunctionGeneral.DinhDanh == "5")
                                    {
                                        u.L = "6";
                                    }
                                    else if (FunctionGeneral.DinhDanh == "6")
                                    {
                                        u.L = "9";
                                    }
                                }
                            }
                            else if (FunctionGeneral.LoaiDot == "2")
                            {
                                if (FunctionGeneral.LoaiXe == "1")
                                {
                                    if (FunctionGeneral.DinhDanh == "4")
                                    {
                                        u.L = ":";
                                    }
                                    else if (FunctionGeneral.DinhDanh == "5")
                                    {
                                        u.L = "=";
                                    }
                                    else if (FunctionGeneral.DinhDanh == "6")
                                    {
                                        u.L = "@";
                                    }
                                }
                                else if (FunctionGeneral.LoaiXe == "2")
                                {
                                    if (FunctionGeneral.DinhDanh == "4")
                                    {
                                        u.L = ";";
                                    }
                                    else if (FunctionGeneral.DinhDanh == "5")
                                    {
                                        u.L = ">";
                                    }
                                    else if (FunctionGeneral.DinhDanh == "6")
                                    {
                                        u.L = "A";
                                    }
                                }
                                else if (FunctionGeneral.LoaiXe == "3")
                                {
                                    if (FunctionGeneral.DinhDanh == "4")
                                    {
                                        u.L = "<";
                                    }
                                    else if (FunctionGeneral.DinhDanh == "5")
                                    {
                                        u.L = "?";
                                    }
                                    else if (FunctionGeneral.DinhDanh == "6")
                                    {
                                        u.L = "B";
                                    }
                                }
                            }
                            if (FunctionGeneral.DinhDanh == "5")
                            {
                                u.X = FunctionGeneral.GiaTri.Substring(1, FunctionGeneral.GiaTri.Length - 1);
                            }
                            else
                            {
                                if (FunctionGeneral.GiaTri.IndexOf("-") > -1)
                                {
                                    u.X = FunctionGeneral.GiaTri.ToUpper();
                                }
                                else
                                {
                                    u.X = FunctionGeneral.GiaTri.ToUpper().Trim().Replace(" ","");
                                }
                               
                            }

                            if (FunctionGeneral.TIDCreat == FunctionGeneral.checkTC)
                            {
                                label11.Text = "Đã tạo EPC cho TID này rồi!";
                                label11.ForeColor = Color.Red;
                            }
                            else
                            {

                            }
                            u.sdt = FunctionGeneral.soVi;
                            u.currentTime = yourDateTimeMilliseconds;
                            u.cks = FunctionGeneral.Md5(u.X + u.currentTime + FunctionGeneral.keyCreateEPC + u.TID + u.sdt).ToLower();
                            string url = "http://210.245.8.7:12318/vimass/services/VUHF/createEPCid";
                            var json = JsonConvert.SerializeObject(u);
                            String res = Service.SendWebrequest_POST_Method(json, url);
                            Response response = JsonConvert.DeserializeObject<Response>(res);
                            if (response.msgCode == 1)
                            {
                                FunctionGeneral.dataShow += response.result.ToString() + "      " + u.TID + "       " + u.X + "\r\n";
                                /*  textBox4.Text += response.result.ToString() +"      "+u.TID + "       "+u.X+ "\r\n";*/
                                FunctionGeneral.dataLuuJson += @"{'TID':'" + u.TID + @"','Giatri':'" + u.X + @"','EPC':'" + response.result.ToString() + @"'},";

                                /*  conFirmEpc(response.result.ToString());*/
                                FunctionGeneral.writeFile(@"D:\VMCreateEPCNewSuccess.txt", timenow2 + "__EPCCreat__" + FunctionGeneral.EPCNew + " " + u.TID + " " + u.X);
                            }
                            else
                            {
                                label11.Text = response.msgContent;
                            }
                        }
                        else
                        {
                            label11.Text = "Thiếu thông tin vui lòng xem lại!";
                            label11.ForeColor = Color.Red;
                        }
                    }
                    new ShowData().Show();
                    File.WriteAllText(FunctionGeneral.pathJson + @"test4.txt", @"[" + FunctionGeneral.dataLuuJson.Substring(0, FunctionGeneral.dataLuuJson.Length - 1) + @"]");
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                String nameJson = textBox8.Text;
                String DulieuUHF = "";
                string path = FunctionGeneral.pathJson + nameJson + @".txt";
                DulieuUHF = docThongTin(path);
                var datauhf = JsonConvert.DeserializeObject<List<DataUHF>>(DulieuUHF);
                for (int i3 = 0; i3 < datauhf.Count; i3++)
                {
                    if (true)
                    {
                        ExceptWriteEpc u = new ExceptWriteEpc();
                        DateTime utcDateTime = DateTime.UtcNow;
                        string vnTimeZoneKey = "SE Asia Standard Time";
                        TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
                        DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
                        string timenow2 = ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
                        long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();
                        u.EPC = datauhf[i3].EPC;
                        u.currentTime = yourDateTimeMilliseconds;
                        u.sdt = FunctionGeneral.soVi;
                        u.cks = FunctionGeneral.Md5(u.currentTime + FunctionGeneral.keyconfirmEPC + u.EPC + u.sdt).ToLower();
                        string url = "http://210.245.8.7:12318/vimass/services/VUHF/confirmWriteEPCid";
                        var json = JsonConvert.SerializeObject(u);
                        String res = Service.SendWebrequest_POST_Method(json, url);
                        Response response = JsonConvert.DeserializeObject<Response>(res);
                        if (response.msgCode == 1)
                        {
                            label15.Text = "OK";
                            label15.ForeColor = Color.Green;
                            label5.Text = "";
                            label6.Text = "";
                            label7.Text = "";
                            label10.Text = "";
                            label11.Text = "";
                            textBox1.Text = "";
                            FunctionGeneral.writeFile(@"D:\VMCREPCOK.txt", timenow2 + "__EPCOK__" + u.EPC);
                            FunctionGeneral.GiaTri = "";
                            FunctionGeneral.TID = "";
                            FunctionGeneral.EPCNew = "";
                              
                           /* textBox4.Text += u.EPC + "\r\n";*/
                            FunctionGeneral.dataShow += u.EPC + "\r\n";
                        }
                        else
                        {
                            label15.Text = response.msgContent;
                        }
                    }
                    else
                    {
                        label15.Text = "";
                    }
                }
                new ShowData().Show();
                
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       
        public void conFirmEpc(string epc)
        {
            try
            {
                ExceptWriteEpc u = new ExceptWriteEpc();
                DateTime utcDateTime = DateTime.UtcNow;
                string vnTimeZoneKey = "SE Asia Standard Time";
                TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
                DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
                string timenow2 = ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
                long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();
                u.EPC = epc;
                u.currentTime = yourDateTimeMilliseconds;
                u.sdt = "0966074236";
                u.cks = FunctionGeneral.Md5(u.currentTime + "PalSWXwDwJd2U" + u.EPC + u.sdt).ToLower();
                string url = "http://210.245.8.7:12318/vimass/services/VUHF/confirmWriteEPCid";
                var json = JsonConvert.SerializeObject(u);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);
                if (response.msgCode == 1)
                {
                    FunctionGeneral.writeFile(FunctionGeneral.pathLog+@"EpcConFirm.txt", timenow2 + "_" + u.EPC);
                    textBox4.Show();
                    textBox4.Text += u.EPC + "\r\n";
                }
                else
                {
                    textBox4.Text = u.EPC +" "+ response.msgContent;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string FileName = textBox6.Text;
            File.WriteAllText(FunctionGeneral.pathJson + FileName + @".txt", @"[" + FunctionGeneral.dataLuuJson.Substring(0, FunctionGeneral.dataLuuJson.Length - 1) + @"]");
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime utcDateTime = DateTime.UtcNow;
                string vnTimeZoneKey = "SE Asia Standard Time";
                TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
                DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
                long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();
                string timenow2 = ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");

                LayDSEPC u = new LayDSEPC();
                if (FunctionGeneral.McName != "")
                {
                    foreach (KeyValuePair<string, string> mcID in FunctionGeneral.DataMcID)
                    {
                        if (mcID.Key == FunctionGeneral.McName)
                        {
                            u.mcID = mcID.Value;
                        }
                    }
                    u.sdt = "0966074236";
                    u.currentTime = yourDateTimeMilliseconds;
                    u.typeS = 1;
                    u.cks = FunctionGeneral.Md5(u.mcID + u.currentTime + "Y+6Mxt&Tta8mBE").ToLower(); 
                    string url = "http://210.245.8.7:12318/vimass/services/VUHF/getDsUcodeTheoDonVi";
                    var json = JsonConvert.SerializeObject(u);
                    String res = Service.SendWebrequest_POST_Method(json, url);
                    Response response = JsonConvert.DeserializeObject<Response>(res);
                    
                    if (response.msgCode == 1)
                    {
                       /* textBox7.Text = "http://210.245.8.7:12318/vimass/services/VUHF/getFile?id="+ response.result.ToString();*/
                        WebClient webClient = new WebClient();
                        webClient.DownloadFile("http://210.245.8.7:12318/vimass/services/VUHF/getFile?id=" + response.result.ToString(), FunctionGeneral.pathExcel+ textBox7.Text+ @".csv");
                    }

                }
          
            }
            catch(Exception ee)
            {

            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            FunctionGeneral.dataLuuJson = "";
            textBox2.Text = "";
            textBox4.Text = "";
            FunctionGeneral.TIDCreat = "";
        }

        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime utcDateTime = DateTime.UtcNow;
                string vnTimeZoneKey = "SE Asia Standard Time";
                TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
                DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
                long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();
                string timenow2 = ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
                String nameJson = textBox9.Text;
                String DulieuUHF = "";
                string path = FunctionGeneral.pathJson + nameJson + @".txt";
                DulieuUHF = docThongTin(path);
                var datauhf = JsonConvert.DeserializeObject<List<DataUHF>>(DulieuUHF);
                for (int i3 = 0; i3 < datauhf.Count; i3++)
                {
                    CheckMapsBs u = new CheckMapsBs();
                    u.sdt = "0966074236";
                    u.TID = datauhf[i3].TID;
                    u.EPC = datauhf[i3].EPC;
                    u.curentTime = yourDateTimeMilliseconds;
                    u.cks = FunctionGeneral.Md5(u.TID + "99v+6Myt&Tta8mBE" + u.curentTime + u.EPC).ToLower();
                    string url = "http://210.245.8.7:12318/vimass/services/VUHF/kiemTraTIDvsEPC";
                    var json = JsonConvert.SerializeObject(u);
                    String res = Service.SendWebrequest_POST_Method(json, url);
                    Response response = JsonConvert.DeserializeObject<Response>(res);
                    if (response.msgCode == 1)
                    {
                        textBox4.Text += u.EPC + "      " + u.TID + "\r\n";
                        
                    }
                    else
                    {
                        FunctionGeneral.dataLuuJson += @"{'TID':'" + u.TID + @"','EPCN':'" + u.EPC + @"'},";
                    }
                }
                textBox4.Show();
            }
            catch(Exception exx)
            {

            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            try
            {
                FunctionGeneral.LoaiDot = "2";
                label21.Text = "Đã chọn Thẻ";
                label21.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                FunctionGeneral.LoaiDot = "1";
                label21.Text = "Đã chọn Tem";
                label21.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void button20_Click(object sender, EventArgs e)
        {
            try
            {
                FunctionGeneral.DinhDanh = "7";
                label6.Text = "Đã chọn ETC";
                label6.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox3.Text);
        }

        private void button22_Click(object sender, EventArgs e)
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
                u.TID = FunctionGeneral.TIDCreat;
                u.EPC = textBox10.Text.Trim();
                u.curentTime = yourDateTimeMilliseconds;
                u.cks = FunctionGeneral.Md5(u.TID + "99v+6Myt&Tta8mBE" + u.curentTime + u.EPC).ToLower();
                string url = "http://210.245.8.7:12318/vimass/services/VUHF/kiemTraTIDvsEPC";
                var json = JsonConvert.SerializeObject(u);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);
                if (response.msgCode == 1)
                {
                    MessageBox.Show("Đã Ghi EPC đúng");
                }
                else
                {
                    MessageBox.Show("Sai rồi");
                }

            }
            catch (Exception exx)
            {

            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text= Clipboard.GetText();
            }
            catch
            {

            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            try
            {
                FunctionGeneral.DinhDanh = "8";
                label6.Text = "Đã chọn Unknown";
                label6.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
