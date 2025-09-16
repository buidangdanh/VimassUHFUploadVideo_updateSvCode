using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace VimassUHFUploadVideo.Vpass.GiaoDien
{
    public partial class NhanDienKhuonMat : Form
    {
        public NhanDienKhuonMat()
        {
            InitializeComponent();
            // Đặt AutoSize cho Form
          
            // Thêm Luxand2 vào Form
            Luxand2 luxand2 = new Luxand2();

            luxand2.SizeChanged += (s, e) =>
            {
                this.ClientSize = luxand2.Size;
            };

            // Cập nhật kích thước ban đầu
            this.ClientSize = luxand2.Size;
            this.Controls.Add(luxand2);

            Debug.WriteLine($"NhanDienKhuonMat initial size: Width = {this.Width}, Height = {this.Height}");
            // Đăng ký sự kiện SizeChanged
          
        }
    }
}
