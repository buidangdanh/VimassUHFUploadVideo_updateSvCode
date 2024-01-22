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

        private void button4_Click(object sender, EventArgs e)
        {

            /*  using (var webClient = new WebClient())
              {


                  try
                  {
                      string nameJson = FunctionGeneral.pathJson + textBox2.Text + @".txt";

                      string path = nameJson;
                      string  DulieuUHF = FunctionGeneral.docThongTin(path);
                      var datauhf = JsonConvert.DeserializeObject<List<RawJson>>(DulieuUHF);
                      for (int i3 = 0; i3 < datauhf.Count; i3++)
                      {
                          string url = "https://shopdunk.com/SelectStore?StateProvinceId=&CountyId=&select_receive_store=";



                              var url2 = url + datauhf[i3].ShowroomSeName;
                              webClient.Encoding = Encoding.UTF8;
                              var htmlString = webClient.DownloadString(url2);
                              //Code lấy html từ một trang web//
                              var dulieuweb = new HtmlAgilityPack.HtmlDocument();
                              dulieuweb.LoadHtml(htmlString);
                              *//*var tencuahang = dulieuweb.DocumentNode.SelectNodes("//strong[@class='title']");
                              var diachi = dulieuweb.DocumentNode.SelectNodes("//div[@class='address']");
                              var diachithanhpho = dulieuweb.DocumentNode.SelectNodes("//div[@class='city']");*//*
                              var sdt = dulieuweb.DocumentNode.SelectNodes("//div[@class='label']");

                              THtruemilk u = new THtruemilk();


                          if(sdt!=null)
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
                              textBox1.Text += datauhf[i3].ShowroomSeName + "%" + datauhf[i3].ShowroomName + "%" + datauhf[i3].ShowroomAddress +"\r\n";
                          }



                      }

                  }
                  catch(Exception ex)
                  {

                  }

              }*/

        }
        public static Dictionary<string, int> myDict;
        public static int i;
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
                    textBox1.Text+= "body.text.thietbi.tb."+ RemoveDiacritics(dsdc[i].Trim().Replace("Đ","D").Replace("đ","d").Replace("/","").Replace("(","").Replace(")","").Replace(" ", "").Replace("-", "").Replace(",", "").Replace("%","").Replace(".", "").Replace(":", "").Replace("+", "")) +"\r\n";
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
    }
}
