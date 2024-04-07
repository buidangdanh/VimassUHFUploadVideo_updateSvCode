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

namespace VimassUHFUploadVideo.Affliate
{
    public partial class ControllerTikTok : Form
    {
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
            { // Khởi tạo WebDriver
                var driver = new ChromeDriver();

                driver.Url = "https://www.tiktok.com/";
               // driver.FindElement(By.Name("q")).SendKeys("webdriver" + Keys.Return);
                Console.WriteLine(driver.Title);

                //driver.Quit();

            }
            catch(Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi roi: " + ex.Message);
            }
        }
    }
}
