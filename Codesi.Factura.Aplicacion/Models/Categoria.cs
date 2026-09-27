namespace Codesi.Factura.Aplicacion.Models
{
    public class CategoriaModel
    {
        public int id_categoria { get; set; }

        public string nombre { get; set; } = string.Empty;

        public bool activo { get; set; }
    }
}