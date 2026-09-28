using System.Text.Json.Serialization;

namespace Codesi.Factura.Api.Models
{
    public class FacturaDetalleRequest
    {
        [JsonPropertyName("idFactura")]
        public int IdFactura { get; set; }

        [JsonPropertyName("idProducto")]
        public int IdProducto { get; set; }

        [JsonPropertyName("cantidad")]
        public int Cantidad { get; set; }

        [JsonPropertyName("precioUnitario")]
        public decimal PrecioUnitario { get; set; }

        [JsonPropertyName("descuento")]
        public decimal? Descuento { get; set; }

        [JsonPropertyName("subtotal")]
        public decimal Subtotal { get; set; }

        [JsonPropertyName("valorIva")]
        public decimal ValorIva { get; set; }

        [JsonPropertyName("total")]
        public decimal Total { get; set; }
    }
}