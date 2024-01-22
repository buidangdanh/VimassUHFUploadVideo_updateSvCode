using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VimassUHFUploadVideo
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        static DateTime utcDateTime = DateTime.UtcNow;
        static string vnTimeZoneKey = "SE Asia Standard Time";
        static TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
        static DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
        long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();
        string timenow = ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
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
                port2.Open();
                string str4 = "";
                if (true)
                {
                    
                        string vUidFull = port2.ReadExisting();
                        FunctionGeneral.writeFile(@"D:\VimassTIDLog.txt", timenow + "_TID_1_" + vUidFull);
                        if (vUidFull.Length < 5)
                        {
                            var action4 = new Action(() =>
                            {
                                detectTID();
                            });
                            AddTIDUID.SetTimeout(action4, 2000);
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
                        FunctionGeneral.writeFile(@"D:\VimassTIDLog.txt", timenow + "_TID_2_" + ListIDTrung);
                        foreach (string vuIDIn in ListIDTrung)
                        {
                            FunctionGeneral.writeFile(@"D:\VimassTIDLog.txt", timenow + "_TID_3_" + vuIDIn);
                            if (vuIDIn.Length > 5)
                            {
                                string vuIDFinal = vuIDIn.Substring(1, vuIDIn.Length - 2);
                                textBox1.Text = vuIDFinal;

                                var actionReRead = new Action(() =>
                                {
                                    detectTID();

                                });
                                AddTIDUID.SetTimeout(actionReRead, 100);
                                label2.Text = vuIDIn;
                                label2.ForeColor = Color.Red;
                                label3.Text = ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
                            }
                        }

                    
                    //   Debug.WriteLine(vuid);
                }
            }
            catch (Exception e3)
            {
                FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", timenow + "__TID__Die" + e3);

            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label2.Text = "";
            label3.Text = "";
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            detectTID();
        }
    }
}
