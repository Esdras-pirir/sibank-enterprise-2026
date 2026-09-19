namespace SIBankDesktop
{
    partial class TransferenciasControl
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

        private System.Windows.Forms.Panel pnlTarjeta;
        private System.Windows.Forms.Label lblTitulo;

        private System.Windows.Forms.Label lblCuentaOrigen;
        private System.Windows.Forms.ComboBox cmbCuentaOrigen;
        private System.Windows.Forms.Label lblSaldoOrigen;

        private System.Windows.Forms.Label lblCuentaDestino;
        private System.Windows.Forms.ComboBox cmbCuentaDestino;
        private System.Windows.Forms.Label lblSaldoDestino;

        private System.Windows.Forms.Label lblMonto;
        private System.Windows.Forms.TextBox txtMonto;

        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Label lblMensaje;

        private void InitializeComponent()
        {
            this.pnlTarjeta = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblCuentaOrigen = new System.Windows.Forms.Label();
            this.cmbCuentaOrigen = new System.Windows.Forms.ComboBox();
            this.lblSaldoOrigen = new System.Windows.Forms.Label();
            this.lblCuentaDestino = new System.Windows.Forms.Label();
            this.cmbCuentaDestino = new System.Windows.Forms.ComboBox();
            this.lblSaldoDestino = new System.Windows.Forms.Label();
            this.lblMonto = new System.Windows.Forms.Label();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.pnlTarjeta.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTarjeta
            // 
            this.pnlTarjeta.BackColor = System.Drawing.Color.White;
            this.pnlTarjeta.Controls.Add(this.lblTitulo);
            this.pnlTarjeta.Controls.Add(this.lblCuentaOrigen);
            this.pnlTarjeta.Controls.Add(this.cmbCuentaOrigen);
            this.pnlTarjeta.Controls.Add(this.lblSaldoOrigen);
            this.pnlTarjeta.Controls.Add(this.lblCuentaDestino);
            this.pnlTarjeta.Controls.Add(this.cmbCuentaDestino);
            this.pnlTarjeta.Controls.Add(this.lblSaldoDestino);
            this.pnlTarjeta.Controls.Add(this.lblMonto);
            this.pnlTarjeta.Controls.Add(this.txtMonto);
            this.pnlTarjeta.Controls.Add(this.btnConfirmar);
            this.pnlTarjeta.Controls.Add(this.lblMensaje);
            this.pnlTarjeta.Location = new System.Drawing.Point(40, 40);
            this.pnlTarjeta.Name = "pnlTarjeta";
            this.pnlTarjeta.Padding = new System.Windows.Forms.Padding(30);
            this.pnlTarjeta.Size = new System.Drawing.Size(480, 470);
            this.pnlTarjeta.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(34)))), ((int)(((byte)(56)))));
            this.lblTitulo.Location = new System.Drawing.Point(30, 25);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(420, 35);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Transferencia entre Cuentas";
            // 
            // lblCuentaOrigen
            // 
            this.lblCuentaOrigen.AutoSize = true;
            this.lblCuentaOrigen.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCuentaOrigen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblCuentaOrigen.Location = new System.Drawing.Point(30, 80);
            this.lblCuentaOrigen.Name = "lblCuentaOrigen";
            this.lblCuentaOrigen.Size = new System.Drawing.Size(90, 15);
            this.lblCuentaOrigen.TabIndex = 1;
            this.lblCuentaOrigen.Text = "Cuenta origen *";
            // 
            // cmbCuentaOrigen
            // 
            this.cmbCuentaOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCuentaOrigen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCuentaOrigen.Location = new System.Drawing.Point(30, 103);
            this.cmbCuentaOrigen.Name = "cmbCuentaOrigen";
            this.cmbCuentaOrigen.Size = new System.Drawing.Size(420, 25);
            this.cmbCuentaOrigen.TabIndex = 2;
            this.cmbCuentaOrigen.SelectedIndexChanged += new System.EventHandler(this.cmbCuentaOrigen_SelectedIndexChanged);
            // 
            // lblSaldoOrigen
            // 
            this.lblSaldoOrigen.AutoSize = true;
            this.lblSaldoOrigen.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Italic);
            this.lblSaldoOrigen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblSaldoOrigen.Location = new System.Drawing.Point(30, 137);
            this.lblSaldoOrigen.Name = "lblSaldoOrigen";
            this.lblSaldoOrigen.Size = new System.Drawing.Size(109, 17);
            this.lblSaldoOrigen.TabIndex = 3;
            this.lblSaldoOrigen.Text = "Saldo disponible: -";
            // 
            // lblCuentaDestino
            // 
            this.lblCuentaDestino.AutoSize = true;
            this.lblCuentaDestino.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCuentaDestino.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblCuentaDestino.Location = new System.Drawing.Point(30, 180);
            this.lblCuentaDestino.Name = "lblCuentaDestino";
            this.lblCuentaDestino.Size = new System.Drawing.Size(95, 15);
            this.lblCuentaDestino.TabIndex = 4;
            this.lblCuentaDestino.Text = "Cuenta destino *";
            // 
            // cmbCuentaDestino
            // 
            this.cmbCuentaDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCuentaDestino.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCuentaDestino.Location = new System.Drawing.Point(30, 203);
            this.cmbCuentaDestino.Name = "cmbCuentaDestino";
            this.cmbCuentaDestino.Size = new System.Drawing.Size(420, 25);
            this.cmbCuentaDestino.TabIndex = 5;
            this.cmbCuentaDestino.SelectedIndexChanged += new System.EventHandler(this.cmbCuentaDestino_SelectedIndexChanged);
            // 
            // lblSaldoDestino
            // 
            this.lblSaldoDestino.AutoSize = true;
            this.lblSaldoDestino.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Italic);
            this.lblSaldoDestino.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblSaldoDestino.Location = new System.Drawing.Point(30, 237);
            this.lblSaldoDestino.Name = "lblSaldoDestino";
            this.lblSaldoDestino.Size = new System.Drawing.Size(88, 17);
            this.lblSaldoDestino.TabIndex = 6;
            this.lblSaldoDestino.Text = "Saldo actual: -";
            // 
            // lblMonto
            // 
            this.lblMonto.AutoSize = true;
            this.lblMonto.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMonto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblMonto.Location = new System.Drawing.Point(30, 280);
            this.lblMonto.Name = "lblMonto";
            this.lblMonto.Size = new System.Drawing.Size(130, 15);
            this.lblMonto.TabIndex = 7;
            this.lblMonto.Text = "Monto a transferir (Q) *";
            // 
            // txtMonto
            // 
            this.txtMonto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMonto.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtMonto.Location = new System.Drawing.Point(30, 303);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Size = new System.Drawing.Size(420, 29);
            this.txtMonto.TabIndex = 8;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(34)))), ((int)(((byte)(56)))));
            this.btnConfirmar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmar.FlatAppearance.BorderSize = 0;
            this.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmar.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnConfirmar.ForeColor = System.Drawing.Color.White;
            this.btnConfirmar.Location = new System.Drawing.Point(30, 355);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(420, 44);
            this.btnConfirmar.TabIndex = 9;
            this.btnConfirmar.Text = "Confirmar Transferencia";
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // lblMensaje
            // 
            this.lblMensaje.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblMensaje.Location = new System.Drawing.Point(30, 410);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(420, 50);
            this.lblMensaje.TabIndex = 10;
            // 
            // TransferenciasControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.pnlTarjeta);
            this.Name = "TransferenciasControl";
            this.Size = new System.Drawing.Size(1207, 538);
            this.pnlTarjeta.ResumeLayout(false);
            this.pnlTarjeta.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}