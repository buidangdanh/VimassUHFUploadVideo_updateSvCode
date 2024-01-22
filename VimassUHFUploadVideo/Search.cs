using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VimassUHFUploadVideo.Ultil;

namespace VimassUHFUploadVideo
{
    public partial class Search : Form
    {
        public Search()
        {
            InitializeComponent();
        }

        private void Search_Load(object sender, EventArgs e)
        {
            textBox3.Hide();
            textBox4.Hide();
            button2.Hide();
            if (FunctionGeneral.soVi.Equals("0966074236"))
            {
                textBox3.Show();
                textBox4.Show();
                button2.Show();
            }
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
            label6.Text = "";
            textBox1.Text = "0966074236";
            label1.Text = "Thông tin tra cứu thẻ";
            label9.Text = "Xem lại thông tin trước khi xóa";
            DateTime utcDateTime = DateTime.UtcNow;
            string vnTimeZoneKey = "SE Asia Standard Time";
            TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
            DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
            long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();
            string timenow2 = ngaygiohientai.ToString("dd/MM/yyyy HH:mm:ss");
            try
            {

                String Uid = FunctionGeneral.UID;
                String Tid = FunctionGeneral.TID;
                String Epc = FunctionGeneral.EPC;
                String Vuid = FunctionGeneral.VuID;
                String soThe = FunctionGeneral.soCuaTem;
                SearchUID u = new SearchUID();
                u.idVid = Vuid;
                u.uID = Uid;
                u.TID = Tid;
                u.EPC = Epc;
                u.soThe= soThe;
                u.timeRequest = yourDateTimeMilliseconds;
                u.dataCheck = FunctionGeneral.Md5("ifafioeqfo" + u.uID + u.idVid + u.TID + "2" + u.EPC + u.timeRequest + "0" + "100").ToLower();
                string url = "https://vimass.vn/vmbank/services/TDNService/requestComand";
                var json = JsonConvert.SerializeObject(u);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);
                FunctionGeneral.writeFile(@"D:\VimassSearchLog.txt", timenow2 + "__" + json);
                if (response.msgCode == 1)
                {
                    SearchResult searchResult = JsonConvert.DeserializeObject<SearchResult>(response.result.ToString());
                    FunctionGeneral.writeFile(@"D:\VimassSearchLog.txt", timenow2 + "__" + searchResult);

                    var dataSearch = JsonConvert.DeserializeObject<List<ListSearch>>(searchResult.list.ToString());
                    FunctionGeneral.writeFile(@"D:\VimassSearchLog.txt", timenow2 + "__" + dataSearch);
                    if (dataSearch != null)
                    {
                        for (int i2 = 0; i2 < dataSearch.Count; i2++)
                        {
                            if (dataSearch[i2].maGiaoDich != "")
                            {
                                label2.Text = "Số thẻ : " + dataSearch[i2].idVid;
                                label3.Text = "uID: " + dataSearch[i2].uID;
                                label4.Text = "TID: " + dataSearch[i2].TID;
                                label5.Text = "EPC: " + dataSearch[i2].EPC;
                                label10.Text = "Số tem: " + dataSearch[i2].soThe;
                                long unixTimeMilliseconds = dataSearch[i2].timeCreate;
                                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(unixTimeMilliseconds);
                                DateTimeOffset vietnamTime = dateTimeOffset.ToOffset(TimeSpan.FromHours(7));
                                label6.Text = "Ngày tạo: " + vietnamTime;
                                FunctionGeneral.MaGd = dataSearch[i2].maGiaoDich;
                            }
                            else
                            {
                                MessageBox.Show("Chưa có thông tin thẻ");
                            }
                        }
                    }
                    else
                    {
                        label2.Text = "Chưa có thông tin thẻ trên DB hoặc map sai vui lòng xóa và map lại";
                    }


                }


            }
            catch (Exception ex)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime utcDateTime = DateTime.UtcNow;
                string vnTimeZoneKey = "SE Asia Standard Time";
                TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
                DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
                long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();

                if (FunctionGeneral.MaGd != "")
                {
                    SearchDelete u = new SearchDelete();

                    u.ma = FunctionGeneral.MaGd;

                    if (textBox1.Text != "")
                    {
                        u.user = textBox1.Text;
                    }
                    else
                    {
                        label9.Text = "Vui lòng nhập số điện thoại";
                    }

                    if (textBox2.Text != "")
                    {
                        u.token = textBox2.Text;
                    }
                    else
                    {
                        label9.Text = "Vui lòng nhập token";
                    }

                    u.timeRequest = yourDateTimeMilliseconds;

                    u.dataCheck = FunctionGeneral.Md5("ifafioeqfo" + u.funcId + u.ma + u.timeRequest + u.token + u.user).ToLower() ;

                    string url = "https://vimass.vn/vmbank/services/TDNService/requestComand";
                    var json = JsonConvert.SerializeObject(u);
                    String res = Service.SendWebrequest_POST_Method(json, url);
                    Response response = JsonConvert.DeserializeObject<Response>(res);
                    FunctionGeneral.writeFile(@"D:\VimassSearchLog.txt", "_" + json);
                    if (response.msgCode == 1)
                    {
                        label9.Text = "Xóa thành công";
                        label2.Text = "";
                        label3.Text = "";
                        label4.Text = "";
                        label5.Text = "";
                        label6.Text = "";
                    }
                    else
                    {
                        label9.Text = response.msgContent;
                    }



                    }
                else
                {
                    label9.Text = "Không tìm thấy giao dịch thẻ";
                }
                
            }
            catch(Exception e2)
            {

            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nameJson = FunctionGeneral.pathJson + textBox3.Text + @".txt";
            String DulieuUHF = "";
            string path = nameJson;
            DulieuUHF = CreatEPCNew.docThongTin(path);
            DateTime utcDateTime = DateTime.UtcNow;
            string vnTimeZoneKey = "SE Asia Standard Time";
            TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
            DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);
            long yourDateTimeMilliseconds = new DateTimeOffset(ngaygiohientai).ToUnixTimeMilliseconds();

            if (DulieuUHF != "")
            {
                var datauhf = JsonConvert.DeserializeObject<List<CardInfo>>(DulieuUHF);
                for (int i3 = 0; i3 < datauhf.Count; i3++)
                {
                    SearchUID u = new SearchUID();
                    u.idVid = datauhf[i3].soThe;
                    u.uID = datauhf[i3].uID; 
                    u.TID = datauhf[i3].TID; 
                    u.EPC = datauhf[i3].EPC; 
                    u.timeRequest = yourDateTimeMilliseconds;
                    u.dataCheck = FunctionGeneral.Md5("ifafioeqfo" + u.uID + u.idVid + u.TID + "2" + u.EPC + u.timeRequest + "0" + "100").ToLower();
                    string url = "https://vimass.vn/vmbank/services/TDNService/requestComand";
                    var json = JsonConvert.SerializeObject(u);
                    String res = Service.SendWebrequest_POST_Method(json, url);
                    Response response = JsonConvert.DeserializeObject<Response>(res);

                    if (response.msgCode == 1)
                    {
                        SearchResult searchResult = JsonConvert.DeserializeObject<SearchResult>(response.result.ToString());


                        var dataSearch = JsonConvert.DeserializeObject<List<ListSearch>>(searchResult.list.ToString());

                        if (dataSearch != null)
                        {
                            for (int i2 = 0; i2 < dataSearch.Count; i2++)
                            {
                                if (dataSearch[i2].maGiaoDich != "")
                                {
                                    textBox4.Text+= u.idVid + "%" +dataSearch[i2].maGiaoDich +"\r\n";
                                    FunctionGeneral.MaGd = u.idVid+"%"+ dataSearch[i2].maGiaoDich;
                                }
                                else
                                {
                                    MessageBox.Show("Chưa có thông tin thẻ");
                                }
                            }
                        }
                        else
                        {
                            label2.Text = "Chưa có thông tin thẻ trên DB hoặc map sai vui lòng xóa và map lại";
                        }
                    }
                }   }
        }
    }
}
