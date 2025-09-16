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

namespace VimassUHFUploadVideo.Vpass.GiaoDien
{
    public partial class TFLuna : Form
    {
        public TFLuna()
        {
            InitializeComponent();
            InitializeSerialPort();
        }
        private SerialPort serialPort;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                    this.button1.Enabled = false;
                    this.button2.Enabled = true;
                    MessageBox.Show("Sensor started.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening port: {ex.Message}");
            }
        }
        private void InitializeSerialPort()
        {
            // Cấu hình cổng serial
            serialPort = new SerialPort
            {
                PortName = "COM7", // Thay "COM1" bằng cổng chính xác của bạn
                BaudRate = 115200, // Tốc độ baud của TFLuna
                DataBits = 8,
                Parity = Parity.None,
                StopBits = StopBits.One,
                ReadTimeout = 1000, // Timeout khi đọc dữ liệu
                WriteTimeout = 1000
            };

            serialPort.DataReceived += SerialPort_DataReceived;
        }

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
                        label1.Text = $"Distance: {distance / 100.0} m";
                        label2.Text = $"Strength: {strength / 100.0} %";
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading data: {ex.Message}");
            }
        }

   

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
            base.OnFormClosing(e);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                    this.button1.Enabled = true;
                    this.button2.Enabled = false;
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
