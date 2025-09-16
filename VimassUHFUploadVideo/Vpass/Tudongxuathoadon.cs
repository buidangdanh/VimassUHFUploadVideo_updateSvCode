using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VimassUHFUploadVideo.Vpass
{
    public partial class Tudongxuathoadon : Form
    {
        // Lấy thời gian hiện tại
        static DateTime utcDateTime = DateTime.UtcNow;
        static string vnTimeZoneKey = "SE Asia Standard Time";
        static TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
        static DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
        long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();
        string timenow = ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");


        private TabControl tabControl;
        private void InitializeTabControl()
        {
            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;
            this.Controls.Add(tabControl);
        }
        public Tudongxuathoadon()
        {
            InitializeComponent();
            InitializeTabControl();
        }


        private WebBrowser webBrowser1;

        private void MoTabMoiThreadSafe(string url)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)(() => MoTabMoiThreadSafe(url)));
                return;
            }

            try
            {
                WebBrowser browser = new WebBrowser();
                browser.Dock = DockStyle.Fill;
                browser.ScriptErrorsSuppressed = true;

                browser.DocumentCompleted += (s, e) =>
                    XuLyHoanThanhTaiTrang(s, e, browser, url);

                TabPage tabPage = new TabPage("Tab " + (tabControl.TabPages.Count + 1));
                tabPage.Controls.Add(browser);
                tabControl.TabPages.Add(tabPage);
                tabControl.SelectedTab = tabPage;

                browser.Navigate(url);
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần
            }
        }

        private void Tudongxuathoadon_Load(object sender, EventArgs e)
        {
            try
            {
                List<string> urlList = new List<string>
                {
                    "https://xuathoadon.phuclong.com.vn/xu-ly?orderNo=201001250603100",
                    "https://xuathoadon.phuclong.com.vn/xu-ly?orderNo=201001250603100",
                    "https://xuathoadon.phuclong.com.vn/xu-ly?orderNo=201001250603100",
                    "https://xuathoadon.phuclong.com.vn/xu-ly?orderNo=201001250603100"
                };

                foreach (var url in urlList)
                {
                    Task.Run(() => MoTabMoiThreadSafe(url));
                }
            }
            catch (Exception ex)
            {
                FunctionGeneral.writeFile(@"D:\LogHoaDon.txt", timenow + "__Tudongxuathoadon_Load__Exception__" + ex);

            }


        }


        public class ObjectThongTinHoaDon
        {
            public String OrderNo;
            public String TaxAuthority;
            public String PaymentAmount;
            public String DeliveryDate;
            public String StoreName;
        }

        /// <summary>
        /// Hàm hỗ trợ lấy nội dung từ phần tử HTML theo ID hoặc class.
        /// </summary>

        private void XuLyHoanThanhTaiTrang(object sender, WebBrowserDocumentCompletedEventArgs e, WebBrowser browser, string url)
        {
            try
            {
                if (browser.ReadyState != WebBrowserReadyState.Complete)
                    return;

                HtmlDocument doc = browser.Document;

                // Thử đăng nhập nếu có form
                HtmlElement username = doc.GetElementById("username");
                HtmlElement password = doc.GetElementById("password");
                HtmlElement loginButton = doc.GetElementById("loginButton");

                if (username != null && password != null && loginButton != null)
                {
                    username.SetAttribute("value", "your_username");
                    password.SetAttribute("value", "your_password");
                    loginButton.InvokeMember("click");
                    return;
                }

                // Tạo đối tượng thông tin hóa đơn
                ObjectThongTinHoaDon thongTin = new ObjectThongTinHoaDon
                {
                    OrderNo = GetValueByIdOrClass(doc, "OrderNo"),
                    TaxAuthority = GetValueByIdOrClass(doc, "TaxAuthority"),
                    PaymentAmount = GetValueByIdOrClass(doc, "PaymentAmount"),
                    DeliveryDate = GetValueByIdOrClass(doc, "DeliveryDate"),
                    StoreName = GetValueByIdOrClass(doc, "StoreNo")
                };

                // Ghi ra log
                string log = $"{DateTime.Now} | {url}\n" +
                             $"OrderNo: {thongTin.OrderNo}\n" +
                             $"TaxAuthority: {thongTin.TaxAuthority}\n" +
                             $"PaymentAmount: {thongTin.PaymentAmount}\n" +
                             $"DeliveryDate: {thongTin.DeliveryDate}\n" +
                             $"StoreNo: {thongTin.StoreName}\n\n";

                FunctionGeneral.writeFile(@"D:\LogHoaDon.txt", timenow + "__XuLyHoanThanhTaiTrang__ThongTin__"+ thongTin.OrderNo + log);

                Console.WriteLine(log);  // Hiển thị ở Output (Debug)
            }
            catch(Exception ex)
            {
                FunctionGeneral.writeFile(@"D:\LogHoaDon.txt", timenow + "__XuLyHoanThanhTaiTrang__Exception__" + ex);
            }
           
        }

        /// <summary>
        /// Hỗ trợ lấy dữ liệu theo ID hoặc class name
        /// </summary>
        private string GetValueByIdOrClass(HtmlDocument doc, string idOrClass)
        {
            var element = doc.GetElementById(idOrClass);
            if (element != null)
                return element.InnerText ?? element.GetAttribute("value");

            foreach (HtmlElement el in doc.All)
            {
                if (el.GetAttribute("className") == idOrClass)
                    return el.InnerText ?? el.GetAttribute("value");
            }

            return "Không tìm thấy";
        }
    }
}
