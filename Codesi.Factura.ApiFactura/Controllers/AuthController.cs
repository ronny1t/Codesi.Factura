using Codesi.Factura.Api.Models;
using Codesi.Factura.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Codesi.Factura.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly JwtService _jwtService;

        public AuthController(
            UsuarioService usuarioService,
            JwtService jwtService)
        {
            _usuarioService = usuarioService;
            _jwtService = jwtService;
        }

        // ============================================
        // POST: api/Auth/login
        // ============================================

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request == null)
            {
                return BadRequest(new
                {
                    mensaje = "Los datos de inicio de sesión son obligatorios."
                });
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest(new
                {
                    mensaje = "El email es obligatorio."
                });
            }

            if (string.IsNullOrWhiteSpace(request.Cedula))
            {
                return BadRequest(new
                {
                    mensaje = "La cédula es obligatoria."
                });
            }

            // ============================================
            // BUSCAR USUARIO
            // ============================================

            var usuario = _usuarioService.Login(
                request.Email.Trim(),
                request.Cedula.Trim()
            );

            // ============================================
            // VALIDAR USUARIO
            // ============================================

            if (usuario == null)
            {
                return Unauthorized(new
                {
                    mensaje = "Email o cédula incorrectos, o usuario inactivo."
                });
            }

            // ============================================
            // GENERAR JWT
            // ============================================

            var token = _jwtService.GenerarToken(usuario);

            // ============================================
            // RESPUESTA
            // ============================================

            return Ok(new
            {
                token,

                usuario = new
                {
                    idUsuario = usuario.IdUsuario,
                    nombre = usuario.Nombre,
                    email = usuario.Email,
                    cedula = usuario.Cedula,
                    tipoPersona = usuario.TipoPersona,

                    roles = usuario.IdRol
                        .Select(r => r.Nombre)
                        .ToList()
                }
            });
        }
    }
}