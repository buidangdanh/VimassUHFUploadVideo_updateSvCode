using com.sun.xml.@internal.txw2.output;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VimassUHFUploadVideo.Vpass.GiaoDien
{
    public partial class DeepSeek : Form
    {
        private static readonly string API_KEY = "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";
        private static readonly string API_URL = "https://openrouter.ai/api/v1/chat/completions";
        public DeepSeek()
        {
            InitializeComponent();
        }

        private void DeepSeek_Load(object sender, EventArgs e)
        {

        }
        private async Task SendRequestAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                // Lấy nội dung từ TextBox
                string userInput = textBox1.Text;

                // JSON request body
                string json = "{"
                    + "\"model\":\"deepseek/deepseek-r1:free\","
                    + "\"messages\":["
                    + "{\"role\":\"user\",\"content\":\"" + userInput + "\"}"
                    + "],"
                    + "\"stream\": true"
                    + "}";

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", API_KEY);

                HttpResponseMessage response = await client.PostAsync(API_URL, content);

                if (response.IsSuccessStatusCode)
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    using (var reader = new System.IO.StreamReader(stream))
                    {
                        StringBuilder accumulatedContent = new StringBuilder(); // StringBuilder to accumulate content

                        while (!reader.EndOfStream)
                        {
                            string line = await reader.ReadLineAsync();
                            try
                            {
                                if (line.Contains("content"))
                                {
                                    // Parse the JSON string
                                    JObject jsonObject = JObject.Parse(line.Substring(5));

                                    // Get the 'choices' array
                                    JArray choices = (JArray)jsonObject["choices"];

                                    // Get the 'delta' object from the first choice
                                    JObject delta = (JObject)choices[0]["delta"];

                                    // Extract the 'content' field
                                    string contentValue = delta["content"].ToString();

                                    if (!string.IsNullOrEmpty(contentValue))
                                    {
                                        accumulatedContent.Append(contentValue); // Append content to StringBuilder
                                        this.richTextBox1.AppendText(contentValue); // Append content to TextBox
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi chuyển đổi JSON: " + ex.Message);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Lỗi khi gửi yêu cầu: " + response.StatusCode);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendRequestAsync();
        }
    }
}
