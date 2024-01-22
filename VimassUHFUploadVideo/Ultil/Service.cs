using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Web;
using VimassUHFUploadVideo;
using VimassUHFUploadVideo.Vpass.Object;

namespace VimassUHFUploadVideo
{
    public class Service
    {
        public const string URL_TRUNG_GIAN = "http://118.69.84.243:8080/vmNoiBo/services/account/web1";
        public const string URL_TRUNG_GIAN2 = "http://118.69.84.243:8080/vmNoiBo/services/account/web2";
        public const string PARAM1 = "8249539tgsdlka";
        static DateTime utcDateTime = DateTime.UtcNow;
        static string vnTimeZoneKey = "SE Asia Standard Time";
        static TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
        static DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
        long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();
        static string timenow = ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
        public static String SendWebrequest_POST_Method2(string json, string url)
        {
            string result = "";
            try
            {
                using (var client = new WebClient() { Encoding = Encoding.UTF8 })
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    result = client.UploadString(url, "POST", json);
                    //result = json;

                    Logger.LogServices("Request: " + json);
                    Logger.LogServices("response: " + result);

                    Debug.WriteLine("request: " + json);
                    Debug.WriteLine("response: " + result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return result;
        }
        public static string SendWebrequest_POST_Method(string json, string url)
        {
            string result = "";
            try
            {
                using (var client = new WebClientWithTimeout() { Encoding = Encoding.UTF8, Timeout = 15000 })
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    result = client.UploadString(url, "POST", json);

                    Logger.LogServices("Request: " + json);
                    Logger.LogServices("Response: " + result);

                    Debug.WriteLine("Request: " + json);
                    Debug.WriteLine("Response: " + result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return result;
        }


        public static string SendWebrequest_Get_Method(string arrayParam, string url)
        {
            string result = "";
            try
            {
                string longurl = url;
                var uriBuilder = new UriBuilder(longurl);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                string[] temp = arrayParam.Split(';');
                for (int i = 0; i < temp.Length / 2; i++)
                {
                    query[temp[i * 2]] = temp[i * 2 + 1];
                }
                uriBuilder.Query = query.ToString();
                longurl = uriBuilder.ToString();
                using (var client = new WebClient() { Encoding = Encoding.UTF8 })
                {
                    result = client.DownloadString(longurl);
                    Debug.WriteLine("request: " + longurl);
                    Debug.WriteLine("response: " + result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return result;
        }
    }
}

