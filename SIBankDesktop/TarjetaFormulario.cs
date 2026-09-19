using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows.Forms;

namespace SIBankDesktop
{
    public partial class TarjetaFormulario : Form
    {
        public long IdCuenta { get; private set; }
        public long IdTipoTarjeta { get; private set; }

        private List<Dictionary<string, JsonElement>> cuentas;
        private List<Dictionary<string, JsonElement>> tipos;

        public TarjetaFormulario(
            List<Dictionary<string, JsonElement>> cuentas,
            List<Dictionary<string, JsonElement>> tipos)
        {
            InitializeComponent();
            this.cuentas = cuentas;
            this.tipos = tipos;

            LlenarCombo(cmbCuenta, cuentas, "ID_CUENTA", "NUMERO_CUENTA", "CLIENTE");
            LlenarCombo(cmbTipoTarjeta, tipos, "ID_TIPOTARJETA", "NOMBRE");

            ActualizarPreview();
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

        private void cmbCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarPreview();
        }

        private void cmbTipoTarjeta_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarPreview();
        }

        // ===== Actualiza la tarjeta visual de vista previa segun lo seleccionado =====
        private void ActualizarPreview()
        {
            if (cmbCuenta.SelectedItem is ComboItem cuentaSel)
            {
                var filaCuenta = cuentas.Find(c => c["ID_CUENTA"].GetInt64() == cuentaSel.Id);
                if (filaCuenta != null)
                {
                    tarjetaPreview.Cliente = filaCuenta["CLIENTE"].ToString();
                }
            }

            if (cmbTipoTarjeta.SelectedItem is ComboItem tipoSel)
            {
                var filaTipo = tipos.Find(t => t["ID_TIPOTARJETA"].GetInt64() == tipoSel.Id);
                if (filaTipo != null)
                {
                    tarjetaPreview.TipoTarjeta = filaTipo["NOMBRE"].ToString();
                    tarjetaPreview.Marca = filaTipo.ContainsKey("MARCA") ? filaTipo["MARCA"].ToString() : "";

                    string limite = filaTipo.ContainsKey("LIMITE_DEFAULT") && filaTipo["LIMITE_DEFAULT"].ValueKind != JsonValueKind.Null
                        ? filaTipo["LIMITE_DEFAULT"].ToString() : null;
                    lblInfoTipo.Text = limite != null
                        ? "Limite por defecto: Q " + decimal.Parse(limite).ToString("N2")
                        : "";
                }
            }

            tarjetaPreview.Invalidate();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbCuenta.SelectedItem == null || cmbTipoTarjeta.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una cuenta y un tipo de tarjeta.", "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            IdCuenta = ((ComboItem)cmbCuenta.SelectedItem).Id;
            IdTipoTarjeta = ((ComboItem)cmbTipoTarjeta.SelectedItem).Id;

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