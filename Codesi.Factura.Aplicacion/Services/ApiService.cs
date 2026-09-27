using System.Net.Http.Json;

namespace Codesi.Factura.Aplicacion.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _http = httpClientFactory.CreateClient("FacturaApi");
        }

        // GET que devuelve una lista
        public async Task<List<T>> GetListAsync<T>(string endpoint)
        {
            var resultado =
                await _http.GetFromJsonAsync<List<T>>(endpoint);

            return resultado ?? new List<T>();
        }

        // GET que devuelve un objeto
        public async Task<T?> GetAsync<T>(string endpoint)
        {
            return await _http.GetFromJsonAsync<T>(endpoint);
        }

        // POST
        public async Task<T?> PostAsync<T>(
            string endpoint,
            T objeto)
        {
            var response =
                await _http.PostAsJsonAsync(
                    endpoint,
                    objeto
                );

            if (!response.IsSuccessStatusCode)
            {
                var mensaje =
                    await response.Content.ReadAsStringAsync();

                throw new Exception(
                    $"Error HTTP {(int)response.StatusCode} " +
                    $"({response.StatusCode}) en {endpoint}: " +
                    mensaje
                );
            }

            return await response.Content
                .ReadFromJsonAsync<T>();
        }

        // PUT
        public async Task<T?> PutAsync<T>(
            string endpoint,
            T objeto)
        {
            var response =
                await _http.PutAsJsonAsync(
                    endpoint,
                    objeto
                );

            if (!response.IsSuccessStatusCode)
            {
                var mensaje =
                    await response.Content.ReadAsStringAsync();

                throw new Exception(
                    $"Error HTTP {(int)response.StatusCode} " +
                    $"({response.StatusCode}) en {endpoint}: " +
                    mensaje
                );
            }

            return await response.Content
                .ReadFromJsonAsync<T>();
        }

        // DELETE
        public async Task DeleteAsync(string endpoint)
        {
            var response =
                await _http.DeleteAsync(endpoint);

            if (!response.IsSuccessStatusCode)
            {
                var mensaje =
                    await response.Content.ReadAsStringAsync();

                throw new Exception(
                    $"Error HTTP {(int)response.StatusCode} " +
                    $"({response.StatusCode}) en {endpoint}: " +
                    mensaje
                );
            }
        }
    }
}