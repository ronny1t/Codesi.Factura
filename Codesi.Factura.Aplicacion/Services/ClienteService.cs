using Codesi.Factura.Aplicacion.Models;

namespace Codesi.Factura.Aplicacion.Services;

public class ClienteApiService
{
    private readonly ApiService _api;

    public ClienteApiService(ApiService api)
    {
        _api = api;
    }

    public async Task<List<clientes>> ObtenerClientesAsync()
    {
        return await _api.GetAsync<List<clientes>>("api/Clientes");
    }

    public async Task<clientes?> ObtenerClienteAsync(int id)
    {
        return await _api.GetAsync<clientes>($"api/Clientes/{id}");
    }

    public async Task<clientes?> ObtenerPorIdentificacionAsync(string identificacion)
    {
        return await _api.GetAsync<clientes>(
            $"api/Clientes/identificacion/{Uri.EscapeDataString(identificacion)}");
    }

    public async Task<clientes?> CrearClienteAsync(clientes cliente)
    {
        return await _api.PostAsync<clientes>("api/Clientes", cliente);
    }

    public async Task ActualizarClienteAsync(int id, clientes cliente)
    {
        await _api.PutAsync($"api/Clientes/{id}", cliente);
    }
}