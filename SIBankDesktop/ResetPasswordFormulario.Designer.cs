namespace SIBankDesktop
{
    partial class ResetPasswordFormulario
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
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblNuevaPassword;
        private System.Windows.Forms.TextBox txtNuevaPassword;
        private System.Windows.Forms.Label lblConfirmar;
        private System.Windows.Forms.TextBox txtConfirmar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblNuevaPassword = new System.Windows.Forms.Label();
            this.txtNuevaPassword = new System.Windows.Forms.TextBox();
            this.lblConfirmar = new System.Windows.Forms.Label();
            this.txtConfirmar = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // ===== Formulario =====
            this.ClientSize = new System.Drawing.Size(380, 320);
            this.Text = "Resetear Contrasena";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = System.Drawing.Color.White;

            // ===== lblTitulo =====
            this.lblTitulo.Text = "Resetear Contrasena";
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(21, 34, 56);
            this.lblTitulo.Location = new System.Drawing.Point(25, 20);
            this.lblTitulo.Size = new System.Drawing.Size(330, 30);

            // ===== lblUsuario =====
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Italic);
            this.lblUsuario.ForeColor = System.Drawing.Color.Gray;
            this.lblUsuario.Location = new System.Drawing.Point(25, 55);
            this.lblUsuario.AutoSize = true;

            // ===== lblNuevaPassword =====
            this.lblNuevaPassword.Text = "Nueva contrasena *";
            this.lblNuevaPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNuevaPassword.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblNuevaPassword.Location = new System.Drawing.Point(25, 95);
            this.lblNuevaPassword.AutoSize = true;

            // ===== txtNuevaPassword =====
            this.txtNuevaPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNuevaPassword.Location = new System.Drawing.Point(25, 118);
            this.txtNuevaPassword.Size = new System.Drawing.Size(330, 26);
            this.txtNuevaPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNuevaPassword.PasswordChar = '*';

            // ===== lblConfirmar =====
            this.lblConfirmar.Text = "Confirmar contrasena *";
            this.lblConfirmar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblConfirmar.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblConfirmar.Location = new System.Drawing.Point(25, 160);
            this.lblConfirmar.AutoSize = true;

            // ===== txtConfirmar =====
            this.txtConfirmar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtConfirmar.Location = new System.Drawing.Point(25, 183);
            this.txtConfirmar.Size = new System.Drawing.Size(330, 26);
            this.txtConfirmar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConfirmar.PasswordChar = '*';

            // ===== btnGuardar =====
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(21, 34, 56);
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Location = new System.Drawing.Point(25, 230);
            this.btnGuardar.Size = new System.Drawing.Size(150, 42);
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
            this.btnCancelar.Location = new System.Drawing.Point(205, 230);
            this.btnCancelar.Size = new System.Drawing.Size(150, 42);
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // ===== Agregar controles =====
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.lblNuevaPassword);
            this.Controls.Add(this.txtNuevaPassword);
            this.Controls.Add(this.lblConfirmar);
            this.Controls.Add(this.txtConfirmar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);

            this.ResumeLayout(false);
        }
    }
}
