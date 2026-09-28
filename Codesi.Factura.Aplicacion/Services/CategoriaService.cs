using Codesi.Factura.Aplicacion.Models;

namespace Codesi.Factura.Aplicacion.Services
{
    public class CategoriaService
    {
        private readonly ApiService _api;

        public CategoriaService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<CategoriaModel>> ObtenerCategorias()
        {
            return await _api.GetListAsync<CategoriaModel>(
                "api/Categorias"
            );
        }

        public async Task<CategoriaModel?> CrearCategoriaAsync(
    CategoriaModel categoria)
        {
            return await _api.PostAsync<CategoriaModel, CategoriaModel>(
                "api/Categorias",
                categoria
            );
        }

        public async Task DesactivarCategoriaAsync(int id)
        {
            await _api.PutAsync<object>(
                $"api/Categorias/{id}/desactivar",
                new { }
            );
        }

        public async Task ActivarCategoriaAsync(int id)
        {
            await _api.PutAsync<object>(
                $"api/Categorias/{id}/activar",
                new { }
            );
        }
    }
}