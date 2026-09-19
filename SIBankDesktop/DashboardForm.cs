using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SIBankDesktop
{
    public partial class DashboardForm : Form
    {
        private long idUsuario;
        private string nombreUsuario;

        // Color corporativo para el área de trabajo
        private readonly Color colorFondoWorkspace = Color.FromArgb(244, 247, 252);

        public DashboardForm(long idUsuario, string nombreUsuario)
        {
            InitializeComponent();
            this.idUsuario = idUsuario;
            this.nombreUsuario = nombreUsuario;
            this.lblUsuarioActivo.Text = "👤  " + nombreUsuario;

            this.pnlMenu.Resize += PnlMenu_Resize;
            this.tbcContenido.SelectedIndexChanged += TbcContenido_SelectedIndexChanged;
            PosicionarBotonCerrarSesion();
        }

        private void PnlMenu_Resize(object sender, EventArgs e)
        {
            PosicionarBotonCerrarSesion();
        }

        private void PosicionarBotonCerrarSesion()
        {
            int margenInferior = 20;
            int y = pnlMenu.ClientSize.Height - btnCerrarSesion.Height - margenInferior;

            int minimoY = 490 + 45;
            if (y < minimoY) y = minimoY;

            btnCerrarSesion.Location = new Point(15, y);
        }

        private void TbcContenido_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbcContenido.SelectedTab != null)
            {
                lblBreadcrumb.Text = "Inicio  /  " + tbcContenido.SelectedTab.Text;
            }
        }

        // ===== Clics del Menú Lateral =====
        private void MenuButton_Click(object sender, EventArgs e)
        {
            Button botonPresionado = (Button)sender;

            string seccion = botonPresionado.Text.Replace("👤", "").Replace("💳", "").Replace("💵", "")
                                                .Replace("🏧", "").Replace("🔄", "").Replace("📋", "")
                                                .Replace("🪪", "").Replace("📊", "").Replace("⚙️", "")
                                                .Replace("📜", "").Trim();

            UserControl controlSeccion = null;

            if (seccion == "Clientes")
                controlSeccion = new ClientesControl(idUsuario);
            else if (seccion == "Cuentas")
                controlSeccion = new CuentasControl(idUsuario);
            else if (seccion == "Depósitos" || seccion == "Depositos" || seccion == "Retiros")
                controlSeccion = new DepositosRetirosControl(idUsuario);
            else if (seccion == "Transferencias")
                controlSeccion = new TransferenciasControl(idUsuario);
            else if (seccion == "Préstamos" || seccion == "Prestamos")
                controlSeccion = new PrestamosControl(idUsuario);
            else if (seccion == "Tarjetas")
                controlSeccion = new TarjetasControl(idUsuario);
            else if (seccion == "Bitácora" || seccion == "Bitacora")
                controlSeccion = new BitacoraControl(idUsuario);
            else if (seccion == "Usuarios" || seccion == "Usuarios")
                controlSeccion = new UsuariosControl(idUsuario);
            else if (seccion == "Reportes" || seccion == "Reportes")
                controlSeccion = new ReportesControl(idUsuario);


            AbrirPestana(seccion, controlSeccion);
        }

        private void AbrirPestana(string titulo, UserControl contenido)
        {
            // Reutilizar pestaña existente
            foreach (TabPage tab in tbcContenido.TabPages)
            {
                if (tab.Text == titulo)
                {
                    tbcContenido.SelectedTab = tab;
                    return;
                }
            }

            // Fondo con el color suave Ice Blue
            TabPage nuevaPestana = new TabPage(titulo)
            {
                BackColor = colorFondoWorkspace
            };

            if (contenido != null)
            {
                contenido.BackColor = colorFondoWorkspace;
                contenido.Dock = DockStyle.Fill;
                nuevaPestana.Controls.Add(contenido);
            }
            else
            {
                // Tarjeta contenedora elevada para módulos en desarrollo
                Panel pnlCard = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = colorFondoWorkspace,
                    Padding = new Padding(30)
                };

                Panel pnlInnerCard = new Panel
                {
                    Dock = DockStyle.Top,
                    Height = 180,
                    BackColor = Color.White
                };

                Label lblTitulo = new Label
                {
                    Text = "Módulo de " + titulo,
                    Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(16, 26, 43),
                    AutoSize = true,
                    Location = new Point(30, 30)
                };

                Label lblDetalle = new Label
                {
                    Text = "Espacio de trabajo listo para operaciones de " + titulo.ToLower() + ".",
                    Font = new Font("Segoe UI", 10F),
                    ForeColor = Color.FromArgb(110, 125, 145),
                    AutoSize = true,
                    Location = new Point(32, 75)
                };

                pnlInnerCard.Controls.Add(lblTitulo);
                pnlInnerCard.Controls.Add(lblDetalle);
                pnlCard.Controls.Add(pnlInnerCard);
                nuevaPestana.Controls.Add(pnlCard);
            }

            tbcContenido.TabPages.Add(nuevaPestana);
            tbcContenido.SelectedTab = nuevaPestana;
        }

        // ===== Dibujo Personalizado de Pestañas FinTech =====
        private void tbcContenido_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = tbcContenido.TabPages[e.Index];
            Rectangle tabRect = tbcContenido.GetTabRect(e.Index);

            bool isSelected = (tbcContenido.SelectedIndex == e.Index);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Paleta: Activa Azul Dark Navy, Inactiva tono Ice Blue
            Color tabBgColor = isSelected ? Color.FromArgb(16, 26, 43) : Color.FromArgb(226, 233, 243);
            Color tabTextColor = isSelected ? Color.White : Color.FromArgb(60, 75, 95);

            using (SolidBrush bgBrush = new SolidBrush(tabBgColor))
            {
                e.Graphics.FillRectangle(bgBrush, tabRect);
            }

            // Acento Neón superior en la pestaña activa
            if (isSelected)
            {
                using (Pen topAccentPen = new Pen(Color.FromArgb(0, 162, 255), 3f))
                {
                    e.Graphics.DrawLine(topAccentPen, tabRect.Left, tabRect.Top + 1, tabRect.Right, tabRect.Top + 1);
                }
            }

            // 1. Título de la Pestaña
            TextRenderer.DrawText(
                e.Graphics,
                page.Text,
                e.Font,
                new Rectangle(tabRect.X + 8, tabRect.Y, tabRect.Width - 48, tabRect.Height),
                tabTextColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis
            );

            // Color de los Íconos
            Color iconColor = isSelected ? Color.FromArgb(160, 190, 230) : Color.FromArgb(120, 135, 155);

            // 2. Ícono Ventana Flotante (🗗)
            Rectangle detachRect = new Rectangle(tabRect.Right - 38, tabRect.Y + (tabRect.Height - 12) / 2, 12, 12);
            using (Pen pen = new Pen(iconColor, 1.4f))
            {
                e.Graphics.DrawRectangle(pen, detachRect.X, detachRect.Y + 3, 7, 7);
                e.Graphics.DrawLine(pen, detachRect.X + 4, detachRect.Y + 4, detachRect.X + 10, detachRect.Y - 1);
                e.Graphics.DrawLine(pen, detachRect.X + 7, detachRect.Y - 1, detachRect.X + 10, detachRect.Y - 1);
                e.Graphics.DrawLine(pen, detachRect.X + 10, detachRect.Y - 1, detachRect.X + 10, detachRect.Y + 2);
            }

            // 3. Botón de Cerrar (✕)
            Rectangle closeRect = new Rectangle(tabRect.Right - 18, tabRect.Y + (tabRect.Height - 14) / 2, 14, 14);
            Color closeColor = isSelected ? Color.FromArgb(255, 110, 110) : Color.FromArgb(170, 60, 60);

            using (Font closeFont = new Font("Segoe UI", 8.5F, FontStyle.Bold))
            {
                TextRenderer.DrawText(
                    e.Graphics,
                    "✕",
                    closeFont,
                    closeRect,
                    closeColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                );
            }
        }

        // ===== Evento de Clics en Pestañas =====
        private void tbcContenido_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < tbcContenido.TabPages.Count; i++)
            {
                Rectangle tabRect = tbcContenido.GetTabRect(i);

                Rectangle detachRect = new Rectangle(tabRect.Right - 42, tabRect.Y + 2, 20, tabRect.Height - 4);
                Rectangle closeRect = new Rectangle(tabRect.Right - 20, tabRect.Y + 2, 18, tabRect.Height - 4);

                // Ventana Flotante
                if (detachRect.Contains(e.Location))
                {
                    ConvertirPestanaEnVentana(tbcContenido.TabPages[i]);
                    break;
                }

                // Cerrar Pestaña
                if (closeRect.Contains(e.Location))
                {
                    TabPage tabACerrar = tbcContenido.TabPages[i];
                    tbcContenido.TabPages.Remove(tabACerrar);
                    tabACerrar.Dispose();
                    break;
                }
            }
        }

        // ===== Desenganchar a Ventana Flotante =====
        private void ConvertirPestanaEnVentana(TabPage tabPage)
        {
            if (tabPage.Controls.Count == 0) return;

            Control contenido = tabPage.Controls[0];
            string titulo = tabPage.Text;

            tbcContenido.TabPages.Remove(tabPage);

            VentanaFlotanteForm ventana = new VentanaFlotanteForm(titulo, contenido);

            ventana.FormClosed += (s, ev) =>
            {
                Control ctrlDevuelto = ventana.ObtenerContenido();

                TabPage tabRestaurada = new TabPage(titulo)
                {
                    BackColor = colorFondoWorkspace
                };

                ctrlDevuelto.Dock = DockStyle.Fill;
                tabRestaurada.Controls.Add(ctrlDevuelto);

                tbcContenido.TabPages.Add(tabRestaurada);
                tbcContenido.SelectedTab = tabRestaurada;
            };

            ventana.Show();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            var confirmacion = MessageBox.Show(
                "¿Estás seguro que deseas cerrar sesión?",
                "SIBank Enterprise",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacion == DialogResult.Yes)
            {
                Form1 login = new Form1();
                login.FormClosed += (s, ev) => Application.Exit();
                this.Hide();
                login.Show();
            }
        }
    }

    // ===== Formulario Helper de Ventana Flotante =====
    public class VentanaFlotanteForm : Form
    {
        private Control contenidoInterno;

        public VentanaFlotanteForm(string titulo, Control contenido)
        {
            this.Text = "SIBank Enterprise - " + titulo;
            this.Size = new Size(1000, 620);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(244, 247, 252);

            this.contenidoInterno = contenido;
            this.contenidoInterno.BackColor = Color.FromArgb(244, 247, 252);
            this.contenidoInterno.Dock = DockStyle.Fill;
            this.Controls.Add(this.contenidoInterno);
        }

        public Control ObtenerContenido()
        {
            this.Controls.Remove(contenidoInterno);
            return contenidoInterno;
        }
    }
}