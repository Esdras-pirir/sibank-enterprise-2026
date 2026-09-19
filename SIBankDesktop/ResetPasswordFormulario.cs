using System;
using System.Drawing;
using System.Windows.Forms;

namespace SIBankDesktop
{
    public partial class ResetPasswordFormulario : Form
    {
        public string PasswordNuevo { get; private set; }

        public ResetPasswordFormulario(string nombreUsuario)
        {
            InitializeComponent();
            lblUsuario.Text = "Usuario: " + nombreUsuario;

            // Asignar los eventos de efectos Hover a los botones
            AsignarEfectosHover();
        }

        private void AsignarEfectosHover()
        {
            // --- Hover para btnGuardar ---
            btnGuardar.MouseEnter += (s, e) => btnGuardar.BackColor = Color.FromArgb(31, 50, 82); // Azul más claro
            btnGuardar.MouseLeave += (s, e) => btnGuardar.BackColor = Color.FromArgb(21, 34, 56);  // Azul original

            // --- Hover para btnCancelar ---
            btnCancelar.MouseEnter += (s, e) => btnCancelar.BackColor = Color.FromArgb(215, 215, 215); // Gris más oscuro
            btnCancelar.MouseLeave += (s, e) => btnCancelar.BackColor = Color.FromArgb(230, 230, 230); // Gris original
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNuevaPassword.Text) || txtNuevaPassword.Text.Length < 6)
            {
                MessageBox.Show("La contrasena debe tener al menos 6 caracteres.", "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtNuevaPassword.Text != txtConfirmar.Text)
            {
                MessageBox.Show("Las contrasenas no coinciden.", "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PasswordNuevo = txtNuevaPassword.Text;

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
