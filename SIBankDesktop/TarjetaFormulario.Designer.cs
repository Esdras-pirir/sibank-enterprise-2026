namespace SIBankDesktop
{
    partial class TarjetaFormulario
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
        private System.Windows.Forms.Label lblCuenta;
        private System.Windows.Forms.ComboBox cmbCuenta;
        private System.Windows.Forms.Label lblTipoTarjeta;
        private System.Windows.Forms.ComboBox cmbTipoTarjeta;
        private System.Windows.Forms.Label lblInfoTipo;
        private TarjetaVisual tarjetaPreview;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblCuenta = new System.Windows.Forms.Label();
            this.cmbCuenta = new System.Windows.Forms.ComboBox();
            this.lblTipoTarjeta = new System.Windows.Forms.Label();
            this.cmbTipoTarjeta = new System.Windows.Forms.ComboBox();
            this.lblInfoTipo = new System.Windows.Forms.Label();
            this.tarjetaPreview = new TarjetaVisual();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // ===== Formulario =====
            this.ClientSize = new System.Drawing.Size(420, 620);
            this.Text = "Emitir Tarjeta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = System.Drawing.Color.FromArgb(245, 246, 248);

            // ===== lblTitulo =====
            this.lblTitulo.Text = "Emitir Nueva Tarjeta";
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(21, 34, 56);
            this.lblTitulo.Location = new System.Drawing.Point(25, 20);
            this.lblTitulo.Size = new System.Drawing.Size(370, 30);

            // ===== tarjetaPreview (vista previa en vivo, arriba) =====
            this.tarjetaPreview.Size = new System.Drawing.Size(370, 210);
            this.tarjetaPreview.Location = new System.Drawing.Point(25, 60);
            this.tarjetaPreview.Cursor = System.Windows.Forms.Cursors.Default;
            this.tarjetaPreview.NumeroTarjeta = "0000000000000000";
            this.tarjetaPreview.Cliente = "Selecciona una cuenta";
            this.tarjetaPreview.TipoTarjeta = "";
            this.tarjetaPreview.Marca = "";
            this.tarjetaPreview.FechaVencimiento = System.DateTime.Now.AddYears(4).ToString("yyyy-MM-dd");
            this.tarjetaPreview.Estado = "A";

            // ===== lblCuenta =====
            this.lblCuenta.Text = "Cuenta *";
            this.lblCuenta.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCuenta.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblCuenta.Location = new System.Drawing.Point(25, 290);
            this.lblCuenta.AutoSize = true;

            // ===== cmbCuenta =====
            this.cmbCuenta.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCuenta.Location = new System.Drawing.Point(25, 313);
            this.cmbCuenta.Size = new System.Drawing.Size(370, 28);
            this.cmbCuenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCuenta.SelectedIndexChanged += new System.EventHandler(this.cmbCuenta_SelectedIndexChanged);

            // ===== lblTipoTarjeta =====
            this.lblTipoTarjeta.Text = "Tipo de Tarjeta *";
            this.lblTipoTarjeta.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTipoTarjeta.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblTipoTarjeta.Location = new System.Drawing.Point(25, 355);
            this.lblTipoTarjeta.AutoSize = true;

            // ===== cmbTipoTarjeta =====
            this.cmbTipoTarjeta.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTipoTarjeta.Location = new System.Drawing.Point(25, 378);
            this.cmbTipoTarjeta.Size = new System.Drawing.Size(370, 28);
            this.cmbTipoTarjeta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoTarjeta.SelectedIndexChanged += new System.EventHandler(this.cmbTipoTarjeta_SelectedIndexChanged);

            // ===== lblInfoTipo =====
            this.lblInfoTipo.Text = "";
            this.lblInfoTipo.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblInfoTipo.ForeColor = System.Drawing.Color.FromArgb(90, 90, 90);
            this.lblInfoTipo.Location = new System.Drawing.Point(25, 410);
            this.lblInfoTipo.Size = new System.Drawing.Size(370, 20);

            // ===== btnGuardar =====
            this.btnGuardar.Text = "Emitir Tarjeta";
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(21, 34, 56);
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Location = new System.Drawing.Point(25, 460);
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
            this.btnCancelar.Location = new System.Drawing.Point(215, 460);
            this.btnCancelar.Size = new System.Drawing.Size(180, 44);
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // ===== Agregar controles =====
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.tarjetaPreview);
            this.Controls.Add(this.lblCuenta);
            this.Controls.Add(this.cmbCuenta);
            this.Controls.Add(this.lblTipoTarjeta);
            this.Controls.Add(this.cmbTipoTarjeta);
            this.Controls.Add(this.lblInfoTipo);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);

            this.ResumeLayout(false);
        }
    }
}