namespace SIBankDesktop
{
    partial class UsuariosControl
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

        private CyberGradientPanel pnlFondoGradient;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;

        private System.Windows.Forms.FlowLayoutPanel flpKpis;
        private System.Windows.Forms.Panel pnlKpiTotal;
        private System.Windows.Forms.Label lblValTotal;
        private System.Windows.Forms.Label lblTagTotal;

        private System.Windows.Forms.Panel pnlKpiActivos;
        private System.Windows.Forms.Label lblValActivos;
        private System.Windows.Forms.Label lblTagActivos;

        private System.Windows.Forms.Panel pnlKpiInactivos;
        private System.Windows.Forms.Label lblValInactivos;
        private System.Windows.Forms.Label lblTagInactivos;

        private System.Windows.Forms.Panel pnlCardCentral;
        private System.Windows.Forms.Panel pnlSuperior;
        private System.Windows.Forms.FlowLayoutPanel flpAcciones;

        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnActivar;
        private System.Windows.Forms.Button btnDesactivar;
        private System.Windows.Forms.Button btnResetPassword;
        private System.Windows.Forms.Button btnActualizar;

        private System.Windows.Forms.DataGridView dgvUsuarios;

        private void InitializeComponent()
        {
            this.pnlFondoGradient = new SIBankDesktop.CyberGradientPanel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();

            this.flpKpis = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlKpiTotal = new System.Windows.Forms.Panel();
            this.lblValTotal = new System.Windows.Forms.Label();
            this.lblTagTotal = new System.Windows.Forms.Label();

            this.pnlKpiActivos = new System.Windows.Forms.Panel();
            this.lblValActivos = new System.Windows.Forms.Label();
            this.lblTagActivos = new System.Windows.Forms.Label();

            this.pnlKpiInactivos = new System.Windows.Forms.Panel();
            this.lblValInactivos = new System.Windows.Forms.Label();
            this.lblTagInactivos = new System.Windows.Forms.Label();

            this.pnlCardCentral = new System.Windows.Forms.Panel();
            this.pnlSuperior = new System.Windows.Forms.Panel();
            this.flpAcciones = new System.Windows.Forms.FlowLayoutPanel();

            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnActivar = new System.Windows.Forms.Button();
            this.btnDesactivar = new System.Windows.Forms.Button();
            this.btnResetPassword = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();

            this.dgvUsuarios = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.pnlHeader.SuspendLayout();
            this.pnlCardCentral.SuspendLayout();
            this.pnlSuperior.SuspendLayout();
            this.flpAcciones.SuspendLayout();
            this.pnlFondoGradient.SuspendLayout();
            this.SuspendLayout();

            // ===== UsuariosControl Root =====
            this.Dock = System.Windows.Forms.DockStyle.Fill;

            // ===== Fondo Gradient de SIBank Enterprise =====
            this.pnlFondoGradient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFondoGradient.Padding = new System.Windows.Forms.Padding(24);

            // ===== Header Superior de Control =====
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 60;
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.Controls.Add(this.lblTitulo);
            this.pnlHeader.Controls.Add(this.lblSubtitulo);

            this.lblTitulo.Text = "🛡️ SIBank Enterprise — Control de Usuarios";
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.AutoSize = true;

            this.lblSubtitulo.Text = "Administración centralizada de cuentas, estado operativo y seguridad de accesos.";
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 8.8F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblSubtitulo.Location = new System.Drawing.Point(0, 32);
            this.lblSubtitulo.AutoSize = true;

            // ===== Panel KPI Informativo =====
            this.flpKpis.Dock = System.Windows.Forms.DockStyle.Top;
            this.flpKpis.Height = 65;
            this.flpKpis.BackColor = System.Drawing.Color.Transparent;

            CrearKpiWidget(pnlKpiTotal, lblValTotal, lblTagTotal, "TOTAL USUARIOS", "0", System.Drawing.Color.FromArgb(56, 189, 248));
            CrearKpiWidget(pnlKpiActivos, lblValActivos, lblTagActivos, "CUENTAS ACTIVAS", "0", System.Drawing.Color.FromArgb(52, 211, 153));
            CrearKpiWidget(pnlKpiInactivos, lblValInactivos, lblTagInactivos, "INACTIVAS / BLOQUEADAS", "0", System.Drawing.Color.FromArgb(251, 113, 133));

            this.flpKpis.Controls.Add(this.pnlKpiTotal);
            this.flpKpis.Controls.Add(this.pnlKpiActivos);
            this.flpKpis.Controls.Add(this.pnlKpiInactivos);

            // ===== Card Central Contenedora =====
            this.pnlCardCentral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardCentral.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.pnlCardCentral.Padding = new System.Windows.Forms.Padding(16);

            // ===== Panel de Acciones con FlowLayoutPanel (Cero Solapamiento) =====
            this.pnlSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSuperior.Height = 52;
            this.pnlSuperior.BackColor = System.Drawing.Color.Transparent;

            this.flpAcciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpAcciones.AutoSize = true;
            this.flpAcciones.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flpAcciones.WrapContents = false;

            // Configuración dinámica de botones con AutoSize
            ConfigurarBoton(this.btnNuevo, "+ Nuevo Usuario", System.Drawing.Color.FromArgb(37, 99, 235));
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);

            ConfigurarBoton(this.btnActivar, "Activar", System.Drawing.Color.FromArgb(16, 185, 129));
            this.btnActivar.Click += new System.EventHandler(this.btnActivar_Click);

            ConfigurarBoton(this.btnDesactivar, "Desactivar", System.Drawing.Color.FromArgb(225, 29, 72));
            this.btnDesactivar.Click += new System.EventHandler(this.btnDesactivar_Click);

            ConfigurarBoton(this.btnResetPassword, "🔑 Resetear Contraseña", System.Drawing.Color.FromArgb(79, 70, 229));
            this.btnResetPassword.Click += new System.EventHandler(this.btnResetPassword_Click);

            ConfigurarBoton(this.btnActualizar, "🔄 Actualizar Lista", System.Drawing.Color.FromArgb(71, 85, 105));
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);

            this.flpAcciones.Controls.Add(this.btnNuevo);
            this.flpAcciones.Controls.Add(this.btnActivar);
            this.flpAcciones.Controls.Add(this.btnDesactivar);
            this.flpAcciones.Controls.Add(this.btnResetPassword);
            this.flpAcciones.Controls.Add(this.btnActualizar);

            this.pnlSuperior.Controls.Add(this.flpAcciones);

            // ===== DataGridView Personalizado =====
            this.dgvUsuarios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsuarios.BackgroundColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.dgvUsuarios.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvUsuarios.ReadOnly = true;
            this.dgvUsuarios.AllowUserToAddRows = false;
            this.dgvUsuarios.AllowUserToDeleteRows = false;
            this.dgvUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsuarios.MultiSelect = false;
            this.dgvUsuarios.RowHeadersVisible = false;
            this.dgvUsuarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsuarios.EnableHeadersVisualStyles = false;

            this.dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.dgvUsuarios.ColumnHeadersHeight = 40;

            this.dgvUsuarios.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.dgvUsuarios.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.dgvUsuarios.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.dgvUsuarios.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(56, 189, 248);
            this.dgvUsuarios.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvUsuarios.RowTemplate.Height = 36;
            this.dgvUsuarios.GridColor = System.Drawing.Color.FromArgb(51, 65, 85);

            this.dgvUsuarios.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvUsuarios_CellFormatting);

            // ===== Ensamblado del Layout =====
            this.pnlCardCentral.Controls.Add(this.dgvUsuarios);
            this.pnlCardCentral.Controls.Add(this.pnlSuperior);

            this.pnlFondoGradient.Controls.Add(this.pnlCardCentral);
            this.pnlFondoGradient.Controls.Add(this.flpKpis);
            this.pnlFondoGradient.Controls.Add(this.pnlHeader);

            this.Controls.Add(this.pnlFondoGradient);

            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlSuperior.ResumeLayout(false);
            this.pnlSuperior.PerformLayout();
            this.flpAcciones.ResumeLayout(false);
            this.flpAcciones.PerformLayout();
            this.pnlCardCentral.ResumeLayout(false);
            this.pnlFondoGradient.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void ConfigurarBoton(System.Windows.Forms.Button btn, string texto, System.Drawing.Color color)
        {
            btn.Text = texto;
            btn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btn.ForeColor = System.Drawing.Color.White;
            btn.BackColor = color;
            btn.UseVisualStyleBackColor = false;
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.AutoSize = true;
            btn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            btn.Padding = new System.Windows.Forms.Padding(16, 0, 16, 0);
            btn.Height = 36;
            btn.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        private void CrearKpiWidget(System.Windows.Forms.Panel pnl, System.Windows.Forms.Label lblVal, System.Windows.Forms.Label lblTag, string tag, string val, System.Drawing.Color accentColor)
        {
            pnl.Size = new System.Drawing.Size(200, 48);
            pnl.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            pnl.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);

            lblTag.Text = tag;
            lblTag.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            lblTag.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            lblTag.Location = new System.Drawing.Point(12, 6);
            lblTag.AutoSize = true;

            lblVal.Text = val;
            lblVal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblVal.ForeColor = accentColor;
            lblVal.Location = new System.Drawing.Point(12, 20);
            lblVal.AutoSize = true;

            pnl.Controls.Add(lblTag);
            pnl.Controls.Add(lblVal);
        }
    }
}