using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VimassUHFUploadVideo
{
    public partial class AutoVpass : Form
    {
        public AutoVpass()
        {
            InitializeComponent();
        }
        private void AutoVpass_Load(object sender, EventArgs e)
        {
           // label10.Text = "Đã cài đặt thành công JAVA 8 và MYSQL WORKBENCH 6.3 CE";
            InstallMySQL();
            //InstallJavaAsync();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //InstallJavaAsync();
               //InstallJava();

                InstallMySQL();
            }
            catch(Exception ex)
            {

            }
        }
        public void InstallJava()
        {
            try
            {
                Process javaInstallProcess = new Process();
                javaInstallProcess.StartInfo.FileName = @"C:\VPass\java-8.exe"; // Thay thế với đường dẫn chính xác tới bộ cài đặt Java
                javaInstallProcess.StartInfo.Arguments = "/s";
                javaInstallProcess.StartInfo.UseShellExecute = true;
                javaInstallProcess.StartInfo.CreateNoWindow = true;
                javaInstallProcess.Start();

                javaInstallProcess.WaitForExit(); // Đợi cho đến khi quá trình cài đặt hoànj tất
            }
            catch (Exception ex)
            {
                FunctionGeneral.writeFile(@"D:\VpassAuto.txt", "Lỗi: " + ex.Message);
                Console.WriteLine("Lỗi: " + ex.Message);
            }
        }

        public async Task InstallJavaAsync()
        {
            string downloadUrl = "https://repo.huaweicloud.com/java/jdk/8u191-b12/jdk-8u191-windows-x64.exe"; // Thay thế với đường dẫn thực tế
            string filePath = @"D:\java-8.exe";

            try
            {
                if (!File.Exists(filePath))
                {
                    using (var client = new HttpClient())
                    {
                        var response = await client.GetAsync(downloadUrl);
                        using (var fs = new FileStream(filePath, FileMode.CreateNew))
                        {
                            await response.Content.CopyToAsync(fs);
                        }
                    }
                }
                else
                {
                    // Tệp đã tồn tại, không cần tải xuống lại
                }
                // Tải xuống file cài đặt
           

                // Chạy file cài đặt
                Process javaInstallProcess = new Process();
                javaInstallProcess.StartInfo.FileName = filePath;
                javaInstallProcess.StartInfo.Arguments = "/s";
                javaInstallProcess.StartInfo.UseShellExecute = false;
                javaInstallProcess.StartInfo.CreateNoWindow = true;
                javaInstallProcess.Start();

                javaInstallProcess.WaitForExit(); // Đợi cho đến khi quá trình cài đặt hoàn tất
            }
            catch (Exception ex)
            {
                FunctionGeneral.writeFile(@"D:\VpassAuto.txt", "Lỗi Java: " + ex.Message);
                Console.WriteLine("Lỗi: " + ex.Message);
            }
        }


        public async Task InstallMySQL2()
        {
            string downloadUrl = "https://downloads.mysql.com/archives/get/p/8/file/mysql-workbench-community-6.3.3-winx64.msi"; // Thay thế với đường dẫn thực tế
            string filePath = @"D:\mysql-workbench-community-6.3.3-winx64.msi";

            // Kiểm tra xem tệp đã tải xuống trước đó có còn tồn tại không
            if (File.Exists(filePath))
            {
                // Tệp đã tồn tại, kiểm tra kích thước của nó
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Length == 5 * 1024)
                {
                    // Tệp bị hỏng, cần tải xuống lại
                    Console.WriteLine("Tệp tải xuống bị hỏng, cần tải xuống lại");
                    return;
                }
            }

            // Tải xuống tệp
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(downloadUrl);
                    using (var fs = new FileStream(filePath, FileMode.CreateNew))
                    {
                        await response.Content.CopyToAsync(fs);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi tải xuống tệp: " + ex.Message);
                return;
            }

            // Cài đặt MySQL Workbench
            Process mysqlInstallProcess = new Process();
            mysqlInstallProcess.StartInfo.FileName = filePath; // Thay thế với đường dẫn chính xác tới bộ cài đặt MySQL
            mysqlInstallProcess.StartInfo.Arguments = "--mode=unattended --unattendedmodeui=none"; // Các tham số cho cài đặt không tương tác
            mysqlInstallProcess.StartInfo.UseShellExecute = false;
            mysqlInstallProcess.StartInfo.CreateNoWindow = true;
            mysqlInstallProcess.Start();

            mysqlInstallProcess.WaitForExit(); // Đợi cho đến khi quá trình cài đặt hoàn tất
        }
        public void InstallMySQL()
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = "msiexec";
                process.StartInfo.Arguments = "/i \"C:\\VPass\\mysql.msi\" /qn";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                process.WaitForExit();  // Chờ cho đến khi quá trình cài đặt hoàn tất
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cài đặt: " + ex.Message);
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
