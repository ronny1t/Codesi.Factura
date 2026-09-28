using System.Text.Json.Serialization;

namespace Codesi.Factura.Api.Models
{
    public class FacturaPagoRequest
    {
        [JsonPropertyName("idFactura")]
        public int IdFactura { get; set; }

        [JsonPropertyName("formaPago")]
        public string FormaPago { get; set; } = string.Empty;

        [JsonPropertyName("total")]
        public decimal Total { get; set; }
    }
}