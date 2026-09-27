namespace Codesi.Factura.Aplicacion.Models
{
    public partial class clientes
    {
        public int id_cliente { get; set; }
        public string tipo_identificacion { get; set; }
        public string identificacion { get; set; }
        public string razon_social { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
    }
}