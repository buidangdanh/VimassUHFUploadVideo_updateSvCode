using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VimassUHFUploadVideo.Ultil;

namespace VimassUHFUploadVideo
{
    public partial class LatLong : Form
    {
        public LatLong()
        {
            InitializeComponent();
        }

        private void LatLong_Load(object sender, EventArgs e)
        {
            try
            {

                dataGridView1.Width = 900;
                dataGridView1.Height = 600;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // Set chế độ tự động thay đổi kích thước của các cột
                dataGridView1.AllowUserToResizeColumns = true; // Ngăn chặn người dùng thay đổi kích thước các cột

                int defaultColumnWidth = 400; // Thiết lập kích thước cột mặc định (ở đây là 100)
                

                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.Width = defaultColumnWidth; // Thiết lập kích thước cột
                }
                dataGridView1.Columns.Add("STT", "STT");
                dataGridView1.Columns.Add("Add", "Địa Chỉ");
                dataGridView1.Columns.Add("Lat", "Lat");
                dataGridView1.Columns.Add("Lng", "Lng");
                string[] diaChiCoLatLng = FunctionGeneral.latlong.Trim().Split('\r');
                for(int i=0;i<diaChiCoLatLng.Length;i++) 
                {
                    if (diaChiCoLatLng[i] != "")
                    {
                        dataGridView1.Rows.Add(i+1,diaChiCoLatLng[i].Split('%')[0], diaChiCoLatLng[i].Split('%')[1], diaChiCoLatLng[i].Split('%')[2]);
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}
