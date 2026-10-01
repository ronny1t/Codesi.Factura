using Codesi.Factura.Persistencia.Models.Universidad;
using Microsoft.EntityFrameworkCore;

namespace Codesi.Factura.Persistencia.Repositories
{
    public class ProductoRepository
    {
        private readonly FacturacionUniversidadContext _context;

        public ProductoRepository(
            FacturacionUniversidadContext context)
        {
            _context = context;
        }

        // ============================================================
        // OBTENER PRODUCTOS ACTIVOS
        // ============================================================

        public List<Producto> ObtenerProductos()
        {
            return _context.Productos
                .AsNoTracking()
                .Where(p => p.Activo == true)
                .ToList();
        }

        // ============================================================
        // OBTENER PRODUCTOS DESACTIVADOS
        // ============================================================

        public List<Producto> ObtenerProductosDesactivados()
        {
            return _context.Productos
                .AsNoTracking()
                .Where(p => p.Activo == false)
                .ToList();
        }

        // ============================================================
        // OBTENER PRODUCTO POR ID
        // ============================================================

        public Producto? ObtenerProductoPorId(int id)
        {
            return _context.Productos
                .AsNoTracking()
                .FirstOrDefault(
                    p => p.IdProducto == id
                );
        }

        // ============================================================
        // BUSCAR PRODUCTOS ACTIVOS
        // ============================================================

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

        // ============================================================
        // VERIFICAR SI EXISTE CODIGO PRINCIPAL
        // ============================================================

        public bool ExisteCodigoPrincipal(
            string codigoPrincipal,
            int? idProducto = null)
        {
            return _context.Productos
                .Any(p =>
                    p.CodigoPrincipal == codigoPrincipal &&
                    (
                        !idProducto.HasValue ||
                        p.IdProducto != idProducto.Value
                    )
                );
        }

        // ============================================================
        // INSERTAR PRODUCTO
        // ============================================================

        public void InsertarProducto(Producto producto)
        {
            if (producto.FechaCreacion <
                new DateTime(1753, 1, 1))
            {
                producto.FechaCreacion =
                    DateTime.Now;
            }

            _context.Productos.Add(producto);

            _context.SaveChanges();
        }

        // ============================================================
        // ACTUALIZAR PRODUCTO
        // ============================================================

        public void ActualizarProducto(
            Producto producto)
        {
            var productoExistente =
                _context.Productos
                    .FirstOrDefault(
                        p => p.IdProducto ==
                             producto.IdProducto
                    );

            if (productoExistente == null)
            {
                throw new Exception(
                    "El producto que intenta actualizar no existe."
                );
            }

            productoExistente.CodigoPrincipal =
                producto.CodigoPrincipal;

            productoExistente.Nombre =
                producto.Nombre;

            productoExistente.PrecioUnitario =
                producto.PrecioUnitario;

            productoExistente.Stock =
                producto.Stock;

            productoExistente.TarifaIva =
                producto.TarifaIva;

            productoExistente.IdCategoria =
                producto.IdCategoria;

            productoExistente.Activo =
                producto.Activo;

            _context.SaveChanges();
        }

        // ============================================================
        // DESACTIVAR PRODUCTO
        // ============================================================

        public void DesactivarProducto(int id)
        {
            var producto =
                _context.Productos
                    .FirstOrDefault(
                        p => p.IdProducto == id
                    );

            if (producto == null)
            {
                throw new Exception(
                    "El producto que intenta desactivar no existe."
                );
            }

            producto.Activo = false;

            _context.SaveChanges();
        }

        // ============================================================
        // ACTIVAR PRODUCTO
        // ============================================================

        public void ActivarProducto(int id)
        {
            var producto =
                _context.Productos
                    .FirstOrDefault(
                        p => p.IdProducto == id
                    );

            if (producto == null)
            {
                throw new Exception(
                    "El producto que intenta activar no existe."
                );
            }

            producto.Activo = true;

            _context.SaveChanges();
        }
    }
}