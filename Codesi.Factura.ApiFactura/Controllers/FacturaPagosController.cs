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
    public class FacturaPagosController : ControllerBase
    {
        private readonly FacturaPagoService _service;

        public FacturaPagosController(
            FacturaPagoService service)
        {
            _service = service;
        }

        // GET: api/FacturaPagos/factura/5
        // Todos los usuarios autenticados pueden consultar
        [HttpGet("factura/{idFactura}")]
        public IActionResult ObtenerPorFactura(int idFactura)
        {
            return Ok(
                _service.ObtenerPorFactura(idFactura)
            );
        }

        // POST: api/FacturaPagos
        // Administrador y Facturación pueden crear pagos
        [Authorize(Roles = "Administrador,Facturacion")]
        [HttpPost]
        public IActionResult CrearPago(
            [FromBody] FacturaPagoRequest request)
        {
            var pago = new FacturaPago
            {
                IdFactura = request.IdFactura,
                FormaPago = request.FormaPago,
                Total = request.Total
            };

            try
            {
                _service.CrearPago(pago);

                return Ok(pago);
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