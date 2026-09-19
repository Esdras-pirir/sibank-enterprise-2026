using System;
using System.Drawing;
using System.Windows.Forms;

namespace SIBankDesktop
{
    partial class DashboardForm
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

        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Label lblSubLogo;
        private System.Windows.Forms.Label lblUsuarioActivo;
        private System.Windows.Forms.Label lblBreadcrumb;
        private System.Windows.Forms.Panel pnlStatusDot;
        private System.Windows.Forms.Label lblStatusText;

        // Contenedor elástico interno para organizar los botones de navegación
        private System.Windows.Forms.FlowLayoutPanel flpContenedorMenu;

        private System.Windows.Forms.Button btnClientes;
        private System.Windows.Forms.Button btnCuentas;
        private System.Windows.Forms.Button btnDepositos;
        private System.Windows.Forms.Button btnRetiros;
        private System.Windows.Forms.Button btnTransferencias;
        private System.Windows.Forms.Button btnPrestamos;
        private System.Windows.Forms.Button btnTarjetas;
        private System.Windows.Forms.Button btnReportes;
        private System.Windows.Forms.Button btnUsuarios;
        private System.Windows.Forms.Button btnBitacora;
        private System.Windows.Forms.Button btnCerrarSesion;

        private System.Windows.Forms.TabControl tbcContenido;

        private void InitializeComponent()
        {
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblLogo = new System.Windows.Forms.Label();
            this.lblSubLogo = new System.Windows.Forms.Label();
            this.lblUsuarioActivo = new System.Windows.Forms.Label();
            this.lblBreadcrumb = new System.Windows.Forms.Label();
            this.pnlStatusDot = new System.Windows.Forms.Panel();
            this.lblStatusText = new System.Windows.Forms.Label();

            // Inicializamos el contenedor dinámico del menú
            this.flpContenedorMenu = new System.Windows.Forms.FlowLayoutPanel();

            this.btnClientes = new System.Windows.Forms.Button();
            this.btnCuentas = new System.Windows.Forms.Button();
            this.btnDepositos = new System.Windows.Forms.Button();
            this.btnRetiros = new System.Windows.Forms.Button();
            this.btnTransferencias = new System.Windows.Forms.Button();
            this.btnPrestamos = new System.Windows.Forms.Button();
            this.btnTarjetas = new System.Windows.Forms.Button();
            this.btnReportes = new System.Windows.Forms.Button();
            this.btnUsuarios = new System.Windows.Forms.Button();
            this.btnBitacora = new System.Windows.Forms.Button();
            this.btnCerrarSesion = new System.Windows.Forms.Button();

            this.tbcContenido = new System.Windows.Forms.TabControl();

            this.pnlMenu.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.flpContenedorMenu.SuspendLayout();
            this.SuspendLayout();

            // ===== DashboardForm =====
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Text = "SIBank Enterprise - Operating System";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(244, 247, 252);
            this.MinimumSize = new System.Drawing.Size(800, 680);

            // ===== pnlMenu (Barra Lateral Dark Navy) =====
            this.pnlMenu.BackColor = System.Drawing.Color.FromArgb(16, 26, 43);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.Width = 230;
            this.pnlMenu.Controls.Add(this.flpContenedorMenu);
            this.pnlMenu.Controls.Add(this.btnCerrarSesion);  // Añadido al contenedor base
            this.pnlMenu.Controls.Add(this.lblSubLogo);
            this.pnlMenu.Controls.Add(this.lblLogo);

            // ===== Logo y Marca =====
            this.lblLogo.Text = "SIBank";
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Location = new System.Drawing.Point(0, 18);
            this.lblLogo.Size = new System.Drawing.Size(230, 30);
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblSubLogo.Text = "ENTERPRISE SYSTEM";
            this.lblSubLogo.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this.lblSubLogo.ForeColor = System.Drawing.Color.FromArgb(0, 162, 255);
            this.lblSubLogo.Location = new System.Drawing.Point(0, 48);
            this.lblSubLogo.Size = new System.Drawing.Size(230, 15);
            this.lblSubLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ===== flpContenedorMenu =====
            this.flpContenedorMenu.Location = new System.Drawing.Point(0, 80);
            this.flpContenedorMenu.Size = new System.Drawing.Size(230, 480); // Reducido para dar espacio perfecto abajo
            this.flpContenedorMenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpContenedorMenu.WrapContents = false;
            this.flpContenedorMenu.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.flpContenedorMenu.BackColor = System.Drawing.Color.Transparent;

            // Colocamos los botones de navegación dentro
            this.flpContenedorMenu.Controls.Add(this.btnClientes);
            this.flpContenedorMenu.Controls.Add(this.btnCuentas);
            this.flpContenedorMenu.Controls.Add(this.btnDepositos);
            this.flpContenedorMenu.Controls.Add(this.btnRetiros);
            this.flpContenedorMenu.Controls.Add(this.btnTransferencias);
            this.flpContenedorMenu.Controls.Add(this.btnPrestamos);
            this.flpContenedorMenu.Controls.Add(this.btnTarjetas);
            this.flpContenedorMenu.Controls.Add(this.btnReportes);
            this.flpContenedorMenu.Controls.Add(this.btnUsuarios);
            this.flpContenedorMenu.Controls.Add(this.btnBitacora);

            // ===== Botones del Menú Lateral =====
            ConfigurarBotonMenu(this.btnClientes, "👤  Clientes");
            ConfigurarBotonMenu(this.btnCuentas, "💳  Cuentas");
            ConfigurarBotonMenu(this.btnDepositos, "💵  Depósitos");
            ConfigurarBotonMenu(this.btnRetiros, "🏧  Retiros");
            ConfigurarBotonMenu(this.btnTransferencias, "🔄  Transferencias");
            ConfigurarBotonMenu(this.btnPrestamos, "📋  Préstamos");
            ConfigurarBotonMenu(this.btnTarjetas, "🪪  Tarjetas");
            ConfigurarBotonMenu(this.btnReportes, "📊  Reportes");
            ConfigurarBotonMenu(this.btnUsuarios, "⚙️  Usuarios");
            ConfigurarBotonMenu(this.btnBitacora, "📜  Bitácora");

            this.btnClientes.Click += new System.EventHandler(this.MenuButton_Click);
            this.btnCuentas.Click += new System.EventHandler(this.MenuButton_Click);
            this.btnDepositos.Click += new System.EventHandler(this.MenuButton_Click);
            this.btnRetiros.Click += new System.EventHandler(this.MenuButton_Click);
            this.btnTransferencias.Click += new System.EventHandler(this.MenuButton_Click);
            this.btnPrestamos.Click += new System.EventHandler(this.MenuButton_Click);
            this.btnTarjetas.Click += new System.EventHandler(this.MenuButton_Click);
            this.btnReportes.Click += new System.EventHandler(this.MenuButton_Click);
            this.btnUsuarios.Click += new System.EventHandler(this.MenuButton_Click);
            this.btnBitacora.Click += new System.EventHandler(this.MenuButton_Click);

            // ===== btnCerrarSesion SOLUCIÓN MAESTRA RESPONSIVA =====
            this.btnCerrarSesion.Text = "Cerrar Sesión";
            this.btnCerrarSesion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCerrarSesion.ForeColor = System.Drawing.Color.White;
            this.btnCerrarSesion.BackColor = System.Drawing.Color.FromArgb(170, 40, 40);
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.FlatAppearance.BorderSize = 0;
            // Al usar DockStyle.Bottom forzamos al botón a pegarse abajo del pnlMenu de forma nativa e indestructible
            this.btnCerrarSesion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCerrarSesion.Size = new System.Drawing.Size(230, 45); // Se adapta al ancho de la barra (230)
            this.btnCerrarSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);

            // ===== pnlHeader (Barra Superior Elegante) =====
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 48;
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblBreadcrumb);
            this.pnlHeader.Controls.Add(this.lblUsuarioActivo);
            this.pnlHeader.Controls.Add(this.pnlStatusDot);
            this.pnlHeader.Controls.Add(this.lblStatusText);

            // ===== lblBreadcrumb =====
            this.lblBreadcrumb.Text = "Inicio / Panel General";
            this.lblBreadcrumb.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblBreadcrumb.ForeColor = System.Drawing.Color.FromArgb(60, 70, 85);
            this.lblBreadcrumb.Location = new System.Drawing.Point(20, 14);
            this.lblBreadcrumb.Size = new System.Drawing.Size(300, 20);
            this.lblBreadcrumb.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ===== lblUsuarioActivo (Anclado a la derecha) =====
            this.lblUsuarioActivo.Text = "admin";
            this.lblUsuarioActivo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblUsuarioActivo.ForeColor = System.Drawing.Color.FromArgb(21, 34, 56);
            this.lblUsuarioActivo.Size = new System.Drawing.Size(150, 20);
            this.lblUsuarioActivo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.lblUsuarioActivo.Location = new System.Drawing.Point(1000, 14);
            this.lblUsuarioActivo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // ===== pnlStatusDot (Punto de Estado Verde) =====
            this.pnlStatusDot.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.pnlStatusDot.Size = new System.Drawing.Size(8, 8);
            this.pnlStatusDot.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.pnlStatusDot.Location = new System.Drawing.Point(1160, 20);
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, 8, 8);
            this.pnlStatusDot.Region = new System.Drawing.Region(path);

            // ===== lblStatusText =====
            this.lblStatusText.Text = "En Línea";
            this.lblStatusText.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblStatusText.ForeColor = System.Drawing.Color.Gray;
            this.lblStatusText.Size = new System.Drawing.Size(55, 20);
            this.lblStatusText.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.lblStatusText.Location = new System.Drawing.Point(1173, 14);
            this.lblStatusText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ===== tbcContenido (Contenedor Central de Pestañas Avanzado) =====
            this.tbcContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcContenido.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.tbcContenido.Location = new System.Drawing.Point(230, 48);
            this.tbcContenido.Size = new System.Drawing.Size(1050, 672);

            // CAMBIO: Activamos el dibujo manual para poder pintar la 'X' y el botón de maximizar ventana
            this.tbcContenido.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tbcContenido.Padding = new System.Drawing.Point(24, 4); // Damos espacio extra a los lados para los iconos
            this.tbcContenido.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tbcContenido_DrawItem);
            this.tbcContenido.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbcContenido_MouseDown);


            // ===== CORRECCIÓN DE ORDEN: Agregar controles finales =====
            // Agregamos primero el botón de cerrar sesión para forzarlo a renderizarse al frente de todo en la barra lateral
            this.pnlMenu.Controls.Add(this.btnCerrarSesion);
            this.pnlMenu.Controls.Add(this.flpContenedorMenu);
            this.pnlMenu.Controls.Add(this.lblSubLogo);
            this.pnlMenu.Controls.Add(this.lblLogo);

            // Agregamos los contenedores principales a la raíz del formulario
            this.Controls.Add(this.tbcContenido);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlMenu);

            this.pnlMenu.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.flpContenedorMenu.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void ConfigurarBotonMenu(System.Windows.Forms.Button btn, string texto)
        {
            btn.Text = texto;
            btn.Font = new System.Drawing.Font("Segoe UI", 10F);
            btn.ForeColor = System.Drawing.Color.FromArgb(200, 205, 215);
            btn.BackColor = System.Drawing.Color.Transparent;
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btn.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            btn.Size = new System.Drawing.Size(230, 40);
            btn.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            btn.Cursor = System.Windows.Forms.Cursors.Hand;

            btn.MouseEnter += (s, e) => {
                btn.BackColor = System.Drawing.Color.FromArgb(28, 41, 64);
                btn.ForeColor = System.Drawing.Color.White;
            };
            btn.MouseLeave += (s, e) => {
                btn.BackColor = System.Drawing.Color.Transparent;
                btn.ForeColor = System.Drawing.Color.FromArgb(200, 205, 215);
            };
        }
    }
}

