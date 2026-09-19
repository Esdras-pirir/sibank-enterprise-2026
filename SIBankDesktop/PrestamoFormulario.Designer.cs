namespace SIBankDesktop
{
    partial class PrestamoFormulario
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
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.ComboBox cmbCliente;
        private System.Windows.Forms.Label lblTipoPrestamo;
        private System.Windows.Forms.ComboBox cmbTipoPrestamo;
        private System.Windows.Forms.Label lblInfoTipo;
        private System.Windows.Forms.Label lblCuentaDesembolso;
        private System.Windows.Forms.ComboBox cmbCuentaDesembolso;
        private System.Windows.Forms.Label lblMonto;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.Label lblPlazoMeses;
        private System.Windows.Forms.TextBox txtPlazoMeses;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.cmbCliente = new System.Windows.Forms.ComboBox();
            this.lblTipoPrestamo = new System.Windows.Forms.Label();
            this.cmbTipoPrestamo = new System.Windows.Forms.ComboBox();
            this.lblInfoTipo = new System.Windows.Forms.Label();
            this.lblCuentaDesembolso = new System.Windows.Forms.Label();
            this.cmbCuentaDesembolso = new System.Windows.Forms.ComboBox();
            this.lblMonto = new System.Windows.Forms.Label();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.lblPlazoMeses = new System.Windows.Forms.Label();
            this.txtPlazoMeses = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // ===== Formulario =====
            this.ClientSize = new System.Drawing.Size(420, 560);
            this.Text = "Nueva Solicitud de Prestamo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = System.Drawing.Color.White;

            // ===== lblTitulo =====
            this.lblTitulo.Text = "Nueva Solicitud de Prestamo";
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(21, 34, 56);
            this.lblTitulo.Location = new System.Drawing.Point(25, 20);
            this.lblTitulo.Size = new System.Drawing.Size(370, 30);

            // ===== lblCliente =====
            this.lblCliente.Text = "Cliente *";
            this.lblCliente.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCliente.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblCliente.Location = new System.Drawing.Point(25, 70);
            this.lblCliente.AutoSize = true;

            // ===== cmbCliente =====
            this.cmbCliente.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCliente.Location = new System.Drawing.Point(25, 93);
            this.cmbCliente.Size = new System.Drawing.Size(370, 28);
            this.cmbCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // ===== lblTipoPrestamo =====
            this.lblTipoPrestamo.Text = "Tipo de Prestamo *";
            this.lblTipoPrestamo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTipoPrestamo.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblTipoPrestamo.Location = new System.Drawing.Point(25, 135);
            this.lblTipoPrestamo.AutoSize = true;

            // ===== cmbTipoPrestamo =====
            this.cmbTipoPrestamo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTipoPrestamo.Location = new System.Drawing.Point(25, 158);
            this.cmbTipoPrestamo.Size = new System.Drawing.Size(370, 28);
            this.cmbTipoPrestamo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoPrestamo.SelectedIndexChanged += new System.EventHandler(this.cmbTipoPrestamo_SelectedIndexChanged);

            // ===== lblInfoTipo =====
            this.lblInfoTipo.Text = "";
            this.lblInfoTipo.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblInfoTipo.ForeColor = System.Drawing.Color.FromArgb(90, 90, 90);
            this.lblInfoTipo.Location = new System.Drawing.Point(25, 190);
            this.lblInfoTipo.Size = new System.Drawing.Size(370, 20);

            // ===== lblCuentaDesembolso =====
            this.lblCuentaDesembolso.Text = "Cuenta de desembolso *";
            this.lblCuentaDesembolso.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCuentaDesembolso.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblCuentaDesembolso.Location = new System.Drawing.Point(25, 220);
            this.lblCuentaDesembolso.AutoSize = true;

            // ===== cmbCuentaDesembolso =====
            this.cmbCuentaDesembolso.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCuentaDesembolso.Location = new System.Drawing.Point(25, 243);
            this.cmbCuentaDesembolso.Size = new System.Drawing.Size(370, 28);
            this.cmbCuentaDesembolso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // ===== lblMonto =====
            this.lblMonto.Text = "Monto solicitado (Q) *";
            this.lblMonto.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMonto.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblMonto.Location = new System.Drawing.Point(25, 285);
            this.lblMonto.AutoSize = true;

            // ===== txtMonto =====
            this.txtMonto.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtMonto.Location = new System.Drawing.Point(25, 308);
            this.txtMonto.Size = new System.Drawing.Size(370, 28);
            this.txtMonto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // ===== lblPlazoMeses =====
            this.lblPlazoMeses.Text = "Plazo en meses *";
            this.lblPlazoMeses.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPlazoMeses.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblPlazoMeses.Location = new System.Drawing.Point(25, 350);
            this.lblPlazoMeses.AutoSize = true;

            // ===== txtPlazoMeses =====
            this.txtPlazoMeses.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtPlazoMeses.Location = new System.Drawing.Point(25, 373);
            this.txtPlazoMeses.Size = new System.Drawing.Size(370, 28);
            this.txtPlazoMeses.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // ===== btnGuardar =====
            this.btnGuardar.Text = "Enviar Solicitud";
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(21, 34, 56);
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Location = new System.Drawing.Point(25, 430);
            this.btnGuardar.Size = new System.Drawing.Size(180, 42);
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
            this.btnCancelar.Location = new System.Drawing.Point(215, 430);
            this.btnCancelar.Size = new System.Drawing.Size(180, 42);
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // ===== Agregar controles =====
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.cmbCliente);
            this.Controls.Add(this.lblTipoPrestamo);
            this.Controls.Add(this.cmbTipoPrestamo);
            this.Controls.Add(this.lblInfoTipo);
            this.Controls.Add(this.lblCuentaDesembolso);
            this.Controls.Add(this.cmbCuentaDesembolso);
            this.Controls.Add(this.lblMonto);
            this.Controls.Add(this.txtMonto);
            this.Controls.Add(this.lblPlazoMeses);
            this.Controls.Add(this.txtPlazoMeses);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);

            this.ResumeLayout(false);
        }
    }
}