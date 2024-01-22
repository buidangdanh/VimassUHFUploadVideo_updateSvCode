using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VimassUHFUploadVideo
{
    public partial class ShowData : Form
    {
        public ShowData()
        {
            InitializeComponent();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void ShowData_Load(object sender, EventArgs e)
        {
            try
            {
                textBox4.Text = FunctionGeneral.dataShow;
            }
            catch
            {

            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string FileName = textBox6.Text;
            File.WriteAllText(FunctionGeneral.pathJson + FileName + @".txt", @"[" + FunctionGeneral.dataLuuJson.Substring(0, FunctionGeneral.dataLuuJson.Length - 1) + @"]");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                FunctionGeneral.dataLuuJson = "";
                textBox4.Text = "";
                FunctionGeneral.dataShow = "";
                this.Close();
            }
            catch
            {

            }
        }
    }
}
