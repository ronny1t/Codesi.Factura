namespace Codesi.Factura.Aplicacion.Models
{
    public class Factura
    {
        public int id_factura { get; set; }

        public string establecimiento { get; set; } = "001";

        public string punto_emision { get; set; } = "001";

        public string secuencial { get; set; } = string.Empty;

        public string clave_acceso { get; set; } = string.Empty;

        public int id_cliente { get; set; }

        public DateTime fecha_emision { get; set; } = DateTime.Now;

        public decimal subtotal_sin_impuestos { get; set; }

        public decimal? total_descuento { get; set; } = 0;

        public decimal subtotal_iva { get; set; }

        public decimal? propina { get; set; } = 0;

        public decimal importe_total { get; set; }

        public string estado_sri { get; set; } = "PENDIENTE";

        public List<FacturaDetalle> factura_detalles { get; set; } = new();

        public List<FacturaPago> factura_pagos { get; set; } = new();
    }
}