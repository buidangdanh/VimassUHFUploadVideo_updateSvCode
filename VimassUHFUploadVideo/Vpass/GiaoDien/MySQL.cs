using System;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySqlConnector;

namespace VimassUHFUploadVideo.Vpass.GiaoDien
{
    public partial class MySQL : Form
    {
        // Chuỗi kết nối của bạn
        private readonly string _connStr =
            "Server=localhost;Port=3306;Database=dbdonvi;" +
            "User ID=root;Password=admin;" +
            "CharSet=utf8mb4;SslMode=None;" +
            "Allow User Variables=True;" +
            "Convert Zero Datetime=True;Allow Zero Datetime=True";

        public MySQL()
        {
            InitializeComponent();
            this.Load += MySQL_Load;
        }

        private async void MySQL_Load(object sender, EventArgs e)
        {
            await TestConnectionAsync();
        }

        /// <summary>
        /// Đặt session time_zone về Việt Nam. Thử 'Asia/Ho_Chi_Minh', nếu không có bảng múi giờ thì fallback '+07:00'.
        /// </summary>
        private void SetVietnamTimeZone(MySqlConnection conn)
        {
            try
            {
                using (var cmd = new MySqlCommand("SET time_zone = 'Asia/Ho_Chi_Minh';", conn))
                    cmd.ExecuteNonQuery();
            }
            catch
            {
                using (var cmd = new MySqlCommand("SET time_zone = '+07:00';", conn))
                    cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Test kết nối + hiển thị NOW() theo giờ VN.
        /// </summary>
        private async Task TestConnectionAsync()
        {
            try
            {
                await Task.Run(() =>
                {
                    using (var conn = new MySqlConnection(_connStr))
                    {
                        conn.Open();

                        // >>> GIỜ VIỆT NAM
                        SetVietnamTimeZone(conn);

                        using (var cmd = new MySqlCommand("SELECT NOW() AS vn_now, @@time_zone AS tz, @@system_time_zone AS sys_tz;", conn))
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var vnNow = reader["vn_now"];
                                var tz = reader["tz"];
                                var sysTz = reader["sys_tz"];

                                this.BeginInvoke((Action)(() =>
                                {
                                   
                                }));
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối MySQL: " + ex.Message, "MySQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ví dụ INSERT: sẽ chạy trong session giờ Việt Nam.
        /// </summary>
        private int InsertDbDevice(string id, string name, int status, string idLoiRaVao)
        {
            const string sql = @"INSERT INTO dbdevice (id, name, status, idLoiRaVao)
                                 VALUES (@id, @name, @status, @idLoiRaVao)";

            using (var conn = new MySqlConnection(_connStr))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@idLoiRaVao", idLoiRaVao);

                conn.Open();

                // >>> GIỜ VIỆT NAM cho session này
                SetVietnamTimeZone(conn);

                return cmd.ExecuteNonQuery();
            }
        }

        // Gọi thử qua 1 nút trên form (nếu có)
        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                var rows = InsertDbDevice("DEV001", "Cam Bien 1", 1, "LRV01");
                MessageBox.Show($"Đã chèn {rows} dòng vào dbdevice.", "MySQL");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi INSERT: " + ex.Message, "MySQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
