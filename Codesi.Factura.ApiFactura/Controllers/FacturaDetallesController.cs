using Codesi.Factura.Api.Services;
using Codesi.Factura.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace Codesi.Factura.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturaDetallesController : ControllerBase
    {
        private readonly FacturaDetalleService _service;

        public FacturaDetallesController(
            FacturaDetalleService service)
        {
            _service = service;
        }

        // GET: api/FacturaDetalles/factura/5
        [HttpGet("factura/{idFactura}")]
        public IActionResult ObtenerPorFactura(int idFactura)
        {
            return Ok(
                _service.ObtenerPorFactura(idFactura)
            );
        }

        // POST: api/FacturaDetalles
        [HttpPost]
        public IActionResult CrearDetalle(
            [FromBody] factura_detalles detalle)
        {
            _service.CrearDetalle(detalle);

            return Ok(detalle);
        }
    }
}