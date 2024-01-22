using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VimassUHFUploadVideo.Ultil;

namespace VimassUHFUploadVideo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "PHẦN MỀM NHẬN DẠNG CHỦ THẺ";
            GetInfoConfig();
        }
        private void GetInfoConfig()
        {
            String duLieuConfig = "";
            duLieuConfig = FunctionGeneral.docThongTin(@"D:\configUHF.json");
            var dataConfig = JsonConvert.DeserializeObject<List<ConfigUHF>>(duLieuConfig);
            for (int i2 = 0; i2 < dataConfig.Count; i2++)
            {
                FunctionGeneral.idThietBiThuyKhueCongVao = dataConfig[i2].idThietBiThuyKhueCongVao;

                FunctionGeneral.idThietBiThuyKhueCongRa = dataConfig[i2].idThietBiThuyKhueCongRa;

                FunctionGeneral.idThietBiHHTCongVao = dataConfig[i2].idThietBiHHTCongVao;

                FunctionGeneral.idThietBiHHTCongRa = dataConfig[i2].idThietBiHHTCongRa;

                FunctionGeneral.COMVaoThuyKhue = dataConfig[i2].COMVaoThuyKhue;

                FunctionGeneral.COMRaThuyKhue = dataConfig[i2].COMRaThuyKhue;

                FunctionGeneral.COMVaoHHT = dataConfig[i2].COMVaoHHT;

                FunctionGeneral.COMRaHHT = dataConfig[i2].COMRaHHT;
               
                FunctionGeneral.idThietBiTest = dataConfig[i2].idThietBiTest;

                FunctionGeneral.COMTEST = dataConfig[i2].COMTEST;

                FunctionGeneral.CamIN = dataConfig[i2].CameraIN;

                FunctionGeneral.CamOUT = dataConfig[i2].CameraOUT;


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new TKIN().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new TKOUT().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new HHTIN().Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new HHTOUT().Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new TEST().Show();
        }
    }
}
