using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VimassUHFUploadVideo.Ultil;
using HtmlAgilityPack;
using System.Web.UI.WebControls;
using System.Xml;
using GoogleMaps.LocationServices;
using System.Device.Location;
using System.Web.Script.Serialization;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Globalization;
using System.Text.RegularExpressions;
using VimassUHFUploadVideo.Ultil;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Response = VimassUHFUploadVideo.Ultil.Response;
using VimassUHFUploadVideo.VPOS.Ultil;
using VimassUHFUploadVideo.Vpass.Object;
using sun.net.www.content.image;
using javax.naming;
using sun.security.krb5.@internal;
using System.Net.Sockets;
using VimassUHFUploadVideo.Vpass.Object.ObjectDiemDinhDanh;
using com.sun.org.apache.xml.@internal.serialize;
using System.Web;
using Polly;
using System.Speech.Synthesis;
using jdk.nashorn.@internal.ir;

namespace VimassUHFUploadVideo
{
    public partial class GetDataStore : Form
    {
        public GetDataStore()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        /*      private void button2_Click(object sender, EventArgs e)
              {
                  String DulieuUHF = "";
                  string nameJson = FunctionGeneral.pathJson + textBox2.Text + @".txt";

                  string path = nameJson;
                  DulieuUHF = FunctionGeneral.docThongTin(path);
                  var datauhf = JsonConvert.DeserializeObject<List<raw>>(DulieuUHF);

                 *//*
                      string[] danh = DulieuUHF.Split('\n');*//*
                      for (int i3 = 0; i3 < datauhf.Count; i3++)
                      {
                      try
                      {
                          textBox1.Text += datauhf[i3].name.Trim() + "%" + datauhf[i3].address.Trim() + "%" + datauhf[i3].hotline + "\r\n";
                      }
                      catch(Exception ex)
                      {

                      }



                      }


              }*/


        private void button3_Click(object sender, EventArgs e)
        {
            /*THtruemilk u = new THtruemilk();
            string url = "http://210.245.8.7:12318/vimass/services/VUHF/createEPCid";
         

            var json = JsonConvert.SerializeObject(u);
            String res = Service.SendWebrequest_POST_Method(json, url);
            Response response = JsonConvert.DeserializeObject<Response>(res);
            if (response.msgCode == 1)
            {
                
            }*/
        }

        private async void button4_Click(object sender, EventArgs e)
        {

            using (var webClient = new WebClient())
            {


                try
                {
                    string nameJson = FunctionGeneral.pathJson + textBox2.Text + @".txt";
                    string filePath = @"D:\dataHTML2.txt";
                    string path = nameJson;
                    string DulieuUHF = FunctionGeneral.docThongTin(filePath);
                    // Create a HtmlDocument object from the HTML content
                    var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                    htmlDoc.LoadHtml(DulieuUHF);
                    // Lấy tất cả các thẻ img có class là 'product-image'
                    var imageNodes = htmlDoc.DocumentNode.SelectNodes("//img[@class='product-image']");

                    if (imageNodes != null)
                    {
                        foreach (var imgNode in imageNodes)
                        {
                            string srcValue = imgNode.GetAttributeValue("src", string.Empty);
                            srcValue = await goiDichVuTaoAnhAsync(srcValue);
                            string ten = imgNode.GetAttributeValue("alt", string.Empty);
                            Console.WriteLine("SRC: " + srcValue);
                            Console.WriteLine("ten: " + ten);
                            // Tìm thẻ div có class='product-card-two__Card-sc-1lvbgq2-0' chứa thẻ img
                            var productCard = imgNode.Ancestors("div")
                                .FirstOrDefault(div => div.GetAttributeValue("class", "").Contains("product-card-two__Card-sc-1lvbgq2-0"));
                            double price = 0;
                            if (productCard != null)
                            {
                                // Tìm thẻ div có class='product-card-two__Price-sc-1lvbgq2-9' là con của thẻ productCard
                                var priceNode = productCard.SelectSingleNode(".//div[contains(@class, 'product-card-two__Price-sc-1lvbgq2-9')]");

                                if (priceNode != null)
                                {
                                    // Lấy giá trị của thẻ div priceNode
                                    price = RemoveNonNumericCharacters(priceNode.InnerText.Trim());
                                    Console.WriteLine($"Giá sản phẩm: {price}");
                                }
                            }
                            goiDichVuTaoSanPham(srcValue, ten, price, "", "", "");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Không tìm thấy thẻ img nào với class 'product-image'.");
                    }

                    //var datauhf = JsonConvert.DeserializeObject<List<RawJson>>(DulieuUHF);
                    /* for (int i3 = 0; i3 < datauhf.Count; i3++)
                     {
                         string url = "https://shopdunk.com/SelectStore?StateProvinceId=&CountyId=&select_receive_store=";



                         var url2 = url + datauhf[i3].ShowroomSeName;
                         webClient.Encoding = Encoding.UTF8;
                         var htmlString = webClient.DownloadString(url2);
                         //Code lấy html từ một trang web//
                         var dulieuweb = new HtmlAgilityPack.HtmlDocument();
                         dulieuweb.LoadHtml(htmlString);
                         var tencuahang = dulieuweb.DocumentNode.SelectNodes("//strong[@class='title']");
                         var diachi = dulieuweb.DocumentNode.SelectNodes("//div[@class='address']");
                         var diachithanhpho = dulieuweb.DocumentNode.SelectNodes("//div[@class='city']");
                         var sdt = dulieuweb.DocumentNode.SelectNodes("//div[@class='label']");

                         THtruemilk u = new THtruemilk();


                         if (sdt != null)
                         {
                             for (int i2 = 0; i2 < sdt.Count; i2++)
                             {


                                 if (sdt[i2].InnerText.Contains("Hotline:"))
                                 {
                                     textBox1.Text += datauhf[i3].ShowroomSeName + "%" + datauhf[i3].ShowroomName + "%" + datauhf[i3].ShowroomAddress + "%" + sdt[i2].InnerText + "\r\n";
                                 }

                             }
                         }
                         else
                         {
                             textBox1.Text += datauhf[i3].ShowroomSeName + "%" + datauhf[i3].ShowroomName + "%" + datauhf[i3].ShowroomAddress + "\r\n";
                         }



                     }*/

                }
                catch (Exception ex)
                {

                }

            }

        }

        private static async Task<string> goiDichVuTaoAnhAsync(String link)
        {
            String kq = "";
            try
            {
                upLoadAnh u = new upLoadAnh();
                u.value = await DownloadImageAsBase64Async(link);
                u.idCheck = FunctionGeneral.Md5(u.value);
                string url = "http://103.21.150.10:8080/VimassMedia/services/VMMedia/uploadImg";
                var json = JsonConvert.SerializeObject(u);
                String res = Service.SendWebrequest_POST_Method(json, url);

                Debug.WriteLine(json);

                Response response = JsonConvert.DeserializeObject<Response>(res);
                if (response.msgCode == 1)
                {
                    kq = response.result.ToString();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("goiDichVuTaoAnhAsync exception" + ex.Message);
            }
            return kq;
        }

        private void goiDichVuTaoSanPham(String anh, string ten, double gia, string mota, string idStore, string idCate)
        {
            try
            {
                taoSanPham u = new taoSanPham();
                u.funcId = 40;
                u.sessionLogin = "95E554766379388097053F018C60881EMNSSMDWUXSGGXCXJAPAQA5K5AG0PMB2";
                u.idLogin = "0966074236";
                u.token = "";
                u.typeLogin = 0;
                u.companyCode = "";
                u.timeRequest = FunCGeneral.timeNow();
                u.gia = gia;
                u.giaGiam = 0;
                u.idStore = idStore;
                u.idCate = idCate;
                u.productName = ten;
                u.image = anh;
                u.thumbnail = "";
                u.description = mota;
                u.cksInput = FunctionGeneral.Md5("0819226669jdqpowrifioefiqo3289r79f" + u.sessionLogin + u.idLogin + u.typeLogin + u.timeRequest + u.idStore + u.idCate + u.image + RemoveDiacritics2(u.productName) + u.image + u.thumbnail + u.gia + u.giaGiam).ToLower();
                Debug.WriteLine("0819226669jdqpowrifioefiqo3289r79f" + u.sessionLogin + u.idLogin + u.typeLogin + u.timeRequest + u.idStore + u.idCate + u.image + RemoveDiacritics2(u.productName) + u.image + u.thumbnail + u.gia + u.giaGiam);
                string url = "https://pos.vimass.vn/VMServices/services/Vpos/requestCommand";
                var json = JsonConvert.SerializeObject(u);
                String res = Service.SendWebrequest_POST_Method(json, url);

                Debug.WriteLine(json);

                Response response = JsonConvert.DeserializeObject<Response>(res);
                if (response != null)
                {
                    if (response.msgCode == 1)
                    {
                        Debug.WriteLine("đ");

                    }

                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception goiDichVuTaoSanPham: " + ex);
            }
        }
        public static string RemoveDiacritics2(string text)
        {
            string normalizedString = text.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            // Chuyển đổi thành chuỗi không dấu
            string noDiacriticsString = stringBuilder.ToString().Normalize(NormalizationForm.FormC);

            // Thay thế các ký tự đặc biệt
            noDiacriticsString = noDiacriticsString.Replace('đ', 'd').Replace('Đ', 'D');

            return noDiacriticsString;
        }

        public static Dictionary<string, int> myDict;
        public static int i;
        public static double RemoveNonNumericCharacters(string input)
        {
            // Biểu thức chính quy để tìm các ký tự không phải số
            string pattern = "[^0-9]";
            // Thay thế các ký tự không phải số bằng chuỗi rỗng
            double result = double.Parse(Regex.Replace(input, pattern, ""));
            return result;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            using (var webClient = new WebClient())
            {
                try
                {
                    string nameJson = FunctionGeneral.pathJson + textBox2.Text + @".txt";
                    string path = nameJson;
                    string DulieuUHF = FunctionGeneral.docThongTin(path);
                    string[] linkList = DulieuUHF.Trim().Split('\r');
                    Dictionary<string, int> myDict = new Dictionary<string, int>();
                    myDict.Add("danh", 0);
                    string linkRaw = textBox3.Text.Trim();
                    for (int i = 0; i < 201; i = i + 25)
                    {
                        string url = textBox3.Text + i;
                        webClient.Encoding = Encoding.UTF8;
                        var htmlString = webClient.DownloadString(url);
                        //Code lấy html từ một trang web//
                        var dulieuweb = new HtmlAgilityPack.HtmlDocument();
                        dulieuweb.LoadHtml(htmlString);
                        var link = dulieuweb.DocumentNode.SelectNodes("//a[@data-testid='title-link']");
                        if (link != null)
                        {
                            for (int j = 0; j < link.Count; j++)
                            {
                                using (var webClient2 = new WebClient())
                                {
                                    webClient2.Encoding = Encoding.UTF8;
                                    var htmlString2 = webClient2.DownloadString(link[j].GetAttributeValue("href", ""));
                                    //Code lấy html từ một trang web//
                                    var dulieuweb2 = new HtmlAgilityPack.HtmlDocument();
                                    dulieuweb2.LoadHtml(htmlString2);
                                    var tenks = dulieuweb2.DocumentNode.SelectSingleNode("//a[@id='hp_hotel_name_reviews']");
                                    var latlong = dulieuweb2.DocumentNode.SelectSingleNode("//a[@id='hotel_address']");
                                    var diachi = dulieuweb2.DocumentNode.SelectSingleNode("//span[@data-source='top_link']");
                                    var img = dulieuweb2.DocumentNode.SelectSingleNode("//img[@class='hide']");

                                    if (tenks != null && !myDict.ContainsKey(tenks.InnerText))
                                    {
                                        // textBox1.Text += link[j].GetAttributeValue("href", "").Trim() + Environment.NewLine;
                                        textBox1.Text += tenks.InnerText.Trim() + "%" + link[j].GetAttributeValue("href", "").Trim() + "%" + diachi.InnerText.Trim() + "%" + latlong.GetAttributeValue("data-atlas-latlng", "").Trim() + "%" + img.GetAttributeValue("src", "").Trim() + Environment.NewLine;
                                        myDict.Add(tenks.InnerText, 0);
                                    }
                                }
                            }
                        }

                    }
                    /*for (int i = 0; i < 26; i=i+25)
                   {
                        string url = linkRaw+i;
                        webClient.Encoding = Encoding.UTF8;
                        var htmlString = webClient.DownloadString(url);
                        //Code lấy html từ một trang web//
                        var dulieuweb = new HtmlAgilityPack.HtmlDocument();
                        dulieuweb.LoadHtml(htmlString);
                        var link = dulieuweb.DocumentNode.SelectNodes("//a[@data-testid='title-link']");
                        if(link != null)
                        {
                            for (int j = 0; j < link.Count; j++)
                            {
                                using (var webClient2 = new WebClient())
                                {
                                    webClient2.Encoding = Encoding.UTF8;
                                    var htmlString2 = webClient2.DownloadString(link[j].GetAttributeValue("href", ""));
                                    //Code lấy html từ một trang web//
                                    var dulieuweb2 = new HtmlAgilityPack.HtmlDocument();
                                    dulieuweb2.LoadHtml(htmlString2);
                                    var tenks = dulieuweb2.DocumentNode.SelectSingleNode("//a[@id='hp_hotel_name_reviews']");
                                    var latlong = dulieuweb2.DocumentNode.SelectSingleNode("//a[@id='hotel_address']");
                                    var diachi = dulieuweb2.DocumentNode.SelectSingleNode("//span[@data-source='top_link']");
                                    var img = dulieuweb2.DocumentNode.SelectSingleNode("//img[@class='hide']");

                                    if (tenks != null && !myDict.ContainsKey(tenks.InnerText))
                                    {
                                       // textBox1.Text += link[j].GetAttributeValue("href", "").Trim() + Environment.NewLine;
                                        textBox1.Text += tenks.InnerText.Trim()+"%"+ link[j].GetAttributeValue("href", "").Trim()+ "%"+diachi.InnerText.Trim()+"%"+latlong.GetAttributeValue("data-atlas-latlng","").Trim() + "%"+img.GetAttributeValue("src","").Trim()+Environment.NewLine;
                                        myDict.Add(tenks.InnerText, 0);
                                    }
                                }
                            }
                        }

                   }*/
                    /*string nameJson = FunctionGeneral.pathJson + textBox2.Text + @".txt";
                    string path = nameJson;
                    string DulieuUHF = FunctionGeneral.docThongTin(path);
                    string[] linkList = DulieuUHF.Trim().Split('\r');
                    for (int i = 0; i < linkList.Length; i++)
                    {
                        string url = linkList[i];
                        webClient.Encoding = Encoding.UTF8;
                        var htmlString = webClient.DownloadString(url);
                        //Code lấy html từ một trang web//
                        var dulieuweb = new HtmlAgilityPack.HtmlDocument();
                        dulieuweb.LoadHtml(htmlString);
                        var diachi = dulieuweb.DocumentNode.SelectNodes("//a[@id='hotel_address']");
                        var tencuahang = dulieuweb.DocumentNode.SelectSingleNode("//h2[@class='d2fee87262 pp-header__title']");
                        var diachis = dulieuweb.DocumentNode.SelectSingleNode("//span[@data-source='top_link']");
                        HtmlNode element = dulieuweb.DocumentNode.SelectSingleNode("//a[@id='hotel_address']");
                        string src = element.GetAttributeValue("data-atlas-latlng", "");
                        var json = JsonConvert.SerializeObject(u);
                        FunctionGeneral.writeFile(FunctionGeneral.pathJson + @"sunhousejson.txt", json);
                        textBox1.Text += src + "\r\n";
                        for (int i2 = 0; i2 < diachi.Count; i2++)
                        {
                            //textBox1.Text += diachi[i2].InnerText + "\r\n";

                        }

                    }*/
                }
                catch (Exception ex)
                {

                }

            }
        }
        public static void subLineString(string nameJson)
        {
            try
            {
                string path = FunctionGeneral.pathJson + nameJson + @".txt";
                string DulieuUHF = FunctionGeneral.docThongTin(path);
                string result = "";
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    int lineCount = 0;

                    while ((line = sr.ReadLine()) != null)
                    {
                        lineCount++;
                        result += line;

                        if (lineCount % 3 == 0)
                        {
                            result += Environment.NewLine;
                        }
                    }
                }
                File.WriteAllText(FunctionGeneral.pathJson + @"tach3.txt", result);
            }
            catch (Exception ex)
            {
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = GetLatLngFromAdD("4 Liễu Giai, Ba Đình, Hà Nội");
        }

        public static string GetLatLngFromAdD(string address)
        {

            string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key=AIzaSyA-3pTPVsa4Wr0XhsccIY48UbfIMpUkb7I";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string data = "";
            using (StreamReader streamreader = new StreamReader(response.GetResponseStream()))
            {
                data = streamreader.ReadToEnd();
            }

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            GoogleGeoCodeResponse googleGeoCodeResponse = jsSerializer.Deserialize<GoogleGeoCodeResponse>(data);
            string latlong;
            if (googleGeoCodeResponse.status == "OK")
            {
                double latitude = googleGeoCodeResponse.results[0].geometry.location.lat;
                double longitude = googleGeoCodeResponse.results[0].geometry.location.lng;

                Console.WriteLine("Latitude: " + latitude.ToString());
                Console.WriteLine("Longitude: " + longitude.ToString());

                FunctionGeneral.check1 = "Latitude: " + latitude.ToString() + ";" + "Longitude: " + longitude.ToString();
            }
            else
            {
                Console.WriteLine("Error: " + googleGeoCodeResponse.status);
            }

            return FunctionGeneral.check1;

        }
        public class GoogleGeoCodeResponse
        {
            public string status { get; set; }
            public results[] results { get; set; }
        }

        public class results
        {
            public geometry geometry { get; set; }
        }

        public class geometry
        {
            public location location { get; set; }
        }

        public class location
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }
        public class raw
        {
            public string email { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string name { get; set; }
            public string phone_one { get; set; }
            public string phone_two { get; set; }
            public string street { get; set; }
            public string state_province { get; set; }




        }

        private void button7_Click(object sender, EventArgs e)
        {

            IWebDriver driver = new ChromeDriver();

            // Mở trang web
            driver.Navigate().GoToUrl("https://www.bachhoaxanh.com/he-thong-sieu-thi/");

            // Tìm và click vào thẻ
            int numberOfClicks = 1000;
            for (int i = 0; i < numberOfClicks; i++)
            {
                IWebElement element = driver.FindElement(By.ClassName("viewmoreshop"));
                try
                {
                    element.Click();

                }
                catch (Exception ex)
                {

                }

            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            String DulieuUHF = "";
            string nameJson = FunctionGeneral.pathJson + textBox2.Text + @".txt";

            string path = nameJson;
            DulieuUHF = FunctionGeneral.docThongTin(path);
            var datauhf = JsonConvert.DeserializeObject<List<raw>>(DulieuUHF);
            for (int i = 0; i < datauhf.Count; i++)
            {
                if (datauhf[i].phone_two != datauhf[i].phone_one)
                {
                    textBox1.Text += datauhf[i].name + "%" + datauhf[i].street + ", " + datauhf[i].state_province + "%" + datauhf[i].latitude + "%" + datauhf[i].longitude + "%" + datauhf[i].phone_one + "," + datauhf[i].phone_two + "%" + datauhf[i].email + "\r\n";
                }
                else
                {
                    textBox1.Text += datauhf[i].name + "%" + datauhf[i].street + ", " + datauhf[i].state_province + "%" + datauhf[i].latitude + "%" + datauhf[i].longitude + "%" + datauhf[i].phone_one + "%" + datauhf[i].email + "\r\n";
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                string[] danh = textBox1.Text.Trim().Split('\r');
                textBox1.Text = "";
                foreach (string danh2 in danh)
                {
                    if (danh2 != "")
                    {
                        string[] danh3 = danh2.Split(',');
                        textBox1.Text += danh3[0].Trim() + "\r\n";
                        FunctionGeneral.writeFile(FunctionGeneral.pathJson + @"tach.txt", danh3[1] + "%" + danh3[0] + "\r\n");
                    }

                }
            }
            catch (Exception c)
            {

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string photo = "";

                ParamGoogleMapAPITextSearch u = new ParamGoogleMapAPITextSearch();
                string url = "https://maps.googleapis.com/maps/api/place/textsearch/json";
                u.key = "AIzaSyA-3pTPVsa4Wr0XhsccIY48UbfIMpUkb7I";
                u.query = textBox4.Text.Trim();
                ResponseGoogleMap messageResult = new ResponseGoogleMap();
                messageResult.msgCode = ErrorCode.FALSE;

                string param = "query" + ";" + u.query + ";" + "key" + ";" + u.key;
                string result2 = Service.SendWebrequest_Get_Method(param, url);
                messageResult = JsonConvert.DeserializeObject<ResponseGoogleMap>(result2);

                try
                {
                    JObject jsonObject = JObject.Parse(result2);
                    JArray results = (JArray)jsonObject["results"];

                    foreach (JObject result in results)
                    {
                        double lat = (double)result["geometry"]["location"]["lat"];
                        double lng = (double)result["geometry"]["location"]["lng"];
                        string formattedAddress = (string)result["formatted_address"];
                        string name = (string)result["name"];

                        JArray photos = (JArray)result["photos"];
                        string photoReference = photos != null && photos.Count > 0 ? (string)photos[0]["photo_reference"] : null;
                        if (photoReference != null && photoReference != "")
                        {
                            photo = "https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photo_reference=" + photoReference + "&key=AIzaSyA-3pTPVsa4Wr0XhsccIY48UbfIMpUkb7I";
                        }
                        textBox1.Text += name + "%" + formattedAddress + "%" + lat + "%" + lng + "%" + photo + Environment.NewLine;

                    }
                    if (messageResult.next_page_token != null && messageResult.next_page_token != "")
                    {
                        Thread.Sleep(2000);
                        textBox1.Text += googleMap(u.query, messageResult.next_page_token);
                    }
                    //messageResult = JsonConvert.DeserializeObject<ResponseGoogleMap>(result);
                }
                catch (Exception e2)
                {
                    Console.WriteLine("Lỗi khi pass json: " + e2);
                }
            }
            catch (Exception ex)
            {
            }
        }
        public static string luuText;
        public static string next;
        public static string googleMap(string query, string nextPage)
        {
            try
            {
                string photo = "";
                ParamGoogleMapAPITextSearch u = new ParamGoogleMapAPITextSearch();
                string url = "https://maps.googleapis.com/maps/api/place/textsearch/json";
                u.key = "AIzaSyA-3pTPVsa4Wr0XhsccIY48UbfIMpUkb7I";
                u.query = query.Trim();
                u.pagetoken = nextPage;
                ResponseGoogleMap messageResult = new ResponseGoogleMap();
                messageResult.msgCode = ErrorCode.FALSE;

                string param = "query" + ";" + u.query + ";" + "key" + ";" + u.key + ";" + "pagetoken" + ";" + u.pagetoken;
                string result2 = Service.SendWebrequest_Get_Method(param, url);
                messageResult = JsonConvert.DeserializeObject<ResponseGoogleMap>(result2);
                next = messageResult.next_page_token;
                Console.WriteLine("Danh: " + messageResult.next_page_token);
                try
                {
                    JObject jsonObject = JObject.Parse(result2);
                    JArray results = (JArray)jsonObject["results"];

                    foreach (JObject result in results)
                    {
                        double lat = (double)result["geometry"]["location"]["lat"];
                        double lng = (double)result["geometry"]["location"]["lng"];
                        string formattedAddress = (string)result["formatted_address"];
                        string name = (string)result["name"];

                        JArray photos = (JArray)result["photos"];
                        string photoReference = photos != null && photos.Count > 0 ? (string)photos[0]["photo_reference"] : null;
                        if (photoReference != null && photoReference != "")
                        {
                            photo = "https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photo_reference=" + photoReference + "&key=AIzaSyA-3pTPVsa4Wr0XhsccIY48UbfIMpUkb7I";
                        }

                        luuText += name + "%" + formattedAddress + "%" + lat + "%" + lng + "%" + photo + Environment.NewLine;


                    }
                    if (next != null && next != "")
                    {
                        Thread.Sleep(2000);
                        googleMap(u.query, messageResult.next_page_token);
                    }

                    //messageResult = JsonConvert.DeserializeObject<ResponseGoogleMap>(result);
                }
                catch (Exception e2)
                {
                    Console.WriteLine("Lỗi khi pass json: " + e2);

                }
                return luuText;
            }
            catch (Exception ex)
            {
                return luuText;
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {

            string nameJson = FunctionGeneral.pathJson + textBox2.Text + @".txt";

            string path = nameJson;
            string DulieuUHF = FunctionGeneral.docThongTin(path);
            var datauhf = DulieuUHF.Split('\r');
            for (int i = 0; i < datauhf.Length; i++)
            {
                if (datauhf[i] != "" && datauhf[i] != null)
                {
                    string url = datauhf[i];
                    GetRedirectedUrl(url, path);
                }


            }


        }
        public static async Task<string> GetRedirectedUrl(string url, string path)
        {
            using (var httpClient = new HttpClient(new HttpClientHandler { AllowAutoRedirect = false }))
            {
                var response = await httpClient.GetAsync(url);
                Console.WriteLine(response.Headers.Location.ToString());
                FunctionGeneral.writeFile(path, response.Headers.Location.ToString());
                return response.Headers.Location.ToString();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string[] dsdc = textBox1.Text.Trim().Split('\r');
            for (int i = 0; i < dsdc.Length; i++)
            {
                if (dsdc[i] != "")
                {
                    textBox1.Text += "body.text.thietbi.tb." + RemoveDiacritics(dsdc[i].Trim().Replace("Đ", "D").Replace("đ", "d").Replace("/", "").Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Replace(",", "").Replace("%", "").Replace(".", "").Replace(":", "").Replace("+", "")) + "\r\n";
                }
            }
        }
        public static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
        public static async Task<string> DownloadImageAsBase64Async(string url)
        {
            string base64String = "";
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    byte[] imageBytes = webClient.DownloadData(url);
                    base64String = Convert.ToBase64String(imageBytes);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DownloadImageAsBase64Async" + ex.Message);
            }
            return base64String;

        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                taoSanPham u = new taoSanPham();
                u.funcId = 40;
                u.sessionLogin = "454593A9433011638D81B658A25D2D5EMNSSMDWUXSG19848GXCXJXJCKKCQPPG";
                u.idLogin = "0966074236";
                u.token = "";
                u.typeLogin = 0;
                u.companyCode = "";
                u.timeRequest = FunCGeneral.timeNow();
                u.gia = 0;
                u.giaGiam = 0;
                u.idStore = "S1717215697810M5B469";
                u.idCate = "1717216259259ellec";
                u.productName = "Xịt họng thảo dược Pharmacity Herbal Throat Spray hỗ trợ bổ phổi, giảm đau họng và ho (Chai 25ml)";
                u.image = "https://prod-cdn.pharmacity.io/e-com/images/ecommerce/1000x1000/P17792.png";
                u.thumbnail = "";
                u.description = "<div id=\"cong-dung\"><div class=\"pmc-content-html [&amp;_a:not(.ignore-css a)]:text-hyperLink max-w-[calc(100vw-32px)] overflow-auto md:max-w-none\"><div data-draftjs-conductor-fragment=\"{&quot;blocks&quot;:[{&quot;key&quot;:&quot;av8p8&quot;,&quot;text&quot;:&quot;Công dụng&quot;,&quot;type&quot;:&quot;header-three&quot;,&quot;depth&quot;:0,&quot;inlineStyleRanges&quot;:[],&quot;entityRanges&quot;:[],&quot;data&quot;:{}},{&quot;key&quot;:&quot;6rgm3&quot;,&quot;text&quot;:&quot;Tăng cường sức đề kháng, cải thiện hệ miễn dịch giúp cơ thể tránh khỏi virus, vi khuẩn&quot;,&quot;type&quot;:&quot;unstyled&quot;,&quot;depth&quot;:0,&quot;inlineStyleRanges&quot;:[],&quot;entityRanges&quot;:[],&quot;data&quot;:{}},{&quot;key&quot;:&quot;d8t78&quot;,&quot;text&quot;:&quot;Hỗ trợ giảm tức thời các cơn đau họng, ngứa họng, giúp cải thiện các triệu chứng ho, cảm cúm hay cảm lạnh&quot;,&quot;type&quot;:&quot;unstyled&quot;,&quot;depth&quot;:0,&quot;inlineStyleRanges&quot;:[],&quot;entityRanges&quot;:[],&quot;data&quot;:{}},{&quot;key&quot;:&quot;5nsnh&quot;,&quot;text&quot;:&quot;Giúp làm sạch đường họng, sát khuẩn nhanh, giảm sưng viêm. Mang lại cảm giác tươi mát, thơm tho, dễ chịu cho vòm họng, tăng cường sức đề kháng trực tiếp bảo vệ vùng họng khỏe mạnh và hỗ trợ tiêu diệt các tác nhân xấu từ bên ngoài.&quot;,&quot;type&quot;:&quot;unstyled&quot;,&quot;depth&quot;:0,&quot;inlineStyleRanges&quot;:[],&quot;entityRanges&quot;:[],&quot;data&quot;:{}},{&quot;key&quot;:&quot;4nj9b&quot;,&quot;text&quot;:&quot;Hỗ trợ tốt các vấn đề liên quan đến đường hô hấp trên&quot;,&quot;type&quot;:&quot;unstyled&quot;,&quot;depth&quot;:0,&quot;inlineStyleRanges&quot;:[],&quot;entityRanges&quot;:[],&quot;data&quot;:{}},{&quot;key&quot;:&quot;7rtok&quot;,&quot;text&quot;:&quot;Kháng khuẩn, sát trùng nhẹ, làm se niêm mạc, tiêu đờm, khử mùi hôi miệng.&quot;,&quot;type&quot;:&quot;unstyled&quot;,&quot;depth&quot;:0,&quot;inlineStyleRanges&quot;:[],&quot;entityRanges&quot;:[],&quot;data&quot;:{}}],&quot;entityMap&quot;:{}}\">\r\n<h3 class=\"public-DraftStyleDefault-block public-DraftStyleDefault-ltr\" data-offset-key=\"cil0d-0-0\"><span data-offset-key=\"cil0d-0-0\"><span data-text=\"true\">Công dụng</span></span></h3>\r\n<div class=\"Draftail-block--unstyled \" data-block=\"true\" data-editor=\"4t8kk\" data-offset-key=\"9ct0d-0-0\">\r\n<div class=\"public-DraftStyleDefault-block public-DraftStyleDefault-ltr\" data-offset-key=\"9ct0d-0-0\"><span data-offset-key=\"9ct0d-0-0\"><span data-text=\"true\">Tăng cường sức đề kháng, cải thiện hệ miễn dịch giúp cơ thể tránh khỏi virus, vi khuẩn</span></span></div>\r\n</div>\r\n<div class=\"Draftail-block--unstyled \" data-block=\"true\" data-editor=\"4t8kk\" data-offset-key=\"b3ndd-0-0\">\r\n<div class=\"public-DraftStyleDefault-block public-DraftStyleDefault-ltr\" data-offset-key=\"b3ndd-0-0\"><span data-offset-key=\"b3ndd-0-0\"><span data-text=\"true\">Hỗ trợ giảm tức thời các cơn đau họng, ngứa họng, giúp cải thiện các triệu chứng ho, cảm cúm hay cảm lạnh</span></span></div>\r\n</div>\r\n<div class=\"Draftail-block--unstyled \" data-block=\"true\" data-editor=\"4t8kk\" data-offset-key=\"86r2-0-0\">\r\n<div class=\"public-DraftStyleDefault-block public-DraftStyleDefault-ltr\" data-offset-key=\"86r2-0-0\"><span data-offset-key=\"86r2-0-0\"><span data-text=\"true\">Giúp làm sạch đường họng, sát khuẩn nhanh, giảm sưng viêm. Mang lại cảm giác tươi mát, thơm tho, dễ chịu cho vòm họng, tăng cường sức đề kháng trực tiếp bảo vệ vùng họng khỏe mạnh và hỗ trợ tiêu diệt các tác nhân xấu từ bên ngoài.</span></span></div>\r\n</div>\r\n<div class=\"Draftail-block--unstyled \" data-block=\"true\" data-editor=\"4t8kk\" data-offset-key=\"d1vdk-0-0\">\r\n<div class=\"public-DraftStyleDefault-block public-DraftStyleDefault-ltr\" data-offset-key=\"d1vdk-0-0\"><span data-offset-key=\"d1vdk-0-0\"><span data-text=\"true\">Hỗ trợ tốt các vấn đề liên quan đến đường hô hấp trên</span></span></div>\r\n</div>\r\n<div class=\"Draftail-block--unstyled \" data-block=\"true\" data-editor=\"4t8kk\" data-offset-key=\"bai48-0-0\">\r\n<div class=\"public-DraftStyleDefault-block public-DraftStyleDefault-ltr\" data-offset-key=\"bai48-0-0\"><span data-offset-key=\"bai48-0-0\"><span data-text=\"true\">Kháng khuẩn, sát trùng nhẹ, làm se niêm mạc, tiêu đờm, khử mùi hôi miệng.</span></span></div>\r\n</div>\r\n</div></div></div>";
                u.cksInput = FunctionGeneral.Md5("0819226669jdqpowrifioefiqo3289r79f" + u.sessionLogin + u.idLogin + u.typeLogin + u.timeRequest + u.idStore + u.idCate + u.image + RemoveDiacritics2(u.productName) + u.image + u.thumbnail + u.gia + u.giaGiam).ToLower();
                Debug.WriteLine("0819226669jdqpowrifioefiqo3289r79f" + u.sessionLogin + u.idLogin + u.typeLogin + u.timeRequest + u.idStore + u.idCate + u.image + u.idCate + RemoveDiacritics2(u.productName) + u.image + u.thumbnail + u.gia + u.giaGiam);
                string url = "https://pos.vimass.vn/VMServices/services/Vpos/requestCommand";
                var json = JsonConvert.SerializeObject(u);
                String res = Service.SendWebrequest_POST_Method(json, url);

                Debug.WriteLine(json);

                Response response = JsonConvert.DeserializeObject<Response>(res);
                if (response.msgCode == 1)
                {
                    Debug.WriteLine("đ");

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                string linkRaw = "https://www.pharmacity.vn/pharmacity?index=";
                using (var webClient = new WebClient()) // Khởi tạo WebClient
                {
                    for (int i = 1; i < 11; i = i + 1)
                    {
                        string url = linkRaw + i + "&refresh=false&total=199";
                        webClient.Encoding = Encoding.UTF8;
                        var htmlString = webClient.DownloadString(url);
                        //Code lấy html từ một trang web//
                        var dulieuweb = new HtmlAgilityPack.HtmlDocument();
                        dulieuweb.LoadHtml(htmlString);
                        var htmlLayVe = dulieuweb.DocumentNode.SelectNodes("//a[@class='product-card']");
                        if (htmlLayVe != null)
                        {
                            foreach (var imgNode in htmlLayVe)
                            {
                                string srcValue = "https://www.pharmacity.vn" + imgNode.GetAttributeValue("href", string.Empty);
                                Debug.WriteLine("https://www.pharmacity.vn" + srcValue);
                                using (var webClient2 = new WebClient())
                                {
                                    webClient2.Encoding = Encoding.UTF8;
                                    var htmlString2 = webClient2.DownloadString(srcValue);
                                    //Code lấy html từ một trang web//
                                    var dulieuweb2 = new HtmlAgilityPack.HtmlDocument();
                                    dulieuweb2.LoadHtml(htmlString2);
                                    string anhSanPham = "";
                                    var anh = dulieuweb2.DocumentNode.SelectSingleNode("//img[@class='h-full w-full']");
                                    if (anh != null)
                                    {
                                        anhSanPham = anh.GetAttributeValue("src", string.Empty);

                                    }
                                    var priceNode = dulieuweb2.DocumentNode.SelectSingleNode("//h3[contains(@class, 'order-2') and contains(@class, 'text-primary-500')]");

                                    double giaSanPham = 0;
                                    if (priceNode != null)
                                    {
                                        giaSanPham = RemoveNonNumericCharacters(priceNode.InnerText);

                                    }



                                    var tenNode = dulieuweb2.DocumentNode.SelectSingleNode("//h1[contains(@class, 'line-clamp-3') and contains(@class, 'text-base')]");
                                    string tenSanPham = "";
                                    if (tenNode != null)
                                    {

                                        tenSanPham = tenNode.InnerText;

                                    }


                                    var divNode = dulieuweb2.DocumentNode.SelectSingleNode("//div[contains(@class, 'grid') and contains(@class, 'px-4') and contains(@class, 'md:px-0') and contains(@class, 'md:pb-2')]");
                                    string innerHtml = "";
                                    if (divNode != null)
                                    {

                                        innerHtml = divNode.InnerHtml;

                                    }


                                    goiDichVuTaoSanPham(anhSanPham, tenSanPham, giaSanPham, chuyenHtmlThanhText(innerHtml), "", "");
                                    Debug.WriteLine("danh + " + i + " " + anh + " " + giaSanPham.ToString() + " " + tenSanPham + " " + innerHtml);
                                }
                            }
                            /*for (int j = 0; j < htmlLayVe.Count; j++)
                            {
                                using (var webClient2 = new WebClient())
                                {
                                    webClient2.Encoding = Encoding.UTF8;
                                    var htmlString2 = webClient2.DownloadString(htmlLayVe[j].GetAttributeValue("href", ""));
                                    //Code lấy html từ một trang web//
                                    var dulieuweb2 = new HtmlAgilityPack.HtmlDocument();
                                    dulieuweb2.LoadHtml(htmlString2);
                                    var tenks = dulieuweb2.DocumentNode.SelectSingleNode("//a[@id='hp_hotel_name_reviews']");
                                    var latlong = dulieuweb2.DocumentNode.SelectSingleNode("//a[@id='hotel_address']");
                                    var diachi = dulieuweb2.DocumentNode.SelectSingleNode("//span[@data-source='top_link']");
                                    var img = dulieuweb2.DocumentNode.SelectSingleNode("//img[@class='hide']");

                                    if (tenks != null && !myDict.ContainsKey(tenks.InnerText))
                                    {
                                        // textBox1.Text += link[j].GetAttributeValue("href", "").Trim() + Environment.NewLine;
                                        textBox1.Text += tenks.InnerText.Trim() + "%" + htmlLayVe[j].GetAttributeValue("href", "").Trim() + "%" + diachi.InnerText.Trim() + "%" + latlong.GetAttributeValue("data-atlas-latlng", "").Trim() + "%" + img.GetAttributeValue("src", "").Trim() + Environment.NewLine;
                                        myDict.Add(tenks.InnerText, 0);
                                    }
                                }
                            }*/
                        }

                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(" button10_Click Lỗi: " + ex.Message);

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                string htmlContent = "<div class=\"pmc-content-html [&amp;_a:not(.ignore-css a)]:text-hyperLink max-w-[calc(100vw-32px)] overflow-auto md:max-w-none\"><p style=\"text-align: left;\"><strong><span style=\"vertical-align: inherit;\">Thành phần</span></strong> &nbsp;<br>Mỗi gói 5g chứa:<br>- Hoạt chất chính: Acid boric 4,35g; Phèn chua (kali nhôm sulfat) 0,6g; Berberin clorid 2mg.<br>- Tá dược: Methyl salicylat, thymol, phenol, menthol.</p><p style=\"text-align: left;\"><strong><span style=\"vertical-align: inherit;\">Chỉ định</span></strong><span style=\"vertical-align: inherit;\"> (Thuốc dùng cho bệnh gì?) <br>Vệ sinh và tẩy trùng niêm mạc phụ khoa.<br>Tẩy mùi hôi.<br>Trị huyết trắng, ngứa, viêm âm đạo hoặc bộ phận sinh dục nam hoặc nữ.</span></p><p style=\"text-align: left;\"><strong><span style=\"vertical-align: inherit;\">Chống chỉ định</span></strong><span style=\"vertical-align: inherit;\"> (Khi nào không nên dùng thuốc này?) <br></span>- Mẫn cảm với acid boric.<br>- Không dùng cho trẻ em.</p><p style=\"text-align: left;\"><strong>Liều dùng</strong> <br>Hòa tan 1 gói trong 1 lít nước ấm, dùng rửa ngoài hoặc bơm vào âm hộ.</p><p style=\"text-align: left;\"><strong>Tác dụng phụ<br></strong>- Tác dụng không mong muốn liên quan đến nhiễm độc acid boric cấp hay mạn như: Rối loạn tiêu hóa, buồn nôn, nôn, tiêu chảy. Ban đỏ, ngứa, kích ứng, rụng lông tóc. Kích thích hoặc ức chế thần kinh trung ương, có thể co giật, sốt. Rối loạn chức năng gan hay vàng da hiếm thấy.<br>- Acid boric thải trừ chậm nên có thể gây độc tính mạn (tích lũy) như: chán ăn, rối loạn tiêu hóa, suy nhược, lú lẫn, viêm da, rối loạn kinh nguyệt, thiếu máu,co giật, rụng tóc.<br><em>-</em>&nbsp;Tính mạng có thể bị đe dọa với trường hợp uống acid boric hoặc trẻ em bên lên vùng da bị trầy.<br>- Hít acid boric và borat có thể kích ứng phổi.<em><br>Thông báo cho bác sĩ tác dụng không mong muốn gặp phải khi dùng thuốc.</em></p><p><strong>Thận trọng</strong> (Những lưu ý khi dùng thuốc)<br>- Tránh để thuốc tiếp xúc với mắt, vùng da bị dị ứng.<br>- Không dùng nhiều lần trên một diện tích da rộng, không dùng lượng lớn thuốc lên các vết thương, vết bỏng, da bị mài mòn, da bị lột.<br>- Khi dùng cho trẻ em vì dễ nhạy cảm hơn người lớn.<br><em><span style=\"text-decoration: underline;\">- Thời kỳ mang thai:</span></em> Tránh dùng cho người mang thai. Chưa có thông tin nào nói về khả năng gây ngộ độc cho bào thai và người mang thai.<br><em><span style=\"text-decoration: underline;\">- Thời kỳ cho con bú:</span></em> Không có thông tin nào nói về độc tính của thuốc khi dùng trong thời kỳ cho con bú. Không nên bôi thuốc vùng quanh vú khi cho con bú.</p><p><strong>Tương tác thuốc</strong> (Những lưu ý khi dùng chung thuốc với thực phẩm hoặc thuốc khác)<br>Acid boric tương tác với các cacbonat, hydroxyd kiềm, benzalkonium clorid.</p><p><strong>Bảo quản:</strong> Nơi khô ráo, nhiệt độ không quá 30°C, tránh ánh sáng.<br>&nbsp; <br><strong>Đóng gói:&nbsp;</strong>Hộp 30 gói x 5g<br>&nbsp; <br><strong>Thương hiệu:</strong> Vidipha<br>&nbsp; <br><strong>Nơi sản xuất:</strong> Công ty cổ phần Dược Phẩm trung ương Vidipha (Việt Nam)<br>&nbsp; <br><em>Mọi thông tin trên đây chỉ mang tính chất tham khảo. Việc sử dụng thuốc phải tuân theo hướng dẫn của bác sĩ, dược sĩ.</em><br><em>Vui lòng đọc kĩ thông tin chi tiết ở tờ rơi bên trong hộp sản phẩm.</em></p></div>";

                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(htmlContent);

                // Extract the text from HTML
                string textOnly = doc.DocumentNode.InnerText;

                // Print the extracted text
                Console.WriteLine("Danhb " + textOnly);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Lỗi: " + ex.Message);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                string htmlContent = "<div class=\"pmc-content-html [&amp;_a:not(.ignore-css a)]:text-hyperLink max-w-[calc(100vw-32px)] overflow-auto md:max-w-none\"><p style=\"text-align: left;\"><strong><span style=\"vertical-align: inherit;\">Thành phần</span></strong> &nbsp;<br>Mỗi gói 5g chứa:<br>- Hoạt chất chính: Acid boric 4,35g; Phèn chua (kali nhôm sulfat) 0,6g; Berberin clorid 2mg.<br>- Tá dược: Methyl salicylat, thymol, phenol, menthol.</p><p style=\"text-align: left;\"><strong><span style=\"vertical-align: inherit;\">Chỉ định</span></strong><span style=\"vertical-align: inherit;\"> (Thuốc dùng cho bệnh gì?) <br>Vệ sinh và tẩy trùng niêm mạc phụ khoa.<br>Tẩy mùi hôi.<br>Trị huyết trắng, ngứa, viêm âm đạo hoặc bộ phận sinh dục nam hoặc nữ.</span></p><p style=\"text-align: left;\"><strong><span style=\"vertical-align: inherit;\">Chống chỉ định</span></strong><span style=\"vertical-align: inherit;\"> (Khi nào không nên dùng thuốc này?) <br></span>- Mẫn cảm với acid boric.<br>- Không dùng cho trẻ em.</p><p style=\"text-align: left;\"><strong>Liều dùng</strong> <br>Hòa tan 1 gói trong 1 lít nước ấm, dùng rửa ngoài hoặc bơm vào âm hộ.</p><p style=\"text-align: left;\"><strong>Tác dụng phụ<br></strong>- Tác dụng không mong muốn liên quan đến nhiễm độc acid boric cấp hay mạn như: Rối loạn tiêu hóa, buồn nôn, nôn, tiêu chảy. Ban đỏ, ngứa, kích ứng, rụng lông tóc. Kích thích hoặc ức chế thần kinh trung ương, có thể co giật, sốt. Rối loạn chức năng gan hay vàng da hiếm thấy.<br>- Acid boric thải trừ chậm nên có thể gây độc tính mạn (tích lũy) như: chán ăn, rối loạn tiêu hóa, suy nhược, lú lẫn, viêm da, rối loạn kinh nguyệt, thiếu máu,co giật, rụng tóc.<br><em>-</em>&nbsp;Tính mạng có thể bị đe dọa với trường hợp uống acid boric hoặc trẻ em bên lên vùng da bị trầy.<br>- Hít acid boric và borat có thể kích ứng phổi.<em><br>Thông báo cho bác sĩ tác dụng không mong muốn gặp phải khi dùng thuốc.</em></p><p><strong>Thận trọng</strong> (Những lưu ý khi dùng thuốc)<br>- Tránh để thuốc tiếp xúc với mắt, vùng da bị dị ứng.<br>- Không dùng nhiều lần trên một diện tích da rộng, không dùng lượng lớn thuốc lên các vết thương, vết bỏng, da bị mài mòn, da bị lột.<br>- Khi dùng cho trẻ em vì dễ nhạy cảm hơn người lớn.<br><em><span style=\"text-decoration: underline;\">- Thời kỳ mang thai:</span></em> Tránh dùng cho người mang thai. Chưa có thông tin nào nói về khả năng gây ngộ độc cho bào thai và người mang thai.<br><em><span style=\"text-decoration: underline;\">- Thời kỳ cho con bú:</span></em> Không có thông tin nào nói về độc tính của thuốc khi dùng trong thời kỳ cho con bú. Không nên bôi thuốc vùng quanh vú khi cho con bú.</p><p><strong>Tương tác thuốc</strong> (Những lưu ý khi dùng chung thuốc với thực phẩm hoặc thuốc khác)<br>Acid boric tương tác với các cacbonat, hydroxyd kiềm, benzalkonium clorid.</p><p><strong>Bảo quản:</strong> Nơi khô ráo, nhiệt độ không quá 30°C, tránh ánh sáng.<br>&nbsp; <br><strong>Đóng gói:&nbsp;</strong>Hộp 30 gói x 5g<br>&nbsp; <br><strong>Thương hiệu:</strong> Vidipha<br>&nbsp; <br><strong>Nơi sản xuất:</strong> Công ty cổ phần Dược Phẩm trung ương Vidipha (Việt Nam)<br>&nbsp; <br><em>Mọi thông tin trên đây chỉ mang tính chất tham khảo. Việc sử dụng thuốc phải tuân theo hướng dẫn của bác sĩ, dược sĩ.</em><br><em>Vui lòng đọc kĩ thông tin chi tiết ở tờ rơi bên trong hộp sản phẩm.</em></p></div>";

                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(htmlContent);

                // Replace <br> tags with newline characters before extracting text
                foreach (var br in doc.DocumentNode.SelectNodes("//br"))
                {
                    br.ParentNode.ReplaceChild(HtmlTextNode.CreateNode("\n"), br);
                }

                string textOnly = doc.DocumentNode.InnerText;

                // Print the extracted text
                Console.WriteLine(textOnly);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Lỗi: " + ex.Message);
            }
        }
        public static String chuyenHtmlThanhText(String htmlCanChuyen)
        {
            String kq = "";
            try
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(htmlCanChuyen);

                // Replace <br> tags with newline characters before extracting text
                var brNodes = doc.DocumentNode.SelectNodes("//br");
                if (brNodes != null)
                {
                    foreach (var br in brNodes)
                    {
                        br.ParentNode.ReplaceChild(HtmlTextNode.CreateNode("\n"), br);
                    }
                }

                // Decode HTML entities to text
                kq = HtmlEntity.DeEntitize(doc.DocumentNode.InnerText);
                // Replace non-breaking spaces with regular spaces
                kq = kq.Replace("&nbsp;", " ");

                // Handle other HTML tags like <p> and <div> if necessary
                var blockNodes = doc.DocumentNode.SelectNodes("//p | //div");
                if (blockNodes != null)
                {
                    foreach (var node in blockNodes)
                    {
                        if (node.InnerText.Trim() != "")
                        {
                            kq += "\n" + HtmlEntity.DeEntitize(node.InnerText.Trim());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
            return kq;
        }


        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                string html = "";

                suaSanPham u = new suaSanPham();
                u.trangThai = 1;
                u.productId = "1718357622918DoMmg";
                u.description = "<div style=\"text-align: left;\" class=\"pmc-content-html [&amp;_a:not(.ignore-css a)]:text-hyperLink max-w-[calc(100vw-32px)] overflow-auto md:max-w-none\"><p><strong>Thành phần</strong> <br>SERETIDE ACCUHALER 50/500mcg: Dụng cụ bằng nhựa dẻo (plastic) dạng khuôn chứa một vỉ nhôm với 60 túi phồng (blister) được phân bố đều đặn trên vỉ, mỗi túi phồng chứa 72,5 microgram salmeterol xinafoate (tương đương 50 microgram salmeterol) và 500 microgram fluticasone propionate.<br>Tá dược: Lactose (chứa protein sữa).<br><br><strong>Chỉ định</strong> (Thuốc dùng cho bệnh gì?)<br>Hen (Bệnh tắc nghẽn đường dẫn khí có hồi phục)<br>SERETIDE được chỉ định trong điều trị thường xuyên bệnh hen (bệnh tắc nghẽn đường dẫn khí có hồi phục). Bao gồm:<br>- Bệnh nhân đang điều trị có hiệu quả với liều duy trì của chất chủ vận β tác dụng dài và corticosteroid dạng hít.<br>- Bệnh nhân vẫn có triệu chứng khi đang điều trị bằng corticosteroid dạng hít.<br>- Bệnh nhân đang điều trị thường xuyên bằng thuốc giãn phế quản và cần dùng cả corticosteroid dạng hít.</p>\r\n<p>Bệnh phổi tắc nghẽn mạn tính (Chronic Obstructive Pulmonary Disease - COPD)<br>SERETIDE được chỉ định trong điều trị thường xuyên bệnh phổi tắc nghẽn mạn tính (COPD) bao gồm viêm phế quản mạn và khí phế thũng và đã được chứng minh làm giảm tỷ lệ tử vong do mọi nguyên nhân.<br><br><strong>Chống chỉ định</strong> (Khi nào không nên dùng thuốc này?)<br>Chống chỉ định SERETIDE cho bệnh nhân có tiền sử quá mẫn với bất cứ thành phần nào của thuốc (xem phần Tá dược).<br><br><strong>Liều dùng</strong> <br>SERETIDE Accuhaler chỉ dùng để hít qua miệng.<br>Cần cho bệnh nhân biết rằng phải dùng SERETIDE Accuhaler thường xuyên để đạt được lợi ích tối ưu, thậm chí ngay cả khi không có triệu chứng.<br>Bệnh nhân cần được bác sĩ tái khám đều đặn để hàm lượng SERETIDE mà bệnh nhân đang dùng luôn là tối ưu và chỉ thay đổi theo lời khuyên của bác sỹ.</p>\r\n<p>Hen (Bệnh tắc nghẽn đường dẫn khí có hồi phục)<br>- Nên điều chỉnh đến liều thấp nhất mà vẫn duy trì được việc kiểm soát triệu chứng một cách hiệu quả. Khi việc kiểm soát triệu chứng được duy trì bằng SERETIDE 2 lần/ngày thì nên chỉnh đến liều SERETIDE thấp nhất có hiệu quả là 1 lần/ngày.<br>- Nên cho bệnh nhân dùng dạng SERETIDE có hàm lượng fluticasone propionate phù hợp với mức độ nặng của bệnh.<br>- Nếu bệnh nhân không được kiểm soát đầy đủ với trị liệu corticosteroid dạng hít đơn thuần, thì việc điều trị thay thế bằng SERETIDE với liều corticosteroid tương đương về mặt điều trị có thể mang lại cải thiện trong việc kiểm soát hen. Đối với bệnh nhân có thể kiểm soát hen bằng corticosteroid dạng hít đơn thuần, điều trị thay thế bằng SERETIDE có thể cho phép giảm liều corticosteroid mà vẫn duy trì kiểm soát hen. Để biết thêm thông tin, xin hãy xem phần “Dược lực học'.<br>Liều đề nghị<br>- Người lớn và thanh thiếu niên từ 12 tuổi trở lên:<br>Một hít (50 microgram salmeterol và 100 microgram fluticasone propionate), 2 lần/ngày; hoặc một hít (50 microgram salmeterol và 250 microgram fluticasone propionate), 2 lần/ngày; hoặc một hít (50 microgram salmeterol và 500 microgram fluticasone propionate), 2 lần/ngày.<br>- Trẻ em từ 4 tuổi trở lên:<br>Một hít (50 microgram salmeterol và 100 microgram fluticasone propionate) 2 lần/ngày. Không có số liệu về sử dụng SERETIDE cho trẻ dưới 4 tuổi.</p>\r\n<p>Bệnh phổi tắc nghẽn mạn tính (COPD)<br>Liều đề nghị cho người lớn là một hít 50/250 microgram tới 50/500 microgram salmeterol/fluticasone propionate x 2 lần/ngày. Ở liều điều trị 50/500 microgram x 2 lần/ngày, SERETIDE đã được chứng minh làm giảm tỷ lệ tử vong do mọi nguyên nhân (xem Các nghiên cứu lâm sàng).</p>\r\n<p>Nhóm bệnh nhân đặc biệt<br>Không cần điều chỉnh liều cho bệnh nhân cao tuổi hoặc bệnh nhân suy thận hoặc suy gan.<br><br><strong>Tác dụng phụ</strong> <br>Các tác dụng không mong muốn liên quan tới các thành phần riêng rẽ, salmeterol xinafoate và fluticasone propionate, được liệt kê dưới đây. Không có thêm tác dụng không mong muốn được cho là do thuốc phối hợp khi so với hồ sơ tác dụng không mong muốn cùa các thành phần riêng rẽ.<br>Các tác dụng không mong muốn được liệt kê dưới đây theo hệ cơ quan và tần suất. Tần suất được định nghĩa như sau: rất phổ biến (≥1/10), phổ biến (≥1/100 và &lt;1/10), không phổ biến (≥1/1000 và &lt;1/100), hiếm (≥1/10.000 và &lt;1/1000) và rất hiếm (&lt;1/10.000). Phần lớn tần suất được xác định từ dữ liệu gộp thử nghiệm lâm sàng từ 23 nghiên cứu hen và 7 nghiên cứu COPD. Không phải tất cả các biến cố đều được báo cáo trong các thử nghiệm lâm sàng. Với những biến cố này, tần suất được tính dựa trên dữ liệu tự phát.</p>\r\n<p>Dữ liệu thử nghiệm lâm sàng<br>- Nhiễm trùng và nhiễm ký sinh trùng<br>Phổ biến: Nhiễm nấm Candida miệng và họng, viêm phổi (ở bệnh nhân mắc bệnh phổi tắc nghẽn mạn tính - COPD).<br>- Rối loạn hệ miễn dịch<br>Phản ứng quá mẫn:<br>Không phổ biến: Phản ứng quá mẫn trên da, khó thở.<br>Hiếm: Phản ứng phản vệ.<br>- Rối loạn nội tiết<br>Các tác động toàn thân có thể bao gồm (xem Cảnh báo và Thận trọng):<br>Không phổ biến: Đục thủy tinh thể.<br>Hiếm: Tăng nhãn áp (glaucoma).<br>- Rối loạn chuyển hóa và dinh dưỡng<br>Không phổ biến: Tăng đường huyết.<br>- Rối loạn tâm thần<br>Không phổ biến: Lo lắng, rối loạn giấc ngủ.<br>Hiếm: Thay đổi hành vi, bao gồm tăng hoạt động và kích thích (chủ yếu ở trẻ em).<br>- Rối loạn hệ thần kinh<br>Rất phổ biến: Đau đầu.<br>Không phổ biến: Run.<br>- Rối loạn tim<br>Không phổ biến: Đánh trống ngực (xem Cảnh báo và Thận trọng), nhịp tim nhanh, rung nhĩ.<br>Hiếm: Loạn nhịp tim bao gồm nhịp nhanh trên thất và ngoại tâm thu.<br>- Rối loạn hô hấp, lồng ngực và trung thất<br>Phổ biến: Khàn giọng/khàn tiếng.<br>Không phổ biến: Kích ứng họng.<br>- Rối loạn da và mô dưới da<br>Không phổ biến: Vết thâm tím.<br>- Rối loạn cơ xương và mô liên kết<br>Phổ biến: Chuột rút, đau khớp.</p>\r\n<p>Dữ liệu sau khi lưu hành thuốc<br>- Rối loạn hệ miễn dịch<br>Các phản ứng quá mẫn biểu hiện dưới dạng:<br>Hiếm: Phù mạch (chủ yếu phù mặt và miệng hầu) và co thắt phế quản.<br>- Rối loạn nội tiết<br>Các tác động toàn thân có thể bao gồm (xem Cảnh báo và Thận trọng).<br>Hiếm: Hội chứng Cushing, các dấu hiệu Cushing, ức chế thượng thận, chậm lớn ở trẻ em và thanh thiếu niên, giảm mật độ khoáng xương.<br>- Rối loạn hô hấp, lồng ngực và trung thất<br>Hiếm: Co thắt phế quản nghịch lý (xem Cảnh báo và Thận trọng).<br>*Thông báo cho bác sỹ những tác dụng không mong muốn gặp phải khi dùng thuốc.<br><br><strong>Thận trọng</strong> (Những lưu ý khi dùng thuốc)<br>SERETIDE Accuhaler không phải là thuốc dùng để giảm triệu chứng cấp tính, mà trong trường hợp này cần dùng một thuốc giãn phế quản tác dụng nhanh và ngắn (thí dụ salbutamol). Nên khuyên bệnh nhân luôn có sẵn thuốc giảm triệu chứng bên mình.<br>Tăng sử dụng thuốc giãn phế quản tác dụng ngắn để giảm triệu chứng cho thấy việc kiểm soát bệnh đang xấu đi và bệnh nhân nên đến bác sỹ để kiểm tra.<br>Kiểm soát hen xấu đi đột ngột và tăng dần là nguy cơ đe dọa mạng sống và bệnh nhân cần được bác sỹ khám lại. Nên cân nhắc tăng liều corticosteroid. Bệnh nhân cũng nên được khám lại khi liều SERETIDE đang dùng không đủ kiểm soát hen.<br>Không nên ngừng sử dụng SERETIDE một cách đột ngột ở bệnh nhân hen do nguy cơ bị cơn kịch phát, nên giảm liều từ từ dưới sự giám sát của bác sỹ. Đối với bệnh nhân COPD, ngừng điều trị có thể gây mất bù có triệu chứng và nên được bác sỹ theo dõi.<br>Đã có sự gia tăng số lượng báo cáo về viêm phổi trong những nghiên cứu trên bệnh nhân COPD dùng SERETIDE (xem Tác dụng không mong muốn). Bác sỹ nên luôn cảnh giác theo dõi khả năng xảy ra viêm phổi trên những bệnh nhân COPD vì các đặc điểm lâm sàng của viêm phổi và đợt kịch phát thường trùng lắp nhau.<br>Cũng như mọi thuốc hít chứa corticosteroid, nên thận trọng khi dùng SERETIDE ở bệnh nhân bị lao phổi thể hoạt động hoặc thể yên lặng.<br>Nên dùng SERETIDE thận trọng ở bệnh nhân bị nhiễm độc giáp.<br>Các tác động trên tim mạch như tăng huyết áp tâm thu và nhịp tim thỉnh thoảng có thể gặp ở tất cả các thuốc giống giao cảm, đặc biệt khi dùng liều cao hơn liều điều trị. Chính vì lý do này, nên sử dụng SERETIDE thận trọng ở bệnh nhân đang có sẵn bệnh tim mạch.<br>Giảm kali huyết thanh thoáng qua có thể xảy ra với tất cả các thuốc giống giao cảm tại liều cao hơn liều điều trị. Vì vậy, nên sử dụng thận trọng SERETIDE trên những bệnh nhân dễ có khả năng hạ nồng độ kali huyết thanh.<br>Tác động toàn thân có thể xảy ra với bất kỳ corticosteroid hít nào, nhất là khi dùng liều cao trong thời gian dài; những tác động này thường ít xảy ra hơn nhiều so với khi dùng corticosteroid dạng uống (xem phần Quá liều). Tác động toàn thân có thể bao gồm hội chứng Cushing, các dấu hiệu Cushing, ức chế thượng thận, chậm tăng trưởng ở trẻ em và thanh thiếu niên, giảm mật độ khoáng xương, đục thủy tinh thể và tăng nhãn áp (glaucoma). Do đó điều quan trọng đối với bệnh nhân hen là nên điều chỉnh liều corticosteroid đến liều thấp nhất mà vẫn duy trì kiểm soát bệnh hiệu quả.<br>Cần luôn nghĩ đến khả năng suy giảm đáp ứng thượng thận trong trường hợp cấp cứu và một số tình huống nhất định có thể gây stress và cân nhắc điều trị bằng corticosteroid thích hợp (xem phần Quá liều).<br>Nên kiểm tra thường xuyên chiều cao của trẻ khi điều trị bằng corticosteroid hít kéo dài.<br>Vì có khả năng đáp ứng thượng thận suy giảm, nên cần đặc biệt thận trọng khi chuyển bệnh nhân từ điều trị steroid uống sang điều trị fluticasone propionate hít, và cần kiểm tra chức năng tuyến thượng thận thường xuyên.<br>Theo những chỉ dẫn về fluticasone propionate hít, ngừng điều trị toàn thân cần thực hiện từ từ và bệnh nhân nên mang theo tấm thẻ cảnh báo về steroid chỉ rõ những điều có thể cần thiết cho điều trị bổ sung trong trường hợp khẩn cấp.<br>Có rất ít báo cáo về hiện tượng tăng mức đường huyết (xem phần Tác dụng không mong muốn) và cần thận trọng khi kê đơn cho những bệnh nhân có tiền sử bị đái tháo đường.<br>Trong thời gian sử dụng thuốc sau khi thuốc được lưu hành, đã có báo cáo về tương tác thuốc đáng kể trên lâm sàng ở những bệnh nhân dùng fluticasone propionate và ritonavir dẫn đến tác dụng toàn thân của corticosteroid bao gồm hội chứng Cushing và ức chế thượng thận. Do đó nên tránh dùng đồng thời fluticasone propionate và ritonavir trừ khi lợi ích điều trị vượt trội nguy cơ tác dụng phụ toàn thân của corticosteroid (xem mục Tương tác thuốc).<br>Dữ liệu từ một nghiên cứu lớn ở Mỹ (SMART) so sánh tính an toàn của Serevent (salmeterol - một thành phần của SERETIDE) hoặc giả dược được thêm vào liệu pháp thông thường cho thấy có sự tăng đáng kể số ca tử vong liên quan đến hen ở bệnh nhân dùng Serevent (salmeterol). Dữ liệu từ nghiên cứu này đã gợi ý cho thấy nhóm bệnh nhân người Mỹ gốc Phi có nguy cơ cao hơn bị các biến cố liên quan đến đường hô hấp nghiêm trọng hoặc tử vong khi sử dụng Serevent (salmeterol) so với nhóm giả dược, vẫn chưa biết liệu điều này là do các yếu tố dược di truyền hay các yếu tố khác. Nghiên cứu SMART không phải được thiết kế để xác định liệu việc sử dụng đồng thời corticosteroid hít có làm thay đổi nguy cơ tử vong liên quan đến hen hay không. (Xem mục Các nghiên cứu lâm sàng).<br>Trong một nghiên cứu về tương tác thuốc đã quan sát thấy việc sử dụng đồng thời ketoconazole đường toàn thân làm tăng nồng độ Serevent (salmeterol). Điều này dẫn đến kéo dài khoảng QTc. Nên thận trọng khi sử dụng các chất ức chế mạnh CYP3A4 (v.d ketoconazole) cùng với Serevent (salmeterol) (xem mục Tương tác).<br>Như những thuốc hít khác, co thắt phế quản nghịch lý có thể xuất hiện cùng với tăng khò khè ngay sau khi hít. Khi đó nên điều trị ngay lập tức bằng thuốc giãn phế quản dạng hít tác dụng nhanh và ngắn. Nên ngừng điều trị Salmeterol-Fluticasone Propionate Accuhaler hay Evohaler ngay, bệnh nhân nên được đánh giá và thay thế phương pháp điều trị nếu cần thiết (xem Tác dụng không mong muốn).<br>Đã có báo cáo về tác dụng không mong muốn dược lý của điều trị bằng chất chủ vận beta2, như đánh trống ngực chủ quan, nhưng có xu hướng thoáng qua và giảm khi điều trị thường xuyên (xem Tác dụng không mong muốn).</p>\r\n<p>Sử dụng cho phụ nữ có thai và cho con bú<br>- Chỉ nên cân nhắc dùng SERETIDE cho thai phụ và người mẹ đang cho con bú nếu lợi ích cho người mẹ lớn hơn bất cứ nguy cơ nào có thể xảy ra cho thai hoặc trẻ.<br>- Không đủ kinh nghiệm về sử dụng salmeterol xinafoate và fluticasone propionate ở người trong thời gian mang thai và cho con bú.<br>- Trong nghiên cứu độc tính đối với khả năng sinh sản ở động vật, dù dùng đơn trị liệu hay kết hợp, đều cho thấy có tác dụng đối với phôi thai khi dùng liều toàn thân rất cao của chất chủ vận thụ thể giao cảm beta2 mạnh và glucocorticosteroid.<br>- Kinh nghiệm lâm sàng rộng rãi với thuốc thuộc nhóm này không cho thấy bất cứ bằng chứng nào về các tác dụng của thuốc có tương quan với các liều điều trị. Cả salmeterol xinafoate lẫn fluticasone propionate đều không cho thấy bất cứ khả năng gây độc tính di truyền nào.<br>- Nồng độ salmeterol và fluticasone propionate trong huyết tương sau khi hít liều điều trị thì rất thấp và do vậy nồng độ trong sữa mẹ có khả năng cũng thấp tương ứng. Điều này được chứng minh trong những nghiên cứu trên động vật đang cho con bú, trong đó đã xác định được nồng độ thấp của thuốc trong sữa mẹ. Không có sẵn dữ liệu nghiên cứu cho sữa người.</p>\r\n<p>Tác động của thuốc lên khả năng lái xe và vận hành máy móc<br>Không có nghiên cứu chuyên biệt về ảnh hưởng của SERETIDE lên các hoạt động trên, nhưng dược lý học của cả hai dược chất này không cho thấy bất kỳ ảnh hưởng nào cả.</p>\r\n<p><strong>Tương tác thuốc</strong> (Những lưu ý khi dùng chung thuốc với thực phẩm hoặc thuốc khác)<br>Nên tránh dùng cả chất chẹn beta chọn lọc và không chọn lọc ở bệnh nhân trừ khi có lý do bắt buộc.<br>Trong điều kiện bình thường, do chuyển hóa bước đầu mạnh và thanh thải toàn thân cao qua trung gian cytochrome P450 3A4 tại ruột và gan nên nồng độ fluticasone propionate huyết tương đạt được thấp sau khi hít. Do đó ít gặp các tương tác thuốc đáng kể trên lâm sàng qua trung gian fluticasone propionate.<br>Trong một nghiên cứu về tương tác thuốc ở những người khỏe mạnh cho thấy rằng ritonavir (chất ức chế mạnh cytochrome P450 3A4) có thể gây tăng cao nồng độ fluticasone propionate trong huyết tương, kết quả là gây giảm đáng kể nồng độ cortisol trong huyết thanh. Trong thời gian sử dụng thuốc sau khi thuốc được lưu hành, đã có báo cáo về tương tác thuốc đáng kể trên lâm sàng ở những bệnh nhân dùng fluticasone propionate hít theo đường mũi hoặc miệng và ritonavir dẫn đến tác động toàn thân của corticosteroid bao gồm hội chứng Cushing và ức chế thượng thận. Do đó nên tránh dùng đồng thời fluticasone propionate và ritonavir trừ khi lợi ích điều trị vượt trội nguy cơ tác dụng phụ toàn thân của corticosteroid.<br>Các nghiên cứu cho thấy rằng các chất ức chế cytochrome P450 3A4 khác làm tăng không đáng kể (erythromycin) và tăng ít (ketoconazole) mức phơi nhiễm toàn thân với fluticasone propionate mà không làm giảm đáng kể nồng độ cortisol trong huyết thanh. Tuy nhiên nên thận trọng khi sử dụng đồng thời các chất ức chế mạnh cytochrome P450 3A4 (như ketoconazole) do khả năng tăng phơi nhiễm toàn thân với fluticasone propionate.<br>Sử dụng đồng thời ketoconazole và Serevent (salmeterol) làm tăng đáng kể nồng độ salmeterol trong huyết tương (Cmax tăng 1,4 lần và AUC tăng 15 lần) và điều này có thể gây kéo dài khoảng QTc (xem mục Cảnh báo và Thận trọng).<br>Trong một nghiên cứu bắt chéo có đối chứng với giả dược, để đánh giá tương tác giữa các thuốc trên 15 đối tượng khỏe mạnh, dùng đồng thời Serevent (salmeterol) (50 microgram 2 lần hít mỗi ngày) và chất ức chế CYP3A4 là ketoconazole (400mg một lần uống/ngày) trong 7 ngày - kết quả là làm tăng đáng kể nồng độ salmeterol huyết tương (Cmax tăng 1,4 lần; AUC tăng 15 lần). Không tăng tích lũy salmeterol khi dùng liều lặp lại. Có 3 đối tượng rút khỏi việc sử dụng đồng thời Serevent (salmeterol) và ketoconazole do kéo dài khoảng QTc hoặc đánh trống ngực với nhịp xoang nhanh. Trong 12 đối tượng còn lại, sử dụng đồng thời Serevent (salmeterol) và ketoconazole không gây ra tác động có ý nghĩa lâm sàng trên nhịp tim, nồng độ kali máu hoặc khoảng QTc. (xem phần Cảnh báo và Thận trọng).<br><br><strong>Bảo quản</strong>: Bảo quản nhiệt độ dưới 30°C. Bảo quản trong bao bì gốc<br><br><strong>Đóng gói</strong>: Hộp chứa 1 accuhaler 60 liều hít<br><br><strong>Thương hiệu</strong>: GlaxoSmithKline - gsk<br><br><strong>Nơi sản xuất</strong>: Glaxo Operations UK Limited (United Kingdom)<br><br><em>Mọi thông tin trên đây chỉ mang tính chất tham khảo. Việc sử dụng thuốc phải tuân theo hướng dẫn của bác sĩ, dược sĩ.</em><br><em>Vui lòng đọc kĩ thông tin chi tiết ở tờ rơi bên trong hộp sản phẩm.</em></p></div>";
                u.funcId = 41;
                u.sessionLogin = "454593A9433011638D81B658A25D2D5EMNSSMDWUXSG19848GXCXJXJCKKCQPPG";
                u.idLogin = "0966074236";
                u.token = "";
                u.typeLogin = 0;
                u.companyCode = "";
                u.timeRequest = FunCGeneral.timeNow();
                u.gia = 0;
                u.giaGiam = 0;
                u.idStore = "S1717215697810M5B469";
                u.idCate = "1717216259259ellec";
                u.productName = "Bột hít Seretide Accuhaler 50/250mcg điều trị hen, bệnh phổi tắc nghẽn mãn tính (hộp 60 liều)";
                u.image = "https://prod-cdn.pharmacity.io/e-com/images/ecommerce/1000x1000/P02088_1_l.webp";
                u.thumbnail = "";
                u.cksInput = FunctionGeneral.Md5("0819226669jdqpowrifioefiqo3289r79f" + u.sessionLogin + u.idLogin + u.typeLogin + u.timeRequest + u.idStore + u.idCate + u.image + RemoveDiacritics2(u.productName) + u.image + u.thumbnail + u.productId + u.trangThai + u.gia + u.giaGiam).ToLower();
                Debug.WriteLine("0819226669jdqpowrifioefiqo3289r79f" + u.sessionLogin + u.idLogin + u.typeLogin + u.timeRequest + u.idStore + u.idCate + u.image + u.idCate + RemoveDiacritics2(u.productName) + u.image + u.thumbnail + u.gia + u.giaGiam);
                string url = "https://pos.vimass.vn/VMServices/services/Vpos/requestCommand";
                var json = JsonConvert.SerializeObject(u);
                String res = Service.SendWebrequest_POST_Method(json, url);

                Debug.WriteLine(json);

                Response response = JsonConvert.DeserializeObject<Response>(res);
                if (response.msgCode == 1)
                {
                    Debug.WriteLine("đ");

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("button14_Click lỗi rồi " + ex.Message);

            }
        }

        private async Task button15_ClickAsync(object sender, EventArgs e)
        {
            using (var webClient = new WebClient())
            {


                try
                {
                    string nameJson = FunctionGeneral.pathJson + textBox2.Text + @".txt";
                    string filePath = @"D:\dataHTML2.txt";
                    string path = nameJson;
                    string DulieuUHF = FunctionGeneral.docThongTin(filePath);
                    // Create a HtmlDocument object from the HTML content
                    var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                    htmlDoc.LoadHtml(DulieuUHF);
                    // Lấy tất cả các thẻ img có class là 'product-image'
                    var imageNodes = htmlDoc.DocumentNode.SelectNodes("//img[@class='product-image']");

                    if (imageNodes != null)
                    {
                        foreach (var imgNode in imageNodes)
                        {
                            string srcValue = imgNode.GetAttributeValue("src", string.Empty);
                            srcValue = await goiDichVuTaoAnhAsync(srcValue);
                            string ten = imgNode.GetAttributeValue("alt", string.Empty);
                            Console.WriteLine("SRC: " + srcValue);
                            Console.WriteLine("ten: " + ten);
                            // Tìm thẻ div có class='product-card-two__Card-sc-1lvbgq2-0' chứa thẻ img
                            var productCard = imgNode.Ancestors("div")
                                .FirstOrDefault(div => div.GetAttributeValue("class", "").Contains("product-card-two__Card-sc-1lvbgq2-0"));
                            double price = 0;
                            if (productCard != null)
                            {
                                // Tìm thẻ div có class='product-card-two__Price-sc-1lvbgq2-9' là con của thẻ productCard
                                var priceNode = productCard.SelectSingleNode(".//div[contains(@class, 'product-card-two__Price-sc-1lvbgq2-9')]");

                                if (priceNode != null)
                                {
                                    // Lấy giá trị của thẻ div priceNode
                                    price = RemoveNonNumericCharacters(priceNode.InnerText.Trim());
                                    Console.WriteLine($"Giá sản phẩm: {price}");
                                }
                            }
                            goiDichVuTaoSanPham(srcValue, ten, price, "", "", "");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Không tìm thấy thẻ img nào với class 'product-image'.");
                    }

                }
                catch (Exception ex)
                {

                }

            }
        }

        private async void button15_Click(object sender, EventArgs e)
        {
            using (var webClient = new WebClient())
            {

                try
                {
                    string filePath = @"D:\" + textBox5.Text + @".txt";
                    Debug.WriteLine("filePath: " + filePath);
                    string DulieuUHF = FunctionGeneral.docThongTin(filePath);
                    // Create a HtmlDocument object from the HTML content
                    Debug.WriteLine("DulieuUHF: " + DulieuUHF);
                    var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                    htmlDoc.LoadHtml(DulieuUHF);

                    // Lấy tất cả các thẻ img có class là 'product-image'
                    var liNode = htmlDoc.DocumentNode.SelectNodes("//ul[contains(@class, 'listproduct')]/li[contains(@class, 'item')]");


                    if (liNode != null)
                    {

                        foreach (var li in liNode)
                        {
                            string anhSanPham = "";
                            int giaSanPham = 0;
                            string tenSanPham = "";

                            var thongTinSanPhamNode = li.SelectSingleNode(".//a[contains(@class, 'main-contain')]");
                            if (thongTinSanPhamNode != null)
                            {
                                tenSanPham = thongTinSanPhamNode.GetAttributeValue("data-name", string.Empty);
                                giaSanPham = int.Parse(thongTinSanPhamNode.GetAttributeValue("data-price", string.Empty).Split('.')[0]);

                            }

                            // Sử dụng .// để chỉ ra rằng cần tìm các thẻ img bên trong thẻ li hiện tại
                            var imgNodes = li.SelectSingleNode(".//img[1]");
                            if (imgNodes != null)
                            {
                                anhSanPham = imgNodes.GetAttributeValue("src", string.Empty);
                            }
                            else
                            {
                                Console.WriteLine("Không tìm thấy thẻ img phù hợp trong thẻ li này.");
                            }
                            Debug.WriteLine("anh: " + anhSanPham + " ten: " + tenSanPham + " gia: " + giaSanPham);
                            goiDichVuTaoSanPham(anhSanPham, tenSanPham, giaSanPham, "", "S1718695774890M7EE4C", textBox6.Text.Trim()); // Giả sử 'ten', 'price' được xác định từ đâu đó
                        }
                    }
                    else
                    {
                        Console.WriteLine("Không tìm thấy thẻ img nào với class 'product-image'.");
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("button15_Click Exception" + ex.Message);
                }

            }

        }



        private async void button16_Click(object sender, EventArgs e)
        {
            var url = "https://www.thegioididong.com/dong-ho-deo-tay#c=7264&o=14&pi=2";
            var httpClientHandler = new HttpClientHandler();

            // Nếu bạn cần sử dụng proxy, hãy cấu hình proxy ở đây
            // httpClientHandler.Proxy = new WebProxy("http://proxyaddress:port");
            // httpClientHandler.UseProxy = true;

            using (var httpClient = new HttpClient(httpClientHandler))
            {
                try
                {
                    var response = await httpClient.GetAsync(url);

                    // Kiểm tra mã trạng thái của phản hồi
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Lỗi: Yêu cầu HTTP thất bại với mã trạng thái {(int)response.StatusCode} ({response.ReasonPhrase}).");
                        return;
                    }

                    var html = await response.Content.ReadAsStringAsync();

                    var htmlDocument = new HtmlAgilityPack.HtmlDocument();
                    htmlDocument.LoadHtml(html);

                    // Bạn có thể thao tác với htmlDocument ở đây để lấy các phần thông tin cụ thể
                    // Ví dụ: Lấy tất cả các nút có thẻ a
                    var nodes = htmlDocument.DocumentNode.SelectNodes("//a");

                    if (nodes != null)
                    {
                        foreach (var node in nodes)
                        {
                            Console.WriteLine("Href: " + node.GetAttributeValue("href", ""));
                            Console.WriteLine("Text: " + node.InnerText);
                        }
                    }
                }
                catch (HttpRequestException httpEx)
                {
                    Console.WriteLine("Có lỗi xảy ra khi gửi yêu cầu HTTP: " + httpEx.Message);

                    // Kiểm tra các nguyên nhân phổ biến của HttpRequestException
                    if (httpEx.InnerException is SocketException socketEx)
                    {
                        switch (socketEx.SocketErrorCode)
                        {
                            case SocketError.HostNotFound:
                                Console.WriteLine("Lỗi: Không thể tìm thấy máy chủ.");
                                break;
                            case SocketError.ConnectionRefused:
                                Console.WriteLine("Lỗi: Kết nối bị từ chối.");
                                break;
                            case SocketError.TimedOut:
                                Console.WriteLine("Lỗi: Kết nối hết thời gian.");
                                break;
                            default:
                                Console.WriteLine("SocketException: " + socketEx.Message);
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("HttpRequestException: " + httpEx.Message);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Có lỗi xảy ra: " + ex.Message);
                }
            }

        }
        public static List<Doc> listBHX = new List<Doc>();
        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                string filePathBHX = @"D:\" + "BHX" + @".txt";
                string stringResBHX = FunctionGeneral.docThongTin(filePathBHX);

                ResponseBHX responseBHX = JsonConvert.DeserializeObject<ResponseBHX>(stringResBHX);


                foreach (var idCate in responseBHX.result.docs)
                {
                    string filePath = @"D:\" + idCate.id + @".txt";
                    Debug.WriteLine("filePath: " + filePath);
                    string DulieuHTML = FunctionGeneral.docThongTin(filePath);
                    // Create a HtmlDocument object from the HTML content

                    var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                    Debug.WriteLine("DulieuHTML: " + htmlDoc);
                    htmlDoc.LoadHtml(DulieuHTML);

                    // Lấy tất cả các thẻ div có class là 'this-item'
                    var divNode = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'this-item')]");


                    if (divNode != null)
                    {

                        foreach (var div in divNode)
                        {
                            string anhSanPham = "";
                            double giaSanPham = 0;
                            string tenSanPham = "";
                            var priceNode = div.SelectSingleNode(".//div[contains(@class, 'product_price')]");

                            if (priceNode != null)
                            {
                                // Lấy nội dung văn bản của node đầu tiên (chứa giá) trong thẻ div product_price
                                var priceText = priceNode.ChildNodes[0].InnerText;

                                // Loại bỏ các ký tự không phải số và dấu chấm
                                giaSanPham = RemoveNonNumericCharacters(priceText); // Giả sử hàm này loại bỏ ký tự '₫'

                            }


                            // Sử dụng .// để chỉ ra rằng cần tìm các thẻ img bên trong thẻ li hiện tại
                            var imgNodes = div.SelectSingleNode(".//img[1]");
                            if (imgNodes != null)
                            {
                                tenSanPham = imgNodes.GetAttributeValue("alt", string.Empty);
                                anhSanPham = imgNodes.GetAttributeValue("src", string.Empty);
                            }
                            else
                            {
                                Console.WriteLine("Không tìm thấy thẻ img phù hợp trong thẻ li này.");
                            }
                            Debug.WriteLine("anh: " + anhSanPham + " ten: " + tenSanPham + " gia: " + giaSanPham);
                            goiDichVuTaoSanPham(anhSanPham, tenSanPham, giaSanPham, "", "S1718770190720MCF22C", idCate.id); // Giả sử 'ten', 'price' được xác định từ đâu đó
                        }
                    }
                    else
                    {
                        Console.WriteLine("Không tìm thấy thẻ img nào với class 'product-image'.");
                    }
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine("button18_Click Exception" + ex.Message);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            try
            {

                IWebDriver driver = new ChromeDriver();

                // Mở trang web
                driver.Navigate().GoToUrl("https://fptshop.com.vn/phu-kien/mieng-dan-man-hinh");

                // Tìm và click vào thẻ
                int numberOfClicks = 1000;
                for (int i = 0; i < numberOfClicks; i++)
                {
                    IWebElement element = driver.FindElement(By.ClassName("txtbtnmore"));
                    try
                    {
                        element.Click();

                    }
                    catch (Exception ex)
                    {

                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("button19_Click Exception" + ex.Message);
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                string linkRaw = "https://www.pharmacity.vn/pharmacity?index=";
                using (var webClient = new WebClient()) // Khởi tạo WebClient
                {

                    string url = "https://fptshop.com.vn/may-hut-bui?sort=ban-chay-nhat&trang=3";
                    webClient.Encoding = Encoding.UTF8;
                    var htmlString = webClient.DownloadString(url);
                    //Code lấy html từ một trang web//
                    var dulieuweb = new HtmlAgilityPack.HtmlDocument();
                    dulieuweb.LoadHtml(htmlString);
                    var htmlLayVe = dulieuweb.DocumentNode.SelectNodes("//div[contains(@class, 'cdt-product')]");
                    if (htmlLayVe != null)
                    {
                        Debug.WriteLine("Danh đẹp trai");

                    }


                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(" button21_Click Lỗi: " + ex.Message);

            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            try
            {
                string filePathBHX = @"D:\" + "FPT" + @".txt";
                string stringResBHX = FunctionGeneral.docThongTin(filePathBHX);

                ResponseBHX responseBHX = JsonConvert.DeserializeObject<ResponseBHX>(stringResBHX);

                Parallel.ForEach(responseBHX.result.docs, (idCate) =>
                {
                    string filePath = @"D:\" + idCate.id + @".txt";
                    string DulieuHTML = "";
                    try
                    {
                        DulieuHTML = FunctionGeneral.docThongTin(filePath);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Exception Không tìm thấy file " + ex);
                    }

                    var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                    htmlDoc.LoadHtml(DulieuHTML);

                    var divNode = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'cdt-product__img')]");


                    if (divNode != null)
                    {
                        foreach (var div in divNode)
                        {
                            // Xử lý mỗi div trong một luồng riêng
                            string anhSanPham = "";
                            string tenSanPham = "";
                            double giaSanPham = 0;
                            string moTaSanPham = "";

                            var aNode = div.SelectSingleNode(".//a[1]");
                            if (aNode != null)
                            {
                                var imgNode = aNode.SelectSingleNode(".//img[1]");
                                if (imgNode != null)
                                {
                                    anhSanPham = imgNode.GetAttributeValue("src", string.Empty);
                                    tenSanPham = imgNode.GetAttributeValue("alt", string.Empty);
                                }
                                string url = "https://fptshop.com.vn/" + aNode.GetAttributeValue("href", string.Empty);

                                ChromeDriverService service = ChromeDriverService.CreateDefaultService(@"D:\chrom\chromedriver-win64");
                                ChromeOptions options = new ChromeOptions();
                                try
                                {
                                    using (IWebDriver driver = new ChromeDriver(service, options))
                                    {
                                        driver.Navigate().GoToUrl(url);
                                        Thread.Sleep(3000); // Đợi trang web tải xong

                                        var content = driver.PageSource;
                                        var dulieuweb = new HtmlAgilityPack.HtmlDocument();
                                        dulieuweb.LoadHtml(content);

                                        var moTaNode = dulieuweb.DocumentNode.SelectSingleNode("//table[contains(@class, 'st-pd-table') or contains(@class, 'table-striped')]");
                                        if (moTaNode != null)
                                        {
                                            moTaSanPham = moTaNode.OuterHtml;
                                        }

                                        var giaNode = dulieuweb.DocumentNode.SelectSingleNode("//div[contains(@class, 'st-price-main')]");
                                        if (giaNode != null)
                                        {
                                            giaSanPham = RemoveNonNumericCharacters(giaNode.InnerText);
                                        }

                                        goiDichVuTaoSanPham(anhSanPham, tenSanPham, giaSanPham, moTaSanPham, "S1718954238028M11534", idCate.id);

                                        Debug.WriteLine(idCate.id + " " + anhSanPham + " tensanpham " + tenSanPham + " gia" + giaSanPham + " mota" + moTaSanPham);

                                        driver.Quit();
                                    }
                                }
                                catch (Exception ex)
                                {

                                    Debug.WriteLine(ex.Message);

                                }

                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Không tìm thấy thẻ img nào với class 'product-image'.");
                    }
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine("button20_Click Exception" + ex.Message);
            }


        }

        private async void button20_Click_1(object sender, EventArgs e)
        {
            var url = "https://www.nhathuocankhang.com/bo-gan-thanh-nhiet/vitabiotics-liveril-hop-30-vien";
            var httpClientHandler = new HttpClientHandler();

            // Nếu bạn cần sử dụng proxy, hãy cấu hình proxy ở đây
            // httpClientHandler.Proxy = new WebProxy("http://proxyaddress:port");
            // httpClientHandler.UseProxy = true;

            using (var httpClient = new HttpClient(httpClientHandler))
            {
                try
                {
                    var response = await httpClient.GetAsync(url);

                    // Kiểm tra mã trạng thái của phản hồi
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Lỗi: Yêu cầu HTTP thất bại với mã trạng thái {(int)response.StatusCode} ({response.ReasonPhrase}).");
                        return;
                    }

                    var html = await response.Content.ReadAsStringAsync();

                    var htmlDocument = new HtmlAgilityPack.HtmlDocument();
                    htmlDocument.LoadHtml(html);

                    // Bạn có thể thao tác với htmlDocument ở đây để lấy các phần thông tin cụ thể
                    // Ví dụ: Lấy tất cả các nút có thẻ a
                    var contentDiv = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='info-article-fieldnews']/div[@class='content']");


                    if (contentDiv != null)
                    {
                        Debug.WriteLine("Html: " + contentDiv.InnerHtml);
                    }
                }
                catch (HttpRequestException httpEx)
                {
                    Console.WriteLine("Có lỗi xảy ra khi gửi yêu cầu HTTP: " + httpEx.Message);

                    // Kiểm tra các nguyên nhân phổ biến của HttpRequestException
                    if (httpEx.InnerException is SocketException socketEx)
                    {
                        switch (socketEx.SocketErrorCode)
                        {
                            case SocketError.HostNotFound:
                                Console.WriteLine("Lỗi: Không thể tìm thấy máy chủ.");
                                break;
                            case SocketError.ConnectionRefused:
                                Console.WriteLine("Lỗi: Kết nối bị từ chối.");
                                break;
                            case SocketError.TimedOut:
                                Console.WriteLine("Lỗi: Kết nối hết thời gian.");
                                break;
                            default:
                                Console.WriteLine("SocketException: " + socketEx.Message);
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("HttpRequestException: " + httpEx.Message);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Có lỗi xảy ra: " + ex.Message);
                }
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            try
            {
                string filePathBHX = @"D:\" + "ANKHANG" + @".txt";
                string stringResBHX = FunctionGeneral.docThongTin(filePathBHX);

                ResponseBHX responseBHX = JsonConvert.DeserializeObject<ResponseBHX>(stringResBHX);
                foreach (var idCate in responseBHX.result.docs)
                {
                    string filePath = @"D:\" + idCate.id + @".txt";
                    string DulieuHTML = "";
                    try
                    {
                        DulieuHTML = FunctionGeneral.docThongTin(filePath);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Exception Không tìm thấy file " + ex);
                    }

                    var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                    htmlDoc.LoadHtml(DulieuHTML);

                    var liNode = htmlDoc.DocumentNode.SelectNodes("//li[contains(@class, 'item') and contains(@class, 'oneUnit')]");


                    if (liNode != null)
                    {
                        foreach (var li in liNode)
                        {
                            // Xử lý mỗi div trong một luồng riêng
                            string anhSanPham = "";
                            string tenSanPham = "";
                            double giaSanPham = 0;
                            string moTaSanPham = "";
                            string url = "";

                            var aNode = li.SelectSingleNode(".//a[1]");


                            if (aNode != null)
                            {
                                url = "https://www.nhathuocankhang.com" + aNode.GetAttributeValue("href", string.Empty);
                                var imgNode = aNode.SelectSingleNode(".//img[1]");
                                var giaNode = aNode.SelectSingleNode(".//div[1]");
                                if (imgNode != null)
                                {
                                    anhSanPham = imgNode.GetAttributeValue("src", string.Empty);
                                    tenSanPham = imgNode.GetAttributeValue("alt", string.Empty);
                                }


                                ChromeDriverService service = ChromeDriverService.CreateDefaultService(@"D:\chrom\chromedriver-win64");
                                ChromeOptions options = new ChromeOptions();
                                try
                                {
                                    using (IWebDriver driver = new ChromeDriver(service, options))
                                    {
                                        driver.Navigate().GoToUrl(url);
                                        Thread.Sleep(3000); // Đợi trang web tải xong

                                        var content = driver.PageSource;
                                        var dulieuweb = new HtmlAgilityPack.HtmlDocument();
                                        dulieuweb.LoadHtml(content);
                                        var contentDiv = dulieuweb.DocumentNode.SelectSingleNode("//div[@class='info-article-fieldnews']/div[@class='content']");
                                        if (contentDiv != null)
                                        {
                                            moTaSanPham = contentDiv.OuterHtml;
                                        }


                                        if (giaNode != null)
                                        {
                                            if (giaNode.GetAttributeValue("data-price", string.Empty) != null && giaNode.GetAttributeValue("data-price", string.Empty).IndexOf('.') > -1)
                                            {
                                                giaSanPham = RemoveNonNumericCharacters(giaNode.GetAttributeValue("data-price", string.Empty).Split('.')[0]);
                                            }
                                            else
                                            {
                                                giaSanPham = RemoveNonNumericCharacters(giaNode.GetAttributeValue("data-price", string.Empty));
                                            }

                                        }

                                        goiDichVuTaoSanPham(anhSanPham, tenSanPham, giaSanPham, moTaSanPham, "S1719203776163M1E622", idCate.id);

                                        Debug.WriteLine(idCate.id + " " + anhSanPham + " tensanpham: " + tenSanPham + " gia: " + giaSanPham + " mota");

                                        driver.Quit();
                                    }
                                }
                                catch (Exception ex)
                                {

                                    Debug.WriteLine(ex.Message);

                                }

                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Không tìm thấy thẻ img nào với class 'product-image'.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("button20_Click Exception" + ex.Message);
            }
        }
        private async void layThongTinSanPham(string link)
        {
            var httpClientHandler = new HttpClientHandler();

            using (var httpClient = new HttpClient(httpClientHandler))
            {
                var retryPolicy = Policy
                    .HandleResult<HttpResponseMessage>(dresponse => dresponse.StatusCode == (HttpStatusCode)429)
                    .WaitAndRetryAsync(3, retryAttempt =>
                        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), // Exponential back-off
                        onRetry: (outcome, timespan, retryAttempt, context) =>
                        {
                            Console.WriteLine($"Request failed with {outcome.Result.StatusCode}. Waiting {timespan} before next retry. Retry attempt {retryAttempt}.");
                        });

                HttpResponseMessage response = null;
                try
                {
                    response = await retryPolicy.ExecuteAsync(() => httpClient.GetAsync(link));

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Lỗi: Yêu cầu HTTP thất bại với mã trạng thái {(int)response.StatusCode} ({response.ReasonPhrase}).");
                        if (response.StatusCode == (HttpStatusCode)429)
                        {
                            Console.WriteLine("Bạn đang gửi quá nhiều yêu cầu. Vui lòng thử lại sau.");
                        }
                        return;
                    }

                    var html = await response.Content.ReadAsStringAsync();
                    var htmlDocument = new HtmlAgilityPack.HtmlDocument();
                    htmlDocument.LoadHtml(html);
                    // Bạn có thể thao tác với htmlDocument ở đây để lấy các phần thông tin cụ thể
                    // Ví dụ: Lấy tất cả các nút có thẻ a
                    var contentDiv = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='info-article-fieldnews']/div[@class='content']");


                    if (contentDiv != null)
                    {
                        Debug.WriteLine("Html: " + contentDiv.InnerHtml);
                    }

                }
                catch (HttpRequestException httpEx)
                {
                    Console.WriteLine("Có lỗi xảy ra khi gửi yêu cầu HTTP: " + httpEx.Message);

                    // Kiểm tra các nguyên nhân phổ biến của HttpRequestException
                    if (httpEx.InnerException is SocketException socketEx)
                    {
                        switch (socketEx.SocketErrorCode)
                        {
                            case SocketError.HostNotFound:
                                Console.WriteLine("Lỗi: Không thể tìm thấy máy chủ.");
                                break;
                            case SocketError.ConnectionRefused:
                                Console.WriteLine("Lỗi: Kết nối bị từ chối.");
                                break;
                            case SocketError.TimedOut:
                                Console.WriteLine("Lỗi: Kết nối hết thời gian.");
                                break;
                            default:
                                Console.WriteLine("SocketException: " + socketEx.Message);
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("HttpRequestException: " + httpEx.Message);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Có lỗi xảy ra: " + ex.Message);
                }
            }
        }

        private async void button27_Click(object sender, EventArgs e)
        {
            try
            {
                string filePathBHX = @"D:\" + "lotte" + @".txt";
                string stringResBHX = FunctionGeneral.docThongTin(filePathBHX);

                ResponseBHX responseBHX = JsonConvert.DeserializeObject<ResponseBHX>(stringResBHX);
                foreach (var idCate in responseBHX.result.docs)
                {
                    string filePath = @"D:\" + idCate.id + @".txt";
                    string DulieuHTML = "";
                    try
                    {
                        DulieuHTML = FunctionGeneral.docThongTin(filePath);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Exception Không tìm thấy file " + ex);
                    }

                    var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                    htmlDoc.LoadHtml(DulieuHTML);

                    var divNode = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'field-img')]");


                    if (divNode != null)
                    {
                        foreach (var li in divNode)
                        {
                            // Xử lý mỗi div trong một luồng riêng
                            string anhSanPham = "";
                            string tenSanPham = "";
                            double giaSanPham = 0;
                            string moTaSanPham = "";
                            string url = "";

                            var imgNode = li.SelectSingleNode(".//img[1]");



                            if (imgNode != null)
                            {
                                anhSanPham = await goiDichVuTaoAnhAsync(imgNode.GetAttributeValue("src", string.Empty));

                                var giaNode = htmlDoc.DocumentNode.SelectSingleNode("//p[contains(@class, 'price')]");
                                if (giaNode != null)
                                {
                                    tenSanPham = imgNode.GetAttributeValue("alt", string.Empty);
                                    giaSanPham = RemoveNonNumericCharacters(giaNode.InnerText);

                                }

                                goiDichVuTaoSanPham(anhSanPham, tenSanPham, giaSanPham, moTaSanPham, "S1719287159244MF1E14", idCate.id);

                                Debug.WriteLine(idCate.id + " " + anhSanPham + " tensanpham: " + tenSanPham + " gia: " + giaSanPham + " mota");


                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Không tìm thấy thẻ img nào với class 'product-image'.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("button20_Click Exception" + ex.Message);
            }
        }

        private async void button30_Click(object sender, EventArgs e)
        {
            try
            {
                string filePathBHX = @"D:\" + "trungnguyen" + @".txt";
                string stringResBHX = FunctionGeneral.docThongTin(filePathBHX);

                ResponseBHX responseBHX = JsonConvert.DeserializeObject<ResponseBHX>(stringResBHX);
                foreach (var idCate in responseBHX.result.docs)
                {
                    string filePath = @"D:\" + idCate.id + @".txt";
                    string DulieuHTML = "";
                    try
                    {
                        DulieuHTML = FunctionGeneral.docThongTin(filePath);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Exception Không tìm thấy file " + ex);
                    }

                    var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                    htmlDoc.LoadHtml(DulieuHTML);

                    var liNode = htmlDoc.DocumentNode.SelectNodes("//li[contains(@class, 'product-item')]");


                    if (liNode != null)
                    {
                        foreach (var li in liNode)
                        {
                            // Xử lý mỗi div trong một luồng riêng
                            string anhSanPham = "";
                            string tenSanPham = "";
                            double giaSanPham = 0;
                            string moTaSanPham = "";
                            string url = "";

                            var aNode = li.SelectSingleNode(".//a[1]");


                            if (aNode != null)
                            {
                                var imgNode = aNode.SelectSingleNode(".//img[1]");
                                var giaNode = li.SelectSingleNode(".//span[contains(@class, 'price')]");
                                if (imgNode != null)
                                {
                                    // anhSanPham = await goiDichVuTaoAnhAsync(imgNode.GetAttributeValue("src", string.Empty)) ;
                                    anhSanPham = imgNode.GetAttributeValue("src", string.Empty);
                                    tenSanPham = imgNode.GetAttributeValue("alt", string.Empty).Replace("&amp;", "").Replace("&quot;", "");
                                    giaSanPham = RemoveNonNumericCharacters(giaNode.InnerText);
                                }
                                goiDichVuTaoSanPham(anhSanPham, tenSanPham, giaSanPham, moTaSanPham, "S1719289440556M368A7", idCate.id);

                                Debug.WriteLine(idCate.id + " " + anhSanPham + " tensanpham: " + tenSanPham + " gia: " + giaSanPham + " mota");


                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Không tìm thấy thẻ img nào với class 'product-image'.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("button20_Click Exception" + ex.Message);
            }
        }

        private async void button33_Click(object sender, EventArgs e)
        {
            try
            {
                string filePathBHX = @"D:\" + "DMX" + @".txt";
                string stringResBHX = FunctionGeneral.docThongTin(filePathBHX);

                ResponseBHX responseBHX = JsonConvert.DeserializeObject<ResponseBHX>(stringResBHX);
                foreach (var idCate in responseBHX.result.docs)
                {
                    string filePath = @"D:\" + idCate.id + @".txt";
                    string DulieuHTML = "";
                    try
                    {
                        DulieuHTML = FunctionGeneral.docThongTin(filePath);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Exception Không tìm thấy file " + ex);
                    }

                    var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                    htmlDoc.LoadHtml(DulieuHTML);

                    var liNode = htmlDoc.DocumentNode.SelectNodes("//li[contains(@class, 'item')]");


                    if (liNode != null)
                    {
                        foreach (var li in liNode)
                        {
                            // Xử lý mỗi div trong một luồng riêng
                            string anhSanPham = "";
                            string tenSanPham = "";
                            double giaSanPham = 0;
                            string moTaSanPham = "";
                            string url = "";

                            var aNode = li.SelectSingleNode(".//a[1]");


                            if (aNode != null)
                            {
                                url = "https://www.dienmayxanh.com" + aNode.GetAttributeValue("href", string.Empty);

                                tenSanPham = aNode.GetAttributeValue("data-name", string.Empty);


                                if (aNode.GetAttributeValue("data-price", string.Empty) != null && aNode.GetAttributeValue("data-price", string.Empty).IndexOf('.') > -1)
                                {
                                    giaSanPham = RemoveNonNumericCharacters(aNode.GetAttributeValue("data-price", string.Empty).Split('.')[0]);
                                }
                                else
                                {
                                    giaSanPham = RemoveNonNumericCharacters(aNode.GetAttributeValue("data-price", string.Empty));
                                }

                                ChromeDriverService service = ChromeDriverService.CreateDefaultService(@"D:\chrom\chromedriver-win64");
                                ChromeOptions options = new ChromeOptions();
                                try
                                {
                                    using (IWebDriver driver = new ChromeDriver(service, options))
                                    {
                                        driver.Navigate().GoToUrl(url);
                                        Thread.Sleep(3000); // Đợi trang web tải xong

                                        var content = driver.PageSource;
                                        var dulieuweb = new HtmlAgilityPack.HtmlDocument();
                                        dulieuweb.LoadHtml(content);

                                        var imgNodes = dulieuweb.DocumentNode.SelectSingleNode("//div[contains(@class, 'box_left')]"); ;
                                        if (imgNodes != null)
                                        {
                                            var imgNode = imgNodes.SelectSingleNode(".//img[1]");
                                            if (imgNode != null)
                                            {
                                                // anhSanPham = await goiDichVuTaoAnhAsync(imgNode.GetAttributeValue("src", string.Empty));
                                                if (imgNode.GetAttributeValue("src", string.Empty) != null && imgNode.GetAttributeValue("src", string.Empty).IndexOf("https") > -1)
                                                {
                                                    anhSanPham = imgNode.GetAttributeValue("src", string.Empty);

                                                }
                                                else
                                                {
                                                    anhSanPham = "https:" + imgNode.GetAttributeValue("src", string.Empty);
                                                }

                                            }
                                        }
                                        var motaNode = dulieuweb.DocumentNode.SelectSingleNode("//ul[contains(@class, 'parameter__list')]"); ;
                                        if (motaNode != null)
                                        {
                                            moTaSanPham = motaNode.OuterHtml;

                                        }

                                        goiDichVuTaoSanPham(anhSanPham, tenSanPham, giaSanPham, moTaSanPham, "S1719398454988M4C439", idCate.id);

                                        Debug.WriteLine(idCate.id + " " + anhSanPham + " tensanpham: " + tenSanPham + " gia: " + giaSanPham + " mota" + moTaSanPham);

                                        driver.Quit();
                                    }
                                }
                                catch (Exception ex)
                                {

                                    Debug.WriteLine(ex.Message);

                                }

                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Không tìm thấy thẻ img nào với class 'item'.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("button20_Click Exception" + ex.Message);
            }
        }

        private async void button36_Click(object sender, EventArgs e)
        {
            try
            {
                string filePathBHX = @"D:\" + "GG" + @".txt";
                string stringResBHX = FunctionGeneral.docThongTin(filePathBHX);

                ResponseBHX responseBHX = JsonConvert.DeserializeObject<ResponseBHX>(stringResBHX);
                foreach (var idCate in responseBHX.result.docs)
                {
                    string filePath = @"D:\" + idCate.id + @".txt";
                    string DulieuHTML = "";
                    try
                    {
                        DulieuHTML = FunctionGeneral.docThongTin(filePath);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Exception Không tìm thấy file " + ex);
                    }

                    var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                    htmlDoc.LoadHtml(DulieuHTML);

                    var divNode = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'css-product')]");


                    if (divNode != null)
                    {
                        foreach (var li in divNode)
                        {
                            // Xử lý mỗi div trong một luồng riêng
                            string anhSanPham = "";
                            string tenSanPham = "";
                            double giaSanPham = 0;
                            string moTaSanPham = "";
                            string url = "";

                            var imgNode = li.SelectSingleNode(".//img[1]");
                            var tenNode = li.SelectSingleNode(".//p[1]");
                            var giaNode = li.SelectSingleNode(".//p[2]");


                            if (imgNode != null)
                            {
                                anhSanPham = await goiDichVuTaoAnhAsync(imgNode.GetAttributeValue("src", string.Empty));
                                // anhSanPham = imgNode.GetAttributeValue("src", string.Empty);
                                tenSanPham = tenNode.InnerText;

                                giaSanPham = RemoveNonNumericCharacters(giaNode.InnerText);



                                goiDichVuTaoSanPham(anhSanPham, tenSanPham, giaSanPham, moTaSanPham, "S1719821781365MD411F", idCate.id);

                                Debug.WriteLine(idCate.id + " " + anhSanPham + " tensanpham: " + tenSanPham + " gia: " + giaSanPham + " mota" + moTaSanPham);




                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Không tìm thấy thẻ img nào với class 'item'.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("button20_Click Exception" + ex.Message);
            }
        }

        private async void button39_Click(object sender, EventArgs e)
        {
            try
            {
                string filePathBHX = @"D:\VimassCrawler\adidas.txt";
                string stringResBHX = FunctionGeneral.docThongTin(filePathBHX);

                ResponseBHX responseBHX = JsonConvert.DeserializeObject<ResponseBHX>(stringResBHX);
                foreach (var idCate in responseBHX.result.docs)
                {
                    switch (idCate.id)
                    {
                        case "1720437119417YMpRV":
                            FunctionGeneral.writeFile(@"D:\AdidasLog.txt", idCate.catName);
                            layDuLieuAdidas("https://www.adidas.com.vn/vi/nam?grid=true&start=", 2496, idCate.id);
                            break;
                        case "1720437124245CEH3O":
                            FunctionGeneral.writeFile(@"D:\AdidasLog.txt", idCate.catName);
                            layDuLieuAdidas("https://www.adidas.com.vn/vi/nu?grid=true&start=", 2016, idCate.id);
                            break;
                        case "17204371287453fObO":
                            FunctionGeneral.writeFile(@"D:\AdidasLog.txt", idCate.catName);
                            layDuLieuAdidas("https://www.adidas.com.vn/vi/tre_em?grid=true&start=", 336, idCate.id);
                            break;
                        case "1720437135214g25Jj":
                            FunctionGeneral.writeFile(@"D:\AdidasLog.txt", idCate.catName);
                            layDuLieuAdidas("https://www.adidas.com.vn/vi/bong_da?grid=true&start=", 192, idCate.id);
                            break;
                        case "1720496352346cHtfc":
                            FunctionGeneral.writeFile(@"D:\AdidasLog.txt", idCate.catName);
                            layDuLieuAdidas("https://www.adidas.com.vn/vi/chay?grid=true&start=", 528, idCate.id);
                            break;
                        case "1720496361908zDuoH":
                            FunctionGeneral.writeFile(@"D:\AdidasLog.txt", idCate.catName);
                            layDuLieuAdidas("https://www.adidas.com.vn/vi/bong_ro?grid=true&start=", 144, idCate.id);
                            break;
                        case "1720496373986KuKst":
                            FunctionGeneral.writeFile(@"D:\AdidasLog.txt", idCate.catName);
                            layDuLieuAdidas("https://www.adidas.com.vn/vi/tap_luyen?grid=true&start=", 384, idCate.id);
                            break;
                        case "1720496387642GqO4l":
                            FunctionGeneral.writeFile(@"D:\AdidasLog.txt", idCate.catName);
                            layDuLieuAdidas("https://www.adidas.com.vn/vi/danh_gon?grid=true&start=", 432, idCate.id);
                            break;
                        case "1720496431322Vq89i":
                            FunctionGeneral.writeFile(@"D:\AdidasLog.txt", idCate.catName);
                            layDuLieuAdidas("https://www.adidas.com.vn/vi/ngoai_troi?start=", 48, idCate.id);
                            break;
                        case "1720496440494asqXQ":
                            FunctionGeneral.writeFile(@"D:\AdidasLog.txt", idCate.catName);
                            layDuLieuAdidas("https://www.adidas.com.vn/vi/quan_vot?grid=true&start=", 96, idCate.id);
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("button20_Click Exception" + ex.Message);
            }
        }

        private async void layDuLieuAdidas(string urlx, int gioiHan, string idCate)
        {
            try
            {
                for (int i = 2112; i <= gioiHan; i = i + 48)
                {

                    string url = urlx + i;

                    ChromeDriverService service = ChromeDriverService.CreateDefaultService(@"D:\chrom\chromedriver-win64");
                    ChromeOptions options = new ChromeOptions();
                    // Thiết lập thời gian chờ ngầm định là 30 giây

                    try
                    {
                        var dulieuweb = new HtmlAgilityPack.HtmlDocument();
                        using (IWebDriver driver = new ChromeDriver(service, options))
                        {
                            driver.Navigate().GoToUrl(url);
                            Thread.Sleep(3000); // Đợi trang web tải xong

                            var content = driver.PageSource;

                            dulieuweb.LoadHtml(content);

                            driver.Quit();


                        }
                        if (dulieuweb != null)
                        {
                            var divNodeCon = dulieuweb.DocumentNode.SelectNodes("//div[contains(@class, 'grid-item')]");

                            if (divNodeCon != null)
                            {
                                foreach (var divSanPham in divNodeCon)
                                {
                                    string anhSanPham = "";
                                    string tenSanPham = "";
                                    double giaSanPham = 0;
                                    string moTaSanPham = "";
                                    string linkSanPham = "";
                                    giaVaMoTaAdidas u = new giaVaMoTaAdidas();
                                    var aNode = divSanPham.SelectSingleNode(".//a[contains(@class, 'glass-product-card__assets-link')]");
                                    if (aNode != null)
                                    {
                                        linkSanPham = aNode.GetAttributeValue("href", string.Empty);
                                        var imgNodes = aNode.SelectSingleNode(".//img[1]");
                                        if (imgNodes != null)
                                        {
                                            anhSanPham = await goiDichVuTaoAnhAsync(imgNodes.GetAttributeValue("src", string.Empty).Trim());
                                        }
                                        u = layGiaSanPhamAdidas("https://www.adidas.com.vn" + linkSanPham);

                                    }
                                    var tenNode = divSanPham.SelectSingleNode(".//p[contains(@class, 'glass-product-card__title')]");
                                    if (tenNode != null)
                                    {
                                        tenSanPham = tenNode.InnerText;
                                    }
                                    FunctionGeneral.writeFile(@"D:\AdidasLog.txt", tenSanPham + " : " + u.gia + " đồng");
                                    goiDichVuTaoSanPham(anhSanPham, tenSanPham, u.gia, u.moTa, "S1720437099513M857D6", idCate);
                                    Debug.WriteLine(idCate + " " + anhSanPham + " tensanpham: " + tenSanPham + " gia: " + u.gia + " mota" + u.moTa);
                                    Thread.Sleep(200);

                                }
                            }

                        }

                    }
                    catch (Exception ex)
                    {

                        Debug.WriteLine(ex.Message);

                    }


                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("layDuLieuAdidas Exception" + ex.Message);
            }
        }

        private giaVaMoTaAdidas layGiaSanPhamAdidas(string url)
        {
            giaVaMoTaAdidas u = new giaVaMoTaAdidas();
            double giaSanPham = 0;
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(@"D:\chrom\chromedriver-win64");
            ChromeOptions options = new ChromeOptions();
            try
            {
                using (IWebDriver driver = new ChromeDriver(service, options))
                {
                    driver.Navigate().GoToUrl(url);
                    // Cuộn trang xuống cuối cùng
                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                    js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");

                    // Chờ đợi một chút để thấy kết quả (chỉ dùng cho mục đích demo)
                    System.Threading.Thread.Sleep(2000);
                    var content = driver.PageSource;
                    var dulieuweb = new HtmlAgilityPack.HtmlDocument();
                    dulieuweb.LoadHtml(content);

                    var giaNode = dulieuweb.DocumentNode.SelectSingleNode(".//div[contains(@class, 'gl-price-item')]");
                    if (giaNode != null)
                    {
                        u.gia = RemoveNonNumericCharacters(giaNode.InnerText);
                    }
                    var motaNode = dulieuweb.DocumentNode.SelectSingleNode(".//div[contains(@class, 'bullets___3bsSs')]");
                    if (motaNode != null)
                    {
                        u.moTa = motaNode.OuterHtml;
                    }


                    driver.Quit();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

            }
            return u;
        }

        static async Task<string> GetHtmlAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                // Gửi yêu cầu HTTP GET
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode(); // Đảm bảo phản hồi thành công

                // Đọc nội dung phản hồi dưới dạng chuỗi
                string htmlBody = await response.Content.ReadAsStringAsync();
                return htmlBody;
            }
        }

        private void button40_Click(object sender, EventArgs e)
        {
            using (SpeechSynthesizer synthesizer = new SpeechSynthesizer())
            {
                // Chọn giọng nói (Nếu có giọng tiếng Việt sẵn có)
                synthesizer.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);

                // Đọc văn bản
                synthesizer.Speak("Xin chào! 50.000 đồng");

            }
        }

        private async void button43_Click(object sender, EventArgs e)
        {

            try
            {
                string filePathBHX = @"D:\VimassCrawler\" + "milano" + @".txt";
                string stringResBHX = FunctionGeneral.docThongTin(filePathBHX);

                ResponseBHX responseBHX = JsonConvert.DeserializeObject<ResponseBHX>(stringResBHX);
                foreach (var idCate in responseBHX.result.docs)
                {
                    string filePath = @"D:\VimassCrawler\" + idCate.id + @".txt";
                    string DulieuHTML = "";
                    try
                    {
                        DulieuHTML = FunctionGeneral.docThongTin(filePath);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Exception Không tìm thấy file " + ex);
                    }

                    var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                    htmlDoc.LoadHtml(DulieuHTML);

                    var divNode = htmlDoc.DocumentNode.SelectNodes("//img[contains(@type, 'product')]");


                    if (divNode != null)
                    {
                        foreach (var li in divNode)
                        {
                            // Xử lý mỗi div trong một luồng riêng
                            string anhSanPham = "";
                            string tenSanPham = "";
                            double giaSanPham = 0;
                            string moTaSanPham = "";
                            string url = "";

                            //var imgNode = li.SelectSingleNode(".//img[1]");



                          
                                anhSanPham = await goiDichVuTaoAnhAsync(li.GetAttributeValue("src", string.Empty));
                                // anhSanPham = imgNode.GetAttributeValue("src", string.Empty);
                                tenSanPham = li.GetAttributeValue("alt", string.Empty);

                                giaSanPham = 0;



                                goiDichVuTaoSanPham(anhSanPham, tenSanPham, giaSanPham, moTaSanPham, "S1720694089086MAC197", idCate.id);

                                Debug.WriteLine(idCate.id + " " + anhSanPham + " tensanpham: " + tenSanPham + " gia: " + giaSanPham + " mota" + moTaSanPham);




                            
                        }
                    }
                    else
                    {
                        Console.WriteLine("Không tìm thấy thẻ img nào với class 'item'.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("button20_Click Exception" + ex.Message);
            }

        }
    }

    public class giaVaMoTaAdidas
    {
        public double gia { get; set; }
        public String moTa { get; set; }
    }
}
