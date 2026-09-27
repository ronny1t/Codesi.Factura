namespace Codesi.Factura.Aplicacion.Models
{
    public class Producto
    {
        public int id_producto { get; set; }

        public string? codigo_principal { get; set; }

        public string nombre { get; set; } = string.Empty;

        public decimal precio_unitario { get; set; }

        public int stock { get; set; }

        public decimal tarifa_iva { get; set; }

        public int? id_categoria { get; set; }

        public bool activo { get; set; }
    }
}