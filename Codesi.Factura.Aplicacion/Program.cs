using Codesi.Factura.Aplicacion.Components;
using Codesi.Factura.Aplicacion.Services;

namespace Codesi.Factura.Aplicacion
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Blazor
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            // Conexión con la API
            builder.Services.AddHttpClient("FacturaApi", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5043/");
            });

            // Servicios de la aplicación
            builder.Services.AddScoped<ApiService>();
            builder.Services.AddScoped<ProductoService>();
            builder.Services.AddScoped<ClienteApiService>();
            builder.Services.AddScoped<CategoriaService>();
            builder.Services.AddScoped<FacturaService>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute(
                "/not-found",
                createScopeForStatusCodePages: true);

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}