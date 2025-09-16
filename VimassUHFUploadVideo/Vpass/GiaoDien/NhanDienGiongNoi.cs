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
using Vosk;
using NAudio.Wave;
using Vosk;
using Antlr4.Runtime;
using com.sun.xml.@internal.txw2.output;
using System.Speech.Recognition;
using System.Net.Http;

namespace VimassUHFUploadVideo.Vpass.GiaoDien
{
    public partial class NhanDienGiongNoi : Form
    {
        public NhanDienGiongNoi()
        {
            InitializeComponent();
            InitializeSpeechRecognition();
        }
        private SpeechRecognitionEngine recognizer2;
        private void InitializeSpeechRecognition()
        {
            String result = Task.Run(async () =>
            {
                String filePath = @"";
                var payload = File.ReadAllBytes(filePath);

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("api-key", "c31ryxcikuV4Iadmbfa4HTfG7Ed2muLw");

                var response = await client.PostAsync("https://api.fpt.ai/hmi/asr/general", new ByteArrayContent(payload));
                return await response.Content.ReadAsStringAsync();
            }).GetAwaiter().GetResult();

            Console.WriteLine(result);
            Console.ReadLine();
        }

        private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            textBox1.Text = e.Result.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }
        private WaveInEvent waveIn;
        private VoskRecognizer recognizer;
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (recognizer == null)
                {
                    MessageBox.Show("Mô hình chưa được khởi tạo!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                label3.Text = "";

                waveIn = new WaveInEvent
                {
                    DeviceNumber = 0, // Sử dụng micro mặc định
                    WaveFormat = new WaveFormat(16000, 1) // Chuẩn 16kHz, Mono
                };

                waveIn.DataAvailable += OnDataAvailable;
                waveIn.StartRecording();

                MessageBox.Show("Bắt đầu nhận diện. Nói vào micro!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khởi động micro: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void NhanDienGiongNoi_Load(object sender, EventArgs e)
        {
            try
            {
                string modelPath = @"D:\vosk-model-vn-0.4";
                if (!Directory.Exists(modelPath))
                {
                    MessageBox.Show("Thư mục mô hình không tồn tại. Vui lòng kiểm tra lại đường dẫn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Model model = new Model(modelPath);
                recognizer = new VoskRecognizer(model, 16000.0f);
                MessageBox.Show("Mô hình được khởi tạo thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khởi tạo mô hình: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Xử lý dữ liệu âm thanh từ micro
        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            if (recognizer == null) return;

            // Chỉ xử lý khi nhận diện hoàn tất (AcceptWaveform trả về true)
            if (recognizer.AcceptWaveform(e.Buffer, e.Buffer.Length))
            {
                string result = recognizer.Result();
                BeginInvoke(new Action(() =>
                {
                    label3.Text = result; // Cập nhật label3 khi nhận diện hoàn tất
                }));
            }
        }


        // Dừng nhận diện và giải phóng tài nguyên
        private void StopSpeechRecognition()
        {
            if (waveIn != null)
            {
                waveIn.StopRecording();
                waveIn.Dispose();
                waveIn = null;
                MessageBox.Show("Nhận diện đã dừng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    
    }
}
