using Codesi.Factura.Persistencia.Models;
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

        public List<productos> ObtenerProductos()
        {
            return _repository.ObtenerProductos();
        }

        public productos? ObtenerProductoPorId(int id)
        {
            return _repository.ObtenerProductoPorId(id);
        }

        public productos? ObtenerPorCodigo(string codigo)
        {
            return _repository.ObtenerPorCodigo(codigo);
        }

        public List<productos> BuscarPorNombre(string nombre)
        {
            return _repository.BuscarPorNombre(nombre);
        }

        public void CrearProducto(productos producto)
        {
            _repository.InsertarProducto(producto);
        }

        public void ActualizarProducto(productos producto)
        {
            _repository.ActualizarProducto(producto);
        }

        public void DesactivarProducto(int id)
        {
            _repository.DesactivarProducto(id);
        }
    }
}