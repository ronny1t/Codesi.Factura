using Codesi.Factura.Api.Services;
using Codesi.Factura.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace Codesi.Factura.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturaPagosController : ControllerBase
    {
        private readonly FacturaPagoService _service;

        public FacturaPagosController(
            FacturaPagoService service)
        {
            _service = service;
        }

        // GET: api/FacturaPagos/factura/5
        [HttpGet("factura/{idFactura}")]
        public IActionResult ObtenerPorFactura(int idFactura)
        {
            return Ok(
                _service.ObtenerPorFactura(idFactura)
            );
        }

        // POST: api/FacturaPagos
        [HttpPost]
        public IActionResult CrearPago(
            [FromBody] factura_pagos pago)
        {
            _service.CrearPago(pago);

            return Ok(pago);
        }
    }
}