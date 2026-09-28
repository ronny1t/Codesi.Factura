using System.Text.Json.Serialization;

namespace Codesi.Factura.Aplicacion.Models
{
    public partial class clientes
    {
        [JsonPropertyName("idCliente")]
        public int id_cliente { get; set; }

        [JsonPropertyName("tipoIdentificacion")]
        public string tipo_identificacion { get; set; } = string.Empty;

        [JsonPropertyName("identificacion")]
        public string identificacion { get; set; } = string.Empty;

        [JsonPropertyName("razonSocial")]
        public string razon_social { get; set; } = string.Empty;

        [JsonPropertyName("direccion")]
        public string direccion { get; set; } = string.Empty;

        [JsonPropertyName("telefono")]
        public string telefono { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string email { get; set; } = string.Empty;
    }
}