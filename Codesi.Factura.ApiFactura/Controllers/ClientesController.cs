using Codesi.Factura.Api.Services;
using Codesi.Factura.Persistencia.Models.Universidad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Codesi.Factura.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteService _service;

        public ClientesController(ClienteService service)
        {
            _service = service;
        }

        // ============================================
        // CONSULTAS
        // Cualquier usuario autenticado
        // ============================================

        // GET: api/Clientes
        [HttpGet]
        public IActionResult ObtenerClientes()
        {
            return Ok(_service.ObtenerClientes());
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public IActionResult ObtenerCliente(int id)
        {
            var cliente = _service.ObtenerClientePorId(id);

            if (cliente == null)
                return NotFound(new
                {
                    mensaje = "Cliente no encontrado"
                });

            return Ok(cliente);
        }

        // GET: api/Clientes/identificacion/0102030405
        [HttpGet("identificacion/0102030405")]
        public IActionResult ObtenerPorIdentificacion(
            string identificacion)
        {
            var cliente =
                _service.ObtenerPorIdentificacion(identificacion);

            if (cliente == null)
                return NotFound(new
                {
                    mensaje = "Cliente no encontrado"
                });

            return Ok(cliente);
        }

        // ============================================
        // CREAR CLIENTE
        // Administrador y Facturación
        // ============================================

        // POST: api/Clientes
        [Authorize(Roles = "Administrador,Facturacion")]
        [HttpPost]
        public IActionResult CrearCliente([FromBody] Cliente cliente)
        {
            Console.WriteLine("========== CREAR CLIENTE ==========");
            Console.WriteLine(
                $"Tipo identificación: '{cliente.TipoIdentificacion}'");
            Console.WriteLine(
                $"Identificación: '{cliente.Identificacion}'");
            Console.WriteLine(
                $"Razón social: '{cliente.RazonSocial}'");
            Console.WriteLine(
                $"Dirección: '{cliente.Direccion}'");
            Console.WriteLine(
                $"Teléfono: '{cliente.Telefono}'");
            Console.WriteLine(
                $"Email: '{cliente.Email}'");

            _service.CrearCliente(cliente);

            return Ok(cliente);
        }

        // ============================================
        // ACTUALIZAR CLIENTE
        // Administrador y Facturación
        // ============================================

        // PUT: api/Clientes/5
        [Authorize(Roles = "Administrador,Facturacion")]
        [HttpPut("{id}")]
        public IActionResult ActualizarCliente(
            int id,
            [FromBody] Cliente cliente)
        {
            cliente.IdCliente = id;

            _service.ActualizarCliente(cliente);

            return Ok(cliente);
        }
    }
}