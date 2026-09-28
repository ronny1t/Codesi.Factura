namespace Codesi.Factura.Api.Models
{
    public class FacturaCrearRequest
    {
        public string establecimiento { get; set; } = "001";

        public string punto_emision { get; set; } = "001";

        public string secuencial { get; set; } = string.Empty;

        public string? clave_acceso { get; set; }

        public DateTime fecha_emision { get; set; } = DateTime.Now;

        public int id_cliente { get; set; }

        public decimal subtotal_sin_impuestos { get; set; }

        public decimal total_descuento { get; set; }

        public decimal subtotal_iva { get; set; }

        public decimal propina { get; set; }

        public decimal importe_total { get; set; }

        public string estado_sri { get; set; } = "CREADA";
    }
}