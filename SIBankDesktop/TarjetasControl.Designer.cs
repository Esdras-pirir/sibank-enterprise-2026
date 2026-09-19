namespace SIBankDesktop
{
    partial class TarjetasControl
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
        private System.Windows.Forms.Button btnEmitir;
        private System.Windows.Forms.Button btnBloquear;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.FlowLayoutPanel flpTarjetas;

        private void InitializeComponent()
        {
            this.pnlSuperior = new System.Windows.Forms.Panel();
            this.btnEmitir = new System.Windows.Forms.Button();
            this.btnBloquear = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.flpTarjetas = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlSuperior.SuspendLayout();
            this.SuspendLayout();

            // ===== TarjetasControl (raiz) =====
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);

            // ===== pnlSuperior =====
            this.pnlSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSuperior.Height = 55;
            this.pnlSuperior.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.pnlSuperior.Controls.Add(this.btnEmitir);
            this.pnlSuperior.Controls.Add(this.btnBloquear);
            this.pnlSuperior.Controls.Add(this.btnActualizar);

            // ===== btnEmitir =====
            ConfigurarBoton(this.btnEmitir, "+ Emitir Tarjeta", 0, System.Drawing.Color.FromArgb(21, 34, 56));
            this.btnEmitir.Click += new System.EventHandler(this.btnEmitir_Click);

            // ===== btnBloquear =====
            ConfigurarBoton(this.btnBloquear, "Bloquear seleccionada", 190, System.Drawing.Color.FromArgb(160, 40, 40));
            this.btnBloquear.Click += new System.EventHandler(this.btnBloquear_Click);

            // ===== btnActualizar =====
            ConfigurarBoton(this.btnActualizar, "Actualizar", 400, System.Drawing.Color.FromArgb(90, 90, 90));
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);

            // ===== flpTarjetas (cuadricula de tarjetas visuales) =====
            this.flpTarjetas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTarjetas.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.flpTarjetas.AutoScroll = true;
            this.flpTarjetas.Padding = new System.Windows.Forms.Padding(20);
            this.flpTarjetas.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flpTarjetas.WrapContents = true;

            // ===== Agregar controles =====
            this.Controls.Add(this.flpTarjetas);
            this.Controls.Add(this.pnlSuperior);

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
            btn.Size = new System.Drawing.Size(180, 36);
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
        }
    }
}