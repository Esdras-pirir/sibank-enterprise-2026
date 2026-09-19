namespace SIBankDesktop
{
    partial class ReportesControl
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
        private System.Windows.Forms.Button btnActualizar;

        private System.Windows.Forms.FlowLayoutPanel flpKpis;

        private System.Windows.Forms.TableLayoutPanel tlpGraficas;
        private System.Windows.Forms.Panel pnlGraficaCuentas;
        private System.Windows.Forms.Label lblTituloGraficaCuentas;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCuentasPorTipo;
        private System.Windows.Forms.Panel pnlGraficaPrestamos;
        private System.Windows.Forms.Label lblTituloGraficaPrestamos;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPrestamosPorEstado;
        private System.Windows.Forms.Panel pnlContenedorScroll;

        private void InitializeComponent()
        {
            this.pnlSuperior = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.flpKpis = new System.Windows.Forms.FlowLayoutPanel();
            this.tlpGraficas = new System.Windows.Forms.TableLayoutPanel();
            this.pnlGraficaCuentas = new System.Windows.Forms.Panel();
            this.lblTituloGraficaCuentas = new System.Windows.Forms.Label();
            this.chartCuentasPorTipo = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnlGraficaPrestamos = new System.Windows.Forms.Panel();
            this.lblTituloGraficaPrestamos = new System.Windows.Forms.Label();
            this.chartPrestamosPorEstado = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnlContenedorScroll = new System.Windows.Forms.Panel();

            ((System.ComponentModel.ISupportInitialize)(this.chartCuentasPorTipo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPrestamosPorEstado)).BeginInit();
            this.pnlSuperior.SuspendLayout();
            this.pnlGraficaCuentas.SuspendLayout();
            this.pnlGraficaPrestamos.SuspendLayout();
            this.tlpGraficas.SuspendLayout();
            this.pnlContenedorScroll.SuspendLayout();
            this.SuspendLayout();

            // ===== Control Raíz =====
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);

            // ===== Panel Header =====
            this.pnlSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSuperior.Height = 85;
            this.pnlSuperior.BackColor = System.Drawing.Color.White;
            this.pnlSuperior.Controls.Add(this.lblTitulo);
            this.pnlSuperior.Controls.Add(this.lblSubtitulo);
            this.pnlSuperior.Controls.Add(this.btnActualizar);

            // ===== Título Principal =====
            this.lblTitulo.Text = "Executive Analytics & Financial Intelligence";
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 15.5F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblTitulo.Location = new System.Drawing.Point(28, 14);
            this.lblTitulo.AutoSize = true;

            // ===== Subtítulo =====
            this.lblSubtitulo.Text = "Monitorización avanzada de liquidez, métricas operativas y carteras de crédito en tiempo real.";
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 8.8F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblSubtitulo.Location = new System.Drawing.Point(28, 46);
            this.lblSubtitulo.AutoSize = true;

            // ===== Botón Actualizar =====
            this.btnActualizar.Text = "⚡ Actualizar Datos";
            this.btnActualizar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnActualizar.ForeColor = System.Drawing.Color.White;
            this.btnActualizar.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizar.FlatAppearance.BorderSize = 0;
            this.btnActualizar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnActualizar.Location = new System.Drawing.Point(900, 24);
            this.btnActualizar.Size = new System.Drawing.Size(145, 36);
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);

            // ===== Contenedor con Scroll Automático Responsivo =====
            this.pnlContenedorScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenedorScroll.AutoScroll = true;
            this.pnlContenedorScroll.Padding = new System.Windows.Forms.Padding(24);

            // ===== Grid de Tarjetas KPI (FlowLayoutPanel con Autowrap) =====
            this.flpKpis.Dock = System.Windows.Forms.DockStyle.Top;
            this.flpKpis.AutoSize = true;
            this.flpKpis.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpKpis.WrapContents = true;
            this.flpKpis.BackColor = System.Drawing.Color.Transparent;
            this.flpKpis.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);

            // ===== Grid de Gráficas =====
            this.tlpGraficas.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpGraficas.Height = 410;
            this.tlpGraficas.ColumnCount = 2;
            this.tlpGraficas.RowCount = 1;
            this.tlpGraficas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpGraficas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpGraficas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpGraficas.BackColor = System.Drawing.Color.Transparent;
            this.tlpGraficas.Controls.Add(this.pnlGraficaCuentas, 0, 0);
            this.tlpGraficas.Controls.Add(this.pnlGraficaPrestamos, 1, 0);

            // ===== Card Gráfica 1 =====
            this.pnlGraficaCuentas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGraficaCuentas.BackColor = System.Drawing.Color.White;
            this.pnlGraficaCuentas.Margin = new System.Windows.Forms.Padding(0, 8, 10, 0);
            this.pnlGraficaCuentas.Padding = new System.Windows.Forms.Padding(20);
            this.pnlGraficaCuentas.Controls.Add(this.chartCuentasPorTipo);
            this.pnlGraficaCuentas.Controls.Add(this.lblTituloGraficaCuentas);

            this.lblTituloGraficaCuentas.Text = "Distribución de Cuentas por Tipo";
            this.lblTituloGraficaCuentas.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTituloGraficaCuentas.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblTituloGraficaCuentas.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTituloGraficaCuentas.Height = 35;

            this.chartCuentasPorTipo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartCuentasPorTipo.BackColor = System.Drawing.Color.White;

            // ===== Card Gráfica 2 =====
            this.pnlGraficaPrestamos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGraficaPrestamos.BackColor = System.Drawing.Color.White;
            this.pnlGraficaPrestamos.Margin = new System.Windows.Forms.Padding(10, 8, 0, 0);
            this.pnlGraficaPrestamos.Padding = new System.Windows.Forms.Padding(20);
            this.pnlGraficaPrestamos.Controls.Add(this.chartPrestamosPorEstado);
            this.pnlGraficaPrestamos.Controls.Add(this.lblTituloGraficaPrestamos);

            this.lblTituloGraficaPrestamos.Text = "Estado de Solicitudes de Crédito";
            this.lblTituloGraficaPrestamos.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTituloGraficaPrestamos.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblTituloGraficaPrestamos.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTituloGraficaPrestamos.Height = 35;

            this.chartPrestamosPorEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartPrestamosPorEstado.BackColor = System.Drawing.Color.White;

            // ===== Jerarquía de Renderizado =====
            this.pnlContenedorScroll.Controls.Add(this.tlpGraficas);
            this.pnlContenedorScroll.Controls.Add(this.flpKpis);

            this.Controls.Add(this.pnlContenedorScroll);
            this.Controls.Add(this.pnlSuperior);

            ((System.ComponentModel.ISupportInitialize)(this.chartCuentasPorTipo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPrestamosPorEstado)).EndInit();
            this.pnlGraficaCuentas.ResumeLayout(false);
            this.pnlGraficaPrestamos.ResumeLayout(false);
            this.tlpGraficas.ResumeLayout(false);
            this.pnlSuperior.ResumeLayout(false);
            this.pnlSuperior.PerformLayout();
            this.pnlContenedorScroll.ResumeLayout(false);
            this.pnlContenedorScroll.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}