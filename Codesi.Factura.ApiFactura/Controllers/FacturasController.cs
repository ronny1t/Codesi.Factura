using Codesi.Factura.Api.Services;
using Codesi.Factura.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using Codesi.Factura.Api.Models;

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

        // PUT: api/Facturas/5
        [HttpPut("{id}")]
        public IActionResult ActualizarFactura(
            int id,
            [FromBody] FacturaActualizarRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var factura =
                    _service.ObtenerFacturaPorId(id);

                if (factura == null)
                {
                    return NotFound(new
                    {
                        mensaje = "Factura no encontrada"
                    });
                }

                if (!string.Equals(
                        factura.estado_sri,
                        "CREADA",
                        StringComparison.OrdinalIgnoreCase))
                {
                    return Conflict(new
                    {
                        mensaje =
                            "Solo se pueden editar facturas en estado CREADA."
                    });
                }

                _service.ActualizarFactura(
                    id,
                    request
                );

                return Ok(new
                {
                    mensaje =
                        "Factura actualizada correctamente"
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    mensaje = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    mensaje = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = "No se pudo actualizar la factura.",
                    detalle = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }
    }
}