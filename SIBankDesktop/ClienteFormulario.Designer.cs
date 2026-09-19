namespace SIBankDesktop
{
    partial class ClienteFormulario
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
        private System.Windows.Forms.Label lblNombres;
        private System.Windows.Forms.TextBox txtNombres;
        private System.Windows.Forms.Label lblApellidos;
        private System.Windows.Forms.TextBox txtApellidos;
        private System.Windows.Forms.Label lblDpi;
        private System.Windows.Forms.TextBox txtDpi;
        private System.Windows.Forms.Label lblNit;
        private System.Windows.Forms.TextBox txtNit;
        private System.Windows.Forms.Label lblFechaNacimiento;
        private System.Windows.Forms.DateTimePicker dtpFechaNacimiento;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label lblCorreo;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblNombres = new System.Windows.Forms.Label();
            this.txtNombres = new System.Windows.Forms.TextBox();
            this.lblApellidos = new System.Windows.Forms.Label();
            this.txtApellidos = new System.Windows.Forms.TextBox();
            this.lblDpi = new System.Windows.Forms.Label();
            this.txtDpi = new System.Windows.Forms.TextBox();
            this.lblNit = new System.Windows.Forms.Label();
            this.txtNit = new System.Windows.Forms.TextBox();
            this.lblFechaNacimiento = new System.Windows.Forms.Label();
            this.dtpFechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblCorreo = new System.Windows.Forms.Label();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // ===== Formulario =====
            this.ClientSize = new System.Drawing.Size(420, 500);
            this.Text = "Nuevo Cliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = System.Drawing.Color.White;

            // ===== lblTitulo =====
            this.lblTitulo.Text = "Registrar Nuevo Cliente";
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(21, 34, 56);
            this.lblTitulo.Location = new System.Drawing.Point(25, 20);
            this.lblTitulo.Size = new System.Drawing.Size(370, 30);

            CrearCampo(this.lblNombres, "Nombres *", this.txtNombres, 70);
            CrearCampo(this.lblApellidos, "Apellidos *", this.txtApellidos, 120);
            CrearCampo(this.lblDpi, "DPI *", this.txtDpi, 170);
            CrearCampo(this.lblNit, "NIT", this.txtNit, 220);

            // ===== lblFechaNacimiento =====
            this.lblFechaNacimiento.Text = "Fecha de nacimiento *";
            this.lblFechaNacimiento.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFechaNacimiento.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblFechaNacimiento.Location = new System.Drawing.Point(25, 270);
            this.lblFechaNacimiento.AutoSize = true;

            // ===== dtpFechaNacimiento =====
            this.dtpFechaNacimiento.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFechaNacimiento.Location = new System.Drawing.Point(25, 293);
            this.dtpFechaNacimiento.Size = new System.Drawing.Size(370, 26);
            this.dtpFechaNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaNacimiento.MaxDate = System.DateTime.Now.AddYears(-18);
            this.dtpFechaNacimiento.Value = System.DateTime.Now.AddYears(-25);

            CrearCampo(this.lblTelefono, "Telefono", this.txtTelefono, 335);
            CrearCampo(this.lblCorreo, "Correo electronico", this.txtCorreo, 385);

            // ===== btnGuardar =====
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(21, 34, 56);
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.Location = new System.Drawing.Point(25, 440);
            this.btnGuardar.Size = new System.Drawing.Size(180, 40);
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);

            // ===== btnCancelar =====
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(230, 230, 230);
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.Location = new System.Drawing.Point(215, 440);
            this.btnCancelar.Size = new System.Drawing.Size(180, 40);
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // ===== Agregar todos los controles =====
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblNombres);
            this.Controls.Add(this.txtNombres);
            this.Controls.Add(this.lblApellidos);
            this.Controls.Add(this.txtApellidos);
            this.Controls.Add(this.lblDpi);
            this.Controls.Add(this.txtDpi);
            this.Controls.Add(this.lblNit);
            this.Controls.Add(this.txtNit);
            this.Controls.Add(this.lblFechaNacimiento);
            this.Controls.Add(this.dtpFechaNacimiento);
            this.Controls.Add(this.lblTelefono);
            this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.lblCorreo);
            this.Controls.Add(this.txtCorreo);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);

            this.ResumeLayout(false);
        }

        // Metodo auxiliar: crea un label + textbox con el mismo estilo, uno debajo del otro
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