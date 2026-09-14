using Codesi.Factura.Persistencia.Models;
using Microsoft.EntityFrameworkCore;

namespace Codesi.Factura.Persistencia.Repositories
{
    public class ProductoRepository
    {
        private readonly licoreriaContext _context;

        public ProductoRepository(licoreriaContext context)
        {
            _context = context;
        }

        // Obtener todos los productos activos
        public List<productos> ObtenerProductos()
        {
            return _context.productos
                .AsNoTracking()
                .Where(p => p.activo == true)
                .ToList();
        }

        // Obtener producto por ID
        public productos? ObtenerProductoPorId(int id)
        {
            return _context.productos
                .AsNoTracking()
                .FirstOrDefault(p => p.id_producto == id);
        }

        // Buscar producto por código
        public productos? ObtenerPorCodigo(string codigo)
        {
            return _context.productos
                .AsNoTracking()
                .FirstOrDefault(p => p.codigo_principal == codigo);
        }

        // Buscar productos por nombre
        public List<productos> BuscarPorNombre(string nombre)
        {
            return _context.productos
                .AsNoTracking()
                .Where(p => p.nombre.Contains(nombre)
                            && p.activo == true)
                .ToList();
        }

        // Insertar producto
        public void InsertarProducto(productos producto)
        {
            _context.productos.Add(producto);
            _context.SaveChanges();
        }

        // Actualizar producto
        public void ActualizarProducto(productos producto)
        {
            _context.productos.Update(producto);
            _context.SaveChanges();
        }

        // Desactivar producto
        public void DesactivarProducto(int id)
        {
            var producto = _context.productos
                .FirstOrDefault(p => p.id_producto == id);

            if (producto != null)
            {
                producto.activo = false;
                _context.SaveChanges();
            }
        }
    }
}