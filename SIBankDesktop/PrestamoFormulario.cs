using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Windows.Forms;

namespace SIBankDesktop
{
    public partial class PrestamoFormulario : Form
    {
        public long IdCliente { get; private set; }
        public long IdTipoPrestamo { get; private set; }
        public long IdCuentaDesembolso { get; private set; }
        public decimal Monto { get; private set; }
        public int PlazoMeses { get; private set; }

        private List<Dictionary<string, JsonElement>> tipos;

        public PrestamoFormulario(
            List<Dictionary<string, JsonElement>> clientes,
            List<Dictionary<string, JsonElement>> tipos,
            List<Dictionary<string, JsonElement>> cuentas)
        {
            InitializeComponent();
            this.tipos = tipos;

            LlenarCombo(cmbCliente, clientes, "ID_CLIENTE", "NOMBRES", "APELLIDOS");
            LlenarCombo(cmbTipoPrestamo, tipos, "ID_TIPOPRESTAMO", "NOMBRE");
            LlenarCombo(cmbCuentaDesembolso, cuentas, "ID_CUENTA", "NUMERO_CUENTA", "CLIENTE");

            ActualizarInfoTipo();
        }

        private void LlenarCombo(ComboBox combo, List<Dictionary<string, JsonElement>> datos,
            string campoId, string campoTexto1, string campoTexto2 = null)
        {
            combo.Items.Clear();
            foreach (var fila in datos)
            {
                string texto = fila[campoTexto1].ToString();
                if (campoTexto2 != null) texto += " - " + fila[campoTexto2].ToString();

                long id = fila[campoId].GetInt64();
                combo.Items.Add(new ComboItem { Id = id, Texto = texto });
            }
            combo.DisplayMember = "Texto";
            if (combo.Items.Count > 0) combo.SelectedIndex = 0;
        }

        private void cmbTipoPrestamo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarInfoTipo();
        }

        private void ActualizarInfoTipo()
        {
            if (!(cmbTipoPrestamo.SelectedItem is ComboItem seleccionado)) return;

            var fila = tipos.Find(t => t["ID_TIPOPRESTAMO"].GetInt64() == seleccionado.Id);
            if (fila == null) return;

            string tasa = fila["TASA_INTERES_ANUAL"].ToString();
            string plazoMax = fila["PLAZO_MAX_MESES"].ToString();
            lblInfoTipo.Text = "Tasa anual: " + tasa + "%  |  Plazo maximo: " + plazoMax + " meses";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbCliente.SelectedItem == null || cmbTipoPrestamo.SelectedItem == null ||
                cmbCuentaDesembolso.SelectedItem == null)
            {
                MessageBox.Show("Selecciona cliente, tipo de prestamo y cuenta de desembolso.", "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtMonto.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal monto)
                || monto <= 0)
            {
                MessageBox.Show("Ingresa un monto valido, mayor a 0.", "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtPlazoMeses.Text, out int plazo) || plazo <= 0)
            {
                MessageBox.Show("Ingresa un plazo en meses valido (numero entero mayor a 0).", "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            IdCliente = ((ComboItem)cmbCliente.SelectedItem).Id;
            IdTipoPrestamo = ((ComboItem)cmbTipoPrestamo.SelectedItem).Id;
            IdCuentaDesembolso = ((ComboItem)cmbCuentaDesembolso.SelectedItem).Id;
            Monto = monto;
            PlazoMeses = plazo;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private class ComboItem
        {
            public long Id { get; set; }
            public string Texto { get; set; }
            public override string ToString() => Texto;
        }
    }
}