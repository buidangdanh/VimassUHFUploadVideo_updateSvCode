using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VimassUHFUploadVideo
{
    public partial class GetLatLng : Form
    {
        public GetLatLng()
        {
            InitializeComponent();
        }

        private void GetLatLng_Load(object sender, EventArgs e)
        {
            label1.Text = "Copy địa chỉ vào ô trên";
            label1.ForeColor= Color.Red;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                label1.Text = "Vui lòng chờ!";
                string[] dsdc = textBox1.Text.Trim().Split('\r');
                FunctionGeneral.latlong = "";
                label1.ForeColor = Color.Red;
                for (int i=0;i<dsdc.Length;i++)
                {
                    if (dsdc[i] != "")
                    {
                        FunctionGeneral.latlong += dsdc[i] +"%"+ FunctionGeneral.GetLatLngFromAdD(dsdc[i]) + "\r\n";
                    }
                }

                new LatLong().Show();


            }
            catch(Exception ex)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string nameJson = FunctionGeneral.pathJson + textBox2.Text + @".txt";

                string path = nameJson;
                string DulieuUHF = FunctionGeneral.docThongTin(path);
                string[] dsdc = DulieuUHF.Trim().Split('\r');
                FunctionGeneral.latlong = "";
                label1.ForeColor = Color.Red;
                for (int i = 0; i < dsdc.Length; i++)
                {
                    if (dsdc[i] != "")
                    {
                        FunctionGeneral.latlong += dsdc[i] + "%" + FunctionGeneral.GetLatLngFromAdD(dsdc[i]) + "\r\n";
                    }
                }

                new LatLong().Show();
            }
            catch(Exception ex)
            {

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
