using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows.Forms;

namespace SIBankDesktop
{
    public partial class UsuarioFormulario : Form
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Correo { get; private set; }
        public long IdRol { get; private set; }

        public UsuarioFormulario(List<Dictionary<string, JsonElement>> roles)
        {
            InitializeComponent();

            cmbRol.Items.Clear();
            foreach (var fila in roles)
            {
                long id = fila["ID_ROL"].GetInt64();
                string nombre = fila["NOMBRE"].ToString();
                cmbRol.Items.Add(new ComboItem { Id = id, Texto = nombre });
            }
            cmbRol.DisplayMember = "Texto";
            if (cmbRol.Items.Count > 0) cmbRol.SelectedIndex = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("El nombre de usuario y la contrasena son obligatorios.", "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtPassword.Text.Length < 6)
            {
                MessageBox.Show("La contrasena debe tener al menos 6 caracteres.", "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbRol.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un rol para el usuario.", "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Username = txtUsername.Text.Trim();
            Password = txtPassword.Text;
            Correo = string.IsNullOrWhiteSpace(txtCorreo.Text) ? null : txtCorreo.Text.Trim();
            IdRol = ((ComboItem)cmbRol.SelectedItem).Id;

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