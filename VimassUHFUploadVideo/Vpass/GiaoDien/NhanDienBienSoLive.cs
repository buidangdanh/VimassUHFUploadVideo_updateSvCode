using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using System.Web.Helpers;
using Vlc.DotNet.Forms;
using Gst.Rtsp;
using FFmpeg.AutoGen;
using System.IO;
using Vlc.DotNet.Core;
using LibVLCSharp.WinForms;
using LibVLCSharp.Shared;
using System.Drawing.Imaging;
using System.Net.Http;
using VimassUHFUploadVideo.Vpass.Object.ObjectNhanDienBienSo;
using Newtonsoft.Json;
using VimassUHFUploadVideo.Ultil;
using static javax.swing.SwingWorker;
using System.Diagnostics;
using System.IO.Ports;

namespace VimassUHFUploadVideo.Vpass.GiaoDien
{
    public partial class NhanDienBienSoLive : Form
    {
        private static readonly string apiKey = NhanDienBienSo.dangNhapCamera.key; // Thay bằng API key của bạn
        private LibVLC _libVLC;
        private MediaPlayer _mediaPlayer;
        public NhanDienBienSoLive()
        {
            InitializeComponent();

            string libVlcPath = @"C:\Program Files\VideoLAN\VLC"; // Đường dẫn tới thư mục chứa libvlc.dll
            Core.Initialize(libVlcPath); // Khởi tạo với đường dẫn này

        }
        private SerialPort serialPort;
        public static Boolean chupAnhDi = true;
        private void InitializeSerialPort()
        {
            // Kiểm tra và khởi tạo đối tượng SerialPort
            if (serialPort == null)
            {
                serialPort = new SerialPort
                {
                    PortName = "COM" + textBox1.Text.Trim(), // Thay "COM1" bằng cổng chính xác của bạn
                    BaudRate = 115200, // Tốc độ baud của TFLuna
                    DataBits = 8,
                    Parity = Parity.None,
                    StopBits = StopBits.One,
                    ReadTimeout = 1000, // Timeout khi đọc dữ liệu
                    WriteTimeout = 1000
                };

                serialPort.DataReceived += SerialPort_DataReceived;
            }
        }
        // Buffer tích lũy cho parser
        private const int FRAME_LEN = 9;
        private const byte HEAD = 0x59;
        private readonly byte[] _rx = new byte[1024];
        private int _rxLen = 0;

        // Lọc median (cửa sổ 5)
        private readonly Queue<ushort> _medianWin = new Queue<ushort>(5);

        // Debounce/hysteresis
        private DateTime _lastTrigger = DateTime.MinValue;
        private TimeSpan _debounce = TimeSpan.FromSeconds(2); // không chụp liên tiếp quá nhanh
        private bool _belowLatch = false; // latch khi dưới ngưỡng cho đến khi quay lại trên ngưỡng + hysteresis

        // Ngưỡng cường độ tín hiệu (tùy chỉnh)
        private const int STRENGTH_MIN = 50; // ví dụ: bỏ khung quá yếu
        private bool wasBelow20 = false;  // Biến trạng thái
        private bool isProcessing = false; // Biến kiểm tra xem có đang xử lý dữ liệu không
        private ushort lastDistance = 0;  // Biến lưu khoảng cách lần trước
        private int i = 0;  // Biến lưu khoảng cách lần trước

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                byte[] data = new byte[9]; // Dữ liệu trả về từ TFLuna có 9 byte
                serialPort.Read(data, 0, 9);

                // Kiểm tra độ dài dữ liệu hợp lệ
                if (data.Length == 9)
                {
                    // Giải mã dữ liệu
                    ushort distance = (ushort)(data[2] + data[3] * 256); // Khoảng cách
                    ushort strength = (ushort)(data[4] + data[5] * 256); // Cường độ tín hiệu

                    // Hiển thị kết quả lên giao diện người dùng
                    Invoke(new Action(() =>
                    {
                        label7.Text = $"Distance: {distance / 100.0} m";  // Hiển thị khoảng cách
                        ushort distancex = ushort.Parse(textBox2.Text);
                        // Kiểm tra xem khoảng cách có thay đổi và cần xử lý không
                        if ( distance > 0 && distance < distancex )
                        {
                            i = 0;
                            if (!wasBelow20 && !isProcessing)  // Chỉ khi chưa xử lý và khoảng cách < 20
                            {
                                isProcessing = true; // Đánh dấu là đang xử lý
                                lastDistance = distance;  // Cập nhật khoảng cách lần trước
                                chupAnh();
                                wasBelow20 = true;  // Đánh dấu đã chụp ảnh khi khoảng cách dưới 20m
                            }
                            // Nếu khoảng cách vẫn dưới 20m nhưng đã xử lý rồi thì không làm gì
                        }
                        else if (distance < 810 && distance > distancex  )
                        {

                            // Khi khoảng cách lớn hơn 20m, reset lại trạng thái và sẵn sàng cho lần tiếp theo
                            wasBelow20 = false;
                            isProcessing = false;  // Đánh dấu là không còn đang xử lý



                        }
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading data: {ex.Message}");
            }
        }
        private void InitializeSerialPort2()
        {
            if (serialPort == null)
            {
                serialPort = new SerialPort
                {
                    // Cho phép người dùng nhập "COM3" hoặc chỉ "3"
                    PortName = textBox1.Text.Trim().StartsWith("COM", StringComparison.OrdinalIgnoreCase)
                        ? textBox1.Text.Trim()
                        : "COM" + textBox1.Text.Trim(),
                    BaudRate = 115200,
                    DataBits = 8,
                    Parity = Parity.None,
                    StopBits = StopBits.One,
                    ReadTimeout = 200,   // ngắn để vòng lặp parser không bị treo
                    WriteTimeout = 1000,
                    ReceivedBytesThreshold = 1
                };

                serialPort.DataReceived += SerialPort_DataReceived;
                serialPort.Open();
            }
        }

        private void SerialPort_DataReceived2(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                // Đọc hết những gì đang có vào buffer tích lũy
                int n = serialPort.BytesToRead;
                if (n <= 0) return;
                int canCopy = Math.Min(n, _rx.Length - _rxLen);
                int read = serialPort.Read(_rx, _rxLen, canCopy);
                _rxLen += read;

                // Cố gắng tách các khung hợp lệ trong buffer
                int idx = 0;
                while (_rxLen - idx >= FRAME_LEN)
                {
                    // Tìm header 0x59 0x59
                    if (!(_rx[idx] == HEAD && _rx[idx + 1] == HEAD))
                    {
                        idx++; // trượt 1 byte để tìm lại header
                        continue;
                    }

                    // Đã có đủ 9 byte từ idx?
                    if (_rxLen - idx < FRAME_LEN)
                        break;

                    // Copy một frame
                    byte[] frame = new byte[FRAME_LEN];
                    Buffer.BlockCopy(_rx, idx, frame, 0, FRAME_LEN);

                    // Kiểm tra checksum
                    int sum = 0;
                    for (int i = 0; i < 8; i++) sum += frame[i];
                    byte checksum = (byte)(sum & 0xFF);
                    if (checksum != frame[8])
                    {
                        // Header giả/không khớp checksum → bỏ 1 byte rồi tiếp
                        idx++;
                        continue;
                    }

                    // Parse dữ liệu
                    ushort dist = (ushort)(frame[2] | (frame[3] << 8));           // cm
                    ushort strength = (ushort)(frame[4] | (frame[5] << 8));
                    // ushort tempRaw = (ushort)(frame[6] | (frame[7] << 8));     // nếu cần

                    // Lọc: bỏ khung quá yếu
                    if (strength >= STRENGTH_MIN && dist > 0 && dist < 1200) // ví dụ giới hạn 0–12m
                    {
                        ushort filtered = ApplyMedianFilter(dist);

                        // Cập nhật UI + logic trên UI thread
                        BeginInvoke(new Action(() =>
                        {
                            label7.Text = $"Distance: {filtered / 100.0:F2} m (raw {dist / 100.0:F2} m, str {strength})";

                            // Ngưỡng do người dùng nhập (cm)
                            ushort threshold = 0;
                            if (!ushort.TryParse(textBox2.Text, out threshold))
                                threshold = 2000; // fallback: 20m

                            // Hysteresis + debounce:
                            // - chỉ kích hoạt khi đi từ trên ngưỡng -> xuống dưới ngưỡng
                            // - sau khi kích hoạt, phải quay lại trên ngưỡng + biên hysteresis mới reset latch
                            int hysteresis = Math.Max(5, threshold / 20); // ~5% ngưỡng hoặc tối thiểu 5cm
                            DateTime now = DateTime.Now;

                            if (!_belowLatch && filtered < threshold) // cạnh xuống
                            {
                                // Debounce: tránh chụp liên tiếp khi nhiễu
                                if (now - _lastTrigger >= _debounce && !isProcessing)
                                {
                                    isProcessing = true;
                                    lastDistance = filtered;
                                    chupAnh(); // hành động của bạn
                                    _lastTrigger = now;
                                    wasBelow20 = true;
                                    isProcessing = false; // xong
                                    _belowLatch = true;   // khóa đến khi quay lại trên ngưỡng + hysteresis
                                }
                            }
                            else if (_belowLatch && filtered > threshold + hysteresis)
                            {
                                // reset latch khi đã rời vùng kích hoạt
                                _belowLatch = false;
                                wasBelow20 = false;
                            }
                        }));
                    }

                    // Tiêu thụ frame này
                    idx += FRAME_LEN;
                }

                // Dồn phần còn lại (chưa xử lý) về đầu buffer
                if (idx > 0)
                {
                    int remain = _rxLen - idx;
                    Buffer.BlockCopy(_rx, idx, _rx, 0, remain);
                    _rxLen = remain;
                }
            }
            catch (Exception ex)
            {
                BeginInvoke(new Action(() =>
                {
                    MessageBox.Show($"Error reading data: {ex.Message}");
                }));
            }
        }

        // Median filter cửa sổ 5
        private ushort ApplyMedianFilter(ushort value)
        {
            if (_medianWin.Count == 5) _medianWin.Dequeue();
            _medianWin.Enqueue(value);

            var arr = _medianWin.ToArray();
            Array.Sort(arr);
            // Trả về median
            return arr[arr.Length / 2];
        }





        //Nút bắt đầu
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                InitializeSerialPort();
                if (serialPort == null)
                {
                    MessageBox.Show("SerialPort is not initialized.");
                    return;
                }

                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                    button2.Enabled = false;
                    button3.Enabled = true;
                    MessageBox.Show("Sensor started.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening port: {ex.Message}");
            }
        }
        //Nút dừng
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort != null && serialPort.IsOpen)
                {
                    serialPort.Close();
                    button2.Enabled = true;
                    button3.Enabled = false;
                    MessageBox.Show("Sensor stopped.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error closing port: {ex.Message}");
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
            }
            base.OnFormClosing(e);
        }
        private void NhanDienBienSoLive_Load(object sender, EventArgs e)
        {
            try
            {
                label3.Text = "";
                label4.Text = "";
                label7.Text = "";
                button1.Hide();
                // Khởi tạo LibVLC và MediaPlayer
                _libVLC = new LibVLC();
                _mediaPlayer = new MediaPlayer(_libVLC);

                // Tạo một VideoView để hiển thị video
                VideoView videoView = new VideoView
                {
                    Dock = DockStyle.Fill,// Đảm bảo VideoView chiếm hết không gian của Panel
                };



                // Thêm VideoView vào Panel (Giả sử bạn có Panel tên là panel1 trên Form)
                panel1.Controls.Add(videoView);

                // Gán videoPlayer cho VideoView
                videoView1.MediaPlayer = _mediaPlayer;

                // Địa chỉ URL RTSP của camera
                var taiKhoan = "";
                var matKhau = "";
                var ip = "";
                if (!string.IsNullOrEmpty(NhanDienBienSo.dangNhapCamera.taiKhoan))
                {
                    taiKhoan = NhanDienBienSo.dangNhapCamera.taiKhoan.Trim();
                }

                if (!string.IsNullOrEmpty(NhanDienBienSo.dangNhapCamera.matKhau))
                {
                    matKhau = NhanDienBienSo.dangNhapCamera.matKhau.Trim();
                }

                if (!string.IsNullOrEmpty(NhanDienBienSo.dangNhapCamera.ip))
                {
                    ip = NhanDienBienSo.dangNhapCamera.ip.Trim();
                }

                string rtspUrl = "rtsp://" + taiKhoan + ":" + matKhau + "@" + ip + ":554/Streaming/Channels/101";

                // Tạo media từ URL RTSP
                Media media = new Media(_libVLC, rtspUrl, FromType.FromLocation);
                _mediaPlayer.Play(media);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("NhanDienBienSoLive_Exception" + ex.Message);

            }
        }
        // Hàm để chụp ảnh từ videoView1
        private void CaptureFrame(string filePath)
        {
            // Tạo một Bitmap với kích thước của video đang phát
            Bitmap bitmap = new Bitmap(videoView1.Width, videoView1.Height);

            // Lấy ảnh từ MediaPlayer và vẽ lên Bitmap
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // Lấy frame từ video và vẽ nó lên bitmap
                g.CopyFromScreen(videoView1.PointToScreen(Point.Empty), Point.Empty, videoView1.ClientSize);
            }

            // Kiểm tra xem thư mục lưu ảnh có tồn tại không, nếu không thì tạo mới
            string directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Lưu Bitmap dưới dạng PNG hoặc JPEG
            bitmap.Save(filePath, ImageFormat.Png); // Hoặc ImageFormat.Jpeg tùy ý

            MessageBox.Show($"Frame saved to {filePath}");
        }

        // Sự kiện để chụp ảnh khi nhấn một nút (ví dụ: buttonCapture)



        private void NhanDienBienSoLive_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Giải phóng tài nguyên khi form đóng
                _mediaPlayer.Dispose();
                _libVLC.Dispose();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("FormClosing_Exception: " + ex.Message);
            }
        }


        private static HttpClient CreateHttpClient(string ip, string username, string password)
        {
            try
            {
                // Tạo một handler cho HttpClient để cấu hình Digest Authentication
                var handler = new HttpClientHandler()
                {
                    Credentials = new NetworkCredential(username, password)
                };

                // Tạo HttpClient với handler được cấu hình
                HttpClient client = new HttpClient(handler);
                client.BaseAddress = new Uri("http://" + ip);
                return client;
            }
            catch (UriFormatException ex)
            {
                // Xử lý lỗi nếu địa chỉ URL không hợp lệ
                MessageBox.Show($"Địa chỉ IP không hợp lệ: {ex.Message}", "Lỗi URL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (ArgumentException ex)
            {
                // Xử lý lỗi nếu tên người dùng hoặc mật khẩu không hợp lệ
                MessageBox.Show($"Thông tin xác thực không hợp lệ: {ex.Message}", "Lỗi Xác Thực", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (Exception ex)
            {
                // Xử lý tất cả các lỗi khác
                MessageBox.Show($"Đã xảy ra lỗi khi tạo HttpClient: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private async Task chupAnh()
        {
            try
            {
                string ip = NhanDienBienSo.dangNhapCamera.ip.Trim(); // Địa chỉ IP của camera
                string username = "admin"; // Tên đăng nhập
                string password = "Vimass6868"; // Mật khẩu của camera
                string channelId = "1"; // ID của kênh muốn chụp ảnh
                string url = $"/ISAPI/Streaming/channels/{channelId}/picture"; // URL API chụp ảnh

                // Tạo HttpClient với Digest Authentication
                var handler = new HttpClientHandler()
                {
                    Credentials = new NetworkCredential(username, password)
                };
                HttpClient client = new HttpClient(handler);
                client.BaseAddress = new Uri("http://" + ip);

                // Gửi yêu cầu GET và nhận phản hồi (ảnh)
                HttpResponseMessage response = await client.GetAsync(url);

                // Đảm bảo phản hồi thành công
                response.EnsureSuccessStatusCode();

                // Đọc nội dung trả về (dữ liệu hình ảnh)
                byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();


                // Gửi dữ liệu ảnh đến API nhận diện biển số
                await RecognizePlateAsync(imageBytes, apiKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"chupAnh Đã có lỗi xảy ra: {ex.Message}");
            }
        }


        public async Task RecognizePlateAsync(byte[] imageBytes, string token, string regions = "vn", string cameraId = null)
        {
            try
            {

                // Chuyển byte array thành hình ảnh
                using (var ms = new MemoryStream(imageBytes))
                {
                    using (var image = Image.FromStream(ms))
                    {
                        // Kiểm tra kích thước của ảnh
                        int newWidth = 1920;
                        int newHeight = 1080;

                        // Tạo một Bitmap mới với kích thước mong muốn
                        var resizedImage = new Bitmap(image, newWidth, newHeight);

                        // Lưu ảnh đã thay đổi kích thước thành byte array
                        using (var resizedMs = new MemoryStream())
                        {
                            resizedImage.Save(resizedMs, System.Drawing.Imaging.ImageFormat.Jpeg);
                            byte[] resizedImageBytes = resizedMs.ToArray();

                            // Gửi dữ liệu ảnh đã thay đổi kích thước
                            await this.SendRequest(resizedImageBytes, token, regions, cameraId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Hàm gửi yêu cầu POST với ảnh đã thay đổi kích thước
        public async Task SendRequest(byte[] imageBytes, string token, string regions = null, string cameraId = null)
        {
            try
            {
                this.DisplayImageWithBox(imageBytes);
                using (var client = new HttpClient())
                {
                    // Thiết lập Authorization header với API Key
                    if (!string.IsNullOrEmpty(token))
                    {
                        client.DefaultRequestHeaders.Add("Authorization", $"Token {token}");
                    }

                    // Tạo MultipartFormDataContent để gửi yêu cầu POST
                    var content = new MultipartFormDataContent();
                    string fileName = "image_resized.jpg";  // Đặt tên tệp ảnh nếu cần

                    // Thêm ảnh vào form data dưới dạng byte array
                    var imageContent = new ByteArrayContent(imageBytes);
                    imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                    content.Add(imageContent, "upload", fileName);

                    // Thêm các tham số tùy chọn vào yêu cầu
                    if (!string.IsNullOrEmpty(regions))
                    {
                        content.Add(new StringContent(regions), "regions");
                    }

                    if (!string.IsNullOrEmpty(cameraId))
                    {
                        content.Add(new StringContent(cameraId), "camera_id");
                    }

                    // Gửi yêu cầu POST
                    var response = await client.PostAsync("https://api.platerecognizer.com/v1/plate-reader/", content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Upload success");
                    }
                    else
                    {
                        Console.WriteLine($"HTTP Error: {response.StatusCode}");
                    }

                    var responseBody = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseBody);
                    Debug.WriteLine("Giá trị của thuộc tính style: " + responseBody);
                    if (apiResponse?.Results?.Count > 0)
                    {
                        var box = apiResponse.Results[0].Box;

                        // Lấy các giá trị từ box (xmin, ymin, xmax, ymax)
                        int xmin = box.Xmin;
                        int ymin = box.Ymin;
                        int xmax = box.Xmax;
                        int ymax = box.Ymax;


                        var vihice = apiResponse.Results[0].Vehicle;
                        String loaixe = vihice.Type;

                        label3.Text = apiResponse.Results[0].Plate.ToUpper();
                        if (loaixe!=null&&(loaixe.ToLower().Equals("suv")|| loaixe.ToLower().Equals("sedan") || loaixe.ToLower().Equals("motorcycle")))
                        {
                            label4.Text = loaixe;
                        }
                        else
                        {
                            label4.Text = "";
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void DisplayImageWithBox(byte[] imageBytes)
        {
            try
            {
                // Kiểm tra xem imageBytes có hợp lệ hay không
                if (imageBytes == null || imageBytes.Length == 0)
                {
                    MessageBox.Show("Dữ liệu ảnh không hợp lệ.");
                    return;
                }

                // Tạo một MemoryStream từ byte array
                using (var ms = new MemoryStream(imageBytes))
                {
                    Image image = null;
                    try
                    {
                        // Kiểm tra xem MemoryStream có thể chuyển thành Image hợp lệ
                        image = Image.FromStream(ms);
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("Dữ liệu không phải là ảnh hợp lệ.");
                        return;
                    }

                    // Kiểm tra nếu ảnh hợp lệ (kích thước không bằng 0)
                    if (image.Width <= 0 || image.Height <= 0)
                    {
                        MessageBox.Show("Ảnh không hợp lệ.");
                        return;
                    }

                    // Gán ảnh vào pictureBox1
                    pictureBox1.Image = image;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã có lỗi xảy ra khi xử lý ảnh: {ex.Message}");
            }
        }






        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                await chupAnh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi tạo HttpClient: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Leave(object sender, EventArgs e)
        {
            try
            {
                if (serialPort != null && serialPort.IsOpen)
                {
                    serialPort.Close();
                    button2.Enabled = true;
                    button3.Enabled = false;
                    MessageBox.Show("Sensor stopped.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error closing port: {ex.Message}");
            }
        }

        private void NhanDienBienSoLive_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (serialPort != null && serialPort.IsOpen)
                {
                    serialPort.Close();
                    button2.Enabled = true;
                    button3.Enabled = false;
                    MessageBox.Show("Sensor stopped.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error closing port: {ex.Message}");
            }
        }
    }
}
