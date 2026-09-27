namespace Codesi.Factura.Aplicacion.Models
{
    public class Factura
    {
        public int id_factura { get; set; }

        public int id_cliente { get; set; }

        public DateTime? fecha_emision { get; set; }

        public string? estado_sri { get; set; }

        public decimal importe_total { get; set; }

        public List<FacturaDetalle> factura_detalles { get; set; }
            = new();

        public List<FacturaPago> factura_pagos { get; set; }
            = new();
    }
}