using Codesi.Factura.Persistencia.Models.Universidad;
using Microsoft.EntityFrameworkCore;

namespace Codesi.Factura.Persistencia.Repositories
{
    public class ProductoRepository
    {
        private readonly FacturacionUniversidadContext _context;

        public ProductoRepository(FacturacionUniversidadContext context)
        {
            _context = context;
        }

        // Obtener todos los productos activos
        public List<Producto> ObtenerProductos()
        {
            return _context.Productos
                .AsNoTracking()
                .Where(p => p.Activo == true)
                .ToList();
        }

        // Obtener producto por ID
        public Producto? ObtenerProductoPorId(int id)
        {
            return _context.Productos
                .AsNoTracking()
                .FirstOrDefault(p => p.IdProducto == id);
        }

        // Buscar productos por nombre o código
        public List<Producto> Buscar(string texto)
        {
            return _context.Productos
                .AsNoTracking()
                .Where(p =>
                    p.Activo == true &&
                    (
                        p.Nombre.Contains(texto) ||
                        p.CodigoPrincipal.Contains(texto)
                    )
                )
                .ToList();
        }

        // Insertar producto
        public void InsertarProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            _context.SaveChanges();
        }

        // Actualizar producto
        public void ActualizarProducto(Producto producto)
        {
            _context.Productos.Update(producto);
            _context.SaveChanges();
        }

        // Desactivar producto
        public void DesactivarProducto(int id)
        {
            var producto = _context.Productos
                .FirstOrDefault(p => p.IdProducto == id);

            if (producto != null)
            {
                producto.Activo = false;
                _context.SaveChanges();
            }
        }
    }
}