namespace SIBankDesktop
{
    partial class CuentaFormulario
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
        private System.Windows.Forms.Label lblTipoCuenta;
        private System.Windows.Forms.ComboBox cmbTipoCuenta;
        private System.Windows.Forms.Label lblSucursal;
        private System.Windows.Forms.ComboBox cmbSucursal;
        private System.Windows.Forms.Label lblSaldoInicial;
        private System.Windows.Forms.TextBox txtSaldoInicial;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.cmbCliente = new System.Windows.Forms.ComboBox();
            this.lblTipoCuenta = new System.Windows.Forms.Label();
            this.cmbTipoCuenta = new System.Windows.Forms.ComboBox();
            this.lblSucursal = new System.Windows.Forms.Label();
            this.cmbSucursal = new System.Windows.Forms.ComboBox();
            this.lblSaldoInicial = new System.Windows.Forms.Label();
            this.txtSaldoInicial = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // ===== Formulario =====
            this.ClientSize = new System.Drawing.Size(420, 420);
            this.Text = "Abrir Cuenta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = System.Drawing.Color.White;

            // ===== lblTitulo =====
            this.lblTitulo.Text = "Abrir Nueva Cuenta";
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

            // ===== lblTipoCuenta =====
            this.lblTipoCuenta.Text = "Tipo de Cuenta *";
            this.lblTipoCuenta.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTipoCuenta.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblTipoCuenta.Location = new System.Drawing.Point(25, 135);
            this.lblTipoCuenta.AutoSize = true;

            // ===== cmbTipoCuenta =====
            this.cmbTipoCuenta.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTipoCuenta.Location = new System.Drawing.Point(25, 158);
            this.cmbTipoCuenta.Size = new System.Drawing.Size(370, 28);
            this.cmbTipoCuenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // ===== lblSucursal =====
            this.lblSucursal.Text = "Sucursal *";
            this.lblSucursal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSucursal.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblSucursal.Location = new System.Drawing.Point(25, 200);
            this.lblSucursal.AutoSize = true;

            // ===== cmbSucursal =====
            this.cmbSucursal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbSucursal.Location = new System.Drawing.Point(25, 223);
            this.cmbSucursal.Size = new System.Drawing.Size(370, 28);
            this.cmbSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // ===== lblSaldoInicial =====
            this.lblSaldoInicial.Text = "Saldo inicial (Q) *";
            this.lblSaldoInicial.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSaldoInicial.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblSaldoInicial.Location = new System.Drawing.Point(25, 265);
            this.lblSaldoInicial.AutoSize = true;

            // ===== txtSaldoInicial =====
            this.txtSaldoInicial.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSaldoInicial.Location = new System.Drawing.Point(25, 288);
            this.txtSaldoInicial.Size = new System.Drawing.Size(370, 28);
            this.txtSaldoInicial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSaldoInicial.Text = "0.00";

            // ===== btnGuardar =====
            this.btnGuardar.Text = "Abrir Cuenta";
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(21, 34, 56);
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Location = new System.Drawing.Point(25, 350);
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
            this.btnCancelar.Location = new System.Drawing.Point(215, 350);
            this.btnCancelar.Size = new System.Drawing.Size(180, 42);
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // ===== Agregar controles =====
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.cmbCliente);
            this.Controls.Add(this.lblTipoCuenta);
            this.Controls.Add(this.cmbTipoCuenta);
            this.Controls.Add(this.lblSucursal);
            this.Controls.Add(this.cmbSucursal);
            this.Controls.Add(this.lblSaldoInicial);
            this.Controls.Add(this.txtSaldoInicial);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);

            this.ResumeLayout(false);
        }
    }
}