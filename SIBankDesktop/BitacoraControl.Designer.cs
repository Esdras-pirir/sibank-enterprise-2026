namespace SIBankDesktop
{
    partial class BitacoraControl
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
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.FlowLayoutPanel flpTimeline;

        private void InitializeComponent()
        {
            this.pnlSuperior = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.flpTimeline = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlSuperior.SuspendLayout();
            this.SuspendLayout();

            // ===== BitacoraControl (raiz) =====
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);

            // ===== pnlSuperior =====
            this.pnlSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSuperior.Height = 95;
            this.pnlSuperior.BackColor = System.Drawing.Color.White;
            this.pnlSuperior.Controls.Add(this.lblTitulo);
            this.pnlSuperior.Controls.Add(this.lblSubtitulo);
            this.pnlSuperior.Controls.Add(this.txtBuscar);
            this.pnlSuperior.Controls.Add(this.btnActualizar);

            // ===== lblTitulo =====
            this.lblTitulo.Text = "Bitacora de Actividad";
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(21, 34, 56);
            this.lblTitulo.Location = new System.Drawing.Point(30, 14);
            this.lblTitulo.AutoSize = true;

            // ===== lblSubtitulo =====
            this.lblSubtitulo.Text = "Historial de acciones realizadas en el sistema";
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitulo.Location = new System.Drawing.Point(30, 46);
            this.lblSubtitulo.AutoSize = true;

            // ===== txtBuscar =====
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBuscar.Location = new System.Drawing.Point(30, 60);
            this.txtBuscar.Size = new System.Drawing.Size(0, 0); // se reposiciona en el codigo (anclado a la derecha)
            this.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.txtBuscar.Location = new System.Drawing.Point(600, 55);
            this.txtBuscar.Size = new System.Drawing.Size(320, 28);
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);

            // ===== btnActualizar =====
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnActualizar.ForeColor = System.Drawing.Color.White;
            this.btnActualizar.BackColor = System.Drawing.Color.FromArgb(21, 34, 56);
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizar.FlatAppearance.BorderSize = 0;
            this.btnActualizar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnActualizar.Location = new System.Drawing.Point(940, 55);
            this.btnActualizar.Size = new System.Drawing.Size(120, 30);
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);

            // ===== flpTimeline =====
            this.flpTimeline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTimeline.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.flpTimeline.AutoScroll = true;
            this.flpTimeline.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpTimeline.WrapContents = false;
            this.flpTimeline.Padding = new System.Windows.Forms.Padding(24, 16, 24, 16);

            // ===== Agregar controles =====
            this.Controls.Add(this.flpTimeline);
            this.Controls.Add(this.pnlSuperior);

            this.pnlSuperior.ResumeLayout(false);
            this.pnlSuperior.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}