using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIBankDesktop
{
    public partial class PrestamosControl : UserControl
    {
        private const string API_BASE_URL = "http://127.0.0.1:8080";
        private static readonly HttpClient httpClient = new HttpClient();
        private long idUsuarioActivo;

        public PrestamosControl(long idUsuarioActivo)
        {
            InitializeComponent();
            this.idUsuarioActivo = idUsuarioActivo;
            this.Load += async (s, e) => await CargarPrestamos();
        }

        // ===== Trae la lista de prestamos desde la API =====
        private async Task CargarPrestamos()
        {
            try
            {
                string json = await httpClient.GetStringAsync(API_BASE_URL + "/prestamos");
                var prestamos = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(json);

                DataTable tabla = new DataTable();
                tabla.Columns.Add("ID", typeof(string));
                tabla.Columns.Add("Cliente", typeof(string));
                tabla.Columns.Add("Tipo", typeof(string));
                tabla.Columns.Add("Monto", typeof(string));
                tabla.Columns.Add("Plazo (meses)", typeof(string));
                tabla.Columns.Add("Tasa %", typeof(string));
                tabla.Columns.Add("Cuota Mensual", typeof(string));
                tabla.Columns.Add("Saldo Pendiente", typeof(string));
                tabla.Columns.Add("Estado", typeof(string));

                foreach (var p in prestamos)
                {
                    tabla.Rows.Add(
                        ObtenerValor(p, "ID_PRESTAMO"),
                        ObtenerValor(p, "CLIENTE"),
                        ObtenerValor(p, "TIPO_PRESTAMO"),
                        "Q " + FormatearMonto(ObtenerValor(p, "MONTO")),
                        ObtenerValor(p, "PLAZO_MESES"),
                        ObtenerValor(p, "TASA_INTERES"),
                        FormatearOpcional(ObtenerValor(p, "CUOTA_MENSUAL")),
                        FormatearOpcional(ObtenerValor(p, "SALDO_PENDIENTE")),
                        TraducirEstado(ObtenerValor(p, "ESTADO"))
                    );
                }

                dgvPrestamos.DataSource = tabla;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar la lista de prestamos.\n" + ex.Message, "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ObtenerValor(Dictionary<string, JsonElement> fila, string clave)
        {
            if (!fila.ContainsKey(clave)) return "";
            var elemento = fila[clave];
            if (elemento.ValueKind == JsonValueKind.Null) return "";
            return elemento.ToString();
        }

        private string FormatearMonto(string valor)
        {
            if (decimal.TryParse(valor, out decimal monto)) return monto.ToString("N2");
            return valor;
        }

        private string FormatearOpcional(string valor)
        {
            if (string.IsNullOrEmpty(valor)) return "-";
            if (decimal.TryParse(valor, out decimal monto)) return "Q " + monto.ToString("N2");
            return valor;
        }

        private string TraducirEstado(string estado)
        {
            switch (estado)
            {
                case "PENDIENTE": return "Pendiente";
                case "APROBADO": return "Aprobado";
                case "RECHAZADO": return "Rechazado";
                case "PAGADO": return "Pagado";
                default: return estado;
            }
        }

        // ===== Boton: Actualizar lista =====
        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            await CargarPrestamos();
        }

        // ===== Boton: Nueva Solicitud =====
        private async void btnNueva_Click(object sender, EventArgs e)
        {
            try
            {
                string jsonClientes = await httpClient.GetStringAsync(API_BASE_URL + "/clientes");
                string jsonTipos = await httpClient.GetStringAsync(API_BASE_URL + "/prestamos/tipos");
                string jsonCuentas = await httpClient.GetStringAsync(API_BASE_URL + "/cuentas");

                var clientes = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(jsonClientes);
                var tipos = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(jsonTipos);
                var cuentas = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(jsonCuentas);

                using (var formulario = new PrestamoFormulario(clientes, tipos, cuentas))
                {
                    if (formulario.ShowDialog() == DialogResult.OK)
                    {
                        await SolicitarPrestamo(formulario);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar los datos necesarios: " + ex.Message, "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task SolicitarPrestamo(PrestamoFormulario f)
        {
            try
            {
                var body = new
                {
                    idCliente = f.IdCliente,
                    idTipoPrestamo = f.IdTipoPrestamo,
                    idCuentaDesembolso = f.IdCuentaDesembolso,
                    monto = f.Monto,
                    plazoMeses = f.PlazoMeses,
                    idUsuarioActor = idUsuarioActivo
                };

                string json = JsonSerializer.Serialize(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(API_BASE_URL + "/prestamos/solicitar", content);
                string responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var resultado = JsonSerializer.Deserialize<RespuestaApi>(responseBody, options);

                if (resultado.Estado == "OK")
                {
                    MessageBox.Show("Solicitud de prestamo registrada correctamente.\nQueda en estado Pendiente.",
                        "SIBank Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarPrestamos();
                }
                else
                {
                    MessageBox.Show(resultado.Mensaje ?? "No se pudo registrar la solicitud.", "SIBank Enterprise",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al solicitar el prestamo: " + ex.Message, "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== Boton: Aprobar =====
        private async void btnAprobar_Click(object sender, EventArgs e)
        {
            await ProcesarAprobacion("S", "aprobar");
        }

        // ===== Boton: Rechazar =====
        private async void btnRechazar_Click(object sender, EventArgs e)
        {
            await ProcesarAprobacion("N", "rechazar");
        }

        private async Task ProcesarAprobacion(string valorAprobar, string accionTexto)
        {
            if (dgvPrestamos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un prestamo de la tabla primero.", "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var fila = dgvPrestamos.SelectedRows[0];
            long idPrestamo = long.Parse(fila.Cells["ID"].Value.ToString());
            string estadoActual = fila.Cells["Estado"].Value.ToString();

            if (estadoActual != "Pendiente")
            {
                MessageBox.Show("Solo se pueden " + accionTexto + " prestamos en estado Pendiente.", "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacion = MessageBox.Show(
                "Deseas " + accionTexto + " este prestamo?",
                "SIBank Enterprise",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacion != DialogResult.Yes) return;

            try
            {
                var body = new
                {
                    idPrestamo = idPrestamo,
                    aprobar = valorAprobar,
                    idEmpleado = idUsuarioActivo, // simplificacion: se usa el mismo id de usuario como empleado
                    idUsuarioActor = idUsuarioActivo
                };

                string json = JsonSerializer.Serialize(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(API_BASE_URL + "/prestamos/aprobar", content);
                string responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var resultado = JsonSerializer.Deserialize<RespuestaApi>(responseBody, options);

                if (resultado.Estado == "OK")
                {
                    MessageBox.Show("Prestamo procesado correctamente.", "SIBank Enterprise",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarPrestamos();
                }
                else
                {
                    MessageBox.Show(resultado.Mensaje ?? "No se pudo procesar el prestamo.", "SIBank Enterprise",
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