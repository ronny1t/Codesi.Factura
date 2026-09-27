using Codesi.Factura.Api.Models;
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

        [HttpGet("factura/{idFactura}")]
        public IActionResult ObtenerPorFactura(int idFactura)
        {
            return Ok(
                _service.ObtenerPorFactura(idFactura)
            );
        }

        [HttpPost]
        public IActionResult CrearDetalle(
            [FromBody] FacturaDetalleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var detalle = new factura_detalles
            {
                id_factura = request.id_factura,
                id_producto = request.id_producto,
                cantidad = request.cantidad,
                precio_unitario = request.precio_unitario,
                descuento = request.descuento,
                subtotal = request.subtotal,
                valor_iva = request.valor_iva,
                total = request.total
            };

            try
            {
                _service.CrearDetalle(detalle);

                return Ok(detalle);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }
    }
}