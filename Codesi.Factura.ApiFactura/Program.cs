using System.Text;

using Codesi.Factura.Persistencia.Models.Universidad;
using Codesi.Factura.Persistencia.Repositories;
using Codesi.Factura.Api.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Codesi.Factura.ApiFactura
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ============================================
            // CONEXIÓN CON SQL SERVER
            // ============================================

            builder.Services.AddDbContext<FacturacionUniversidadContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                ));

            // ============================================
            // REPOSITORIES
            // ============================================

            builder.Services.AddScoped<CategoriaRepository>();
            builder.Services.AddScoped<ProductoRepository>();
            builder.Services.AddScoped<ClienteRepository>();
            builder.Services.AddScoped<FacturaRepository>();
            builder.Services.AddScoped<FacturaDetalleRepository>();
            builder.Services.AddScoped<FacturaPagoRepository>();

            // Usuarios y Roles
            builder.Services.AddScoped<UsuarioRepository>();
            builder.Services.AddScoped<RolRepository>();

            // ============================================
            // SERVICES
            // ============================================

            builder.Services.AddScoped<CategoriaService>();
            builder.Services.AddScoped<ProductoService>();
            builder.Services.AddScoped<ClienteService>();
            builder.Services.AddScoped<FacturaService>();
            builder.Services.AddScoped<FacturaDetalleService>();
            builder.Services.AddScoped<FacturaPagoService>();

            // Usuarios y Roles
            builder.Services.AddScoped<UsuarioService>();
            builder.Services.AddScoped<RolService>();

            // JWT
            builder.Services.AddScoped<JwtService>();

            //Email
            builder.Services.AddScoped<EmailService>();

            // ============================================
            // AUTENTICACIÓN JWT
            // ============================================

            var jwtKey = builder.Configuration["Jwt:Key"];

            if (string.IsNullOrWhiteSpace(jwtKey))
            {
                throw new InvalidOperationException(
                    "No se encontró Jwt:Key en appsettings.json."
                );
            }

            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,

                            ValidateAudience = true,

                            ValidateLifetime = true,

                            ValidateIssuerSigningKey = true,

                            ValidIssuer =
                                builder.Configuration["Jwt:Issuer"],

                            ValidAudience =
                                builder.Configuration["Jwt:Audience"],

                            IssuerSigningKey =
                                new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(jwtKey)
                                ),

                            ClockSkew = TimeSpan.Zero
                        };
                });

            // ============================================
            // AUTORIZACIÓN
            // ============================================

            builder.Services.AddAuthorization();

            // ============================================
            // CORS
            // ============================================

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

            // ============================================
            // CONTROLLERS
            // ============================================

            builder.Services.AddControllers();

            // ============================================
            // OPENAPI
            // ============================================

            builder.Services.AddOpenApi();

            // ============================================
            // CONSTRUIR APLICACIÓN
            // ============================================

            var app = builder.Build();

            // ============================================
            // OPENAPI
            // ============================================

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            // ============================================
            // HTTP REQUEST PIPELINE
            // ============================================

            app.UseHttpsRedirection();

            // CORS
            app.UseCors("FlutterPolicy");

            // IMPORTANTE:
            // Authentication debe ejecutarse ANTES
            // de Authorization.
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}