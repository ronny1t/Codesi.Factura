using Codesi.Factura.Api.Services;
using Codesi.Factura.Persistencia.Models.Universidad;
using Microsoft.AspNetCore.Mvc;

namespace Codesi.Factura.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuariosController(UsuarioService service)
        {
            _service = service;
        }

        // GET: api/Usuarios
        [HttpGet]
        public IActionResult ObtenerUsuarios()
        {
            return Ok(_service.ObtenerUsuarios());
        }

        // GET: api/Usuarios/activos
        [HttpGet("activos")]
        public IActionResult ObtenerUsuariosActivos()
        {
            return Ok(_service.ObtenerUsuariosActivos());
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public IActionResult ObtenerUsuario(int id)
        {
            var usuario = _service.ObtenerUsuarioPorId(id);

            if (usuario == null)
            {
                return NotFound(new
                {
                    mensaje = "Usuario no encontrado"
                });
            }

            return Ok(usuario);
        }

        // GET: api/Usuarios/microsoft/xxxxx
        [HttpGet("microsoft/{idMicrosoft}")]
        public IActionResult ObtenerPorIdMicrosoft(string idMicrosoft)
        {
            var usuario = _service.ObtenerPorIdMicrosoft(idMicrosoft);

            if (usuario == null)
            {
                return NotFound(new
                {
                    mensaje = "Usuario no encontrado"
                });
            }

            return Ok(usuario);
        }

        // GET: api/Usuarios/email/usuario@empresa.com
        [HttpGet("email/{email}")]
        public IActionResult ObtenerPorEmail(string email)
        {
            var usuario = _service.ObtenerPorEmail(email);

            if (usuario == null)
            {
                return NotFound(new
                {
                    mensaje = "Usuario no encontrado"
                });
            }

            return Ok(usuario);
        }

        // GET: api/Usuarios/5/roles
        [HttpGet("{id}/roles")]
        public IActionResult ObtenerUsuarioConRoles(int id)
        {
            var usuario = _service.ObtenerUsuarioConRoles(id);

            if (usuario == null)
            {
                return NotFound(new
                {
                    mensaje = "Usuario no encontrado"
                });
            }

            return Ok(new
            {
                usuario.IdUsuario,
                usuario.IdMicrosoft,
                usuario.Email,
                usuario.Nombre,
                usuario.Activo,
                usuario.FechaCreacion,
                roles = usuario.IdRol.Select(r => new
                {
                    r.IdRol,
                    r.Nombre,
                    r.Activo
                })
            });
        }

        // GET: api/Usuarios/microsoft/xxxxx/roles
        [HttpGet("microsoft/{idMicrosoft}/roles")]
        public IActionResult ObtenerPorIdMicrosoftConRoles(
            string idMicrosoft)
        {
            var usuario =
                _service.ObtenerPorIdMicrosoftConRoles(idMicrosoft);

            if (usuario == null)
            {
                return NotFound(new
                {
                    mensaje = "Usuario no encontrado"
                });
            }

            return Ok(new
            {
                usuario.IdUsuario,
                usuario.IdMicrosoft,
                usuario.Email,
                usuario.Nombre,
                usuario.Activo,
                usuario.FechaCreacion,
                roles = usuario.IdRol.Select(r => new
                {
                    r.IdRol,
                    r.Nombre,
                    r.Activo
                })
            });
        }

        // POST: api/Usuarios
        [HttpPost]
        public IActionResult CrearUsuario(
            [FromBody] Usuarios usuario)
        {
            _service.CrearUsuario(usuario);

            return Ok(usuario);
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public IActionResult ActualizarUsuario(
            int id,
            [FromBody] Usuarios usuario)
        {
            usuario.IdUsuario = id;

            _service.ActualizarUsuario(usuario);

            return Ok(usuario);
        }

        // PUT: api/Usuarios/5/desactivar
        [HttpPut("{id}/desactivar")]
        public IActionResult DesactivarUsuario(int id)
        {
            _service.DesactivarUsuario(id);

            return Ok(new
            {
                mensaje = "Usuario desactivado correctamente"
            });
        }

        // POST: api/Usuarios/5/roles/1
        [HttpPost("{idUsuario}/roles/{idRol}")]
        public IActionResult AsignarRol(
            int idUsuario,
            int idRol)
        {
            var resultado = _service.AsignarRol(
                idUsuario,
                idRol);

            if (!resultado)
            {
                return NotFound(new
                {
                    mensaje = "Usuario o rol no encontrado"
                });
            }

            return Ok(new
            {
                mensaje = "Rol asignado correctamente",
                idUsuario,
                idRol
            });
        }
    }
}