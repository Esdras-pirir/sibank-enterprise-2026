namespace SIBankDesktop
{
    partial class DepositosRetirosControl
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

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabDeposito;
        private System.Windows.Forms.TabPage tabRetiro;

        // ===== Controles pestana Deposito =====
        private System.Windows.Forms.Label lblTituloDeposito;
        private System.Windows.Forms.Label lblCuentaDeposito;
        private System.Windows.Forms.ComboBox cmbCuentaDeposito;
        private System.Windows.Forms.Label lblSaldoActualDeposito;
        private System.Windows.Forms.Label lblMontoDeposito;
        private System.Windows.Forms.TextBox txtMontoDeposito;
        private System.Windows.Forms.Label lblDescripcionDeposito;
        private System.Windows.Forms.TextBox txtDescripcionDeposito;
        private System.Windows.Forms.Button btnConfirmarDeposito;
        private System.Windows.Forms.Label lblMensajeDeposito;

        // ===== Controles pestana Retiro =====
        private System.Windows.Forms.Label lblTituloRetiro;
        private System.Windows.Forms.Label lblCuentaRetiro;
        private System.Windows.Forms.ComboBox cmbCuentaRetiro;
        private System.Windows.Forms.Label lblSaldoActualRetiro;
        private System.Windows.Forms.Label lblMontoRetiro;
        private System.Windows.Forms.TextBox txtMontoRetiro;
        private System.Windows.Forms.Label lblDescripcionRetiro;
        private System.Windows.Forms.TextBox txtDescripcionRetiro;
        private System.Windows.Forms.Button btnConfirmarRetiro;
        private System.Windows.Forms.Label lblMensajeRetiro;

        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabDeposito = new System.Windows.Forms.TabPage();
            this.tabRetiro = new System.Windows.Forms.TabPage();

            this.lblTituloDeposito = new System.Windows.Forms.Label();
            this.lblCuentaDeposito = new System.Windows.Forms.Label();
            this.cmbCuentaDeposito = new System.Windows.Forms.ComboBox();
            this.lblSaldoActualDeposito = new System.Windows.Forms.Label();
            this.lblMontoDeposito = new System.Windows.Forms.Label();
            this.txtMontoDeposito = new System.Windows.Forms.TextBox();
            this.lblDescripcionDeposito = new System.Windows.Forms.Label();
            this.txtDescripcionDeposito = new System.Windows.Forms.TextBox();
            this.btnConfirmarDeposito = new System.Windows.Forms.Button();
            this.lblMensajeDeposito = new System.Windows.Forms.Label();

            this.lblTituloRetiro = new System.Windows.Forms.Label();
            this.lblCuentaRetiro = new System.Windows.Forms.Label();
            this.cmbCuentaRetiro = new System.Windows.Forms.ComboBox();
            this.lblSaldoActualRetiro = new System.Windows.Forms.Label();
            this.lblMontoRetiro = new System.Windows.Forms.Label();
            this.txtMontoRetiro = new System.Windows.Forms.TextBox();
            this.lblDescripcionRetiro = new System.Windows.Forms.Label();
            this.txtDescripcionRetiro = new System.Windows.Forms.TextBox();
            this.btnConfirmarRetiro = new System.Windows.Forms.Button();
            this.lblMensajeRetiro = new System.Windows.Forms.Label();

            this.tabControl.SuspendLayout();
            this.tabDeposito.SuspendLayout();
            this.tabRetiro.SuspendLayout();
            this.SuspendLayout();

            // ===== DepositosRetirosControl (raiz) =====
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);

            // ===== tabControl =====
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.tabControl.Controls.Add(this.tabDeposito);
            this.tabControl.Controls.Add(this.tabRetiro);

            // ===== tabDeposito =====
            this.tabDeposito.Text = "  Deposito  ";
            this.tabDeposito.BackColor = System.Drawing.Color.White;
            this.tabDeposito.Padding = new System.Windows.Forms.Padding(30);
            this.tabDeposito.Controls.Add(this.lblTituloDeposito);
            this.tabDeposito.Controls.Add(this.lblCuentaDeposito);
            this.tabDeposito.Controls.Add(this.cmbCuentaDeposito);
            this.tabDeposito.Controls.Add(this.lblSaldoActualDeposito);
            this.tabDeposito.Controls.Add(this.lblMontoDeposito);
            this.tabDeposito.Controls.Add(this.txtMontoDeposito);
            this.tabDeposito.Controls.Add(this.lblDescripcionDeposito);
            this.tabDeposito.Controls.Add(this.txtDescripcionDeposito);
            this.tabDeposito.Controls.Add(this.btnConfirmarDeposito);
            this.tabDeposito.Controls.Add(this.lblMensajeDeposito);

            // ===== tabRetiro =====
            this.tabRetiro.Text = "  Retiro  ";
            this.tabRetiro.BackColor = System.Drawing.Color.White;
            this.tabRetiro.Padding = new System.Windows.Forms.Padding(30);
            this.tabRetiro.Controls.Add(this.lblTituloRetiro);
            this.tabRetiro.Controls.Add(this.lblCuentaRetiro);
            this.tabRetiro.Controls.Add(this.cmbCuentaRetiro);
            this.tabRetiro.Controls.Add(this.lblSaldoActualRetiro);
            this.tabRetiro.Controls.Add(this.lblMontoRetiro);
            this.tabRetiro.Controls.Add(this.txtMontoRetiro);
            this.tabRetiro.Controls.Add(this.lblDescripcionRetiro);
            this.tabRetiro.Controls.Add(this.txtDescripcionRetiro);
            this.tabRetiro.Controls.Add(this.btnConfirmarRetiro);
            this.tabRetiro.Controls.Add(this.lblMensajeRetiro);

            // ===================== PESTANA DEPOSITO =====================
            ConfigurarTitulo(this.lblTituloDeposito, "Realizar Deposito", System.Drawing.Color.FromArgb(30, 120, 60));
            ConfigurarEtiqueta(this.lblCuentaDeposito, "Cuenta *", 70);
            ConfigurarCombo(this.cmbCuentaDeposito, 93);
            this.cmbCuentaDeposito.SelectedIndexChanged += new System.EventHandler(this.cmbCuentaDeposito_SelectedIndexChanged);

            ConfigurarLabelSaldo(this.lblSaldoActualDeposito, 135);

            ConfigurarEtiqueta(this.lblMontoDeposito, "Monto a depositar (Q) *", 175);
            ConfigurarTexto(this.txtMontoDeposito, 198);

            ConfigurarEtiqueta(this.lblDescripcionDeposito, "Descripcion (opcional)", 240);
            ConfigurarTexto(this.txtDescripcionDeposito, 263);

            ConfigurarBotonConfirmar(this.btnConfirmarDeposito, "Confirmar Deposito", System.Drawing.Color.FromArgb(30, 120, 60), 315);
            this.btnConfirmarDeposito.Click += new System.EventHandler(this.btnConfirmarDeposito_Click);

            ConfigurarMensaje(this.lblMensajeDeposito, 370);

            // ===================== PESTANA RETIRO =====================
            ConfigurarTitulo(this.lblTituloRetiro, "Realizar Retiro", System.Drawing.Color.FromArgb(160, 40, 40));
            ConfigurarEtiqueta(this.lblCuentaRetiro, "Cuenta *", 70);
            ConfigurarCombo(this.cmbCuentaRetiro, 93);
            this.cmbCuentaRetiro.SelectedIndexChanged += new System.EventHandler(this.cmbCuentaRetiro_SelectedIndexChanged);

            ConfigurarLabelSaldo(this.lblSaldoActualRetiro, 135);

            ConfigurarEtiqueta(this.lblMontoRetiro, "Monto a retirar (Q) *", 175);
            ConfigurarTexto(this.txtMontoRetiro, 198);

            ConfigurarEtiqueta(this.lblDescripcionRetiro, "Descripcion (opcional)", 240);
            ConfigurarTexto(this.txtDescripcionRetiro, 263);

            ConfigurarBotonConfirmar(this.btnConfirmarRetiro, "Confirmar Retiro", System.Drawing.Color.FromArgb(160, 40, 40), 315);
            this.btnConfirmarRetiro.Click += new System.EventHandler(this.btnConfirmarRetiro_Click);

            ConfigurarMensaje(this.lblMensajeRetiro, 370);

            // ===== Agregar tabControl =====
            this.Controls.Add(this.tabControl);

            this.tabDeposito.ResumeLayout(false);
            this.tabRetiro.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // ===== Metodos auxiliares de estilo =====
        private void ConfigurarTitulo(System.Windows.Forms.Label lbl, string texto, System.Drawing.Color color)
        {
            lbl.Text = texto;
            lbl.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lbl.ForeColor = color;
            lbl.Location = new System.Drawing.Point(0, 10);
            lbl.Size = new System.Drawing.Size(400, 35);
        }

        private void ConfigurarEtiqueta(System.Windows.Forms.Label lbl, string texto, int posY)
        {
            lbl.Text = texto;
            lbl.Font = new System.Drawing.Font("Segoe UI", 9F);
            lbl.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            lbl.Location = new System.Drawing.Point(0, posY);
            lbl.AutoSize = true;
        }

        private void ConfigurarCombo(System.Windows.Forms.ComboBox combo, int posY)
        {
            combo.Font = new System.Drawing.Font("Segoe UI", 10F);
            combo.Location = new System.Drawing.Point(0, posY);
            combo.Size = new System.Drawing.Size(420, 28);
            combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }

        private void ConfigurarLabelSaldo(System.Windows.Forms.Label lbl, int posY)
        {
            lbl.Text = "Saldo actual: -";
            lbl.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Italic);
            lbl.ForeColor = System.Drawing.Color.FromArgb(90, 90, 90);
            lbl.Location = new System.Drawing.Point(0, posY);
            lbl.AutoSize = true;
        }

        private void ConfigurarTexto(System.Windows.Forms.TextBox txt, int posY)
        {
            txt.Font = new System.Drawing.Font("Segoe UI", 11F);
            txt.Location = new System.Drawing.Point(0, posY);
            txt.Size = new System.Drawing.Size(420, 28);
            txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }

        private void ConfigurarBotonConfirmar(System.Windows.Forms.Button btn, string texto, System.Drawing.Color color, int posY)
        {
            btn.Text = texto;
            btn.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            btn.ForeColor = System.Drawing.Color.White;
            btn.BackColor = color;
            btn.UseVisualStyleBackColor = false;
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Location = new System.Drawing.Point(0, posY);
            btn.Size = new System.Drawing.Size(420, 42);
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        private void ConfigurarMensaje(System.Windows.Forms.Label lbl, int posY)
        {
            lbl.Text = "";
            lbl.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            lbl.Location = new System.Drawing.Point(0, posY);
            lbl.Size = new System.Drawing.Size(420, 50);
        }
    }
}