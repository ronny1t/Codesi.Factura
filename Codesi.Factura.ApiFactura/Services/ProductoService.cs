using Codesi.Factura.Persistencia.Models.Universidad;
using Codesi.Factura.Persistencia.Repositories;

namespace Codesi.Factura.Api.Services
{
    public class ProductoService
    {
        private readonly ProductoRepository _repository;

        public ProductoService(
            ProductoRepository repository)
        {
            _repository = repository;
        }

        // ============================================================
        // OBTENER PRODUCTOS ACTIVOS
        // ============================================================

        public List<Producto> ObtenerProductos()
        {
            return _repository.ObtenerProductos();
        }

        // ============================================================
        // OBTENER PRODUCTO POR ID
        // ============================================================

        public Producto? ObtenerProductoPorId(int id)
        {
            return _repository.ObtenerProductoPorId(id);
        }

        // ============================================================
        // BUSCAR PRODUCTOS
        // ============================================================

        public List<Producto> Buscar(string texto)
        {
            return _repository.Buscar(texto);
        }

        // ============================================================
        // CREAR PRODUCTO
        // ============================================================

        public void CrearProducto(Producto producto)
        {
            if (string.IsNullOrWhiteSpace(
                producto.CodigoPrincipal))
            {
                throw new Exception(
                    "El código principal es obligatorio."
                );
            }

            if (_repository.ExisteCodigoPrincipal(
                producto.CodigoPrincipal))
            {
                throw new Exception(
                    "Ya existe un producto con el código principal: " +
                    producto.CodigoPrincipal
                );
            }

            _repository.InsertarProducto(producto);
        }

        // ============================================================
        // ACTUALIZAR PRODUCTO
        // ============================================================

        public void ActualizarProducto(
            Producto producto)
        {
            if (string.IsNullOrWhiteSpace(
                producto.CodigoPrincipal))
            {
                throw new Exception(
                    "El código principal es obligatorio."
                );
            }

            if (_repository.ExisteCodigoPrincipal(
                producto.CodigoPrincipal,
                producto.IdProducto))
            {
                throw new Exception(
                    "Ya existe otro producto con el código principal: " +
                    producto.CodigoPrincipal
                );
            }

            _repository.ActualizarProducto(producto);
        }

        // ============================================================
        // DESACTIVAR PRODUCTO
        // ============================================================

        public void DesactivarProducto(int id)
        {
            _repository.DesactivarProducto(id);
        }

        // ============================================================
        // ACTIVAR PRODUCTO
        // ============================================================

        public void ActivarProducto(int id)
        {
            _repository.ActivarProducto(id);
        }

        // ============================================================
        // OBTENER PRODUCTOS DESACTIVADOS
        // ============================================================

        public List<Producto> ObtenerProductosDesactivados()
        {
            return _repository.ObtenerProductosDesactivados();
        }
    }
}