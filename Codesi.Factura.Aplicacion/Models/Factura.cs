using System.Text.Json.Serialization;

namespace Codesi.Factura.Aplicacion.Models
{
    public class Factura
    {
        [JsonPropertyName("idFactura")]
        public int id_factura { get; set; }

        [JsonPropertyName("establecimiento")]
        public string establecimiento { get; set; } = "001";

        [JsonPropertyName("puntoEmision")]
        public string punto_emision { get; set; } = "001";

        [JsonPropertyName("secuencial")]
        public string secuencial { get; set; } = string.Empty;

        [JsonPropertyName("claveAcceso")]
        public string clave_acceso { get; set; } = string.Empty;

        [JsonPropertyName("idCliente")]
        public int id_cliente { get; set; }

        [JsonPropertyName("fechaEmision")]
        public DateTime fecha_emision { get; set; } = DateTime.Now;

        [JsonPropertyName("subtotalSinImpuestos")]
        public decimal subtotal_sin_impuestos { get; set; }

        [JsonPropertyName("totalDescuento")]
        public decimal? total_descuento { get; set; } = 0;

        [JsonPropertyName("subtotalIva")]
        public decimal subtotal_iva { get; set; }

        [JsonPropertyName("propina")]
        public decimal? propina { get; set; } = 0;

        [JsonPropertyName("importeTotal")]
        public decimal importe_total { get; set; }

        [JsonPropertyName("estadoSri")]
        public string estado_sri { get; set; } = "CREADA";

        [JsonPropertyName("facturaDetalles")]
        public List<FacturaDetalle> factura_detalles { get; set; } = new();

        [JsonPropertyName("facturaPagos")]
        public List<FacturaPago> factura_pagos { get; set; } = new();
    }
}