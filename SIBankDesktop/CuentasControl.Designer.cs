namespace SIBankDesktop
{
    partial class CuentasControl
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

        private System.Windows.Forms.Panel pnlSuperior;
        private System.Windows.Forms.Button btnNueva;
        private System.Windows.Forms.Button btnVerSaldo;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.DataGridView dgvCuentas;

        private void InitializeComponent()
        {
            this.pnlSuperior = new System.Windows.Forms.Panel();
            this.btnNueva = new System.Windows.Forms.Button();
            this.btnVerSaldo = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.dgvCuentas = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCuentas)).BeginInit();
            this.pnlSuperior.SuspendLayout();
            this.SuspendLayout();

            // ===== CuentasControl (raiz) =====
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);

            // ===== pnlSuperior (barra de botones) =====
            this.pnlSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSuperior.Height = 55;
            this.pnlSuperior.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.pnlSuperior.Controls.Add(this.btnNueva);
            this.pnlSuperior.Controls.Add(this.btnVerSaldo);
            this.pnlSuperior.Controls.Add(this.btnActualizar);

            // ===== btnNueva =====
            ConfigurarBoton(this.btnNueva, "+ Abrir Cuenta", 0, System.Drawing.Color.FromArgb(21, 34, 56));
            this.btnNueva.Click += new System.EventHandler(this.btnNueva_Click);

            // ===== btnVerSaldo =====
            ConfigurarBoton(this.btnVerSaldo, "Ver Saldo", 170, System.Drawing.Color.FromArgb(60, 90, 130));
            this.btnVerSaldo.Click += new System.EventHandler(this.btnVerSaldo_Click);

            // ===== btnActualizar =====
            ConfigurarBoton(this.btnActualizar, "Actualizar lista", 340, System.Drawing.Color.FromArgb(90, 90, 90));
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);

            // ===== dgvCuentas =====
            this.dgvCuentas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCuentas.BackgroundColor = System.Drawing.Color.White;
            this.dgvCuentas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCuentas.ReadOnly = true;
            this.dgvCuentas.AllowUserToAddRows = false;
            this.dgvCuentas.AllowUserToDeleteRows = false;
            this.dgvCuentas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCuentas.MultiSelect = false;
            this.dgvCuentas.RowHeadersVisible = false;
            this.dgvCuentas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCuentas.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dgvCuentas.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(21, 34, 56);
            this.dgvCuentas.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvCuentas.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.dgvCuentas.ColumnHeadersHeight = 38;
            this.dgvCuentas.EnableHeadersVisualStyles = false;
            this.dgvCuentas.RowTemplate.Height = 32;
            this.dgvCuentas.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(248, 249, 251);

            // ===== Agregar controles =====
            this.Controls.Add(this.dgvCuentas);
            this.Controls.Add(this.pnlSuperior);

            ((System.ComponentModel.ISupportInitialize)(this.dgvCuentas)).EndInit();
            this.pnlSuperior.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // Metodo auxiliar para no repetir estilo en cada boton
        private void ConfigurarBoton(System.Windows.Forms.Button btn, string texto, int posX, System.Drawing.Color color)
        {
            btn.Text = texto;
            btn.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btn.ForeColor = System.Drawing.Color.White;
            btn.BackColor = color;
            btn.UseVisualStyleBackColor = false;
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            btn.Location = new System.Drawing.Point(15 + posX, 10);
            btn.Size = new System.Drawing.Size(150, 36);
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
        }
    }
}