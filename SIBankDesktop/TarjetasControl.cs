using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIBankDesktop
{
    public partial class TarjetasControl : UserControl
    {
        private const string API_BASE_URL = "http://127.0.0.1:8080";
        private static readonly HttpClient httpClient = new HttpClient();
        private long idUsuarioActivo;

        private TarjetaVisual tarjetaSeleccionada;

        public TarjetasControl(long idUsuarioActivo)
        {
            InitializeComponent();
            this.idUsuarioActivo = idUsuarioActivo;
            this.Load += async (s, e) => await CargarTarjetas();
        }

        // ===== Trae la lista de tarjetas y dibuja una TarjetaVisual por cada una =====
        private async Task CargarTarjetas()
        {
            try
            {
                flpTarjetas.Controls.Clear();
                tarjetaSeleccionada = null;

                string json = await httpClient.GetStringAsync(API_BASE_URL + "/tarjetas");
                var tarjetas = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(json);

                if (tarjetas.Count == 0)
                {
                    var lblVacio = new Label
                    {
                        Text = "Todavia no hay tarjetas emitidas.\nUsa el boton \"+ Emitir Tarjeta\" para crear la primera.",
                        Font = new Font("Segoe UI", 11F),
                        ForeColor = Color.Gray,
                        AutoSize = true,
                        Margin = new Padding(20)
                    };
                    flpTarjetas.Controls.Add(lblVacio);
                    return;
                }

                foreach (var t in tarjetas)
                {
                    var tarjetaVisual = new TarjetaVisual
                    {
                        IdTarjeta = t["ID_TARJETA"].GetInt64(),
                        NumeroTarjeta = t["NUMERO_TARJETA"].ToString(),
                        Cliente = t["CLIENTE"].ToString(),
                        TipoTarjeta = t["TIPO_TARJETA"].ToString(),
                        Marca = ObtenerValor(t, "MARCA"),
                        NumeroCuenta = t["NUMERO_CUENTA"].ToString(),
                        FechaVencimiento = ObtenerValor(t, "FECHA_VENCIMIENTO"),
                        Estado = t["ESTADO"].ToString(),
                        Margin = new Padding(10)
                    };
                    tarjetaVisual.TarjetaClick += TarjetaVisual_Click;
                    flpTarjetas.Controls.Add(tarjetaVisual);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar la lista de tarjetas.\n" + ex.Message, "SIBank Enterprise",
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

        // ===== Al hacer clic en una tarjeta, la marcamos como seleccionada =====
        private void TarjetaVisual_Click(object sender, EventArgs e)
        {
            if (tarjetaSeleccionada != null) tarjetaSeleccionada.Seleccionada = false;

            tarjetaSeleccionada = (TarjetaVisual)sender;
            tarjetaSeleccionada.Seleccionada = true;
        }

        // ===== Boton: Actualizar =====
        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            await CargarTarjetas();
        }

        // ===== Boton: Emitir Tarjeta =====
        private async void btnEmitir_Click(object sender, EventArgs e)
        {
            try
            {
                string jsonCuentas = await httpClient.GetStringAsync(API_BASE_URL + "/cuentas");
                string jsonTipos = await httpClient.GetStringAsync(API_BASE_URL + "/tarjetas/tipos");

                var cuentas = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(jsonCuentas);
                var tipos = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(jsonTipos);

                using (var formulario = new TarjetaFormulario(cuentas, tipos))
                {
                    if (formulario.ShowDialog() == DialogResult.OK)
                    {
                        await EmitirTarjeta(formulario);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar los datos necesarios: " + ex.Message, "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task EmitirTarjeta(TarjetaFormulario f)
        {
            try
            {
                var body = new
                {
                    idCuenta = f.IdCuenta,
                    idTipoTarjeta = f.IdTipoTarjeta,
                    idUsuarioActor = idUsuarioActivo
                };

                string json = JsonSerializer.Serialize(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(API_BASE_URL + "/tarjetas/emitir", content);
                string responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var resultado = JsonSerializer.Deserialize<RespuestaApi>(responseBody, options);

                if (resultado.Estado == "OK")
                {
                    MessageBox.Show("Tarjeta emitida correctamente.", "SIBank Enterprise",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarTarjetas();
                }
                else
                {
                    MessageBox.Show(resultado.Mensaje ?? "No se pudo emitir la tarjeta.", "SIBank Enterprise",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al emitir la tarjeta: " + ex.Message, "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== Boton: Bloquear seleccionada =====
        private async void btnBloquear_Click(object sender, EventArgs e)
        {
            if (tarjetaSeleccionada == null)
            {
                MessageBox.Show("Selecciona una tarjeta primero (haz clic sobre ella).", "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (tarjetaSeleccionada.Estado != "A")
            {
                MessageBox.Show("Esta tarjeta ya no esta activa.", "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacion = MessageBox.Show(
                "Deseas bloquear la tarjeta terminada en " + tarjetaSeleccionada.NumeroTarjeta.Substring(tarjetaSeleccionada.NumeroTarjeta.Length - 4) + "?",
                "SIBank Enterprise",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacion != DialogResult.Yes) return;

            try
            {
                var body = new { idTarjeta = tarjetaSeleccionada.IdTarjeta, idUsuarioActor = idUsuarioActivo };
                string json = JsonSerializer.Serialize(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(API_BASE_URL + "/tarjetas/bloquear", content);
                string responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var resultado = JsonSerializer.Deserialize<RespuestaApi>(responseBody, options);

                if (resultado.Estado == "OK")
                {
                    MessageBox.Show("Tarjeta bloqueada correctamente.", "SIBank Enterprise",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarTarjetas();
                }
                else
                {
                    MessageBox.Show(resultado.Mensaje ?? "No se pudo bloquear la tarjeta.", "SIBank Enterprise",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // =====================================================================
    // Control personalizado que se dibuja como una tarjeta bancaria real:
    // degradado de color segun la marca, esquinas redondeadas, chip dorado,
    // numero de tarjeta enmascarado, nombre del cliente y vencimiento.
    // =====================================================================
    public class TarjetaVisual : Panel
    {
        public long IdTarjeta { get; set; }
        public string NumeroTarjeta { get; set; } = "";
        public string Cliente { get; set; } = "";
        public string TipoTarjeta { get; set; } = "";
        public string Marca { get; set; } = "";
        public string NumeroCuenta { get; set; } = "";
        public string FechaVencimiento { get; set; } = "";
        public string Estado { get; set; } = "A";

        private bool seleccionada;
        public bool Seleccionada
        {
            get => seleccionada;
            set { seleccionada = value; Invalidate(); }
        }

        public event EventHandler TarjetaClick;

        public TarjetaVisual()
        {
            this.Size = new Size(320, 190);
            this.Cursor = Cursors.Hand;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);
            this.Click += (s, e) => TarjetaClick?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(2, 2, this.Width - 4, this.Height - 4);
            int radio = 18;
            using (GraphicsPath path = RutaRedondeada(rect, radio))
            {
                // ===== Colores segun si esta bloqueada o segun la marca =====
                Color colorInicio, colorFin;
                if (Estado != "A")
                {
                    colorInicio = Color.FromArgb(120, 120, 120);
                    colorFin = Color.FromArgb(70, 70, 70);
                }
                else if (Marca.ToUpper().Contains("VISA"))
                {
                    colorInicio = Color.FromArgb(30, 60, 130);
                    colorFin = Color.FromArgb(10, 20, 60);
                }
                else if (Marca.ToUpper().Contains("MASTER"))
                {
                    colorInicio = Color.FromArgb(150, 40, 30);
                    colorFin = Color.FromArgb(60, 15, 10);
                }
                else
                {
                    colorInicio = Color.FromArgb(21, 34, 56);
                    colorFin = Color.FromArgb(50, 70, 100);
                }

                using (var brush = new LinearGradientBrush(rect, colorInicio, colorFin, 45F))
                {
                    g.FillPath(brush, path);
                }

                // ===== Borde de seleccion =====
                if (Seleccionada)
                {
                    using (var penSel = new Pen(Color.FromArgb(255, 200, 0), 3))
                    {
                        g.DrawPath(penSel, path);
                    }
                }
            }

            // ===== Chip dorado =====
            using (var brushChip = new LinearGradientBrush(
                new Rectangle(24, 30, 42, 32), Color.FromArgb(230, 200, 120), Color.FromArgb(180, 140, 60), 45F))
            {
                g.FillRectangle(brushChip, 24, 30, 42, 32);
            }

            // ===== Marca (VISA / MASTERCARD / etc) arriba a la derecha =====
            using (var fontMarca = new Font("Segoe UI", 11F, FontStyle.Bold))
            {
                var sizeMarca = g.MeasureString(Marca.ToUpper(), fontMarca);
                g.DrawString(Marca.ToUpper(), fontMarca, Brushes.White,
                    this.Width - sizeMarca.Width - 20, 20);
            }

            // ===== Tipo de tarjeta (Debito/Credito) =====
            using (var fontTipo = new Font("Segoe UI", 8F))
            {
                g.DrawString(TipoTarjeta.ToUpper(), fontTipo, new SolidBrush(Color.FromArgb(210, 255, 255, 255)), 24, 14);
            }

            // ===== Numero de tarjeta enmascarado =====
            string numeroMostrar = EnmascararNumero(NumeroTarjeta);
            using (var fontNumero = new Font("Consolas", 15F, FontStyle.Bold))
            {
                g.DrawString(numeroMostrar, fontNumero, Brushes.White, 24, 90);
            }

            // ===== Nombre del cliente =====
            using (var fontNombre = new Font("Segoe UI", 10F, FontStyle.Bold))
            {
                g.DrawString(Cliente.ToUpper(), fontNombre, Brushes.White, 24, 130);
            }

            // ===== Vencimiento =====
            using (var fontVenc = new Font("Segoe UI", 8F))
            {
                string venc = FormatearFecha(FechaVencimiento);
                g.DrawString("VENCE " + venc, fontVenc, new SolidBrush(Color.FromArgb(210, 255, 255, 255)), 24, 155);
            }

            // ===== Badge de estado (si no esta activa) =====
            if (Estado != "A")
            {
                string textoEstado = Estado == "B" ? "BLOQUEADA" : "VENCIDA";
                using (var fontEstado = new Font("Segoe UI", 9F, FontStyle.Bold))
                {
                    var sizeEstado = g.MeasureString(textoEstado, fontEstado);
                    var rectBadge = new RectangleF(this.Width - sizeEstado.Width - 34, 150, sizeEstado.Width + 14, sizeEstado.Height + 4);
                    using (var brushBadge = new SolidBrush(Color.FromArgb(180, 200, 40, 40)))
                    {
                        g.FillRectangle(brushBadge, rectBadge);
                    }
                    g.DrawString(textoEstado, fontEstado, Brushes.White, rectBadge.X + 7, rectBadge.Y + 2);
                }
            }
        }

        private string EnmascararNumero(string numero)
        {
            if (string.IsNullOrEmpty(numero) || numero.Length < 4) return "**** **** **** ****";
            string ultimos4 = numero.Substring(numero.Length - 4);
            return "**** **** **** " + ultimos4;
        }

        private string FormatearFecha(string fecha)
        {
            if (DateTime.TryParse(fecha, out DateTime dt))
                return dt.ToString("MM/yy");
            return "--/--";
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
    }
}