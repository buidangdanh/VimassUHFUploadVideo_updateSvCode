using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VimassUHFUploadVideo.Ultil;
namespace VimassUHFUploadVideo.Vpass.Object
{
    public class FunCGeneral
    {
        public static LuuSessionLogin lGinKQ = new LuuSessionLogin();
        public static String ipMayChuDonVi = "";
        public static String mcID = "";
        public static Dictionary<String, ObjecthttpMayChuDonVi> hashMCID = new Dictionary<String, ObjecthttpMayChuDonVi>();
        public static Dictionary<String, ObjectGoup> hashNhom = new Dictionary<String, ObjectGoup>();


        public static long timeNow()
        {
            DateTime utcDateTime = DateTime.UtcNow;
            string vnTimeZoneKey = "SE Asia Standard Time";
            TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
            DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
            string timenow2 = ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
            long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();
            return yourDateTimeMilliseconds;
        }
        public static void checkQuyen()
        {
            try
            {
                if (lGinKQ != null)
                {
                    if (lGinKQ.soThe != null && !lGinKQ.soThe.Equals(""))
                    {
                        httpMayChuDonVi(lGinKQ.soThe);

                    }
                    else
                    {
                        httpMayChuDonVi(lGinKQ.phone);
                    }


                    Logger.LogServices("Dang nhap sdt thanh cong: " + FunCGeneral.lGinKQ.phone);
                }
            }
            catch (Exception ex)
            {
                Logger.LogServices("checkQuyen Exception: " + ex.Message);
            }
        }
        public static void httpMayChuDonVi(String user)
        {
            try
            {
                ObjectQuyenHeThong obj = new ObjectQuyenHeThong();
                obj.user = user;
                obj.currentTime = timeNow();
                obj.cks = FunctionGeneral.Md5("L99JAuGHYaBYYyycsLy26" + obj.currentTime).ToLower();
                string url = "http://210.245.8.7:12318/vimass/services/VUHF/checkQuyenThietLapHT";
                var json = JsonConvert.SerializeObject(obj);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);
                if (response!=null&& response.msgCode == 1)
                {
                    var arrL = JsonConvert.DeserializeObject<List<ObjecthttpMayChuDonVi>>(response.result.ToString());
                    foreach (ObjecthttpMayChuDonVi arr in arrL)
                    {
                        hashMCID.Add(arr.mcName, arr);
                    }

                }


            }
            catch (Exception ex)
            {
                Logger.LogServices("httpMayChuDonVi Exception: " + ex.Message);

            }
        }
        public static void AutoSizeCombobox(ComboBox comboBox)
        {
            Graphics g = comboBox.CreateGraphics();
            float largestWidth = 0;
            foreach (var item in comboBox.Items)
            {
                float itemWidth = g.MeasureString(item.ToString(), comboBox.Font).Width;
                if (itemWidth > largestWidth)
                    largestWidth = itemWidth;
            }
            g.Dispose();

            comboBox.Width = (int)largestWidth + SystemInformation.VerticalScrollBarWidth;
        }
        public static int TinhSoTrang(int soMuc)
        {
            int soTrang = 0;
            try
            {
                int soDu = soMuc % 100;
                if(soDu == 0)
                {
                    soTrang = soMuc / 100 -1;
                }
                else
                {
                    soTrang = soMuc/ 100;
                }
            }
            catch (Exception e)
            {
                Logger.LogServices("TinhSoTrang: " + e.Message);
            }
            return soTrang;
        }
        public static string GenerateRandomCharacters(int length)
        {
            Random random = new Random();
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string result = "";

            for (int i = 0; i < length; i++)
            {
                result += characters[random.Next(characters.Length)];
            }

            return result;
        }


    }

}
