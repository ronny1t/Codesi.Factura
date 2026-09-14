using Codesi.Factura.Api.Services;
using Codesi.Factura.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace Codesi.Factura.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturasController : ControllerBase
    {
        private readonly FacturaService _service;

        public FacturasController(FacturaService service)
        {
            _service = service;
        }

        // GET: api/Facturas
        [HttpGet]
        public IActionResult ObtenerFacturas()
        {
            return Ok(_service.ObtenerFacturas());
        }

        // GET: api/Facturas/5
        [HttpGet("{id}")]
        public IActionResult ObtenerFactura(int id)
        {
            var factura = _service.ObtenerFacturaPorId(id);

            if (factura == null)
                return NotFound(new
                {
                    mensaje = "Factura no encontrada"
                });

            return Ok(factura);
        }

        // POST: api/Facturas
        [HttpPost]
        public IActionResult CrearFactura([FromBody] facturas factura)
        {
            _service.CrearFactura(factura);

            return Ok(factura);
        }

        // PUT: api/Facturas/5/estado/AUTORIZADA
        [HttpPut("{id}/estado/{estado}")]
        public IActionResult ActualizarEstado(
            int id,
            string estado)
        {
            _service.ActualizarEstado(id, estado);

            return Ok(new
            {
                mensaje = "Estado actualizado correctamente"
            });
        }
    }
}