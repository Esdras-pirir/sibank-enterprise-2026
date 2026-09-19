using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIBankDesktop
{
    public partial class ClientesControl : UserControl
    {
        private const string API_BASE_URL = "http://127.0.0.1:8080";
        private static readonly HttpClient httpClient = new HttpClient();
        private long idUsuarioActivo;

        public ClientesControl(long idUsuarioActivo)
        {
            InitializeComponent();
            this.idUsuarioActivo = idUsuarioActivo;

            // Cuando el control termina de cargarse, traemos la lista de clientes
            this.Load += async (s, e) => await CargarClientes();
        }

        // ===== Trae la lista de clientes desde la API y la muestra en la tabla =====
        private async Task CargarClientes()
        {
            try
            {
                string json = await httpClient.GetStringAsync(API_BASE_URL + "/clientes");
                var clientes = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(json);

                DataTable tabla = new DataTable();
                tabla.Columns.Add("ID", typeof(string));
                tabla.Columns.Add("Nombres", typeof(string));
                tabla.Columns.Add("Apellidos", typeof(string));
                tabla.Columns.Add("DPI", typeof(string));
                tabla.Columns.Add("Telefono", typeof(string));
                tabla.Columns.Add("Correo", typeof(string));
                tabla.Columns.Add("Estado", typeof(string));

                foreach (var c in clientes)
                {
                    tabla.Rows.Add(
                        ObtenerValor(c, "ID_CLIENTE"),
                        ObtenerValor(c, "NOMBRES"),
                        ObtenerValor(c, "APELLIDOS"),
                        ObtenerValor(c, "DPI"),
                        ObtenerValor(c, "TELEFONO"),
                        ObtenerValor(c, "CORREO"),
                        TraducirEstado(ObtenerValor(c, "ESTADO"))
                    );
                }

                dgvClientes.DataSource = tabla;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo cargar la lista de clientes.\n" + ex.Message,
                    "SIBank Enterprise",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // Convierte el JsonElement de un campo a texto, manejando nulos
        private string ObtenerValor(Dictionary<string, JsonElement> fila, string clave)
        {
            if (!fila.ContainsKey(clave)) return "";
            var elemento = fila[clave];
            if (elemento.ValueKind == JsonValueKind.Null) return "";
            return elemento.ToString();
        }

        private string TraducirEstado(string estado)
        {
            switch (estado)
            {
                case "A": return "Activo";
                case "I": return "Inactivo";
                case "B": return "Bloqueado";
                default: return estado;
            }
        }

        // ===== Boton: Actualizar lista =====
        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            await CargarClientes();
        }

        // ===== Boton: Nuevo Cliente =====
        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            using (var formulario = new ClienteFormulario())
            {
                if (formulario.ShowDialog() == DialogResult.OK)
                {
                    await RegistrarCliente(formulario);
                }
            }
        }

        private async Task RegistrarCliente(ClienteFormulario f)
        {
            try
            {
                var body = new
                {
                    nombres = f.Nombres,
                    apellidos = f.Apellidos,
                    dpi = f.Dpi,
                    nit = f.Nit,
                    fechaNacimiento = f.FechaNacimiento,
                    telefono = f.Telefono,
                    correo = f.Correo,
                    idUsuarioActor = idUsuarioActivo
                };

                string json = JsonSerializer.Serialize(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(API_BASE_URL + "/clientes", content);
                string responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var resultado = JsonSerializer.Deserialize<RespuestaApi>(responseBody, options);

                if (resultado.Estado == "OK")
                {
                    MessageBox.Show("Cliente registrado correctamente.", "SIBank Enterprise",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarClientes();
                }
                else
                {
                    MessageBox.Show(resultado.Mensaje ?? "No se pudo registrar el cliente.", "SIBank Enterprise",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar: " + ex.Message, "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== Boton: Editar (telefono y correo) =====
        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un cliente de la tabla primero.", "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var fila = dgvClientes.SelectedRows[0];
            long idCliente = long.Parse(fila.Cells["ID"].Value.ToString());
            string telefonoActual = fila.Cells["Telefono"].Value?.ToString() ?? "";
            string correoActual = fila.Cells["Correo"].Value?.ToString() ?? "";

            using (var formulario = new ClienteEditFormulario(telefonoActual, correoActual))
            {
                if (formulario.ShowDialog() == DialogResult.OK)
                {
                    await EditarCliente(idCliente, formulario);
                }
            }
        }

        private async Task EditarCliente(long idCliente, ClienteEditFormulario f)
        {
            try
            {
                var body = new
                {
                    idCliente = idCliente,
                    telefono = f.Telefono,
                    correo = f.Correo,
                    idUsuarioActor = idUsuarioActivo
                };

                string json = JsonSerializer.Serialize(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage(HttpMethod.Put, API_BASE_URL + "/clientes")
                {
                    Content = content
                };
                var response = await httpClient.SendAsync(request);
                string responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var resultado = JsonSerializer.Deserialize<RespuestaApi>(responseBody, options);

                if (resultado.Estado == "OK")
                {
                    MessageBox.Show("Cliente actualizado correctamente.", "SIBank Enterprise",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarClientes();
                }
                else
                {
                    MessageBox.Show(resultado.Mensaje ?? "No se pudo actualizar el cliente.", "SIBank Enterprise",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar: " + ex.Message, "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== Boton: Dar de baja (eliminar logico) =====
        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un cliente de la tabla primero.", "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var fila = dgvClientes.SelectedRows[0];
            long idCliente = long.Parse(fila.Cells["ID"].Value.ToString());
            string nombreCompleto = fila.Cells["Nombres"].Value + " " + fila.Cells["Apellidos"].Value;

            var confirmacion = MessageBox.Show(
                "Estas seguro que deseas dar de baja a " + nombreCompleto + "?",
                "SIBank Enterprise",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacion != DialogResult.Yes) return;

            try
            {
                string url = API_BASE_URL + "/clientes/" + idCliente + "?idUsuarioActor=" + idUsuarioActivo;
                var response = await httpClient.DeleteAsync(url);
                string responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var resultado = JsonSerializer.Deserialize<RespuestaApi>(responseBody, options);

                if (resultado.Estado == "OK")
                {
                    MessageBox.Show("Cliente dado de baja correctamente.", "SIBank Enterprise",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarClientes();
                }
                else
                {
                    MessageBox.Show(resultado.Mensaje ?? "No se pudo dar de baja al cliente.", "SIBank Enterprise",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al dar de baja: " + ex.Message, "SIBank Enterprise",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // Respuesta generica que devuelven varios endpoints de la API
    public class RespuestaApi
    {
        public string Estado { get; set; }
        public string Mensaje { get; set; }
    }
}