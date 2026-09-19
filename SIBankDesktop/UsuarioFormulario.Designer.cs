namespace SIBankDesktop
{
    partial class UsuarioFormulario
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblCorreo;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Label lblRol;
        private System.Windows.Forms.ComboBox cmbRol;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblCorreo = new System.Windows.Forms.Label();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.lblRol = new System.Windows.Forms.Label();
            this.cmbRol = new System.Windows.Forms.ComboBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // ===== Formulario =====
            this.ClientSize = new System.Drawing.Size(420, 480);
            this.Text = "Nuevo Usuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = System.Drawing.Color.White;

            // ===== lblTitulo =====
            this.lblTitulo.Text = "Nuevo Usuario del Sistema";
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(21, 34, 56);
            this.lblTitulo.Location = new System.Drawing.Point(25, 20);
            this.lblTitulo.Size = new System.Drawing.Size(370, 30);

            CrearCampo(this.lblUsername, "Nombre de usuario *", this.txtUsername, 70);

            // ===== lblPassword =====
            this.lblPassword.Text = "Contrasena *";
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblPassword.Location = new System.Drawing.Point(25, 120);
            this.lblPassword.AutoSize = true;

            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPassword.Location = new System.Drawing.Point(25, 143);
            this.txtPassword.Size = new System.Drawing.Size(370, 26);
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.PasswordChar = '*';

            CrearCampo(this.lblCorreo, "Correo electronico", this.txtCorreo, 180);

            // ===== lblRol =====
            this.lblRol.Text = "Rol *";
            this.lblRol.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRol.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblRol.Location = new System.Drawing.Point(25, 230);
            this.lblRol.AutoSize = true;

            // ===== cmbRol =====
            this.cmbRol.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbRol.Location = new System.Drawing.Point(25, 253);
            this.cmbRol.Size = new System.Drawing.Size(370, 28);
            this.cmbRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // ===== btnGuardar =====
            this.btnGuardar.Text = "Crear Usuario";
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(21, 34, 56);
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Location = new System.Drawing.Point(25, 310);
            this.btnGuardar.Size = new System.Drawing.Size(180, 44);
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);

            // ===== btnCancelar =====
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(230, 230, 230);
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Location = new System.Drawing.Point(215, 310);
            this.btnCancelar.Size = new System.Drawing.Size(180, 44);
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // ===== Agregar controles =====
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblCorreo);
            this.Controls.Add(this.txtCorreo);
            this.Controls.Add(this.lblRol);
            this.Controls.Add(this.cmbRol);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);

            this.ResumeLayout(false);
        }

        private void CrearCampo(System.Windows.Forms.Label lbl, string texto,
            System.Windows.Forms.TextBox txt, int posY)
        {
            lbl.Text = texto;
            lbl.Font = new System.Drawing.Font("Segoe UI", 9F);
            lbl.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            lbl.Location = new System.Drawing.Point(25, posY);
            lbl.AutoSize = true;

            txt.Font = new System.Drawing.Font("Segoe UI", 10F);
            txt.Location = new System.Drawing.Point(25, posY + 23);
            txt.Size = new System.Drawing.Size(370, 26);
            txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }
    }
}