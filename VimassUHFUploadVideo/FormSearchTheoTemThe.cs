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
    public partial class FormSearchTheoTemThe : Form
    {
        public FormSearchTheoTemThe()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                double number;
                if (double.TryParse(textBox1.Text, out number))
                {
                    textBox1.Text = number.ToString("N0"); // Định dạng số với phân cách hàng nghìn
                    textBox1.SelectionStart = textBox1.Text.Length;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                string soThe = textBox4.Text.Trim();
                string soTem = textBox1.Text.Replace(",",".").Trim();
                if(soThe!=null&&!soThe.Equals("")&&soTem != null && !soTem.Equals(""))
                {
                    MessageBox.Show("Chỉ tra cứu được 1 thứ");
                }
                else
                {
                    FunctionGeneral.VuID = soThe;
                    FunctionGeneral.soCuaTem = soTem;
                    new Search().Show();

                }
                
            }
            catch(Exception ex)
            {

            }
        }

        internal class Show
        {
            public Show()
            {
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ngăn không cho ký tự được hiển thị trong TextBox
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // Ngăn không cho ký tự không hợp lệ được hiển thị trong TextBox
            }

            if (e.KeyChar == '.' && ((TextBox)sender).Text.Contains("."))
            {
                e.Handled = true; // Ngăn không cho phép nhiều dấu '.' trong TextBox
            }
        }

        private void FormSearchTheoTemThe_Load(object sender, EventArgs e)
        {
            textBox1.Font = new Font("Arial", 16);
            textBox4.Font = new Font("Arial", 16);
        }
    }
}
