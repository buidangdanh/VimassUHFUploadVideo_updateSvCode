using Newtonsoft.Json;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using VimassUHFUploadVideo.Vpass.Object;
using static VimassUHFUploadVideo.GetDataStore;
using javax.crypto;
using javax.crypto.spec;

namespace VimassUHFUploadVideo
{
    public class FunctionGeneral
    {
        public static string idThietBiTest = "";
        public static string idThietBiThuyKhueCongVao = "";
        public static string idThietBiThuyKhueCongRa = "";
        public static string idThietBiHHTCongVao = "";
        public static string idThietBiHHTCongRa = "";
        public static string COMVaoThuyKhue = "";
        public static string COMRaThuyKhue = "";
        public static string COMVaoHHT = "";
        public static string COMRaHHT = "";
        public static string COMTEST = "";
        public static string KeyName = "";
        public static string KeyViD = "";
        public static int CamIN = 0;
        public static int CamOUT = 1;
        public static string UID = "";
        public static string TID = "";
        public static string EPC = "";
        public static string SoCuaTem = "";
        public static int check = 0;
        public static int dem = 1;
        public static string port = "";
        public static string checkTC = "";
        public static string VuID = "";
        public static string soCuaTem = "";
        public static string MaGd = "";
        public static string LoaiXe = "";
        public static string LoaiDot = "";
        public static string DinhDanh = "";
        public static string GiaTri = "";
        public static string TIDCreat = "";
        public static string EPCNew = "";
        public static string McName = "";
        public static string pathLog = "";
        public static string pathExcel = "";
        public static string pathJson = "";
        public static string soVi = "";
        public static string tenDN = "";
        public static string dataLuuJson = "";
        public static string readTID = "";
        public static string nameFileRead = "";
        public static string dataShow = "";
        public static string TID1 = "";
        public static string TID2 = "";
        public static string check1 = "";
        public static string check2 = "";
        public static string check3 = "";
        public static string check4 = "";

        public static string keyconfirmEPCUnkown = "";
        public static string keyCreateEPCUnkown = "";
        public static string keyconfirmEPC = "";
        public static string keyCreateEPC = "";
        public static string user = "";

        public static string latlong = "";











        public static Dictionary<string, string> DataMcID = new Dictionary<string, string>();









        public static string docThongTin(string url)
        {
            try
            {
                string temp = File.ReadAllText(url);
                return temp;
            }
            catch (Exception e7)
            {
                return "";
            }

        }
        public static void writeFile(String url, String content)
        {
            FileStream fs = new FileStream(url, FileMode.Append);
            StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);
            try
            {
                sWriter.WriteLine(content);
            }
            catch (Exception e)
            {
            }
            finally
            {
                sWriter.Flush();
                try
                {
                    fs.Close();
                }
                catch (Exception e)
                {
                }
            }
        }
        public static string Md5(string input, bool isLowercase = false)
        {
            using (var md5 = MD5.Create())
            {
                var byteHash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                var hash = BitConverter.ToString(byteHash).Replace("-", "");
                return (isLowercase) ? hash.ToLower() : hash;
            }
        }
        public static void SetTimeout(Action action, int timeout)
        {
            var timer = new Timer();
            timer.Interval = timeout;
            timer.Tick += (s, e) =>
            {
                action();
                timer.Stop();
            };
            timer.Start();
        }
        public static string ExcelToJson(string fileName, string fileSheet, int socot)
        {
            var pathToExcel = FunctionGeneral.pathExcel + fileName + ".xlsx";
            var sheetName = fileSheet;

            //This connection string works if you have Office 2007+ installed and your 
            //data is saved in a .xlsx file
            var connectionString = String.Format(@"
            Provider=Microsoft.ACE.OLEDB.12.0;
            Data Source={0};
            Extended Properties=""Excel 12.0 Xml;HDR=YES""
        ", pathToExcel);

            //Creating and opening a data connection to the Excel sheet 
            using (var conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = String.Format(
                    @"SELECT * FROM [{0}$]",
                    sheetName
                    );
                using (var rdr = cmd.ExecuteReader())
                {
                    //LINQ query - when executed will create anonymous objects for each row
                    var query =
                        (from DbDataRecord row in rdr
                         select row).Select(x =>
                         {
                             //dynamic item = new ExpandoObject();
                             Dictionary<string, object> item = new Dictionary<string, object>();
                             for (int i = 0; i < socot; i++)
                             {
                                 item.Add(rdr.GetName(i), x[i]);
                             }
                             /*item.Add(rdr.GetName(0), x[0]);
                             item.Add(rdr.GetName(1), x[1]);*/
                             return item;

                         });
                    //Generates JSON from the LINQ query
                    var json = JsonConvert.SerializeObject(query);
                    return json;
                }
            }
        }
        public static string CreatFolder(string nameFolder)
        {
            string directoryPath = @"D:\" + nameFolder;
            DirectoryInfo directory = new DirectoryInfo(directoryPath);
            directory.Create();
            directory.Attributes = FileAttributes.Hidden;
            return directoryPath;
        }
        public static void writeInsert(String url, String content)
        {
            FileStream fs = new FileStream(url, FileMode.Append);
            StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);
            try
            {
                sWriter.Write(content);
            }
            catch (Exception e)
            {
            }
            finally
            {
                sWriter.Flush();
                try
                {
                    fs.Close();
                }
                catch (Exception e)
                {
                }
            }
        }
        public static string ConvertCSVtoJson(string nameCSV)
        {

            try
            {
                var csv = new List<string[]>();
                var lines = System.IO.File.ReadAllLines(pathExcel + nameCSV + @".csv"); // csv file location

                // loop through all lines and add it in list as string
                foreach (string line in lines)
                    csv.Add(line.Split(','));

                //split string to get first line, header line as JSON properties
                var properties = lines[0].Split(',');

                var listObjResult = new List<Dictionary<string, string>>();

                //loop all remaining lines, except header so starting it from 1
                // instead of 0
                for (int i = 1; i < lines.Length; i++)
                {
                    var objResult = new Dictionary<string, string>();
                    for (int j = 0; j < properties.Length; j++)
                        objResult.Add(properties[j], csv[i][j]);

                    listObjResult.Add(objResult);
                }

                // convert dictionary into JSON
                var json2 = JsonConvert.SerializeObject(listObjResult);

                //print
                return json2;
            }
            catch
            {
                return "";
            }
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

                FunctionGeneral.check1 = latitude.ToString() + "%" + longitude.ToString();
            }
            else
            {
                Console.WriteLine("Error: " + googleGeoCodeResponse.status);
            }

            return FunctionGeneral.check1;

        }
        public static void SendUdp(string serverIp, int serverPort, string message)
        {
            using (UdpClient udpClient = new UdpClient())
            {
                try
                {
                    byte[] bytesToSend = Encoding.ASCII.GetBytes(message);

                    // Send the message
                    udpClient.Send(bytesToSend, bytesToSend.Length, serverIp, serverPort);
                    Console.WriteLine("Message sent to {0}:{1}", serverIp, serverPort);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
        public static string DecodeBase64String(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static String chuyenLongSangGioPhut(long time,String fomat)
        {
            // Chuyển đổi timestamp từ millisecond sang DateTime
            DateTime dateTime = DateTimeOffset.FromUnixTimeMilliseconds(time).DateTime;

            // Chuyển đổi thời gian sang giờ địa phương
            DateTime localTime = dateTime.ToLocalTime();

            // Định dạng thời gian theo "HH:mm"
            return localTime.ToString(fomat);
        }
        public static String chuyenLongSangNgayThangNam(long time)
        {
            // Chuyển đổi timestamp từ millisecond sang DateTime
            DateTime dateTime = DateTimeOffset.FromUnixTimeMilliseconds(time).DateTime;

            // Lấy thông tin múi giờ "SE Asia Standard Time" (UTC+7)
            TimeZoneInfo seAsiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

            // Chuyển đổi thời gian sang múi giờ Đông Nam Á
            DateTime seAsiaTime = TimeZoneInfo.ConvertTime(dateTime, seAsiaTimeZone);

            // Định dạng thời gian theo "dd/MM/yyyy"
            return seAsiaTime.ToString("dd/MM/yyyy");
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
        public static string DecryptTripleDES(string key1, string key2, string key3, string data)
        {
            string result = "";
            try
            {
                result = decryptDES(key3, data);
                result = decryptDES(key2, result);
                result = decryptDES(key1, result);
            }
            catch (Exception e)
            {
                result = "";
                // Handle the exception here
            }
            return result;
        }

        public static String decryptDES(String key, String data)
        {
            if (key == null || data == null)
                return "";
            try
            {
                data = data.Trim();
                byte[] dataBytes = Base64.Decode(UTF8Encoding.UTF8.GetBytes(data));
                DESKeySpec dESKeySpec = new DESKeySpec(UTF8Encoding.UTF8.GetBytes(key));
                SecretKeyFactory secretKeyFactory = SecretKeyFactory.getInstance("DES");
                SecretKey secretKey = secretKeyFactory.generateSecret(dESKeySpec);
                Cipher cipher = Cipher.getInstance("DES");
                cipher.init(Cipher.DECRYPT_MODE, secretKey);
                byte[] ciphertext = (cipher.doFinal(dataBytes));
                return UTF8Encoding.UTF8.GetString(ciphertext);
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
 



}
