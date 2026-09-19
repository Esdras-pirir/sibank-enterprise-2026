using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIBankDesktop
{
    public partial class BitacoraControl : UserControl
    {
        private const string API_BASE_URL = "http://127.0.0.1:8080";
        private static readonly HttpClient httpClient = new HttpClient();

        private List<RegistroBitacora> registros = new List<RegistroBitacora>();

        private const string PLACEHOLDER_BUSCAR = "Buscar por usuario, accion o modulo...";

        public BitacoraControl(long idUsuarioActivo)
        {
            InitializeComponent();
            ConfigurarPlaceholderBuscar();
            this.Load += async (s, e) => await CargarBitacora();
        }

        private void ConfigurarPlaceholderBuscar()
        {
            txtBuscar.Text = PLACEHOLDER_BUSCAR;
            txtBuscar.ForeColor = Color.Gray;

            txtBuscar.Enter += (s, e) =>
            {
                if (txtBuscar.Text == PLACEHOLDER_BUSCAR)
                {
                    txtBuscar.Text = "";
                    txtBuscar.ForeColor = Color.Black;
                }
            };

            txtBuscar.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    txtBuscar.Text = PLACEHOLDER_BUSCAR;
                    txtBuscar.ForeColor = Color.Gray;
                }
            };
        }

        private async Task CargarBitacora()
        {
            try
            {
                string json = await httpClient.GetStringAsync(API_BASE_URL + "/bitacora");
                var datos = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(json);

                registros.Clear();
                foreach (var d in datos)
                {
                    registros.Add(new RegistroBitacora
                    {
                        Usuario = ObtenerValor(d, "USERNAME"),
                        Accion = ObtenerValor(d, "ACCION"),
                        Modulo = ObtenerValor(d, "MODULO"),
                        Detalle = ObtenerValor(d, "DETALLE"),
                        Fecha = ParsearFecha(ObtenerValor(d, "FECHA"))
                    });
                }

                MostrarRegistros(registros);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar la bitacora.\n" + ex.Message, "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ObtenerValor(Dictionary<string, JsonElement> fila, string clave)
        {
            if (!fila.ContainsKey(clave)) return "";
            var elemento = fila[clave];
            if (elemento.ValueKind == JsonValueKind.Null) return "";
            return elemento.ToString();
        }

        private DateTime ParsearFecha(string texto)
        {
            if (DateTime.TryParse(texto, out DateTime dt)) return dt;
            return DateTime.MinValue;
        }

        // ===== Dibuja la lista de tarjetas en el panel =====
        private void MostrarRegistros(List<RegistroBitacora> lista)
        {
            flpTimeline.Controls.Clear();

            if (lista.Count == 0)
            {
                var lblVacio = new Label
                {
                    Text = "No se encontraron registros.",
                    Font = new Font("Segoe UI", 11F),
                    ForeColor = Color.Gray,
                    AutoSize = true,
                    Margin = new Padding(10)
                };
                flpTimeline.Controls.Add(lblVacio);
                return;
            }

            foreach (var r in lista)
            {
                var item = new BitacoraItemPanel
                {
                    Usuario = string.IsNullOrEmpty(r.Usuario) ? "Sistema" : r.Usuario,
                    Accion = r.Accion,
                    Modulo = r.Modulo,
                    Detalle = r.Detalle,
                    Fecha = r.Fecha,
                    Width = flpTimeline.ClientSize.Width - 50,
                    Margin = new Padding(0, 0, 0, 10)
                };
                flpTimeline.Controls.Add(item);
            }
        }

        // ===== Buscador: filtra por usuario, accion o modulo =====
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string texto = txtBuscar.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(texto) || texto == PLACEHOLDER_BUSCAR.ToLower())
            {
                MostrarRegistros(registros);
                return;
            }

            var filtrados = registros.Where(r =>
                (r.Usuario ?? "").ToLower().Contains(texto) ||
                (r.Accion ?? "").ToLower().Contains(texto) ||
                (r.Modulo ?? "").ToLower().Contains(texto) ||
                (r.Detalle ?? "").ToLower().Contains(texto)
            ).ToList();

            MostrarRegistros(filtrados);
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            await CargarBitacora();
        }

        private class RegistroBitacora
        {
            public string Usuario { get; set; }
            public string Accion { get; set; }
            public string Modulo { get; set; }
            public string Detalle { get; set; }
            public DateTime Fecha { get; set; }
        }
    }

    // =====================================================================
    // Tarjeta visual de un registro de bitacora: icono circular de color
    // segun el tipo de accion, texto en negrita, pill de modulo, y tiempo
    // relativo ("Hace 5 min", "Hace 2 h", etc). Se dibuja con esquinas
    // redondeadas y una sombra sutil, como una tarjeta moderna tipo "card".
    // =====================================================================
    public class BitacoraItemPanel : Panel
    {
        public string Usuario { get; set; } = "";
        public string Accion { get; set; } = "";
        public string Modulo { get; set; } = "";
        public string Detalle { get; set; } = "";
        public DateTime Fecha { get; set; }

        public BitacoraItemPanel()
        {
            this.Height = 86;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);
            this.BackColor = Color.FromArgb(240, 242, 245);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // ===== Sombra sutil (rectangulo gris desplazado detras de la tarjeta) =====
            Rectangle rectSombra = new Rectangle(4, 4, this.Width - 8, this.Height - 8);
            using (var pathSombra = RutaRedondeada(rectSombra, 12))
            using (var brushSombra = new SolidBrush(Color.FromArgb(18, 0, 0, 0)))
            {
                g.FillPath(brushSombra, pathSombra);
            }

            // ===== Tarjeta blanca =====
            Rectangle rectTarjeta = new Rectangle(0, 0, this.Width - 8, this.Height - 10);
            using (var pathTarjeta = RutaRedondeada(rectTarjeta, 12))
            using (var brushTarjeta = new SolidBrush(Color.White))
            {
                g.FillPath(brushTarjeta, pathTarjeta);
            }

            // ===== Barra de color a la izquierda segun el tipo de accion =====
            Color colorAccento = ColorPorAccion(Accion);
            using (var pathBarra = RutaRedondeadaIzquierda(new Rectangle(0, 0, 6, rectTarjeta.Height), 12))
            using (var brushBarra = new SolidBrush(colorAccento))
            {
                g.FillPath(brushBarra, pathBarra);
            }

            // ===== Icono circular =====
            int iconoDiam = 44;
            var rectIcono = new Rectangle(24, (rectTarjeta.Height - iconoDiam) / 2, iconoDiam, iconoDiam);
            using (var brushIconoFondo = new SolidBrush(Color.FromArgb(35, colorAccento.R, colorAccento.G, colorAccento.B)))
            {
                g.FillEllipse(brushIconoFondo, rectIcono);
            }
            string simbolo = SimboloPorAccion(Accion);
            using (var fontIcono = new Font("Segoe UI", 16F, FontStyle.Bold))
            using (var brushIconoTexto = new SolidBrush(colorAccento))
            {
                var sizeSimbolo = g.MeasureString(simbolo, fontIcono);
                g.DrawString(simbolo, fontIcono, brushIconoTexto,
                    rectIcono.X + (iconoDiam - sizeSimbolo.Width) / 2,
                    rectIcono.Y + (iconoDiam - sizeSimbolo.Height) / 2);
            }

            // ===== Texto: accion + usuario =====
            int xTexto = rectIcono.Right + 18;
            using (var fontAccion = new Font("Segoe UI", 11F, FontStyle.Bold))
            {
                g.DrawString(FormatearAccion(Accion), fontAccion, new SolidBrush(Color.FromArgb(30, 30, 30)), xTexto, 14);
            }

            using (var fontUsuario = new Font("Segoe UI", 9F))
            {
                g.DrawString("por " + Usuario, fontUsuario, new SolidBrush(Color.Gray), xTexto, 36);
            }

            // ===== Pill de modulo =====
            if (!string.IsNullOrEmpty(Modulo))
            {
                using (var fontPill = new Font("Segoe UI", 8F, FontStyle.Bold))
                {
                    var sizePill = g.MeasureString(Modulo.ToUpper(), fontPill);
                    var rectPill = new RectangleF(xTexto, 56, sizePill.Width + 16, sizePill.Height + 6);
                    using (var pathPill = RutaRedondeada(Rectangle.Round(rectPill), (int)(rectPill.Height / 2)))
                    using (var brushPill = new SolidBrush(Color.FromArgb(230, 232, 236)))
                    {
                        g.FillPath(brushPill, pathPill);
                    }
                    g.DrawString(Modulo.ToUpper(), fontPill, new SolidBrush(Color.FromArgb(80, 80, 80)),
                        rectPill.X + 8, rectPill.Y + 3);
                }
            }

            // ===== Tiempo relativo, alineado a la derecha =====
            string tiempoTexto = TiempoRelativo(Fecha);
            using (var fontTiempo = new Font("Segoe UI", 8.5F))
            {
                var sizeTiempo = g.MeasureString(tiempoTexto, fontTiempo);
                g.DrawString(tiempoTexto, fontTiempo, new SolidBrush(Color.Gray),
                    rectTarjeta.Width - sizeTiempo.Width - 20, 14);
            }
        }

        private string FormatearAccion(string accion)
        {
            if (string.IsNullOrEmpty(accion)) return "Accion desconocida";
            string texto = accion.Replace("_", " ").ToLower();
            return char.ToUpper(texto[0]) + texto.Substring(1);
        }

        private Color ColorPorAccion(string accion)
        {
            if (string.IsNullOrEmpty(accion)) return Color.Gray;
            string a = accion.ToUpper();

            if (a.Contains("LOGIN")) return Color.FromArgb(50, 120, 220);
            if (a.Contains("DEPOSITAR") || a.Contains("DEPOSITO")) return Color.FromArgb(30, 140, 70);
            if (a.Contains("RETIR")) return Color.FromArgb(200, 120, 20);
            if (a.Contains("TRANSFER")) return Color.FromArgb(120, 70, 200);
            if (a.Contains("PRESTAMO")) return Color.FromArgb(20, 130, 140);
            if (a.Contains("TARJETA")) return Color.FromArgb(180, 40, 90);
            if (a.Contains("ELIMINAR") || a.Contains("BLOQUE") || a.Contains("RECHAZ")) return Color.FromArgb(190, 40, 40);
            if (a.Contains("CLIENTE") || a.Contains("REGISTRAR")) return Color.FromArgb(21, 34, 56);
            return Color.FromArgb(100, 100, 100);
        }

        private string SimboloPorAccion(string accion)
        {
            if (string.IsNullOrEmpty(accion)) return "•";
            string a = accion.ToUpper();

            if (a.Contains("LOGIN")) return "→";
            if (a.Contains("DEPOSITAR") || a.Contains("DEPOSITO")) return "+";
            if (a.Contains("RETIR")) return "−";
            if (a.Contains("TRANSFER")) return "⇄";
            if (a.Contains("PRESTAMO")) return "$";
            if (a.Contains("TARJETA")) return "▭";
            if (a.Contains("ELIMINAR") || a.Contains("BLOQUE") || a.Contains("RECHAZ")) return "✕";
            if (a.Contains("CLIENTE") || a.Contains("REGISTRAR")) return "✓";
            return "•";
        }

        private string TiempoRelativo(DateTime fecha)
        {
            if (fecha == DateTime.MinValue) return "";

            var diferencia = DateTime.Now - fecha;

            if (diferencia.TotalMinutes < 1) return "Hace un momento";
            if (diferencia.TotalMinutes < 60) return "Hace " + (int)diferencia.TotalMinutes + " min";
            if (diferencia.TotalHours < 24) return "Hace " + (int)diferencia.TotalHours + " h";
            if (diferencia.TotalDays < 7) return "Hace " + (int)diferencia.TotalDays + " dias";
            return fecha.ToString("dd/MM/yyyy HH:mm");
        }

        private GraphicsPath RutaRedondeada(Rectangle rect, int radio)
        {
            var path = new GraphicsPath();
            int d = radio * 2;
            if (d > rect.Height) d = rect.Height;
            if (d > rect.Width) d = rect.Width;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        // Redondea solo las esquinas izquierdas (para la barra de color lateral)
        private GraphicsPath RutaRedondeadaIzquierda(Rectangle rect, int radio)
        {
            var path = new GraphicsPath();
            int d = radio * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddLine(rect.X + radio, rect.Y, rect.Right, rect.Y);
            path.AddLine(rect.Right, rect.Y, rect.Right, rect.Bottom);
            path.AddLine(rect.Right, rect.Bottom, rect.X + radio, rect.Bottom);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}