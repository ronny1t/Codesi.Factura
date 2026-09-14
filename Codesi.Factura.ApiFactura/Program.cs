using Codesi.Factura.Persistencia.Models;
using Codesi.Factura.Persistencia.Repositories;
using Microsoft.EntityFrameworkCore;
using Codesi.Factura.Api.Services;

namespace Codesi.Factura.ApiFactura
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Conexión con SQL Server
            builder.Services.AddDbContext<licoreriaContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                ));

            // Repositories
            builder.Services.AddScoped<CategoriaRepository>();
            builder.Services.AddScoped<ProductoRepository>();
            builder.Services.AddScoped<ClienteRepository>();
            builder.Services.AddScoped<FacturaRepository>();
            builder.Services.AddScoped<FacturaDetalleRepository>();
            builder.Services.AddScoped<FacturaPagoRepository>();

            // Services
            builder.Services.AddScoped<CategoriaService>();
            builder.Services.AddScoped<ProductoService>();
            builder.Services.AddScoped<ClienteService>();
            builder.Services.AddScoped<FacturaService>();
            builder.Services.AddScoped<FacturaDetalleService>();
            builder.Services.AddScoped<FacturaPagoService>();

            // CORS para permitir conexiones desde Flutter Web
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("FlutterPolicy", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            // Add services to the container.
            builder.Services.AddControllers();

            // OpenAPI
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            // CORS
            app.UseCors("FlutterPolicy");


            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}