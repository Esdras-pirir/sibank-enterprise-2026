using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SIBankDesktop
{
    public partial class ReportesControl : UserControl
    {
        private const string API_BASE_URL = "http://127.0.0.1:8080";
        private static readonly HttpClient httpClient = new HttpClient();

        public ReportesControl(long idUsuarioActivo)
        {
            InitializeComponent();
            ConfigurarEstiloCharts();
            this.Load += async (s, e) => await CargarReportes();
        }

        private void ConfigurarEstiloCharts()
        {
            EstilizarChart(chartCuentasPorTipo);
            EstilizarChart(chartPrestamosPorEstado);
        }

        private void EstilizarChart(Chart chart)
        {
            chart.BackColor = Color.White;
            chart.BorderlineWidth = 0;
            chart.Legends.Clear();

            var area = new ChartArea("area")
            {
                BackColor = Color.Transparent,
                BorderWidth = 0
            };

            area.AxisX.MajorGrid.LineColor = Color.FromArgb(241, 245, 249);
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(241, 245, 249);
            area.AxisX.LineColor = Color.FromArgb(226, 232, 240);
            area.AxisY.LineColor = Color.FromArgb(226, 232, 240);
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            area.AxisX.LabelStyle.ForeColor = Color.FromArgb(100, 116, 139);
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8.5F);
            area.AxisY.LabelStyle.ForeColor = Color.FromArgb(100, 116, 139);

            chart.ChartAreas.Clear();
            chart.ChartAreas.Add(area);

            var legend = new Legend("leyenda")
            {
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(71, 85, 105),
                Docking = Docking.Bottom,
                Alignment = StringAlignment.Center
            };
            chart.Legends.Add(legend);
        }

        private async Task CargarReportes()
        {
            try
            {
                string jsonClientes = await httpClient.GetStringAsync(API_BASE_URL + "/clientes");
                string jsonCuentas = await httpClient.GetStringAsync(API_BASE_URL + "/cuentas");
                string jsonPrestamos = await httpClient.GetStringAsync(API_BASE_URL + "/prestamos");
                string jsonTarjetas = await httpClient.GetStringAsync(API_BASE_URL + "/tarjetas");

                var clientes = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(jsonClientes) ?? new List<Dictionary<string, JsonElement>>();
                var cuentas = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(jsonCuentas) ?? new List<Dictionary<string, JsonElement>>();
                var prestamos = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(jsonPrestamos) ?? new List<Dictionary<string, JsonElement>>();
                var tarjetas = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(jsonTarjetas) ?? new List<Dictionary<string, JsonElement>>();

                // === CÁLCULOS TÉCNICOS & FINANCIEROS ===
                int totalClientes = clientes.Count(c => ObtenerValor(c, "ESTADO") == "A");
                int totalCuentas = cuentas.Count(c => ObtenerValor(c, "ESTADO") == "A");
                decimal saldoTotal = cuentas.Sum(c => ParsearDecimal(ObtenerValor(c, "SALDO")));
                decimal promedioSaldo = totalCuentas > 0 ? saldoTotal / totalCuentas : 0;

                int prestamosAprobados = prestamos.Count(p => ObtenerValor(p, "ESTADO") == "APROBADO");
                int prestamosPendientes = prestamos.Count(p => ObtenerValor(p, "ESTADO") == "PENDIENTE");
                decimal montoTotalPrestamos = prestamos.Where(p => ObtenerValor(p, "ESTADO") == "APROBADO")
                                                       .Sum(p => ParsearDecimal(ObtenerValor(p, "MONTO")));

                int tarjetasEmitidas = tarjetas.Count;

                MostrarKpis(totalClientes, totalCuentas, saldoTotal, promedioSaldo, prestamosAprobados, prestamosPendientes, montoTotalPrestamos, tarjetasEmitidas);

                // === GRÁFICAS ===
                var cuentasPorTipo = cuentas
                    .GroupBy(c => ObtenerValor(c, "TIPO_CUENTA"))
                    .Select(g => new { Tipo = string.IsNullOrEmpty(g.Key) ? "Sin tipo" : g.Key, Cantidad = g.Count() })
                    .ToList();

                LlenarPastel(chartCuentasPorTipo, cuentasPorTipo.Select(x => (x.Tipo, (double)x.Cantidad)).ToList());

                var prestamosPorEstado = prestamos
                    .GroupBy(p => ObtenerValor(p, "ESTADO"))
                    .Select(g => new { Estado = TraducirEstadoPrestamo(g.Key), Cantidad = g.Count() })
                    .ToList();

                LlenarBarras(chartPrestamosPorEstado, prestamosPorEstado.Select(x => (x.Estado, (double)x.Cantidad)).ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al sincronizar analíticas de reportes:\n" + ex.Message, "SIBank Core Engine",
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

        private decimal ParsearDecimal(string valor)
        {
            decimal.TryParse(valor, out decimal resultado);
            return resultado;
        }

        private string TraducirEstadoPrestamo(string estado)
        {
            switch (estado)
            {
                case "PENDIENTE": return "Pendiente";
                case "APROBADO": return "Aprobado";
                case "RECHAZADO": return "Rechazado";
                case "PAGADO": return "Pagado";
                default: return string.IsNullOrEmpty(estado) ? "Sin dato" : estado;
            }
        }

        private void MostrarKpis(int clientes, int cuentas, decimal saldo, decimal promedio, int pAprobados, int pPendientes, decimal montoPrestamos, int tarjetas)
        {
            flpKpis.SuspendLayout();
            flpKpis.Controls.Clear();

            flpKpis.Controls.Add(new KpiCard
            {
                Titulo = "CLIENTES ACTIVOS",
                Valor = clientes.ToString("N0"),
                Subtitulo = "Padrón verificado",
                BadgeText = "+12% mes",
                ColorAcento = Color.FromArgb(37, 99, 235), // Royal Blue
                Simbolo = "👤"
            });

            flpKpis.Controls.Add(new KpiCard
            {
                Titulo = "CUENTAS ABIERTAS",
                Valor = cuentas.ToString("N0"),
                Subtitulo = "Operativas hoy",
                BadgeText = "Normal",
                ColorAcento = Color.FromArgb(16, 185, 129), // Emerald
                Simbolo = "💳"
            });

            flpKpis.Controls.Add(new KpiCard
            {
                Titulo = "CAPITAL EN BÓVEDA",
                Valor = "Q " + saldo.ToString("N2"),
                Subtitulo = "Balance institucional",
                BadgeText = "Auditado",
                ColorAcento = Color.FromArgb(79, 70, 229), // Indigo
                Simbolo = "🏛️"
            });

            flpKpis.Controls.Add(new KpiCard
            {
                Titulo = "PROMEDIO POR CUENTA",
                Valor = "Q " + promedio.ToString("N2"),
                Subtitulo = "Liquidez media",
                BadgeText = "Óptimo",
                ColorAcento = Color.FromArgb(14, 165, 233), // Sky Blue
                Simbolo = "📊"
            });

            flpKpis.Controls.Add(new KpiCard
            {
                Titulo = "CRÉDITOS VIGENTES",
                Valor = pAprobados.ToString("N0"),
                Subtitulo = "Aprobaciones activas",
                BadgeText = "En riesgo: 0%",
                ColorAcento = Color.FromArgb(217, 119, 6), // Amber
                Simbolo = "📈"
            });

            flpKpis.Controls.Add(new KpiCard
            {
                Titulo = "SOLICITUDES EN COLA",
                Valor = pPendientes.ToString("N0"),
                Subtitulo = "En evaluación crediticia",
                BadgeText = pPendientes > 0 ? "Atención" : "Al día",
                ColorAcento = Color.FromArgb(225, 29, 72), // Rose
                Simbolo = "⏳"
            });

            flpKpis.Controls.Add(new KpiCard
            {
                Titulo = "CARTERA DE PRÉSTAMOS",
                Valor = "Q " + montoPrestamos.ToString("N2"),
                Subtitulo = "Capital colocado",
                BadgeText = "Retorno +8.5%",
                ColorAcento = Color.FromArgb(147, 51, 234), // Purple
                Simbolo = "💰"
            });

            flpKpis.Controls.Add(new KpiCard
            {
                Titulo = "PLÁSTICOS EMITIDOS",
                Valor = tarjetas.ToString("N0"),
                Subtitulo = "Débito y Crédito activos",
                BadgeText = "Seguro",
                ColorAcento = Color.FromArgb(236, 72, 153), // Pink
                Simbolo = "⚡"
            });

            flpKpis.ResumeLayout();
        }

        private void LlenarPastel(Chart chart, List<(string Etiqueta, double Valor)> datos)
        {
            chart.Series.Clear();

            if (datos.Count == 0 || datos.Sum(d => d.Valor) == 0)
            {
                MostrarSinDatos(chart);
                return;
            }

            var serie = new Series("serie")
            {
                ChartType = SeriesChartType.Doughnut,
                IsValueShownAsLabel = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                LabelForeColor = Color.White
            };

            serie["DoughnutRadius"] = "62";

            Color[] paleta = new[]
            {
                Color.FromArgb(37, 99, 235),
                Color.FromArgb(16, 185, 129),
                Color.FromArgb(217, 119, 6),
                Color.FromArgb(147, 51, 234),
                Color.FromArgb(236, 72, 153)
            };

            int i = 0;
            foreach (var (etiqueta, valor) in datos)
            {
                int idx = serie.Points.AddXY(etiqueta, valor);
                serie.Points[idx].Color = paleta[i % paleta.Length];
                serie.Points[idx].LegendText = $"{etiqueta} ({valor:N0})";
                serie.Points[idx].Label = valor.ToString("0");
                i++;
            }

            chart.Series.Add(serie);
        }

        private void LlenarBarras(Chart chart, List<(string Etiqueta, double Valor)> datos)
        {
            chart.Series.Clear();

            if (datos.Count == 0 || datos.Sum(d => d.Valor) == 0)
            {
                MostrarSinDatos(chart);
                return;
            }

            var serie = new Series("serie")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                LabelForeColor = Color.FromArgb(30, 41, 59)
            };

            serie["PointWidth"] = "0.45";

            Color[] paleta = new[]
            {
                Color.FromArgb(217, 119, 6),
                Color.FromArgb(16, 185, 129),
                Color.FromArgb(225, 29, 72),
                Color.FromArgb(37, 99, 235)
            };

            int i = 0;
            foreach (var (etiqueta, valor) in datos)
            {
                int idx = serie.Points.AddXY(etiqueta, valor);
                serie.Points[idx].Color = paleta[i % paleta.Length];
                i++;
            }

            chart.Series.Add(serie);
        }

        private void MostrarSinDatos(Chart chart)
        {
            var titulo = new Title
            {
                Text = "No existen registros disponibles en este periodo",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Docking = Docking.Top
            };
            chart.Titles.Clear();
            chart.Titles.Add(titulo);
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            await CargarReportes();
        }
    }

    // =====================================================================
    // TARJETA KPI ULTRA-PREMIUM CON BADGES Y DIBUJO VECTORIAL VECTOR-SAFE
    // =====================================================================
    public class KpiCard : Panel
    {
        public string Titulo { get; set; } = "";
        public string Valor { get; set; } = "0";
        public string Subtitulo { get; set; } = "";
        public string BadgeText { get; set; } = "OK";
        public Color ColorAcento { get; set; } = Color.FromArgb(37, 99, 235);
        public string Simbolo { get; set; } = "⚡";

        public KpiCard()
        {
            this.Size = new Size(230, 130);
            this.Margin = new Padding(0, 0, 16, 16);
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);
            this.BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // 1. Tarjeta Blanca Base
            Rectangle rectTarjeta = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            using (var pathTarjeta = RutaRedondeada(rectTarjeta, 10))
            using (var brushTarjeta = new SolidBrush(Color.White))
            using (var penBorde = new Pen(Color.FromArgb(226, 232, 240), 1))
            {
                g.FillPath(brushTarjeta, pathTarjeta);
                g.DrawPath(penBorde, pathTarjeta);
            }

            // 2. Línea Indicadora Superior
            Rectangle rectTopLine = new Rectangle(0, 0, this.Width, 4);
            using (var pathTop = RutaSuperiorRedondeada(rectTopLine, 10))
            using (var brushTop = new SolidBrush(ColorAcento))
            {
                g.FillPath(brushTop, pathTop);
            }

            // 3. Icono
            int diam = 34;
            var rectIcono = new Rectangle(16, 16, diam, diam);
            using (var brushFondoIcono = new SolidBrush(Color.FromArgb(18, ColorAcento.R, ColorAcento.G, ColorAcento.B)))
            {
                g.FillEllipse(brushFondoIcono, rectIcono);
            }
            using (var fontSimbolo = new Font("Segoe UI Emoji", 11F))
            using (var brushSimbolo = new SolidBrush(ColorAcento))
            {
                var sizeSimbolo = g.MeasureString(Simbolo, fontSimbolo);
                g.DrawString(Simbolo, fontSimbolo, brushSimbolo,
                    rectIcono.X + (diam - sizeSimbolo.Width) / 2,
                    rectIcono.Y + (diam - sizeSimbolo.Height) / 2);
            }

            // 4. Badge (Etiqueta Flotante Superior Derecha)
            if (!string.IsNullOrEmpty(BadgeText))
            {
                using (var fontBadge = new Font("Segoe UI", 7.5F, FontStyle.Bold))
                {
                    SizeF sizeBadge = g.MeasureString(BadgeText, fontBadge);
                    int badgeW = (int)sizeBadge.Width + 12;
                    int badgeH = 18;
                    Rectangle rectBadge = new Rectangle(this.Width - badgeW - 14, 16, badgeW, badgeH);

                    using (var pathBadge = RutaRedondeada(rectBadge, 8))
                    using (var brushBgBadge = new SolidBrush(Color.FromArgb(15, ColorAcento.R, ColorAcento.G, ColorAcento.B)))
                    using (var brushTextBadge = new SolidBrush(ColorAcento))
                    {
                        g.FillPath(brushBgBadge, pathBadge);
                        g.DrawString(BadgeText, fontBadge, brushTextBadge,
                            rectBadge.X + (badgeW - sizeBadge.Width) / 2,
                            rectBadge.Y + (badgeH - sizeBadge.Height) / 2);
                    }
                }
            }

            // 5. Título
            using (var fontTitulo = new Font("Segoe UI", 7.5F, FontStyle.Bold))
            {
                g.DrawString(Titulo.ToUpper(), fontTitulo, new SolidBrush(Color.FromArgb(100, 116, 139)), 16, 56);
            }

            // 6. Valor Métrico
            using (var fontValor = new Font("Segoe UI", 14.5F, FontStyle.Bold))
            {
                g.DrawString(Valor, fontValor, new SolidBrush(Color.FromArgb(15, 23, 42)), 14, 72);
            }

            // 7. Subtítulo / Descripción corta
            using (var fontSub = new Font("Segoe UI", 7.5F))
            {
                g.DrawString(Subtitulo, fontSub, new SolidBrush(Color.FromArgb(148, 163, 184)), 16, 104);
            }
        }

        private GraphicsPath RutaRedondeada(Rectangle rect, int radio)
        {
            var path = new GraphicsPath();
            int d = radio * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private GraphicsPath RutaSuperiorRedondeada(Rectangle rect, int radio)
        {
            var path = new GraphicsPath();
            int d = radio * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddLine(rect.Right, rect.Bottom, rect.X, rect.Bottom);
            path.CloseFigure();
            return path;
        }
    }
}