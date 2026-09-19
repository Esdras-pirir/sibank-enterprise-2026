namespace SIBankDesktop
{
    partial class PrestamosControl
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
        private System.Windows.Forms.Button btnAprobar;
        private System.Windows.Forms.Button btnRechazar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.DataGridView dgvPrestamos;

        private void InitializeComponent()
        {
            this.pnlSuperior = new System.Windows.Forms.Panel();
            this.btnNueva = new System.Windows.Forms.Button();
            this.btnAprobar = new System.Windows.Forms.Button();
            this.btnRechazar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.dgvPrestamos = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrestamos)).BeginInit();
            this.pnlSuperior.SuspendLayout();
            this.SuspendLayout();

            // ===== PrestamosControl (raiz) =====
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);

            // ===== pnlSuperior (barra de botones) =====
            this.pnlSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSuperior.Height = 55;
            this.pnlSuperior.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.pnlSuperior.Controls.Add(this.btnNueva);
            this.pnlSuperior.Controls.Add(this.btnAprobar);
            this.pnlSuperior.Controls.Add(this.btnRechazar);
            this.pnlSuperior.Controls.Add(this.btnActualizar);

            // ===== btnNueva =====
            ConfigurarBoton(this.btnNueva, "+ Nueva Solicitud", 0, System.Drawing.Color.FromArgb(21, 34, 56));
            this.btnNueva.Click += new System.EventHandler(this.btnNueva_Click);

            // ===== btnAprobar =====
            ConfigurarBoton(this.btnAprobar, "Aprobar", 200, System.Drawing.Color.FromArgb(30, 120, 60));
            this.btnAprobar.Click += new System.EventHandler(this.btnAprobar_Click);

            // ===== btnRechazar =====
            ConfigurarBoton(this.btnRechazar, "Rechazar", 380, System.Drawing.Color.FromArgb(160, 40, 40));
            this.btnRechazar.Click += new System.EventHandler(this.btnRechazar_Click);

            // ===== btnActualizar =====
            ConfigurarBoton(this.btnActualizar, "Actualizar lista", 560, System.Drawing.Color.FromArgb(90, 90, 90));
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);

            // ===== dgvPrestamos =====
            this.dgvPrestamos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPrestamos.BackgroundColor = System.Drawing.Color.White;
            this.dgvPrestamos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPrestamos.ReadOnly = true;
            this.dgvPrestamos.AllowUserToAddRows = false;
            this.dgvPrestamos.AllowUserToDeleteRows = false;
            this.dgvPrestamos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPrestamos.MultiSelect = false;
            this.dgvPrestamos.RowHeadersVisible = false;
            this.dgvPrestamos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPrestamos.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dgvPrestamos.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(21, 34, 56);
            this.dgvPrestamos.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvPrestamos.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.dgvPrestamos.ColumnHeadersHeight = 38;
            this.dgvPrestamos.EnableHeadersVisualStyles = false;
            this.dgvPrestamos.RowTemplate.Height = 32;
            this.dgvPrestamos.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(248, 249, 251);

            // ===== Agregar controles =====
            this.Controls.Add(this.dgvPrestamos);
            this.Controls.Add(this.pnlSuperior);

            ((System.ComponentModel.ISupportInitialize)(this.dgvPrestamos)).EndInit();
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
            btn.Size = new System.Drawing.Size(170, 36);
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
        }
    }
}