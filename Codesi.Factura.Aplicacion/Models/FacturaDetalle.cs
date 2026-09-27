namespace Codesi.Factura.Aplicacion.Models
{
    public class FacturaDetalle
    {
        public int id_detalle { get; set; }

        public int id_factura { get; set; }

        public int id_producto { get; set; }

        public int cantidad { get; set; }

        public decimal precio_unitario { get; set; }

        public decimal? descuento { get; set; }

        public decimal subtotal { get; set; }

        public decimal valor_iva { get; set; }

        public decimal total { get; set; }
    }
}