using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VimassUHFUploadVideo
{
    public partial class FormCheck : Form
    {
        public FormCheck()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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
                FunctionGeneral.port = comboBox1.Text;
                SerialPort port2 = new SerialPort(FunctionGeneral.port, 9600, Parity.None, 8, StopBits.One);
                string timenow2 = ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
                port2.Open();
                // string vuid = readingVuid();
                // FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", timenow + "_HHTIN_" + vuid);
                string str4 = "";
                if (true)
                {
                    string vUidFull = port2.ReadExisting();
                    FunctionGeneral.writeFile(@"D:\VimassTIDLog.txt", timenow2 + "_TID_1_" + vUidFull);
                    if (vUidFull.Length < 5)
                    {
                        var action4 = new Action(() =>
                        {
                            detectTID();
                        });
                        SetTimeout(action4, 100);
                    }
                    port2.Close();
                    string[] ListID = vUidFull.Split('\n');
                    foreach (string text in ListID)
                    {
                        /* if (str4.IndexOf(text) < 0)
                            {
                                str4 += text;
                            }*/
                        string[] ListIDTrung = str4.Split('');
                        FunctionGeneral.writeFile(@"D:\VimassTIDLog.txt", timenow2 + "_TID_2_" + ListIDTrung);
                        /*foreach (string vuIDIn in ListIDTrung)
                        {*/
                        if (text.Length > 5)
                        {
                            string vuIDFinal = text.Substring(1, text.Length - 2);
                            if (textBox1.Text.IndexOf(vuIDFinal) < 0)
                            {
                                textBox1.Text += FunctionGeneral.dem.ToString()+ "            "+ vuIDFinal + "\r\n";
                                FunctionGeneral.dem++;
                                if (FunctionGeneral.dem == 10)
                                {
                                    port2.Close() ;
                                }
                            }
                        }
                    }
                    var actionReRead = new Action(() =>
                    {
                        detectTID();

                    });
                    SetTimeout(actionReRead, 100);
                }
                
            }
            catch (Exception e3)
            {
                FunctionGeneral.writeFile(@"D:\VimassTIDLog.txt",  "__TID__Die" + e3);

            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            String strPort = comboBox1.Text;
            SerialPort port2 = new SerialPort(strPort, 9600, Parity.None, 8, StopBits.One);
            port2.Close();
            FunctionGeneral.dem = 1;
            
           /* this.Close();
            new FormCheck().Show();*/

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                FunctionGeneral.port = comboBox1.Text;
                SerialPort port2 = new SerialPort(FunctionGeneral.port, 9600, Parity.None, 8, StopBits.One);
                port2.Open();
                string vUidFull = port2.ReadLine();
               
                    var action4 = new Action(() =>
                    {
                        button3_Click(sender,e);
                    });
                    SetTimeout(action4, 100);
                
                port2.Close() ;
                string vuIDFinal = vUidFull.Substring(1, vUidFull.Length - 2);
                if (textBox1.Text.IndexOf(vuIDFinal) < 0)
                {
                    textBox1.Text += FunctionGeneral.dem.ToString() + "            " + vuIDFinal + "\r\n";
                    FunctionGeneral.dem++;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FunctionGeneral.port = "COM100";
            SerialPort port2 = new SerialPort(FunctionGeneral.port, 9600, Parity.None, 8, StopBits.One);
      
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            FunctionGeneral.dem= 1;

        }
    }

}
