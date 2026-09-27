namespace Codesi.Factura.Aplicacion.Models
{
    public class FacturaPago
    {
        public int id_pago { get; set; }

        public int id_factura { get; set; }

        public string forma_pago { get; set; } = string.Empty;

        public decimal total { get; set; }
    }
}