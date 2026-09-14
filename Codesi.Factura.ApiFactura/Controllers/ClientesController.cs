using Codesi.Factura.Api.Services;
using Codesi.Factura.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace Codesi.Factura.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteService _service;

        public ClientesController(ClienteService service)
        {
            _service = service;
        }

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
        [HttpGet("identificacion/{identificacion}")]
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

        // POST: api/Clientes
        [HttpPost]
        public IActionResult CrearCliente([FromBody] clientes cliente)
        {
            _service.CrearCliente(cliente);

            return Ok(cliente);
        }

        // PUT: api/Clientes/5
        [HttpPut("{id}")]
        public IActionResult ActualizarCliente(
            int id,
            [FromBody] clientes cliente)
        {
            cliente.id_cliente = id;

            _service.ActualizarCliente(cliente);

            return Ok(cliente);
        }
    }
}