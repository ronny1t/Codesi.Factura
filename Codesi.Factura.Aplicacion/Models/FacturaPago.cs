using System.Text.Json.Serialization;

namespace Codesi.Factura.Aplicacion.Models
{
    public class FacturaPago
    {
        [JsonPropertyName("idPago")]
        public int id_pago { get; set; }

        [JsonPropertyName("idFactura")]
        public int id_factura { get; set; }

        [JsonPropertyName("formaPago")]
        public string forma_pago { get; set; } = string.Empty;

        [JsonPropertyName("total")]
        public decimal total { get; set; }
    }
}