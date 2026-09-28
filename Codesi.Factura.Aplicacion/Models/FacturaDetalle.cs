using System.Text.Json.Serialization;

namespace Codesi.Factura.Aplicacion.Models
{
    public class FacturaDetalle
    {
        [JsonPropertyName("idDetalle")]
        public int id_detalle { get; set; }

        [JsonPropertyName("idFactura")]
        public int id_factura { get; set; }

        [JsonPropertyName("idProducto")]
        public int id_producto { get; set; }

        [JsonPropertyName("cantidad")]
        public int cantidad { get; set; }

        [JsonPropertyName("precioUnitario")]
        public decimal precio_unitario { get; set; }

        [JsonPropertyName("descuento")]
        public decimal? descuento { get; set; }

        [JsonPropertyName("subtotal")]
        public decimal subtotal { get; set; }

        [JsonPropertyName("valorIva")]
        public decimal valor_iva { get; set; }

        [JsonPropertyName("total")]
        public decimal total { get; set; }
    }
}