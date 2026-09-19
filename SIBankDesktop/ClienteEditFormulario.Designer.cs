namespace SIBankDesktop
{
    partial class ClienteEditFormulario
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
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label lblCorreo;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblCorreo = new System.Windows.Forms.Label();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // ===== Formulario =====
            this.ClientSize = new System.Drawing.Size(380, 260);
            this.Text = "Editar Cliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = System.Drawing.Color.White;

            // ===== lblTitulo =====
            this.lblTitulo.Text = "Editar Datos de Contacto";
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(21, 34, 56);
            this.lblTitulo.Location = new System.Drawing.Point(25, 20);
            this.lblTitulo.Size = new System.Drawing.Size(330, 30);

            // ===== lblTelefono =====
            this.lblTelefono.Text = "Telefono";
            this.lblTelefono.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTelefono.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblTelefono.Location = new System.Drawing.Point(25, 70);
            this.lblTelefono.AutoSize = true;

            // ===== txtTelefono =====
            this.txtTelefono.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTelefono.Location = new System.Drawing.Point(25, 93);
            this.txtTelefono.Size = new System.Drawing.Size(330, 26);
            this.txtTelefono.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // ===== lblCorreo =====
            this.lblCorreo.Text = "Correo electronico";
            this.lblCorreo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCorreo.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblCorreo.Location = new System.Drawing.Point(25, 135);
            this.lblCorreo.AutoSize = true;

            // ===== txtCorreo =====
            this.txtCorreo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCorreo.Location = new System.Drawing.Point(25, 158);
            this.txtCorreo.Size = new System.Drawing.Size(330, 26);
            this.txtCorreo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // ===== btnGuardar =====
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(21, 34, 56);
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.Location = new System.Drawing.Point(25, 200);
            this.btnGuardar.Size = new System.Drawing.Size(160, 40);
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);

            // ===== btnCancelar =====
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(230, 230, 230);
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.Location = new System.Drawing.Point(195, 200);
            this.btnCancelar.Size = new System.Drawing.Size(160, 40);
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // ===== Agregar controles =====
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblTelefono);
            this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.lblCorreo);
            this.Controls.Add(this.txtCorreo);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);

            this.ResumeLayout(false);
        }
    }
}