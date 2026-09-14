using Codesi.Factura.Persistencia.Models;
using Microsoft.EntityFrameworkCore;

namespace Codesi.Factura.Persistencia.Repositories
{
    public class CategoriaRepository
    {
        private readonly licoreriaContext _context;

        public CategoriaRepository(licoreriaContext context)
        {
            _context = context;
        }

        // Obtener todas las categorías
        public List<categorias> ObtenerCategorias()
        {
            return _context.categorias
                .AsNoTracking()
                .ToList();
        }

        // Obtener una categoría por ID
        public categorias? ObtenerCategoriaPorId(int id)
        {
            return _context.categorias
                .AsNoTracking()
                .FirstOrDefault(c => c.id_categoria == id);
        }

        // Insertar categoría
        public void InsertarCategoria(categorias categoria)
        {
            _context.categorias.Add(categoria);
            _context.SaveChanges();
        }

        // Actualizar categoría
        public void ActualizarCategoria(categorias categoria)
        {
            _context.categorias.Update(categoria);
            _context.SaveChanges();
        }

        // Desactivar / ocultar categoría
        public void DesactivarCategoria(int id)
        {
            var categoria = _context.categorias
                .FirstOrDefault(c => c.id_categoria == id);

            if (categoria != null)
            {
                categoria.activo = false;
                _context.SaveChanges();
            }
        }

        // Activar / mostrar categoría
        public void ActivarCategoria(int id)
        {
            var categoria = _context.categorias
                .FirstOrDefault(c => c.id_categoria == id);

            if (categoria != null)
            {
                categoria.activo = true;
                _context.SaveChanges();
            }
        }
    }
}
