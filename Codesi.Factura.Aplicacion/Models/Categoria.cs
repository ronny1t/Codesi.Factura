using System.Text.Json.Serialization;

namespace Codesi.Factura.Aplicacion.Models
{
    public class CategoriaModel
    {
        [JsonPropertyName("idCategoria")]
        public int id_categoria { get; set; }

        [JsonPropertyName("nombre")]
        public string nombre { get; set; } = string.Empty;

        [JsonPropertyName("activo")]
        public bool activo { get; set; }
    }
}