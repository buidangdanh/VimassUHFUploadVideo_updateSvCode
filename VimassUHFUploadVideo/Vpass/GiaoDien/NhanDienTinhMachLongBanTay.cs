using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VimassUHFUploadVideo.Vpass.Object.ObjectThietBi;
using VimassUHFUploadVideo.Vpass.Object.ObjectVanTay;
using VimassUHFUploadVideo.Vpass.Object;
using VimassUHFUploadVideo.Vpass.Object.ObjectNhanDienBanTay;
using VimassUHFUploadVideo.Ultil;

namespace VimassUHFUploadVideo.Vpass.GiaoDien
{
    public partial class NhanDienTinhMachLongBanTay : Form
    {
        private ClientWebSocket _ws;
        private CancellationTokenSource _cts;
        private Task _recvLoop;

        // Hàng đợi xử lý thông điệp (tách nhận WS và xử lý JSON/UI)
        private BlockingCollection<string> _inbox;
        private Task _procLoop;

        private PictureBox picture;
        private TextBox txtUrl, txtPalmId, txtCardNo;
        private Button btnConnect, btnDisconnect, btnRegister, btnUpdate, btnDelete, btnCompare;
        private Label lblStatus, lbConfirm;

        private string _lastFeature;

        private const int StatusBottomMargin = 12; // px
        private const int MinFrameIntervalMs = 80; // throttle frame
        private long _lastFrameMs;

        public NhanDienTinhMachLongBanTay()
        {
            InitializeComponent();
            BuildUi();
        }

        // Xây dựng các nút
        private void BuildUi()
        {
            Text = "Nhận diện tĩnh mạch lòng bàn tay";
            Width = 1400; Height = 680;

            var top = new Panel { Dock = DockStyle.Top, Height = 50 };
            var lblUrl = new Label { Text = "WS URL:", AutoSize = true, Left = 8, Top = 14 };
            txtUrl = new TextBox { Left = 70, Top = 10, Width = 420, Text = "ws://localhost:8188/video" };

            var lblPalm = new Label { Text = "palm_vein_id:", AutoSize = true, Left = 500, Top = 14 };
            txtPalmId = new TextBox { Left = 600, Top = 10, Width = 100, Text = "" };

            var lblCard = new Label { Text = "Số thẻ:", AutoSize = true, Left = 760, Top = 14 };
            txtCardNo = new TextBox { Left = 820, Top = 10, Width = 100, Text = "" };

            // Hàng nút thao tác
            btnConnect = new Button { Text = "Connect", Left = 930, Top = 8, Width = 75 };
            btnRegister = new Button { Text = "Đăng Ký", Left = 1010, Top = 8, Width = 75 };
            btnCompare = new Button { Text = "Xác thực", Left = 1090, Top = 8, Width = 75 };
            btnUpdate = new Button { Text = "Cập nhật", Left = 1170, Top = 8, Width = 75 };
            btnDelete = new Button { Text = "Xoá", Left = 1250, Top = 8, Width = 75 };
            btnDisconnect = new Button { Text = "Disconnect", Left = 1330, Top = 8, Width = 85, Enabled = false };

            btnConnect.Click += btnConnect_Click;
            btnDisconnect.Click += btnDisconnect_Click;
            btnRegister.Click += async (s, e) => await SendRegisterAsync();
            btnCompare.Click += async (s, e) => await SendCompareAsync();
            btnUpdate.Click += async (s, e) => await SendUpdateAsync();
            btnDelete.Click += async (s, e) => await SendDeleteAsync();

            top.Controls.AddRange(new Control[] {
                lblUrl, txtUrl,
                lblPalm, txtPalmId,
                lblCard, txtCardNo,
                btnConnect, btnRegister, btnCompare, btnUpdate, btnDelete, btnDisconnect
            });
            Controls.Add(top);

            picture = new PictureBox { Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.Zoom, BackColor = Color.Black };
            Controls.Add(picture);

            // Thanh trạng thái nhỏ ở đáy form
            lblStatus = new Label { Dock = DockStyle.Bottom, Height = 22, Text = "Ready", TextAlign = ContentAlignment.MiddleLeft };
            Controls.Add(lblStatus);

            // Label xác nhận chồng lên picture (giữa ngang, sát mép dưới, màu đỏ)
            lbConfirm = new Label
            {
                AutoSize = true,
                Text = "Ready",
                ForeColor = Color.Red,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 30f, FontStyle.Bold)
            };
            lbConfirm.Parent = picture;
            picture.Controls.Add(lbConfirm);

            // Căn giữa ngang + sát mép dưới khi khởi tạo và khi đổi kích thước
            picture.Resize += (s, e) => PositionStatusBottom();
            PositionStatusBottom();
        }


        // ===== Helpers cho lbConfirm =====
        private void SetLbConfirm(string s)
        {
            if (!IsHandleCreated) return;

            try
            {
                if (InvokeRequired)
                    BeginInvoke(new Action(() => UpdateConfirmLabelSafe(s)));
                else
                    UpdateConfirmLabelSafe(s);
            }
            catch (ObjectDisposedException)
            {
                // Form/controls có thể đã dispose trong lúc async
            }
        }

        private void UpdateConfirmLabelSafe(string s)
        {
            if (lbConfirm == null || lbConfirm.IsDisposed || picture == null || picture.IsDisposed) return;
            lbConfirm.Text = s;
            PositionStatusBottom();
        }

        private void PositionStatusBottom()
        {
            if (lbConfirm == null || lbConfirm.IsDisposed || picture == null || picture.IsDisposed) return;

            lbConfirm.Left = Math.Max(0, (picture.Width - lbConfirm.Width) / 2);
            lbConfirm.Top = Math.Max(0, picture.Height - lbConfirm.Height - StatusBottomMargin);
            lbConfirm.BringToFront();
        }

        private void CenterStatusLabel()
        {
            if (lbConfirm == null || lbConfirm.IsDisposed || picture == null || picture.IsDisposed) return;

            lbConfirm.Left = Math.Max(0, (picture.Width - lbConfirm.Width) / 2);
            lbConfirm.Top = Math.Max(0, (picture.Height - lbConfirm.Height) / 2);
            lbConfirm.BringToFront();
        }

        // ===== Hàm hiển thị log trạng thái nhỏ (an toàn thread) =====
        private void SetStatus(string s)
        {
            if (!IsHandleCreated) return;

            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() =>
                    {
                        if (lblStatus != null && !lblStatus.IsDisposed)
                            lblStatus.Text = s;
                    }));
                }
                else
                {
                    if (lblStatus != null && !lblStatus.IsDisposed)
                        lblStatus.Text = s;
                }
            }
            catch (ObjectDisposedException) { }
        }

        // ===== Hiển thị lỗi =====
        private void ShowWsError(WebSocketException ex, string where)
        {
            var sb = new StringBuilder();
            sb.AppendLine("WebSocketException at: " + where);
            sb.AppendLine("Message: " + ex.Message);
            sb.AppendLine("WebSocketErrorCode: " + ex.WebSocketErrorCode);
            try { sb.AppendLine("ErrorCode: " + ex.ErrorCode); } catch { }
            if (ex.InnerException != null)
                sb.AppendLine("Inner: " + ex.InnerException.GetType().Name + " - " + ex.InnerException.Message);
            MessageBox.Show(sb.ToString(), "WS Error");
        }

        // ===== Nút kết nối =====
        private async void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                await DisconnectAsync();

                _ws = new ClientWebSocket();
                _cts = new CancellationTokenSource();

                _ws.Options.Proxy = null;
                _ws.Options.KeepAliveInterval = TimeSpan.FromSeconds(20);

                var url = txtUrl.Text.Trim();
                if (!url.StartsWith("ws://", StringComparison.OrdinalIgnoreCase) &&
                    !url.StartsWith("wss://", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("URL phải bắt đầu bằng ws:// hoặc wss://");
                    return;
                }

                SetStatus("Connecting...");
                await _ws.ConnectAsync(new Uri(url), _cts.Token);

                // Hàng đợi xử lý (tách nhận và xử lý)
                _inbox = new BlockingCollection<string>(boundedCapacity: 8);
                _procLoop = Task.Run(() =>
                {
                    foreach (var s in _inbox.GetConsumingEnumerable())
                    {
                        try { HandleMessage(s); } catch { /* log nếu cần */ }
                    }
                });

                // Vòng nhận
                _recvLoop = ReceiveLoopAsync(_cts.Token);

                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
                SetStatus("Connected");
                Text = "Connected - Nhận diện tĩnh mạch";
            }
            catch (WebSocketException)
            {
                SetStatus("Connect failed");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connect failed: " + ex.Message);
                SetStatus("Connect failed");
            }
        }

        private async void btnDisconnect_Click(object sender, EventArgs e)
        {
            await DisconnectAsync();
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
            SetStatus("Disconnected");
            Text = "Disconnected";
        }

        private async Task DisconnectAsync()
        {
            try
            {
                if (_cts != null && !_cts.IsCancellationRequested) _cts.Cancel();
                if (_ws != null && _ws.State == WebSocketState.Open)
                    await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "bye", CancellationToken.None);
            }
            catch { }
            finally
            {
                try { _inbox?.CompleteAdding(); } catch { }
                try { if (_procLoop != null) _procLoop.Wait(200); } catch { }
                try { _inbox?.Dispose(); } catch { }
                _inbox = null;
                _procLoop = null;

                if (_ws != null) _ws.Dispose();
                _ws = null;
                if (_cts != null) _cts.Dispose();
                _cts = null;
            }
        }

        private async Task ReceiveLoopAsync(CancellationToken token)
        {
            var buffer = new ArraySegment<byte>(new byte[64 * 1024]);

            try
            {
                using (var ms = new MemoryStream(capacity: 256 * 1024))
                {
                    while (_ws != null && _ws.State == WebSocketState.Open && !token.IsCancellationRequested)
                    {
                        ms.SetLength(0);
                        WebSocketReceiveResult result;
                        do
                        {
                            result = await _ws.ReceiveAsync(buffer, token).ConfigureAwait(false);
                            if (result.MessageType == WebSocketMessageType.Close)
                            {
                                await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "closing", token).ConfigureAwait(false);
                                BeginInvoke(new Action(() => SetStatus("Closed by server")));
                                return;
                            }
                            ms.Write(buffer.Array, buffer.Offset, result.Count);
                        }
                        while (!result.EndOfMessage);

                        // Nhanh hơn: không ToArray(), decode trực tiếp từ buffer nội bộ
                        int count = (int)ms.Length;
                        var buf = ms.GetBuffer();
                        var msg = Encoding.UTF8.GetString(buf, 0, count);
                        EnqueueMessage(msg);
                    }
                }
            }
            catch (WebSocketException wsex)
            {
                BeginInvoke(new Action(() => ShowWsError(wsex, "ReceiveAsync")));
                BeginInvoke(new Action(() => SetStatus("Receive error")));
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                BeginInvoke(new Action(() => MessageBox.Show("Receive error: " + ex.Message)));
                BeginInvoke(new Action(() => SetStatus("Receive error")));
            }
        }

        private void EnqueueMessage(string s)
        {
            var q = _inbox;
            if (q == null || q.IsAddingCompleted) return;

            // Nếu đầy, bỏ bớt 1 phần tử cũ để ưu tiên thông điệp mới (giảm lag)
            if (!q.TryAdd(s))
            {
                if (q.TryTake(out _)) { q.TryAdd(s); }
            }
        }

        private void HandleMessage(string msg)
        {
            try
            {
                if (msg.IndexOf('\n') >= 0)
                {
                    var lines = msg.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var line in lines) SafeHandleJson(line);
                }
                else
                {
                    SafeHandleJson(msg);
                }
            }
            catch { }
        }

        private void SafeHandleJson(string json)
        {
            try
            {
                var jo = JObject.Parse(json);

                // frame preview (có throttle để giảm tải)
                var frame = (string)jo["frame"];
                if (!string.IsNullOrEmpty(frame))
                {
                    long nowMs = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
                    if (nowMs - _lastFrameMs >= MinFrameIntervalMs)
                    {
                        _lastFrameMs = nowMs;
                        var img = DataUrlToImage(frame);
                        if (img != null)
                        {
                            BeginInvoke(new Action(() =>
                            {
                                var old = picture.Image;
                                picture.Image = img;
                                if (old != null) old.Dispose();
                            }));
                        }
                    }
                }

                var info = (string)jo["info"];
                var success = (string)jo["success"];
                var palmId = (string)jo["palmId"];
                var feature = (string)jo["feature"];

                if (!string.IsNullOrEmpty(feature))
                    _lastFeature = feature;

                if (!string.IsNullOrEmpty(palmId))
                    BeginInvoke(new Action(() => txtPalmId.Text = palmId));

                // đọc cardNo nếu server trả về
                var cardNo = (string)jo["cardNo"] ?? (string)jo["card_no"] ?? (string)jo["cardNumber"];
                if (!string.IsNullOrEmpty(cardNo))
                    BeginInvoke(new Action(() => txtCardNo.Text = cardNo));

                // phân nhánh theo success/info
                if (!string.IsNullOrEmpty(success))
                {
                    SetStatus(success);
                    if (success.IndexOf("Successful registration", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        BeginInvoke(new Action(() =>
                        {
                            SetLbConfirm("Đăng ký thành công: " + (palmId ?? txtPalmId.Text));
                        }));
                        ThemTinhMachVaoCSDL(txtPalmId, txtCardNo);
                    }
                    else if (success.IndexOf("Modification successful", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        BeginInvoke(new Action(() =>
                            MessageBox.Show("Sửa thông tin thành công: " + (palmId ?? txtPalmId.Text),
                                            "UPDATE_PALM_VEIN")));
                    }
                    else if (success.IndexOf("Comparison successful", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        if (!string.IsNullOrEmpty(info)) SetStatus(info);
                        BeginInvoke(new Action(() =>
                        {
                            SetLbConfirm("" + palmId);

                            _ = Task.Run(() => GoiDichVu1001(palmId));
                        }));
                    }
                }
                else if (!string.IsNullOrEmpty(info))
                {
                    SetStatus(info);
                    if (info.IndexOf("successfully deleted", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        _lastFeature = null;
                        BeginInvoke(new Action(() =>
                            MessageBox.Show("Đã xoá palm vein thành công.", "DELETE_PALM_VEIN")));
                    }
                    else if (info.IndexOf("failed", StringComparison.OrdinalIgnoreCase) >= 0 ||
                             info.IndexOf("not qualified", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // có thể hiển thị thêm nếu cần
                    }
                }
            }
            catch { }
        }

        private void GoiDichVu1001(string palmId)
        {
            try
            {
                ObjectGoiDichVuMini o = new ObjectGoiDichVuMini();
                o.funcId = 1011;
                o.device = 2;
                o.currentime = FunCGeneral.timeNow();

                XacThucBanTayRequest oData = new XacThucBanTayRequest();
                oData.emptyID = palmId;
                oData.idThietBiPalm = "";

                o.data = JsonConvert.SerializeObject(oData);

                //String url = FunCGeneral.ipMayChuDonVi;
                String url = "http://192.168.1.254:58080/autobank/services/vimassTool/dieuPhoi";
                var json = JsonConvert.SerializeObject(o);
                String res = Service.SendWebrequest_POST_Method(json, url);
                Response response = JsonConvert.DeserializeObject<Response>(res);
                System.Diagnostics.Debug.WriteLine("V reques" + json.ToString());
            }
            catch (Exception ex)
            {
                try
                {
                    if (IsHandleCreated)
                        BeginInvoke(new Action(() =>
                            MessageBox.Show("GoiDichVu1001_Exception: " + ex.Message)));
                }
                catch { /* ignore */ }
            }
        }

        private void ThemTinhMachVaoCSDL(TextBox txtPalmId, TextBox txtCardNo)
        {
            try
            {
                // Sau khi đăng ký thành công 1: Lấy dữ liệu tĩnh mạch 2: Thêm vào infoVid
                // TODO: Implement theo DB thực tế của bạn
            }
            catch (Exception ex)
            {
                MessageBox.Show("ThemTinhMachVaoCSDL: " + ex.Message);
            }
        }

        private static Image DataUrlToImage(string dataUrl)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dataUrl)) return null;
                dataUrl = dataUrl.Trim();
                int comma = dataUrl.IndexOf(',');
                if (comma >= 0) dataUrl = dataUrl.Substring(comma + 1);
                dataUrl = dataUrl.Replace("\r", "").Replace("\n", "").Trim();

                var bytes = Convert.FromBase64String(dataUrl);
                using (var ms = new MemoryStream(bytes))
                using (var img = Image.FromStream(ms))
                {
                    return new Bitmap(img);
                }
            }
            catch { return null; }
        }

        private async Task SendJsonAsync(object obj)
        {
            if (_ws == null || _ws.State != WebSocketState.Open) return;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            byte[] data = Encoding.UTF8.GetBytes(json);
            await _ws.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        // ====== REGISTER ======
        private async Task SendRegisterAsync()
        {
            if (_ws == null || _ws.State != WebSocketState.Open)
            {
                MessageBox.Show("Chưa kết nối WebSocket.");
                return;
            }
            var palmId = txtPalmId.Text.Trim();
            if (palmId.Length == 0)
            {
                MessageBox.Show("Nhập palm_vein_id trước khi đăng ký.");
                return;
            }

            var cardNo = txtCardNo.Text.Trim();
            var jo = new JObject
            {
                ["command"] = "register_palm_vein",
                ["palm_vein_id"] = palmId
            };
            if (!string.IsNullOrEmpty(cardNo)) jo["card_no"] = cardNo;

            SetStatus("Sending: " + jo.ToString(Newtonsoft.Json.Formatting.None));
            await SendJsonAsync(jo);
        }

        // ====== COMPARE (Xác thực) ======
        private async Task SendCompareAsync()
        {
            if (_ws == null || _ws.State != WebSocketState.Open)
            {
                MessageBox.Show("Chưa kết nối WebSocket.");
                return;
            }

            var jo = new JObject
            {
                ["COMMAND"] = "compare_palm_vein" // theo yêu cầu: khóa in hoa
            };

            SetStatus("Sending: " + jo.ToString(Newtonsoft.Json.Formatting.None));
            await SendJsonAsync(jo);
        }

        // ====== UPDATE (Sửa) ======
        private async Task SendUpdateAsync()
        {
            if (_ws == null || _ws.State != WebSocketState.Open)
            {
                MessageBox.Show("Chưa kết nối WebSocket.");
                return;
            }
            var palmId = txtPalmId.Text.Trim();
            if (palmId.Length == 0)
            {
                MessageBox.Show("Nhập palm_vein_id để sửa.");
                return;
            }

            var cardNo = txtCardNo.Text.Trim();
            var jo = new JObject
            {
                ["command"] = "update_palm_vein",
                ["palm_vein_id"] = palmId
            };
            if (!string.IsNullOrEmpty(cardNo)) jo["card_no"] = cardNo;

            MessageBox.Show(jo.ToString(Newtonsoft.Json.Formatting.None), "WS SEND");
            SetStatus("Sending: " + jo.ToString(Newtonsoft.Json.Formatting.None));
            await SendJsonAsync(jo);
        }

        // ====== DELETE (Xoá) ======
        private async Task SendDeleteAsync()
        {
            if (_ws == null || _ws.State != WebSocketState.Open)
            {
                MessageBox.Show("Chưa kết nối WebSocket.");
                return;
            }
            var palmId = txtPalmId.Text.Trim();
            if (palmId.Length == 0)
            {
                MessageBox.Show("Nhập palm_vein_id để xoá.");
                return;
            }

            var cardNo = txtCardNo.Text.Trim();
            var jo = new JObject
            {
                ["command"] = "delete_palm_vein",
                ["palm_vein_id"] = palmId
            };
            if (!string.IsNullOrEmpty(cardNo)) jo["card_no"] = cardNo;

            MessageBox.Show(jo.ToString(Newtonsoft.Json.Formatting.None), "WS SEND");
            SetStatus("Sending: " + jo.ToString(Newtonsoft.Json.Formatting.None));
            await SendJsonAsync(jo);
        }

        protected override async void OnFormClosing(FormClosingEventArgs e)
        {
            await DisconnectAsync();
            base.OnFormClosing(e);
        }

        private void NhanDienTinhMachLongBanTay_Load(object sender, EventArgs e)
        {
            // placeholder
        }
    }
}
