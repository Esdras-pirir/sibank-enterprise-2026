using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Windows.Forms;

namespace SIBankDesktop
{
    public partial class CuentaFormulario : Form
    {
        public long IdCliente { get; private set; }
        public long IdTipoCuenta { get; private set; }
        public long IdSucursal { get; private set; }
        public decimal SaldoInicial { get; private set; }

        // Guardamos los datos originales para poder leer el ID seleccionado en cada combo
        private List<Dictionary<string, JsonElement>> clientes;
        private List<Dictionary<string, JsonElement>> tipos;
        private List<Dictionary<string, JsonElement>> sucursales;

        public CuentaFormulario(
            List<Dictionary<string, JsonElement>> clientes,
            List<Dictionary<string, JsonElement>> tipos,
            List<Dictionary<string, JsonElement>> sucursales)
        {
            InitializeComponent();
            this.clientes = clientes;
            this.tipos = tipos;
            this.sucursales = sucursales;

            LlenarCombo(cmbCliente, clientes, "ID_CLIENTE", "NOMBRES", "APELLIDOS");
            LlenarCombo(cmbTipoCuenta, tipos, "ID_TIPOCUENTA", "NOMBRE");
            LlenarCombo(cmbSucursal, sucursales, "ID_SUCURSAL", "NOMBRE");
        }

        // Llena un combo mostrando texto legible pero guardando el ID como "tag" de cada item
        private void LlenarCombo(ComboBox combo, List<Dictionary<string, JsonElement>> datos,
            string campoId, string campoTexto1, string campoTexto2 = null)
        {
            combo.Items.Clear();
            foreach (var fila in datos)
            {
                string texto = fila[campoTexto1].ToString();
                if (campoTexto2 != null) texto += " " + fila[campoTexto2].ToString();

                long id = fila[campoId].GetInt64();
                combo.Items.Add(new ComboItem { Id = id, Texto = texto });
            }
            combo.DisplayMember = "Texto";
            if (combo.Items.Count > 0) combo.SelectedIndex = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbCliente.SelectedItem == null || cmbTipoCuenta.SelectedItem == null || cmbSucursal.SelectedItem == null)
            {
                MessageBox.Show("Selecciona cliente, tipo de cuenta y sucursal.", "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtSaldoInicial.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal saldo)
                || saldo < 0)
            {
                MessageBox.Show("Ingresa un saldo inicial valido (numero mayor o igual a 0).", "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            IdCliente = ((ComboItem)cmbCliente.SelectedItem).Id;
            IdTipoCuenta = ((ComboItem)cmbTipoCuenta.SelectedItem).Id;
            IdSucursal = ((ComboItem)cmbSucursal.SelectedItem).Id;
            SaldoInicial = saldo;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Clase auxiliar para guardar Id + Texto dentro de cada item del combo
        private class ComboItem
        {
            public long Id { get; set; }
            public string Texto { get; set; }
            public override string ToString() => Texto;
        }
    }
}