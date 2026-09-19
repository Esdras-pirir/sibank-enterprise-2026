using System;
using System.Windows.Forms;

namespace SIBankDesktop
{
    public partial class ClienteFormulario : Form
    {
        public string Nombres { get; private set; }
        public string Apellidos { get; private set; }
        public string Dpi { get; private set; }
        public string Nit { get; private set; }
        public string FechaNacimiento { get; private set; }
        public string Telefono { get; private set; }
        public string Correo { get; private set; }

        public ClienteFormulario()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombres.Text) ||
                string.IsNullOrWhiteSpace(txtApellidos.Text) ||
                string.IsNullOrWhiteSpace(txtDpi.Text))
            {
                MessageBox.Show("Nombres, Apellidos y DPI son obligatorios.", "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Nombres = txtNombres.Text.Trim();
            Apellidos = txtApellidos.Text.Trim();
            Dpi = txtDpi.Text.Trim();
            Nit = string.IsNullOrWhiteSpace(txtNit.Text) ? null : txtNit.Text.Trim();
            FechaNacimiento = dtpFechaNacimiento.Value.ToString("yyyy-MM-dd");
            Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text) ? null : txtTelefono.Text.Trim();
            Correo = string.IsNullOrWhiteSpace(txtCorreo.Text) ? null : txtCorreo.Text.Trim();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}