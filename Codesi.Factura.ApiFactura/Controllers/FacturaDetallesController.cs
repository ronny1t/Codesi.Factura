using Codesi.Factura.Api.Models;
using Codesi.Factura.Api.Services;
using Codesi.Factura.Persistencia.Models.Universidad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Codesi.Factura.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FacturaDetallesController : ControllerBase
    {
        private readonly FacturaDetalleService _service;

        public FacturaDetallesController(
            FacturaDetalleService service)
        {
            _service = service;
        }

        // GET: api/FacturaDetalles/factura/5
        // Todos los usuarios autenticados pueden consultar
        [HttpGet("factura/{idFactura}")]
        public IActionResult ObtenerPorFactura(int idFactura)
        {
            return Ok(
                _service.ObtenerPorFactura(idFactura)
            );
        }

        // POST: api/FacturaDetalles
        // Administrador y Facturación pueden crear detalles
        [Authorize(Roles = "Administrador,Facturacion")]
        [HttpPost]
        public IActionResult CrearDetalle(
            [FromBody] FacturaDetalleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var detalle = new FacturaDetalle
            {
                IdFactura = request.IdFactura,
                IdProducto = request.IdProducto,
                Cantidad = request.Cantidad,
                PrecioUnitario = request.PrecioUnitario,
                Descuento = request.Descuento ?? 0,
                Subtotal = request.Subtotal,
                ValorIva = request.ValorIva,
                Total = request.Total
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