using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIBankDesktop
{
    public partial class Form1 : Form
    {
        private const string API_BASE_URL = "http://127.0.0.1:8080";
        private static readonly HttpClient httpClient = new HttpClient();

        // Permite mover la ventana sin bordes desde la barra
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private static extern void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        public Form1()
        {
            InitializeComponent();
            ConfigurarEstilosVisuales();
            ConfigurarEventos();
        }

        private void ConfigurarEstilosVisuales()
        {
            // Redondear bordes del panel principal (Card)
            pnlCard.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = pnlCard.ClientRectangle;
                int radius = 18;
                using (GraphicsPath path = GetRoundedRectanglePath(rect, radius))
                using (Pen borderPen = new Pen(Color.FromArgb(40, 55, 85), 1.5f))
                {
                    pnlCard.Region = new Region(path);
                    e.Graphics.DrawPath(borderPen, path);
                }
            };

            // Redondear el botón
            btnLogin.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = btnLogin.ClientRectangle;
                using (GraphicsPath path = GetRoundedRectanglePath(rect, 10))
                {
                    btnLogin.Region = new Region(path);
                }
            };
        }

        private void ConfigurarEventos()
        {
            // Permitir arrastrar el formulario sin bordes desde el fondo
            pnlBackground.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage(this.Handle, 0x112, 0xf012, 0);
                }
            };

            // Efectos de foco en los campos de texto (Subrayado reactivo Cyan)
            txtUsuario.GotFocus += (s, e) => pnlUsuarioUnderline.BackColor = Color.FromArgb(0, 210, 255);
            txtUsuario.LostFocus += (s, e) => pnlUsuarioUnderline.BackColor = Color.FromArgb(50, 65, 95);

            txtPassword.GotFocus += (s, e) => pnlPasswordUnderline.BackColor = Color.FromArgb(0, 210, 255);
            txtPassword.LostFocus += (s, e) => pnlPasswordUnderline.BackColor = Color.FromArgb(50, 65, 95);

            // Manejo de eventos de entrada y acciones
            txtUsuario.KeyDown += txtUsuario_KeyDown;
            txtPassword.KeyDown += txtPassword_KeyDown;
            txtPassword.KeyUp += PasswordField_KeyUp;
            chkMostrarPassword.CheckedChanged += chkMostrarPassword_CheckedChanged;
            btnLogin.Click += btnLogin_Click;
        }

        private GraphicsPath GetRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = cornerRadius * 2;
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnLogin_Click(sender, e);
            }
        }

        private void PasswordField_KeyUp(object sender, KeyEventArgs e)
        {
            lblCapsLock.Visible = Control.IsKeyLocked(Keys.CapsLock);
        }

        private void chkMostrarPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkMostrarPassword.Checked ? '\0' : '●';
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                lblMensaje.ForeColor = Color.FromArgb(255, 100, 100);
                lblMensaje.Text = "Por favor, ingresa tu usuario y contraseña.";
                return;
            }

            btnLogin.Enabled = false;
            btnLogin.Text = "AUTENTICANDO...";
            lblMensaje.Text = "";

            try
            {
                var resultado = await LoginAsync(usuario, password);

                if (resultado != null && resultado.Estado == "OK")
                {
                    lblMensaje.ForeColor = Color.FromArgb(0, 230, 150);
                    lblMensaje.Text = "¡Acceso concedido! Redirigiendo...";

                    DashboardForm dashboard = new DashboardForm(resultado.IdUsuario ?? 0, usuario);
                    dashboard.FormClosed += (s2, e2) => this.Close();
                    this.Hide();
                    dashboard.Show();
                }
                else
                {
                    lblMensaje.ForeColor = Color.FromArgb(255, 80, 100);
                    lblMensaje.Text = resultado?.Mensaje ?? "Credenciales inválidas.";
                }
            }
            catch (HttpRequestException)
            {
                lblMensaje.ForeColor = Color.FromArgb(255, 80, 100);
                lblMensaje.Text = "Error de conexión: No se pudo establecer comunicación con el servidor.";
            }
            catch (Exception ex)
            {
                lblMensaje.ForeColor = Color.FromArgb(255, 80, 100);
                lblMensaje.Text = "Error inesperado: " + ex.Message;
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "INICIAR SESIÓN";
            }
        }

        private async Task<LoginResponse> LoginAsync(string username, string password)
        {
            var body = new { username = username, password = password };
            string json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(API_BASE_URL + "/usuarios/login", content);
            string responseBody = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<LoginResponse>(responseBody, options);
        }
    }

    public class LoginResponse
    {
        public string Estado { get; set; }
        public long? IdUsuario { get; set; }
        public string Mensaje { get; set; }
    }
}