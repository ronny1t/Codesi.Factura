using Codesi.Factura.Api.Services;
using Codesi.Factura.Persistencia.Models.Universidad;
using Microsoft.AspNetCore.Mvc;

namespace Codesi.Factura.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly RolService _service;

        public RolesController(RolService service)
        {
            _service = service;
        }

        // GET: api/Roles
        [HttpGet]
        public IActionResult ObtenerRoles()
        {
            return Ok(_service.ObtenerRoles());
        }

        // GET: api/Roles/activos
        [HttpGet("activos")]
        public IActionResult ObtenerRolesActivos()
        {
            return Ok(_service.ObtenerRolesActivos());
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public IActionResult ObtenerRol(int id)
        {
            var rol = _service.ObtenerRolPorId(id);

            if (rol == null)
            {
                return NotFound(new
                {
                    mensaje = "Rol no encontrado"
                });
            }

            return Ok(rol);
        }

        // GET: api/Roles/nombre/Administrador
        [HttpGet("nombre/{nombre}")]
        public IActionResult ObtenerPorNombre(string nombre)
        {
            var rol = _service.ObtenerPorNombre(nombre);

            if (rol == null)
            {
                return NotFound(new
                {
                    mensaje = "Rol no encontrado"
                });
            }

            return Ok(rol);
        }

        // POST: api/Roles
        [HttpPost]
        public IActionResult CrearRol([FromBody] Roles rol)
        {
            _service.CrearRol(rol);

            return Ok(rol);
        }

        // PUT: api/Roles/5
        [HttpPut("{id}")]
        public IActionResult ActualizarRol(
            int id,
            [FromBody] Roles rol)
        {
            rol.IdRol = id;

            _service.ActualizarRol(rol);

            return Ok(rol);
        }

        // PUT: api/Roles/5/desactivar
        [HttpPut("{id}/desactivar")]
        public IActionResult DesactivarRol(int id)
        {
            _service.DesactivarRol(id);

            return Ok(new
            {
                mensaje = "Rol desactivado correctamente"
            });
        }
    }
}