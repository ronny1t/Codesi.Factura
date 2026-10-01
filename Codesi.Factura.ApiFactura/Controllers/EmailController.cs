using Codesi.Factura.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Codesi.Factura.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly EmailService _emailService;

        public EmailController(
            EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("prueba")]
        public async Task<IActionResult> Prueba(
            [FromQuery] string correo)
        {
            try
            {
                await _emailService.EnviarCorreoAsync(
                    correo,
                    "Prueba de Codesi Factura",
                    "Este es un correo de prueba enviado desde la API de Codesi Factura."
                );

                return Ok(new
                {
                    mensaje =
                        "Correo enviado correctamente."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje =
                        "No se pudo enviar el correo.",
                    detalle =
                        ex.Message
                });
            }
        }
    }
}