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
    public partial class DepositosRetirosControl : UserControl
    {
        private const string API_BASE_URL = "http://127.0.0.1:8080";
        private static readonly HttpClient httpClient = new HttpClient();
        private long idUsuarioActivo;

        // Guardamos la lista de cuentas para poder leer el id y el saldo de la seleccionada
        private List<CuentaResumen> listaCuentas = new List<CuentaResumen>();

        public DepositosRetirosControl(long idUsuarioActivo)
        {
            InitializeComponent();
            this.idUsuarioActivo = idUsuarioActivo;
            this.Load += async (s, e) => await CargarCuentasEnCombos();
        }

        // ===== Carga las cuentas activas y llena ambos combos (Deposito y Retiro) =====
        private async Task CargarCuentasEnCombos()
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

                    listaCuentas.Add(new CuentaResumen
                    {
                        IdCuenta = c["ID_CUENTA"].GetInt64(),
                        NumeroCuenta = c["NUMERO_CUENTA"].ToString(),
                        Cliente = c["CLIENTE"].ToString(),
                        Saldo = c["SALDO"].GetDecimal()
                    });
                }

                LlenarCombo(cmbCuentaDeposito);
                LlenarCombo(cmbCuentaRetiro);
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

        // ===== Al cambiar de cuenta seleccionada, actualizamos el label de saldo =====
        private void cmbCuentaDeposito_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCuentaDeposito.SelectedItem is CuentaResumen cuenta)
            {
                lblSaldoActualDeposito.Text = "Saldo actual: Q " + cuenta.Saldo.ToString("N2");
            }
        }

        private void cmbCuentaRetiro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCuentaRetiro.SelectedItem is CuentaResumen cuenta)
            {
                lblSaldoActualRetiro.Text = "Saldo actual: Q " + cuenta.Saldo.ToString("N2");
            }
        }

        // ===== Boton: Confirmar Deposito =====
        private async void btnConfirmarDeposito_Click(object sender, EventArgs e)
        {
            if (!(cmbCuentaDeposito.SelectedItem is CuentaResumen cuenta))
            {
                MostrarMensaje(lblMensajeDeposito, "Selecciona una cuenta.", false);
                return;
            }

            if (!decimal.TryParse(txtMontoDeposito.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal monto)
                || monto <= 0)
            {
                MostrarMensaje(lblMensajeDeposito, "Ingresa un monto valido, mayor a 0.", false);
                return;
            }

            btnConfirmarDeposito.Enabled = false;
            try
            {
                var body = new
                {
                    idCuenta = cuenta.IdCuenta,
                    monto = monto,
                    descripcion = string.IsNullOrWhiteSpace(txtDescripcionDeposito.Text)
                        ? "Deposito en ventanilla" : txtDescripcionDeposito.Text.Trim(),
                    idUsuarioActor = idUsuarioActivo
                };

                var resultado = await EnviarOperacion("/cuentas/depositar", body);

                if (resultado.Estado == "OK")
                {
                    MostrarMensaje(lblMensajeDeposito, "Deposito realizado correctamente.", true);
                    txtMontoDeposito.Text = "";
                    txtDescripcionDeposito.Text = "";
                    await CargarCuentasEnCombos();
                }
                else
                {
                    MostrarMensaje(lblMensajeDeposito, resultado.Mensaje ?? "No se pudo realizar el deposito.", false);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(lblMensajeDeposito, "Error: " + ex.Message, false);
            }
            finally
            {
                btnConfirmarDeposito.Enabled = true;
            }
        }

        // ===== Boton: Confirmar Retiro =====
        private async void btnConfirmarRetiro_Click(object sender, EventArgs e)
        {
            if (!(cmbCuentaRetiro.SelectedItem is CuentaResumen cuenta))
            {
                MostrarMensaje(lblMensajeRetiro, "Selecciona una cuenta.", false);
                return;
            }

            if (!decimal.TryParse(txtMontoRetiro.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal monto)
                || monto <= 0)
            {
                MostrarMensaje(lblMensajeRetiro, "Ingresa un monto valido, mayor a 0.", false);
                return;
            }

            if (monto > cuenta.Saldo)
            {
                MostrarMensaje(lblMensajeRetiro, "El monto excede el saldo disponible en la cuenta.", false);
                return;
            }

            btnConfirmarRetiro.Enabled = false;
            try
            {
                var body = new
                {
                    idCuenta = cuenta.IdCuenta,
                    monto = monto,
                    descripcion = string.IsNullOrWhiteSpace(txtDescripcionRetiro.Text)
                        ? "Retiro en ventanilla" : txtDescripcionRetiro.Text.Trim(),
                    idUsuarioActor = idUsuarioActivo
                };

                var resultado = await EnviarOperacion("/cuentas/retirar", body);

                if (resultado.Estado == "OK")
                {
                    MostrarMensaje(lblMensajeRetiro, "Retiro realizado correctamente.", true);
                    txtMontoRetiro.Text = "";
                    txtDescripcionRetiro.Text = "";
                    await CargarCuentasEnCombos();
                }
                else
                {
                    MostrarMensaje(lblMensajeRetiro, resultado.Mensaje ?? "No se pudo realizar el retiro.", false);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(lblMensajeRetiro, "Error: " + ex.Message, false);
            }
            finally
            {
                btnConfirmarRetiro.Enabled = true;
            }
        }

        // ===== Metodo compartido para llamar deposito o retiro =====
        private async Task<RespuestaApi> EnviarOperacion(string endpoint, object body)
        {
            string json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(API_BASE_URL + endpoint, content);
            string responseBody = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<RespuestaApi>(responseBody, options);
        }

        private void MostrarMensaje(Label lbl, string texto, bool exito)
        {
            lbl.ForeColor = exito ? System.Drawing.Color.FromArgb(30, 120, 60) : System.Drawing.Color.Red;
            lbl.Text = texto;
        }

        // Representa una cuenta dentro del combo, mostrando texto legible
        private class CuentaResumen
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