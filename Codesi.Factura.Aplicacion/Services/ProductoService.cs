using Codesi.Factura.Aplicacion.Models;

namespace Codesi.Factura.Aplicacion.Services
{
    public class ProductoService
    {
        private readonly ApiService _api;

        public ProductoService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<Producto>> ObtenerProductos()
        {
            return await _api.GetListAsync<Producto>(
                "api/Productos"
            );
        }

        public async Task<Producto?> ObtenerPorId(int id)
        {
            return await _api.GetAsync<Producto>(
                $"api/Productos/{id}"
            );
        }

        public async Task<List<Producto>> Buscar(string texto)
        {
            return await _api.GetListAsync<Producto>(
                $"api/Productos/buscar/{Uri.EscapeDataString(texto)}"
            );
        }

        public async Task<Producto?> CrearProducto(
            Producto producto)
        {
            return await _api.PostAsync<Producto, Producto>(
     "api/Productos",
     producto
 );
        }

        public async Task<Producto?> ActualizarProducto(
            int id,
            Producto producto)
        {
            return await _api.PutAsync(
                $"api/Productos/{id}",
                producto
            );
        }

        public async Task DesactivarProducto(int id)
        {
            await _api.PutAsync<object>(
                $"api/Productos/{id}/desactivar",
                new { }
            );
        }
    }
}