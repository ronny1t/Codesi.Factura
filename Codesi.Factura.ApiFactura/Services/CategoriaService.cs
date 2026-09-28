using Codesi.Factura.Persistencia.Models.Universidad;
using Codesi.Factura.Persistencia.Repositories;

namespace Codesi.Factura.Api.Services
{
    public class CategoriaService
    {
        private readonly CategoriaRepository _repository;

        public CategoriaService(CategoriaRepository repository)
        {
            _repository = repository;
        }

        public List<Categoria> ObtenerCategorias()
        {
            return _repository.ObtenerCategorias();
        }

        public void CrearCategoria(Categoria categoria)
        {
            _repository.InsertarCategoria(categoria);
        }

        public void DesactivarCategoria(int id)
        {
            _repository.DesactivarCategoria(id);
        }

        public void ActivarCategoria(int id)
        {
            _repository.ActivarCategoria(id);
        }
    }
}