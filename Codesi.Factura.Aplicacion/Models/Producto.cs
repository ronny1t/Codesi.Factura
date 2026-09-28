using System.Text.Json.Serialization;

namespace Codesi.Factura.Aplicacion.Models
{
    public class Producto
    {
        [JsonPropertyName("idProducto")]
        public int id_producto { get; set; }

        [JsonPropertyName("codigoPrincipal")]
        public string? codigo_principal { get; set; }

        [JsonPropertyName("nombre")]
        public string nombre { get; set; } = string.Empty;

        [JsonPropertyName("precioUnitario")]
        public decimal precio_unitario { get; set; }

        [JsonPropertyName("stock")]
        public int stock { get; set; }

        [JsonPropertyName("tarifaIva")]
        public decimal tarifa_iva { get; set; }

        [JsonPropertyName("idCategoria")]
        public int? id_categoria { get; set; }

        [JsonPropertyName("activo")]
        public bool activo { get; set; }
    }
}