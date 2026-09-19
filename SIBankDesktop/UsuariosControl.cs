using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIBankDesktop
{
    public partial class UsuariosControl : UserControl
    {
        private const string API_BASE_URL = "http://127.0.0.1:8080";
        private static readonly HttpClient httpClient = new HttpClient();
        private long idUsuarioActivo;

        public UsuariosControl(long idUsuarioActivo)
        {
            InitializeComponent();
            ConfigurarRenderizadoDobleBuffer();
            this.idUsuarioActivo = idUsuarioActivo;
            this.Load += async (s, e) => await CargarUsuarios();
        }

        private void ConfigurarRenderizadoDobleBuffer()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint, true);
            this.UpdateStyles();
        }

        // ===== Trae la lista de usuarios desde la API =====
        private async Task CargarUsuarios()
        {
            try
            {
                string json = await httpClient.GetStringAsync(API_BASE_URL + "/usuarios");
                var usuarios = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(json) ?? new List<Dictionary<string, JsonElement>>();

                DataTable tabla = new DataTable();
                tabla.Columns.Add("ID", typeof(string));
                tabla.Columns.Add("Usuario", typeof(string));
                tabla.Columns.Add("Correo", typeof(string));
                tabla.Columns.Add("Rol", typeof(string));
                tabla.Columns.Add("Estado", typeof(string));
                tabla.Columns.Add("Intentos Fallidos", typeof(string));
                tabla.Columns.Add("Ultimo Login", typeof(string));

                int countActivos = 0;
                int countInactivos = 0;

                foreach (var u in usuarios)
                {
                    string estadoTexto = ObtenerValor(u, "ACTIVO") == "S" ? "Activo" : "Inactivo";
                    if (estadoTexto == "Activo") countActivos++; else countInactivos++;

                    tabla.Rows.Add(
                        ObtenerValor(u, "ID_USUARIO"),
                        ObtenerValor(u, "USERNAME"),
                        ObtenerValor(u, "CORREO"),
                        ObtenerValor(u, "ROL"),
                        estadoTexto,
                        ObtenerValor(u, "INTENTOS_FALLIDOS"),
                        FormatearFechaOpcional(ObtenerValor(u, "ULTIMO_LOGIN"))
                    );
                }

                dgvUsuarios.DataSource = tabla;
                ActualizarKPIs(usuarios.Count, countActivos, countInactivos);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar la lista de usuarios.\n" + ex.Message, "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarKPIs(int total, int activos, int inactivos)
        {
            lblValTotal.Text = total.ToString();
            lblValActivos.Text = activos.ToString();
            lblValInactivos.Text = inactivos.ToString();
        }

        private string ObtenerValor(Dictionary<string, JsonElement> fila, string clave)
        {
            if (!fila.ContainsKey(clave)) return "";
            var elemento = fila[clave];
            if (elemento.ValueKind == JsonValueKind.Null) return "";
            return elemento.ToString();
        }

        private string FormatearFechaOpcional(string valor)
        {
            if (string.IsNullOrEmpty(valor)) return "Nunca";
            if (DateTime.TryParse(valor, out DateTime dt)) return dt.ToString("dd/MM/yyyy HH:mm");
            return valor;
        }

        // ===== Colorea la columna Estado: verde si Activo, rojo si Inactivo =====
        private void dgvUsuarios_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvUsuarios.Columns[e.ColumnIndex].Name != "Estado") return;
            if (e.Value == null) return;

            e.CellStyle.Font = new Font(dgvUsuarios.Font, FontStyle.Bold);
            if (e.Value.ToString() == "Activo")
                e.CellStyle.ForeColor = Color.FromArgb(52, 211, 153);
            else
                e.CellStyle.ForeColor = Color.FromArgb(251, 113, 133);
        }

        // ===== Botón: Actualizar =====
        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            await CargarUsuarios();
        }

        // ===== Botón: Nuevo Usuario =====
        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                string jsonRoles = await httpClient.GetStringAsync(API_BASE_URL + "/usuarios/roles");
                var roles = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(jsonRoles);

                using (var formulario = new UsuarioFormulario(roles))
                {
                    if (formulario.ShowDialog() == DialogResult.OK)
                    {
                        await RegistrarUsuario(formulario);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar los roles: " + ex.Message, "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task RegistrarUsuario(UsuarioFormulario f)
        {
            try
            {
                var body = new
                {
                    username = f.Username,
                    password = f.Password,
                    correo = f.Correo,
                    idRol = f.IdRol,
                    idUsuarioActor = idUsuarioActivo
                };

                string json = JsonSerializer.Serialize(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(API_BASE_URL + "/usuarios/registrar", content);
                string responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var resultado = JsonSerializer.Deserialize<RespuestaApi>(responseBody, options);

                if (resultado.Estado == "OK")
                {
                    MessageBox.Show("Usuario registrado correctamente.", "SIBank Enterprise",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarUsuarios();
                }
                else
                {
                    MessageBox.Show(resultado.Mensaje ?? "No se pudo registrar el usuario.", "SIBank Enterprise",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar: " + ex.Message, "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== Botones: Activar / Desactivar =====
        private async void btnActivar_Click(object sender, EventArgs e)
        {
            await CambiarEstado("S");
        }

        private async void btnDesactivar_Click(object sender, EventArgs e)
        {
            await CambiarEstado("N");
        }

        private async Task CambiarEstado(string nuevoEstado)
        {
            if (dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un usuario de la tabla primero.", "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var fila = dgvUsuarios.SelectedRows[0];
            long idUsuario = long.Parse(fila.Cells["ID"].Value.ToString());
            string nombreUsuario = fila.Cells["Usuario"].Value.ToString();

            if (nombreUsuario == "admin" && nuevoEstado == "N")
            {
                MessageBox.Show("No puedes desactivar al usuario administrador principal.", "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string accion = nuevoEstado == "S" ? "activar" : "desactivar";
            var confirmacion = MessageBox.Show(
                "¿Deseas " + accion + " al usuario \"" + nombreUsuario + "\"?",
                "SIBank Enterprise",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacion != DialogResult.Yes) return;

            try
            {
                var body = new { idUsuario = idUsuario, nuevoEstado = nuevoEstado, idUsuarioActor = idUsuarioActivo };
                string json = JsonSerializer.Serialize(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(API_BASE_URL + "/usuarios/cambiar-estado", content);
                string responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var resultado = JsonSerializer.Deserialize<RespuestaApi>(responseBody, options);

                if (resultado.Estado == "OK")
                {
                    MessageBox.Show("Estado actualizado correctamente.", "SIBank Enterprise",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarUsuarios();
                }
                else
                {
                    MessageBox.Show(resultado.Mensaje ?? "No se pudo actualizar el estado.", "SIBank Enterprise",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== Botón: Resetear Contraseña =====
        private async void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un usuario de la tabla primero.", "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var fila = dgvUsuarios.SelectedRows[0];
            long idUsuario = long.Parse(fila.Cells["ID"].Value.ToString());
            string nombreUsuario = fila.Cells["Usuario"].Value.ToString();

            using (var formulario = new ResetPasswordFormulario(nombreUsuario))
            {
                if (formulario.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var body = new
                        {
                            idUsuario = idUsuario,
                            passwordNuevo = formulario.PasswordNuevo,
                            idUsuarioActor = idUsuarioActivo
                        };
                        string json = JsonSerializer.Serialize(body);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        var response = await httpClient.PostAsync(API_BASE_URL + "/usuarios/resetear-password", content);
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var resultado = JsonSerializer.Deserialize<RespuestaApi>(responseBody, options);

                        if (resultado.Estado == "OK")
                        {
                            MessageBox.Show("Contraseña reseteada correctamente.", "SIBank Enterprise",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(resultado.Mensaje ?? "No se pudo resetear la contraseña.", "SIBank Enterprise",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "SIBank Enterprise",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }

    // Panel personalizado con gradiente continuo Anti-Flicker
    public class CyberGradientPanel : Panel
    {
        public Color ColorGradientStart { get; set; } = Color.FromArgb(15, 23, 42);
        public Color ColorGradientEnd { get; set; } = Color.FromArgb(30, 41, 59);

        protected override void OnPaint(PaintEventArgs e)
        {
            using (var brush = new LinearGradientBrush(this.ClientRectangle, ColorGradientStart, ColorGradientEnd, 45F))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
            base.OnPaint(e);
        }
    }
}