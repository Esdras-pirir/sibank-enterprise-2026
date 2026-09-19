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
    public partial class CuentasControl : UserControl
    {
        private const string API_BASE_URL = "http://127.0.0.1:8080";
        private static readonly HttpClient httpClient = new HttpClient();
        private long idUsuarioActivo;

        public CuentasControl(long idUsuarioActivo)
        {
            InitializeComponent();
            this.idUsuarioActivo = idUsuarioActivo;
            this.Load += async (s, e) => await CargarCuentas();
        }

        // ===== Trae la lista de cuentas desde la API =====
        private async Task CargarCuentas()
        {
            try
            {
                string json = await httpClient.GetStringAsync(API_BASE_URL + "/cuentas");
                var cuentas = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(json);

                DataTable tabla = new DataTable();
                tabla.Columns.Add("ID", typeof(string));
                tabla.Columns.Add("Numero de Cuenta", typeof(string));
                tabla.Columns.Add("Cliente", typeof(string));
                tabla.Columns.Add("Tipo", typeof(string));
                tabla.Columns.Add("Saldo", typeof(string));
                tabla.Columns.Add("Estado", typeof(string));

                foreach (var c in cuentas)
                {
                    tabla.Rows.Add(
                        ObtenerValor(c, "ID_CUENTA"),
                        ObtenerValor(c, "NUMERO_CUENTA"),
                        ObtenerValor(c, "CLIENTE"),
                        ObtenerValor(c, "TIPO_CUENTA"),
                        "Q " + FormatearMonto(ObtenerValor(c, "SALDO")),
                        TraducirEstado(ObtenerValor(c, "ESTADO"))
                    );
                }

                dgvCuentas.DataSource = tabla;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo cargar la lista de cuentas.\n" + ex.Message,
                    "SIBank Enterprise",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
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
            if (decimal.TryParse(valor, out decimal monto))
                return monto.ToString("N2");
            return valor;
        }

        private string TraducirEstado(string estado)
        {
            switch (estado)
            {
                case "A": return "Activa";
                case "I": return "Inactiva";
                case "C": return "Cerrada";
                default: return estado;
            }
        }

        // ===== Boton: Actualizar lista =====
        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            await CargarCuentas();
        }

        // ===== Boton: Ver Saldo (consulta en tiempo real via fn_obtener_saldo) =====
        private async void btnVerSaldo_Click(object sender, EventArgs e)
        {
            if (dgvCuentas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona una cuenta de la tabla primero.", "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var fila = dgvCuentas.SelectedRows[0];
            long idCuenta = long.Parse(fila.Cells["ID"].Value.ToString());
            string numeroCuenta = fila.Cells["Numero de Cuenta"].Value.ToString();

            try
            {
                string json = await httpClient.GetStringAsync(API_BASE_URL + "/cuentas/saldo/" + idCuenta);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var resultado = JsonSerializer.Deserialize<SaldoResponse>(json, options);

                MessageBox.Show(
                    "Cuenta: " + numeroCuenta + "\nSaldo actual: Q " + resultado.Saldo.ToString("N2"),
                    "SIBank Enterprise",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar el saldo: " + ex.Message, "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== Boton: Abrir Cuenta nueva =====
        private async void btnNueva_Click(object sender, EventArgs e)
        {
            try
            {
                // Traemos clientes, tipos de cuenta y sucursales para llenar los combos
                string jsonClientes = await httpClient.GetStringAsync(API_BASE_URL + "/clientes");
                string jsonTipos = await httpClient.GetStringAsync(API_BASE_URL + "/cuentas/tipos");
                string jsonSucursales = await httpClient.GetStringAsync(API_BASE_URL + "/cuentas/sucursales");

                var clientes = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(jsonClientes);
                var tipos = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(jsonTipos);
                var sucursales = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(jsonSucursales);

                using (var formulario = new CuentaFormulario(clientes, tipos, sucursales))
                {
                    if (formulario.ShowDialog() == DialogResult.OK)
                    {
                        await AbrirCuenta(formulario);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar los datos necesarios: " + ex.Message, "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task AbrirCuenta(CuentaFormulario f)
        {
            try
            {
                var body = new
                {
                    idCliente = f.IdCliente,
                    idTipoCuenta = f.IdTipoCuenta,
                    idSucursal = f.IdSucursal,
                    saldoInicial = f.SaldoInicial,
                    idUsuarioActor = idUsuarioActivo
                };

                string json = JsonSerializer.Serialize(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(API_BASE_URL + "/cuentas/abrir", content);
                string responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var resultado = JsonSerializer.Deserialize<RespuestaApi>(responseBody, options);

                if (resultado.Estado == "OK")
                {
                    MessageBox.Show("Cuenta abierta correctamente.", "SIBank Enterprise",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarCuentas();
                }
                else
                {
                    MessageBox.Show(resultado.Mensaje ?? "No se pudo abrir la cuenta.", "SIBank Enterprise",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir la cuenta: " + ex.Message, "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class SaldoResponse
    {
        public long IdCuenta { get; set; }
        public decimal Saldo { get; set; }
        public string Estado { get; set; }
    }
}