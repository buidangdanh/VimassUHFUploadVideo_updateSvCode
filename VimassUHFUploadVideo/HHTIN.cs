using AForge.Video;
using AForge.Video.DirectShow;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VimassUHFUploadVideo.Ultil;

namespace VimassUHFUploadVideo
{
    public partial class HHTIN : Form
    {
        static SerialPort port2 = new SerialPort(FunctionGeneral.COMVaoHHT, 9600, Parity.None, 8, StopBits.One);
        private VideoCaptureDevice videoCapture;
        FilterInfoCollection filterInfo;
        private void startCamera()
        {
            try
            {
                filterInfo = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                videoCapture = new VideoCaptureDevice(filterInfo[FunctionGeneral.CamIN].MonikerString);
                videoCapture.NewFrame += new NewFrameEventHandler(Camera_On);
                videoCapture.Start();
            }
            catch (Exception e)
            {

            }
        }
        private void Camera_On(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();

            }
            catch (Exception e9)
            {
                pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
            }

        }
        public HHTIN()
        {
            InitializeComponent();
        }
        static DateTime utcDateTime = DateTime.UtcNow;
        static string vnTimeZoneKey = "SE Asia Standard Time";
        static TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
        static DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
        long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();
        string timenow = ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
        private static long getTime()
        {
            DateTime utcDateTime = DateTime.UtcNow;
            string vnTimeZoneKey = "SE Asia Standard Time";
            TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
            DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
            long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();
            return yourDateTimeMilliseconds;
        }
      
        public static string readingVuid()
        {
            try
            {
                string vuid = port2.ReadLine();
                //port2.Close();
                return vuid;

            }
            catch (Exception e4)
            {
                return "";
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
        /*        private string capture()
                {
                    try
                    {
                        DateTime utcDateTime = DateTime.UtcNow;
                        string vnTimeZoneKey = "SE Asia Standard Time";
                        TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
                        DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
                        string timenow2 = ngaygiohientai.ToString("HHmmss");

                        string fileName = @"D:\OUT" + timenow2 + ".jpg";
                        var bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                        pictureBox1.DrawToBitmap(bitmap, pictureBox1.ClientRectangle);

                        System.Drawing.Imaging.ImageFormat imageFomat = null;
                        imageFomat = System.Drawing.Imaging.ImageFormat.Jpeg;
                        bitmap.Save(fileName, imageFomat);

                        Ulti.writeFile(@"D:\VimassUHFLog.txt", timenow + " Chup anh Out Thanh cong " + fileName);
                        return fileName;

                    }
                    catch (Exception e11)
                    {
                        return "";

                    }
                }*/
        public static string Md5(string input, bool isLowercase = false)
        {
            using (var md5 = MD5.Create())
            {
                var byteHash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                var hash = BitConverter.ToString(byteHash).Replace("-", "");
                return (isLowercase) ? hash.ToLower() : hash;
            }
        }
        public static void goidichvu(string s, long time, string idthe)
        {
            try
            {
                FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", "_HHTIN_Nhayvaohamgoidichvu_");
                DateTime utcDateTime = DateTime.UtcNow;
                string vnTimeZoneKey = "SE Asia Standard Time";
                TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
                DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
                long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();
                string t = yourDateTimeMilliseconds.ToString();
                SendNotification u = new SendNotification();

                string url = "http://210.245.8.7:12318/vimass/services/VUHF/sendNotification";
                u.idThietBi = "C18D2111112A5A";
                u.uCodeID = idthe;
                u.timeInit = yourDateTimeMilliseconds;
                u.mcID = "VM_TEST";
                u.cks = Md5("SafFPMPKCjauZ%Ma" + "VM_TEST" + yourDateTimeMilliseconds + "C18D2111112A5A" + u.uCodeID).ToLower();
                var json = JsonConvert.SerializeObject(u);
                String res = Service.SendWebrequest_POST_Method(json, url);
                History u5 = new History();
                string url5 = "http://210.245.8.7:12318/vimass/services/VUHF/danhDauXacThuc";
                Response response = JsonConvert.DeserializeObject<Response>(res);
                if (response.msgCode == 1)
                {
                    FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", "_HHTIN_Nhayvaohamgoidichvu_1");
                    GetDataCloud push2 = JsonConvert.DeserializeObject<GetDataCloud>(response.result.ToString());
                    FunctionGeneral.KeyName = push2.accName;
                    FunctionGeneral.KeyViD = push2.vID;
                    //History u5 = new History();
                    u5.imgBase64 = s;
                    u5.uID = "";
                    u5.uCodeID = idthe;
                    u5.thoiGianGhiNhan = yourDateTimeMilliseconds;
                    u5.deviceID = 3;
                    u5.idThietBi = FunctionGeneral.idThietBiHHTCongVao;
                    u5.typeDataAuThen = 3;
                    u5.sdt = "0353465132";
                    u5.idthe = idthe;

                    u5.cks = Md5("77a+6Mx2&Ata8mBE" + u5.deviceID + u5.thoiGianGhiNhan + "" + u5.typeDataAuThen).ToLower();
                    // var json7 = JsonConvert.SerializeObject(u5);
                    // String res7 = Service.SendWebrequest_POST_Method(json7, url5);
                    // Ulti.writeFile(@"D:\VimassUHFLog.txt", " Upload Anh: " + res7);
                }
                else if (response.msgCode == 2)
                {
                    FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", "_HHTIN_Nhayvaohamgoidichvu_2");
                    u5.imgBase64 = s;
                    FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", " _HHTIN_3");
                    u5.uID = "";
                    FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", " _HHTIN_4");
                    u5.uCodeID = idthe;
                    FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", "_HHTIN_5");
                    u5.thoiGianGhiNhan = yourDateTimeMilliseconds;
                    FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", "_HHTIN_6");
                    u5.deviceID = 3;
                    FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", "_HHTIN_7");
                    u5.idThietBi = FunctionGeneral.idThietBiHHTCongVao;
                    FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", "_HHTIN_8");
                    u5.typeDataAuThen = 0;
                    FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", "_HHTIN_9");
                    u5.sdt = "0353465132";
                    FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", "_HHTIN_10");
                    u5.idthe = idthe;
                    FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", "_HHTIN_11");
                    u5.name = FunctionGeneral.KeyName;
                    FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", "_HHTIN_12");
                    u5.sothe = FunctionGeneral.KeyViD;
                    FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", "_HHTIN_13");
                    u5.cks = Md5("77a+6Mx2&Ata8mBE" + u5.deviceID + u5.thoiGianGhiNhan + "" + u5.typeDataAuThen).ToLower();
                    FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", "_HHTIN_14");

                }
                var json7 = JsonConvert.SerializeObject(u5);
                FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", "_TKCR_15");
                String res7 = Service.SendWebrequest_POST_Method(json7, url5);
                FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", "_HHTIN_Nhayvaohamgoidichvu_3" + res7);

            }
            catch (Exception ex)
            {
                FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", "_HHTIN_UploadAnhloi: " + ex.Message);
            }
        }
      
        private void detectID()
        {
            try
            {
                port2.Open();
                // string vuid = readingVuid();
                // FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", timenow + "_HHTIN_" + vuid);
                string str4 = "";
                if (true)
                {
                    var action = new Action(() =>
                    {
                        string vUidFull = port2.ReadExisting();
                        FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", timenow + "_HHTIN_2_" + vUidFull);
                        if (vUidFull.Length < 5)
                        {
                            var action4 = new Action(() =>
                            {
                                detectID();
                            });
                            SetTimeout(action4, 2000);
                        }
                        port2.Close();
                        string[] ListID = vUidFull.Split('\n');
                        FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", timenow + "_HHTIN_3_" + ListID);
                        foreach (string text in ListID)
                        {
                            if (str4.IndexOf(text) < 0)
                            {
                                str4 += text;
                            }
                        }
                        string[] ListIDTrung = str4.Split('');
                        FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", timenow + "_HHTIN_4_" + ListIDTrung);
                        foreach (string vuIDIn in ListIDTrung)
                        {
                            FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", timenow + "_HHTIN_5_" + vuIDIn);
                            if (vuIDIn.Length > 5)
                            {
                                DateTime utcDateTime = DateTime.UtcNow;
                                string vnTimeZoneKey = "SE Asia Standard Time";
                                TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
                                DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);

                                // string vuIDFinal = vuIDIn.Substring(0,4);
                                //  string vuIDFinal = vuIDIn.Substring(4, vuIDIn.Length-5);
                                string vuIDFinal = vuIDIn.Substring(1, vuIDIn.Length - 2);
                                //string idthietbi = GetHex(vuIDIn.Substring(0, 4));
                                // FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", timenow + " ID THIET BI 2" + idthietbi);
                                string linkCaptureIn = capture();
                                //FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", timenow + " liNKanh In: " + linkCaptureIn);
                                //Thread.Sleep(2000);
                                var actionUploadImg = new Action(() =>
                                {
                                    string ImgBase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(linkCaptureIn));
                                    //FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", timenow + " IMGBase64 In: " + ImgBase64);
                                    goidichvu(ImgBase64, 1000, vuIDFinal);
                                    FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", timenow + "_HHTIN_6_" + vuIDFinal + "Goidichvuthanhcong");

                                });
                                SetTimeout(actionUploadImg, 500);
                                var actionReRead = new Action(() =>
                                {
                                    detectID();

                                });
                                SetTimeout(actionReRead, 4000);
                                label2.Text = "Đã nhận ID: " + vuIDIn + "_" + ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
                            }
                        }

                    });
                    SetTimeout(action, 2000);
                    //   Debug.WriteLine(vuid);
                }
            }
            catch (Exception e3)
            {
                FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", timenow + "_HHTIN_Die Out ");

            }
        }
        private string capture()
        {
            try
            {
                DateTime utcDateTime = DateTime.UtcNow;
                string vnTimeZoneKey = "SE Asia Standard Time";
                TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
                DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
                string timenow2 = ngaygiohientai.ToString("ddMMyyyyHHmmss");

                string fileName = @"D:\INHHT" + timenow2 + ".jpg";
                var bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                pictureBox1.DrawToBitmap(bitmap, pictureBox1.ClientRectangle);

                System.Drawing.Imaging.ImageFormat imageFomat = null;
                imageFomat = System.Drawing.Imaging.ImageFormat.Jpeg;
                bitmap.Save(fileName, imageFomat);

                FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", timenow + " Chup anh in Thanh cong " + fileName);
                return fileName;

            }
            catch (Exception e)
            {
                return "";

            }
        }

        private void HHTIN_Load(object sender, EventArgs e)
        {
            label1.Text = "ID Thiết bị: " + FunctionGeneral.idThietBiHHTCongVao + "_Đầu vào: " + FunctionGeneral.COMVaoHHT;
            label2.Text = "";
            try
            {
                startCamera();
                FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", timenow + " Khoi dong camerain thanh cong");
                CanhBao();
            }
            catch (Exception e2)
            {
                FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", timenow + " Khoi dong camerain khong thanh cong");
            }
        }
        private void CanhBao()
        {
            try
            {
                CanhBao canhBao = new CanhBao();
                FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", "goidichvuCanhBao_1");
                string url = "http://210.245.8.7:12318/vimass/services/VUHF/capNhatTrangThaiHoatDong";
                canhBao.idThietBi = FunctionGeneral.idThietBiHHTCongVao;
                canhBao.maDichVu = 100;
                canhBao.curentTime = yourDateTimeMilliseconds;
                canhBao.cks = Md5("RAuJyCYRZfL$Ya5S" + canhBao.curentTime + canhBao.idThietBi + canhBao.maDichVu).ToLower();
                var json = JsonConvert.SerializeObject(canhBao);
                String res = Service.SendWebrequest_POST_Method(json, url);
                FunctionGeneral.writeFile(@"D:\VimassUHFLog.txt", "goidichvuCanhBao_2" + res);
                var actionReload = new Action(() =>
                {
                    CanhBao();
                });
                SetTimeout(actionReload, 60000);

            }
            catch (Exception e)
            {

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            detectID();
        }
    }
}
