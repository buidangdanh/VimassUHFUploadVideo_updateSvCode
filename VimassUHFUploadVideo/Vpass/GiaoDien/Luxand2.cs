using Luxand;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Newtonsoft.Json;
using VimassUHFUploadVideo.Vpass.Object.ObjectThietBi;
using VimassUHFUploadVideo.Vpass.Object.ObjectVanTay;
using VimassUHFUploadVideo.Vpass.Object;
using VimassUHFUploadVideo.Vpass.Object.ObjectNhanDienKhuonMat;
using VimassUHFUploadVideo.Ultil;
using System.Security.Cryptography;
using sun.security.krb5.@internal.crypto.dk;
using java.rmi.server;

namespace VimassUHFUploadVideo.Vpass.GiaoDien
{
    public partial class Luxand2 : UserControl
    {
        bool needClose = false;
        bool mocam = true;
        string cameraName;
        FSDK.TFacePosition facePosition_Global;
        bool isLiveness = false;
        FSDK.CImage image_Global;
        int soLanXacThuc = 0;
        Font LargeFont = new Font("Arial", 20);
        public static Dictionary<byte[], InfomationVID> templateFaceCloudsMap = new Dictionary<byte[], InfomationVID>();
        public Luxand2()
        {
            InitializeComponent();
            // Đặt AutoSize cho Form


        }
        private void Luxand2_Load(object sender, EventArgs e)
        {
            try
            {
                GoiLayKhuonMat();
                initCamera("danh");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Luxand2_LoadException: " + ex.Message);
            }

        }

        private void GoiLayKhuonMat()
        {
            try
            {
                ObjectGoiDichVuMini o = new ObjectGoiDichVuMini();
                o.funcId = 1060012;
                o.device = 2;
                o.currentime = FunCGeneral.timeNow();

                LayDanhSachKhuonMatRequest oData = new LayDanhSachKhuonMatRequest();
                oData.idQRgreat = "0966074236";
                oData.offset = 0;
                oData.limit = 1;
                oData.textSearch = "";

                o.data = JsonConvert.SerializeObject(oData);

                //String url = FunCGeneral.ipMayChuDonVi;
                String url = "http://192.168.1.45:58080/autobank/services/vimassTool/dieuPhoi";
                var json = JsonConvert.SerializeObject(o);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);
                System.Diagnostics.Debug.WriteLine("V reques" + response.ToString()); ;
                if (response != null && response.msgCode == 1)
                {
                    try
                    {
                        // Giải mã kết quả từ JSON response
                        ResultResponeMini value = JsonConvert.DeserializeObject<ResultResponeMini>(response.result.ToString());
                        string valueTraVe = FunctionGeneral.DecodeBase64String(value.value);

                        // Chuyển đổi chuỗi JSON đã giải mã thành danh sách
                        List<InfomationVID> listVanTay = JsonConvert.DeserializeObject<List<InfomationVID>>(valueTraVe);
                        Debug.WriteLine($"Vui lòng đến gần camera, số lượng: {listVanTay.Count}");

                        foreach (InfomationVID arr in listVanTay)
                        {
                            if (arr.idVid.Equals("3050002")) {
                                try
                                {
                           
                                    string data = arr.uID + "w4aAw69vaF*GKmnQ";
                                    String key = FunctionGeneral.Md5(data).ToLower();
                                    // Giải mã Base64
                            
                                    // Chuyển byte[] sang sbyte[]
                                    String a= AesDecrypt(arr.faceData, key);
                                    Debug.WriteLine(arr.face);
                                    sbyte[] danh = ConvertToSByteArray(arr.face);

                                    // Thêm dữ liệu vào map
                                    byte[] unsigned = (byte[])(Array)danh;
                                    templateFaceCloudsMap.Add(unsigned, arr);
                                }
                                catch (FormatException ex)
                                {
                                    Debug.WriteLine($"Invalid Base64 format for faceData: {arr.faceData}, Error: {ex.Message}");
                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine($"Exception while processing faceData: {ex.Message}");
                                }
                            }
                            
                        }
                    }
                    catch (JsonSerializationException ex)
                    {
                        Debug.WriteLine($"JSON Deserialization Exception: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"General Exception: {ex.Message}");
                    }
                }
                else
                {
                    Debug.WriteLine("Lỗi gọi dịch vụ");
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine("GoiLayKhuonMatException: " + ex.Message);
            }
        }
        public static sbyte[] ConvertToSByteArray(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Chuỗi đầu vào không hợp lệ.");
            }

            try
            {
                // Tách chuỗi bằng dấu chấm phẩy
                string[] stringArray = input.Split(';');

                // Chuyển đổi từng phần tử thành sbyte
                sbyte[] sbyteArray = stringArray.Select(value => Convert.ToSByte(value)).ToArray();

                return sbyteArray;
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Lỗi định dạng: {ex.Message}");
                return null;
            }
            catch (OverflowException ex)
            {
                Console.WriteLine($"Lỗi tràn số: {ex.Message}");
                return null;
            }
        }
        public static sbyte[] TextToSByteArray(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            // Chuyển chuỗi sang mảng byte
            byte[] byteArray = Encoding.UTF8.GetBytes(text);

            // Chuyển mảng byte sang mảng sbyte
            sbyte[] sbyteArray = new sbyte[byteArray.Length];
            for (int i = 0; i < byteArray.Length; i++)
            {
                sbyteArray[i] = unchecked((sbyte)byteArray[i]);
            }

            return sbyteArray;
        }
        public static string AesDecrypt(string encryptedText, string key)
        {
            if (string.IsNullOrEmpty(encryptedText) || string.IsNullOrEmpty(key))
            {
                return null;
            }

            try
            {
                // Tạo đối tượng AES
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.Mode = CipherMode.ECB;
                    aes.Padding = PaddingMode.Zeros;

                    // Giải mã
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, null);
                    byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                    byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                return null;
            }
        }


        public static string Decrypt3(string toDecrypt, string key)
        {
            bool useHashing = true;
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.Zeros;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        public static sbyte[] ConvertStringToSbyteArray(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Input string cannot be null or empty.");
            }

            // Chuyển chuỗi thành mảng byte
            byte[] byteArray = Encoding.UTF8.GetBytes(input);

            // Tạo mảng sbyte có cùng kích thước
            sbyte[] sbyteArray = new sbyte[byteArray.Length];

            // Chuyển từng phần tử từ byte sang sbyte
            for (int i = 0; i < byteArray.Length; i++)
            {
                sbyteArray[i] = unchecked((sbyte)byteArray[i]); // Cast byte sang sbyte
            }

            return sbyteArray;
        }
        public static string AESDecrypt2(byte[] encryptedBytes, string key)
        {
            if (encryptedBytes == null || key == null)
            {
                return null;
            }

            try
            {
                // Tạo đối tượng AES
                using (var aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.Mode = CipherMode.ECB;
                    aes.Padding = PaddingMode.PKCS7;

                    // Tạo decryptor
                    using (var decryptor = aes.CreateDecryptor())
                    {
                        // Giải mã dữ liệu
                        byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                        // Chuyển byte[] thành string
                        return Encoding.UTF8.GetString(decryptedBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong quá trình giải mã: {ex.Message}");
                return null;
            }
        }
        public static sbyte[] GiaiMa(string base64Data, string uID)
        {
            if (string.IsNullOrEmpty(base64Data) || string.IsNullOrEmpty(uID))
            {
                throw new ArgumentException("Base64 data hoặc uID không được để trống.");
            }

            try
            {
                // Tạo khóa từ uID và chuỗi cố định
                string data = uID + "w4aAw69vaF*GKmQ";
                byte[] key = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(data));

                // Chuyển đổi Base64 thành mảng byte
                byte[] audioData = Convert.FromBase64String(base64Data);

                // Kiểm tra kích thước dữ liệu (phải là bội số của block size 16)
                if (audioData.Length % 16 != 0)
                {
                    Console.WriteLine("Dữ liệu đầu vào không hợp lệ. Đang kiểm tra và tự động thêm padding...");
                    int paddingSize = 16 - (audioData.Length % 16);
                    Array.Resize(ref audioData, audioData.Length + paddingSize); // Thêm padding vào cuối
                }

                // Tạo đối tượng AES để giải mã
                using (var aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.Mode = CipherMode.ECB;
                    aes.Padding = PaddingMode.None; // Chế độ padding mặc định

                    using (var decryptor = aes.CreateDecryptor())
                    {
                        // Giải mã dữ liệu
                        byte[] decryptedData = decryptor.TransformFinalBlock(audioData, 0, audioData.Length);

                        // Chuyển mảng byte thành sbyte
                        sbyte[] decryptedSByteData = Array.ConvertAll(decryptedData, b => unchecked((sbyte)b));

                        // Trả về kết quả
                        return decryptedSByteData;
                    }
                }
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine($"Lỗi giải mã: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khác: {ex.Message}");
                throw;
            }
        }


        /// <summary>
        /// Chuyển đổi mảng sbyte[] thành mảng byte[]
        /// </summary>
        /// <param name="sbyteArray">Mảng sbyte[]</param>
        /// <returns>Mảng byte[]</returns>
        public static byte[] ConvertSbyteArrayToByteArray(sbyte[] sbyteArray)
        {
            if (sbyteArray == null)
                throw new ArgumentNullException(nameof(sbyteArray));

            // Chuyển đổi từng phần tử sbyte thành byte
            byte[] result = new byte[sbyteArray.Length];
            for (int i = 0; i < sbyteArray.Length; i++)
            {
                // Chuyển đổi giá trị âm về dạng byte (0–255)
                result[i] = unchecked((byte)sbyteArray[i]);
            }
            return result;
        }

   

        public void initCamera(string s)
        {
            try
            {
                if (FSDK.FSDKE_OK != FSDK.ActivateLibrary("hI5B8NDetxQvB5/1K7/FyQfpceyug3W+ZcnSZo+UfT1jlgD06dZti+HBb5/" +
                "yBCwzD5fgvvIl9yyS8GheKn6+ly5q8clHz4iObbL2hzg8HjxI5jSlpToQZ4pt17rK6/" +
                "LPV1Zbiwp48m1+qJnl8hwTs+E9rLztYBORDcJ9rYduLz0="))
                {
                    MessageBox.Show("License Key Hết Hạn", "Lỗi rồi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
                FSDK.InitializeLibrary();
                FSDKCam.InitializeCapturing();
                string[] cameraList;
                int count;
                FSDKCam.GetCameraList(out cameraList, out count);

                if (0 == count)
                {
                    MessageBox.Show("Vui lòng cho phép sử dụng camera", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
                Debug.WriteLine("Danh sach camera" + cameraList.Length);
                foreach (string text in cameraList)
                {
                    Debug.WriteLine("Danh sach Camera: " + text);
                }
                cameraName = cameraList[0]; //Code lấy camera

                FSDKCam.VideoFormatInfo[] formatList;
                FSDKCam.GetVideoFormatList(ref cameraName, out formatList, out count); //lấy danh sách camera, không hỗ trợ cam IP
                int VideoFormat = 0; // Chọn một định dạng video

                pictureBox1.Width = formatList[VideoFormat].Width;
                pictureBox1.Height = formatList[VideoFormat].Height;

                // Đặt Dock cho pictureBox1 và flowLayoutPanel1

                flowLayoutPanel1.Dock = DockStyle.Fill; // Lấp đầy Luxand2
                Debug.WriteLine($"flowLayoutPanel1 initial size: Width = {flowLayoutPanel1.Width}, Height = {flowLayoutPanel1.Height}");
                // Thêm pictureBox1 vào flowLayoutPanel1
                flowLayoutPanel1.Controls.Clear();
                flowLayoutPanel1.Controls.Add(pictureBox1);
                Debug.WriteLine($"Luxand2 initial size: Width = {this.Width}, Height = {this.Height}");

                taoThreadMoi(s);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("initCameraException " + ex.Message);
            }
        }
        Thread thread;
        public void taoThreadMoi(string s)
        {
            try
            {
                thread = new Thread(delegate ()
                {
                    khoiDongCamera(s);
                });
                thread.Start();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("taoThreadMoiException" + ex.Message);
            }
        }
        public void khoiDongCamera(string s)
        {
            try
            {
                int cameraHandle = 0;

                int r = FSDKCam.OpenVideoCamera(ref cameraName, ref cameraHandle);
                Debug.WriteLine($"r:" + r);
                if (r != FSDK.FSDKE_OK)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        MessageBox.Show("Lỗi mở camera thứ 1", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                    Application.Exit();
                }

                int tracker = 0;    // creating a Tracker
                FSDK.CreateTracker(ref tracker); // if could not be loaded, create a new tracker

                int err = 0; // set realtime face detection parameters
                FSDK.SetTrackerMultipleParameters(tracker, "HandleArbitraryRotations=false; DetermineFaceRotationAngle=false; InternalResizeWidth=100; FaceDetectionThreshold=5;", ref err);
                FSDK.SetTrackerParameter(tracker, "DetectLiveness", "true"); // enable liveness
                FSDK.SetTrackerParameter(tracker, "SmoothAttributeLiveness", "true"); // use smooth minimum function for liveness values
                FSDK.SetTrackerParameter(tracker, "AttributeLivenessSmoothingAlpha", "1"); // smooth minimum parameter, 0 -> mean, inf -> min
                FSDK.SetTrackerParameter(tracker, "LivenessFramesCount", "15"); // minimal number of frames required to output liveness attribute

                while (!needClose)
                {
                    Int32 imageHandle = 0;
                    if (FSDK.FSDKE_OK != FSDKCam.GrabFrame(cameraHandle, ref imageHandle)) // grab the current frame from the camera
                    {
                        Application.DoEvents();
                        continue;
                    }
                    image_Global = new FSDK.CImage(imageHandle);
                    long[] IDs;
                    long faceCount = 0;
                    FSDK.FeedFrame(tracker, 0, image_Global.ImageHandle, ref faceCount, out IDs, sizeof(long) * 1); // maximum of 256 faces detected
                    Array.Resize(ref IDs, (int)faceCount);

                    // make UI controls accessible (to find if the user clicked on a face)
                    Application.DoEvents();
                    Image frameImage = image_Global.ToCLRImage();
                    Graphics gr = Graphics.FromImage(frameImage);
                    if (s.Equals("1"))
                    {
                        Debug.WriteLine("danhhhh");
                        var action = new Action(() =>
                        {
                            this.Invoke(new MethodInvoker(delegate
                            {
                                needClose = true;
                            }));
                        });
                        FunctionGeneral.SetTimeout(action, 2000);
                    }

                    for (int i = 0; i < IDs.Length; ++i)
                    {
                        FSDK.TFacePosition facePosition = new FSDK.TFacePosition();
                        FSDK.GetTrackerFacePosition(tracker, 0, IDs[i], ref facePosition);

                        int left = facePosition.xc - (int)(facePosition.w * 0.6);
                        int top = facePosition.yc - (int)(facePosition.w * 0.5);
                        int w = (int)(facePosition.w * 1.2);

                        String statusText;
                        StringFormat format = new StringFormat();
                        format.Alignment = StringAlignment.Center;
                        Brush brush;
                        Pen pen;
                        string value;
                        float liveness = 0;

                        int res = FSDK.GetTrackerFacialAttribute(tracker, cameraHandle, IDs[i], "Liveness", out value, 1024);
                        if (res == FSDK.FSDKE_OK)
                        {
                            res = FSDK.GetValueConfidence(value, "Liveness", ref liveness);
                        }

                        if (res != FSDK.FSDKE_OK)
                        {
                            pen = Pens.LightGreen;
                            brush = new System.Drawing.SolidBrush(System.Drawing.Color.LightGreen);
                            statusText = "";
                            /* gr.DrawString(statusText, new System.Drawing.Font("Arial", 16),
                             brush, facePosition.xc, top + w + 5, format);
                             gr.DrawRectangle(pen, left, top, w, w);*/
                        }
                        else if (liveness > 0.5f)
                        {
                            isLiveness = true;
                            pen = Pens.LightGreen;
                            brush = new System.Drawing.SolidBrush(System.Drawing.Color.LightGreen);
                            statusText = "";
                            statusText = xacThuc(ref facePosition);

                        }
                        else
                        {
                            pen = Pens.Red;
                            brush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
                            statusText = "\"Đây không phải người thật\"";
                        }
                        //Thread.Sleep(150);

                        gr.DrawRectangle(pen, left, top, w, w);
                        /* gr.DrawString(statusText, new System.Drawing.Font("Arial", 16),
                        brush, facePosition.xc, top + w + 5, format);*/

                    }
                    // display current frame
                    pictureBox1.Image = frameImage;
                    GC.Collect(); // collect the garbage after the deletion
                }

                FSDK.FreeTracker(tracker);
                FSDKCam.CloseVideoCamera(cameraHandle);
                FSDKCam.FinalizeCapturing();
                this.Invoke(new MethodInvoker(delegate
                {
                    System.Windows.Forms.Application.Exit();
                }));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("khoiDongCameraException: " + ex.Message);
            }

        }
        private string xacThuc(ref FSDK.TFacePosition FacePosition)
        {
            string kq = "";
            int res2 = FSDK.DetectFace(image_Global.ImageHandle, ref facePosition_Global);
            if (res2 == FSDK.FSDKE_FACE_NOT_FOUND)
            {
                Debug.WriteLine("Khong tim thay khuon mat");
            }
            else if (res2 == FSDK.FSDKE_IMAGE_TOO_SMALL)
            {
                Debug.WriteLine("Vui long den gan camera");
            }
            else if (res2 == FSDK.FSDKE_OK)
            {
                if (isLiveness)
                {
                 
                    soLanXacThuc++;
                    byte[] template_Global = new byte[FSDK.TemplateSize];
                    FSDK.GetFaceTemplate(image_Global.ImageHandle, out template_Global);
                    //FSDK.GetFaceTemplateInRegion(image_Global.ImageHandle, ref facePosition_Global, out template_Global);

                    float matchingThreshold = 0;
                    float similarity = (float)0.5;
                    FSDK.GetMatchingThresholdAtFAR((float)0.005, ref matchingThreshold);
            
                    foreach (KeyValuePair<byte[], InfomationVID> templateFaceCloudMap in templateFaceCloudsMap)
                    {
                        Debug.WriteLine("Vui long den gan camera");
                        byte[] templateFaceCloud2 = templateFaceCloudMap.Key;
                        FSDK.MatchFaces(ref template_Global, ref templateFaceCloud2, ref similarity);
             
                        if (similarity > matchingThreshold)
                        {
                            Debug.WriteLine("Thành công "+templateFaceCloudMap.Value.idVid);
                   



                            DateTime utcDateTime = DateTime.UtcNow;
                            string vnTimeZoneKey = "SE Asia Standard Time";
                            TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
                            DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
                            long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();
                            string timenow = ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
                            string timenow2 = ngaygiohientai.ToString("HHmmss");
                            this.Invoke(new MethodInvoker(delegate
                            {

                            }));




                            Thread.Sleep(2000);


                            try
                            {
                                var action = new Action(() =>
                                {
                                    this.Invoke(new MethodInvoker(delegate
                                    {

                                    }));
                                });
                                FunctionGeneral.SetTimeout(action, 10);

                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.Message);
                            }


                        }
                        else
                        {
                            DateTime utcDateTime = DateTime.UtcNow;
                            string vnTimeZoneKey = "SE Asia Standard Time";
                            TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
                            DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
                            long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();
                            string timenow = ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");

                            Debug.WriteLine("Xác thực không thành công: ");

                            Thread.Sleep(500);
                            this.Invoke(new MethodInvoker(delegate
                            {


                            }));
                            var action = new Action(() =>
                            {
                                this.Invoke(new MethodInvoker(delegate
                                {


                                }));
                            });
                            FunctionGeneral.SetTimeout(action, 10000);
                        }
                    }
                }
            }
            return kq;

        }


    }
}
