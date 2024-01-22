using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VimassUHFUploadVideo.Ultil;

namespace VimassUHFUploadVideo
{
    public partial class ConvertExcelToJson : Form
    {
        public ConvertExcelToJson()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nameExcel = textBox1.Text;
            string nameSheet = textBox2.Text;
            int soCot =Int32.Parse(textBox3.Text);
            string nameJson = FunctionGeneral.pathJson+ textBox4.Text+@".txt";
            File.WriteAllText(nameJson, FunctionGeneral.ExcelToJson(nameExcel,nameSheet,soCot));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nameJson = FunctionGeneral.pathJson+ textBox5.Text+@".txt";

            String DulieuUHF = "";
            string path = nameJson;
            DulieuUHF = FunctionGeneral.docThongTin(path);
            if (DulieuUHF != "")
            {
                var datauhf = JsonConvert.DeserializeObject<List<RawJson>>(DulieuUHF);
                THtruemilk u = new THtruemilk();
                for (int i3 = 0; i3 < datauhf.Count; i3++)
                {
                    if (datauhf[i3].idCuaHang != null && datauhf[i3].idCuaHang != "")
                    {
                        u.idCuaHang = datauhf[i3].idCuaHang.Trim();
                    }
                    else 
                    {
                        u.idCuaHang = "";
                    }
                    if (datauhf[i3].tenCuaHang != null && datauhf[i3].tenCuaHang != "")
                    {
                        u.tenCuaHang = datauhf[i3].tenCuaHang;
                    }
                    else
                    {
                        u.tenCuaHang = "";
                    }


                    if (datauhf[i3].diaChi != null && datauhf[i3].diaChi != "")
                    {
                        u.diaChi = datauhf[i3].diaChi;
                    }
                    else
                    {
                        u.diaChi = "";
                    }
                    if (datauhf[i3].lat != null && datauhf[i3].lat != "")
                    {
                        u.lat = float.Parse(datauhf[i3].lat);
                    }
                    else
                    {
                        u.lat = 0;
                    }
                    if (datauhf[i3].lng != null && datauhf[i3].lng != "")
                    {
                        u.lng = float.Parse(datauhf[i3].lng); ;
                    }
                    else
                    {
                        u.lng = 0;
                    }
                    if (datauhf[i3].email != null && datauhf[i3].email != "")
                    {
                        u.email = datauhf[i3].email;
                    }
                    else
                    {
                        u.email = "";
                    }
                    if (datauhf[i3].url != null && datauhf[i3].url != "")
                    {
                        u.url = datauhf[i3].url;
                    }
                    else
                    {
                        u.url = "";
                    }
                    if (datauhf[i3].sdt != null && datauhf[i3].sdt != "")
                    {
                        u.sdt = datauhf[i3].sdt;
                    }
                    else
                    {
                        u.sdt = "";
                    }
                    if (datauhf[i3].image != null && datauhf[i3].image != "")
                    {
                        u.image = datauhf[i3].image;
                    }
                    else
                    {
                        u.image = "";
                    }
              /*      u.lat =float.Parse(datauhf[i3].lat);
                    u.lng = float.Parse(datauhf[i3].lng);
                    u.idCuaHang = datauhf[i3].id.Trim();
                    u.tenCuaHang = datauhf[i3].name.Trim();
                    u.email = datauhf[i3].email.Trim();
                    u.diaChi = datauhf[i3].address.Trim();
                    u.url = datauhf[i3].web.Trim();
                    u.image = "";
                    u.sdt = "";*/
                    var json2 = JsonConvert.SerializeObject(u);
                    FunctionGeneral.writeFile(FunctionGeneral.pathJson + textBox5.Text+@"json.txt", json2);
                }
         
            }
        }
    }
}
