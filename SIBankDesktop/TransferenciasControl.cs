using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIBankDesktop
{
    public partial class TransferenciasControl : UserControl
    {
        private const string API_BASE_URL = "http://127.0.0.1:8080";
        private static readonly HttpClient httpClient = new HttpClient();
        private long idUsuarioActivo;

        private List<CuentaResumenTransferencia> listaCuentas = new List<CuentaResumenTransferencia>();

        public TransferenciasControl(long idUsuarioActivo)
        {
            InitializeComponent();
            this.idUsuarioActivo = idUsuarioActivo;
            this.Load += async (s, e) => await CargarCuentas();
        }

        private async Task CargarCuentas()
        {
            try
            {
                string json = await httpClient.GetStringAsync(API_BASE_URL + "/cuentas");
                var cuentas = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(json);

                listaCuentas.Clear();
                foreach (var c in cuentas)
                {
                    string estado = c["ESTADO"].ToString();
                    if (estado != "A") continue; // solo cuentas activas

                    listaCuentas.Add(new CuentaResumenTransferencia
                    {
                        IdCuenta = c["ID_CUENTA"].GetInt64(),
                        NumeroCuenta = c["NUMERO_CUENTA"].ToString(),
                        Cliente = c["CLIENTE"].ToString(),
                        Saldo = c["SALDO"].GetDecimal()
                    });
                }

                LlenarCombo(cmbCuentaOrigen);
                LlenarCombo(cmbCuentaDestino);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar las cuentas.\n" + ex.Message, "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LlenarCombo(ComboBox combo)
        {
            combo.Items.Clear();
            foreach (var cuenta in listaCuentas)
            {
                combo.Items.Add(cuenta);
            }
            combo.DisplayMember = "TextoVisible";
            if (combo.Items.Count > 0) combo.SelectedIndex = 0;
        }

        private void cmbCuentaOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCuentaOrigen.SelectedItem is CuentaResumenTransferencia cuenta)
            {
                lblSaldoOrigen.Text = "Saldo disponible: Q " + cuenta.Saldo.ToString("N2");
            }
        }

        private void cmbCuentaDestino_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCuentaDestino.SelectedItem is CuentaResumenTransferencia cuenta)
            {
                lblSaldoDestino.Text = "Saldo actual: Q " + cuenta.Saldo.ToString("N2");
            }
        }

        private async void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (!(cmbCuentaOrigen.SelectedItem is CuentaResumenTransferencia origen) ||
                !(cmbCuentaDestino.SelectedItem is CuentaResumenTransferencia destino))
            {
                MostrarMensaje("Selecciona la cuenta origen y destino.", false);
                return;
            }

            if (origen.IdCuenta == destino.IdCuenta)
            {
                MostrarMensaje("La cuenta origen y destino no pueden ser la misma.", false);
                return;
            }

            if (!decimal.TryParse(txtMonto.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal monto)
                || monto <= 0)
            {
                MostrarMensaje("Ingresa un monto valido, mayor a 0.", false);
                return;
            }

            if (monto > origen.Saldo)
            {
                MostrarMensaje("El monto excede el saldo disponible en la cuenta origen.", false);
                return;
            }

            var confirmacion = MessageBox.Show(
                "Vas a transferir Q " + monto.ToString("N2") + "\n" +
                "De: " + origen.TextoVisible + "\n" +
                "A: " + destino.TextoVisible + "\n\n" +
                "Deseas continuar?",
                "SIBank Enterprise",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacion != DialogResult.Yes) return;

            btnConfirmar.Enabled = false;
            try
            {
                var body = new
                {
                    idCuentaOrigen = origen.IdCuenta,
                    idCuentaDestino = destino.IdCuenta,
                    monto = monto,
                    idUsuarioActor = idUsuarioActivo
                };

                string json = JsonSerializer.Serialize(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(API_BASE_URL + "/cuentas/transferir", content);
                string responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var resultado = JsonSerializer.Deserialize<RespuestaApi>(responseBody, options);

                if (resultado.Estado == "OK")
                {
                    MostrarMensaje("Transferencia realizada correctamente.", true);
                    txtMonto.Text = "";
                    await CargarCuentas();
                }
                else
                {
                    MostrarMensaje(resultado.Mensaje ?? "No se pudo realizar la transferencia.", false);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, false);
            }
            finally
            {
                btnConfirmar.Enabled = true;
            }
        }

        private void MostrarMensaje(string texto, bool exito)
        {
            lblMensaje.ForeColor = exito ? System.Drawing.Color.FromArgb(30, 120, 60) : System.Drawing.Color.Red;
            lblMensaje.Text = texto;
        }

        private class CuentaResumenTransferencia
        {
            public long IdCuenta { get; set; }
            public string NumeroCuenta { get; set; }
            public string Cliente { get; set; }
            public decimal Saldo { get; set; }
            public string TextoVisible => NumeroCuenta + " - " + Cliente;
            public override string ToString() => TextoVisible;
        }
    }
}