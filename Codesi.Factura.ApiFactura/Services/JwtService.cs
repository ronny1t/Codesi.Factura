using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Codesi.Factura.Persistencia.Models.Universidad;
using Microsoft.IdentityModel.Tokens;

namespace Codesi.Factura.Api.Services
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerarToken(Usuarios usuario)
        {
            var key = _configuration["Jwt:Key"];

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new InvalidOperationException(
                    "No se encontró la clave JWT en la configuración.");
            }

            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var expirationMinutesString =
                _configuration["Jwt:ExpirationMinutes"];

            var expirationMinutes = 120;

            if (int.TryParse(
                expirationMinutesString,
                out var minutos))
            {
                expirationMinutes = minutos;
            }

            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    usuario.IdUsuario.ToString()),

                new Claim(
                    ClaimTypes.Name,
                    usuario.Nombre ?? string.Empty),

                new Claim(
                    ClaimTypes.Email,
                    usuario.Email ?? string.Empty),

                new Claim(
                    "cedula",
                    usuario.Cedula ?? string.Empty),

                new Claim(
                    "tipoPersona",
                    usuario.TipoPersona ?? string.Empty)
            };

            foreach (var rol in usuario.IdRol)
            {
                if (!string.IsNullOrWhiteSpace(rol.Nombre))
                {
                    claims.Add(
                        new Claim(
                            ClaimTypes.Role,
                            rol.Nombre));
                }
            }

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key));

            var credentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.UtcNow.AddMinutes(
                    expirationMinutes),

                Issuer = issuer,

                Audience = audience,

                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(
                tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}