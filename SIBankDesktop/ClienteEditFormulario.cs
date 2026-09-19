using System;
using System.Windows.Forms;

namespace SIBankDesktop
{
    public partial class ClienteEditFormulario : Form
    {
        public string Telefono { get; private set; }
        public string Correo { get; private set; }

        public ClienteEditFormulario(string telefonoActual, string correoActual)
        {
            InitializeComponent();
            txtTelefono.Text = telefonoActual;
            txtCorreo.Text = correoActual;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
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