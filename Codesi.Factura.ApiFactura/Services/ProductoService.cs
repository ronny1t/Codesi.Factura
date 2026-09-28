using Codesi.Factura.Persistencia.Models.Universidad;
using Codesi.Factura.Persistencia.Repositories;

namespace Codesi.Factura.Api.Services
{
    public class ProductoService
    {
        private readonly ProductoRepository _repository;

        public ProductoService(ProductoRepository repository)
        {
            _repository = repository;
        }

        public List<Producto> ObtenerProductos()
        {
            return _repository.ObtenerProductos();
        }

        public Producto? ObtenerProductoPorId(int id)
        {
            return _repository.ObtenerProductoPorId(id);
        }

        public List<Producto> Buscar(string texto)
        {
            return _repository.Buscar(texto);
        }

        public void CrearProducto(Producto producto)
        {
            _repository.InsertarProducto(producto);
        }

        public void ActualizarProducto(Producto producto)
        {
            _repository.ActualizarProducto(producto);
        }

        public void DesactivarProducto(int id)
        {
            _repository.DesactivarProducto(id);
        }
    }
}