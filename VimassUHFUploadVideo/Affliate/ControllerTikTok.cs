using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Diagnostics;
using java.lang;
using System.Threading;
using Exception = System.Exception;
using com.sun.imageio.plugins.common;
using System.Net.Http;
using System.IO;

namespace VimassUHFUploadVideo.Affliate
{
    public partial class ControllerTikTok : Form
    {
        private ChromeDriver driver;  // Khai báo biến toàn cục cho driver
        public ControllerTikTok()
        {
            InitializeComponent();
        }

        private void ControllerTikTok_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Auto();
        }

        private void Auto()
        {
            try
            {
                // Khởi tạo WebDriver
                // Khởi tạo WebDriver nếu chưa được khởi tạo
                if (driver == null)
                {
                    driver = new ChromeDriver();
                    driver.Url = "https://www.tiktok.com/";
                }

                // Tạo WebDriverWait với timeout
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(100));

                // Sử dụng WebDriverWait để chờ đợi element có ID 'header-more-menu-icon' xuất hiện và kiểm tra thuộc tính 'style' của nó
                var element = wait.Until(d =>
                {
                    try
                    {
                        var el = d.FindElement(By.Id("header-more-menu-icon"));
                        var styleValue = el.GetAttribute("style");
                        // Kiểm tra xem thuộc tính 'style' có giá trị không
                        if (!string.IsNullOrEmpty(styleValue))
                        {
                            return el;
                        }
                    }
                    catch (NoSuchElementException)
                    {
                        // Trả về null nếu không tìm thấy element, khiến WebDriverWait tiếp tục chờ
                        return null;
                    }
                    catch (StaleElementReferenceException)
                    {
                        // Trả về null nếu tham chiếu đến element không còn hợp lệ, khiến WebDriverWait tiếp tục chờ
                        return null;
                    }
                    // Trả về null để tiếp tục vòng lặp chờ nếu 'style' chưa có giá trị
                    return null;
                });

                // Lấy và in giá trị thuộc tính 'style', sau khi đã đảm bảo rằng element sẵn sàng và 'style' có giá trị
                if (element != null)
                {
                    string styleValue = element.GetAttribute("style");
                    Debug.WriteLine("Giá trị của thuộc tính style: " + styleValue);
                    label1.Text = "Đăng nhập thành công!";
                    label1.ForeColor = Color.Green;

                }

                // Optional: Đóng trình duyệt
                //driver.Quit();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Đã xảy ra lỗi roi: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (driver != null)
                {

                    // Tìm thẻ div có thuộc tính data-e2e="explore-item-list"
                    IWebElement parentDiv = driver.FindElement(By.CssSelector("div[data-e2e='explore-item-list']"));

                    // Tìm tất cả các thẻ div con của thẻ div trên
                    var childDivs = parentDiv.FindElements(By.CssSelector("div"));

                    // Kiểm tra xem có thẻ div con nào không và thực hiện click vào thẻ div đầu tiên
                    if (childDivs.Count > 0)
                    {
                        childDivs[0].Click();  // Click vào thẻ div con đầu tiên
                        while (true)
                        {
                            // Create a new thread that calls the DoWork method
                            System.Threading.Thread thread = new System.Threading.Thread(new ThreadStart(vuotCapCha));

                            // Start the thread
                            thread.Start();
      
                            
                            // Sau khi click vào thẻ div, tìm thẻ span có data-e2e="browse-like-icon"
                            IWebElement likeIcon = driver.FindElement(By.CssSelector("span[data-e2e='browse-like-icon']"));

                            // Click vào thẻ span
                            likeIcon.Click();
                            System.Threading.Thread.Sleep(7000);

                            IWebElement nextIcon = driver.FindElement(By.CssSelector("button[data-e2e='arrow-right']"));
                            nextIcon.Click();

                            

                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Đã xảy ra lỗi roi: " + ex.Message);

            }
        }

        private void vuotCapCha()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(100));

                // Use a custom wait condition to wait for the captcha image to be present
                IWebElement el = wait.Until(drv =>
                {
                    try
                    {
                        return drv.FindElement(By.Id("captcha-verify-image"));
                    }
                    catch (NoSuchElementException)
                    {
                        return null;
                    }
                });

                if (el != null)
                {
                    string srcValue = el.GetAttribute("src");
                    // Make sure to call the async method correctly
                    Task.Run(() => phanTichAnh(srcValue));
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine("vuotCapCha Đã xảy ra lỗi roi: " + ex.Message);

            }
        }

        private async void phanTichAnh(string srcValue)
        {
            try
            {
                // Load the image
                // Use HttpClient to download the image
                Bitmap image = await DownloadImageAsync(srcValue);

                // Initialize the left edge of the puzzle piece to the width of the image (max possible value)
                int leftEdge = image.Width;

                // Loop through pixels to find the left edge of the puzzle piece
                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        // Assuming the puzzle piece is white and we have a significant color difference
                        Color pixelColor = image.GetPixel(x, y);
                        if (pixelColor.R > 200 && pixelColor.G > 200 && pixelColor.B > 200) // Adjust thresholds as needed
                        {
                            leftEdge = System.Math.Min(leftEdge, x);
                            // Once the left edge is found, no need to check further pixels in this row
                            break;
                        }
                    }
                }

                // Calculate distance from the left edge of the image to the left edge of the puzzle piece
                int distance = leftEdge;

                Debug.WriteLine("Distance of the puzzle piece from the left edge of the image: " + distance + " pixels");

            }
            catch (Exception ex) {
                Debug.WriteLine("phanTichAnh Đã xảy ra lỗi roi: " + ex.Message);

            }
        }

        private static async Task<Bitmap> DownloadImageAsync(string imageUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    using (HttpResponseMessage response = await client.GetAsync(imageUrl))
                    {
                        if (response.IsSuccessStatusCode)  // Kiểm tra mã trạng thái là 200 OK
                        {
                            using (Stream stream = await response.Content.ReadAsStreamAsync())
                            {
                                Bitmap bitmap = new Bitmap(stream);
                                if (bitmap != null && !bitmap.Size.IsEmpty)  // Kiểm tra bitmap có hợp lệ không
                                {
                                    Console.WriteLine($"Đã tải ảnh: "+ bitmap.ToString());
                                    return bitmap;
                                }
                                else
                                {
                                    throw new InvalidOperationException("Không thể tạo đối tượng Bitmap từ luồng dữ liệu.");
                                }
                            }
                        }
                        else
                        {
                            throw new HttpRequestException($"Lỗi HTTP: {response.StatusCode}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý ngoại lệ tại đây
                    Console.WriteLine($"Lỗi khi tải ảnh: {ex.Message}");
                    return null;
                }
            }
        }

    }
}
