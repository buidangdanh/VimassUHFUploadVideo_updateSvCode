using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VimassUHFUploadVideo
{
    public partial class ExceltoJson : Form
    {
        public ExceltoJson()
        {
            InitializeComponent();
        }

        private void ExceltoJson_Load(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FunctionGeneral.CreatFolder(textBox1.Text);
        }
    }
}
