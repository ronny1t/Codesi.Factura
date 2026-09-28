using Codesi.Factura.Persistencia.Models.Universidad;
using Microsoft.EntityFrameworkCore;

namespace Codesi.Factura.Persistencia.Repositories
{
    public class CategoriaRepository
    {
        private readonly FacturacionUniversidadContext _context;

        public CategoriaRepository(FacturacionUniversidadContext context)
        {
            _context = context;
        }

        // Obtener todas las categorías
        public List<Categoria> ObtenerCategorias()
        {
            return _context.Categorias
                .AsNoTracking()
                .ToList();
        }

        // Obtener una categoría por ID
        public Categoria? ObtenerCategoriaPorId(int id)
        {
            return _context.Categorias
                .AsNoTracking()
                .FirstOrDefault(c => c.IdCategoria == id);
        }

        // Insertar categoría
        public void InsertarCategoria(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
        }

        // Actualizar categoría
        public void ActualizarCategoria(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            _context.SaveChanges();
        }

        // Desactivar / ocultar categoría
        public void DesactivarCategoria(int id)
        {
            var categoria = _context.Categorias
                .FirstOrDefault(c => c.IdCategoria == id);

            if (categoria != null)
            {
                categoria.Activo = false;
                _context.SaveChanges();
            }
        }

        // Activar / mostrar categoría
        public void ActivarCategoria(int id)
        {
            var categoria = _context.Categorias
                .FirstOrDefault(c => c.IdCategoria == id);

            if (categoria != null)
            {
                categoria.Activo = true;
                _context.SaveChanges();
            }
        }
    }
}