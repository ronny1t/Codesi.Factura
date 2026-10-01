using Codesi.Factura.Api.Models;
using Codesi.Factura.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Codesi.Factura.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FacturasController : ControllerBase
    {
        private readonly FacturaService _service;
        private readonly EmailService _emailService;

        public FacturasController(
            FacturaService service,
            EmailService emailService)
        {
            _service = service;
            _emailService = emailService;
        }

        // ============================================
        // GET: api/Facturas
        // Todos los usuarios autenticados pueden consultar
        // ============================================

        [HttpGet]
        public IActionResult ObtenerFacturas()
        {
            return Ok(
                _service.ObtenerFacturas()
            );
        }

        // ============================================
        // GET: api/Facturas/5
        // Todos los usuarios autenticados pueden consultar
        // ============================================

        [HttpGet("{id}")]
        public IActionResult ObtenerFactura(int id)
        {
            var factura =
                _service.ObtenerFacturaPorId(id);

            if (factura == null)
            {
                return NotFound(new
                {
                    mensaje =
                        "Factura no encontrada"
                });
            }

            return Ok(factura);
        }

        // ============================================
        // POST: api/Facturas
        // Crear solamente cabecera
        // Administrador y Facturación
        // ============================================

        [Authorize(Roles = "Administrador,Facturacion")]
        [HttpPost]
        public IActionResult CrearFactura(
            [FromBody] FacturaCrearRequest request)
        {
            Console.WriteLine(
                "===================================="
            );

            Console.WriteLine(
                "POST api/Facturas"
            );

            Console.WriteLine(
                $"IdCliente recibido: {request.id_cliente}"
            );

            Console.WriteLine(
                $"Fecha: {request.fecha_emision}"
            );

            Console.WriteLine(
                $"Total: {request.importe_total}"
            );

            Console.WriteLine(
                "===================================="
            );

            if (request.id_cliente <= 0)
            {
                return BadRequest(new
                {
                    mensaje =
                        "El id_cliente recibido es 0 o inválido.",

                    id_cliente =
                        request.id_cliente
                });
            }

            var factura =
                new Codesi.Factura.Persistencia.Models.Universidad.Factura
                {
                    Establecimiento =
                        request.establecimiento,

                    PuntoEmision =
                        request.punto_emision,

                    Secuencial =
                        request.secuencial,

                    ClaveAcceso =
                        request.clave_acceso,

                    FechaEmision =
                        request.fecha_emision,

                    IdCliente =
                        request.id_cliente,

                    SubtotalSinImpuestos =
                        request.subtotal_sin_impuestos,

                    TotalDescuento =
                        request.total_descuento,

                    SubtotalIva =
                        request.subtotal_iva,

                    Propina =
                        request.propina,

                    ImporteTotal =
                        request.importe_total,

                    EstadoSri =
                        request.estado_sri
                };

            Console.WriteLine(
                $"IdCliente de factura: {factura.IdCliente}"
            );

            _service.CrearFactura(
                factura
            );

            // No devolvemos directamente la entidad EF.
            // Devolvemos únicamente datos simples para
            // evitar ciclos de navegación.

            return Ok(new
            {
                idFactura =
                    factura.IdFactura,

                establecimiento =
                    factura.Establecimiento,

                puntoEmision =
                    factura.PuntoEmision,

                secuencial =
                    factura.Secuencial,

                claveAcceso =
                    factura.ClaveAcceso,

                fechaEmision =
                    factura.FechaEmision,

                idCliente =
                    factura.IdCliente,

                subtotalSinImpuestos =
                    factura.SubtotalSinImpuestos,

                totalDescuento =
                    factura.TotalDescuento,

                subtotalIva =
                    factura.SubtotalIva,

                propina =
                    factura.Propina,

                importeTotal =
                    factura.ImporteTotal,

                estadoSri =
                    factura.EstadoSri
            });
        }

        // ============================================
        // POST: api/Facturas/completa
        //
        // Crea:
        // 1. Cabecera de factura
        // 2. Todos los detalles
        // 3. Pago
        //
        // Todo dentro de UNA sola transacción.
        // ============================================

        [Authorize(Roles = "Administrador,Facturacion")]
        [HttpPost("completa")]
        public IActionResult CrearFacturaCompleta(
            [FromBody] FacturaCompletaRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (request.id_cliente <= 0)
                {
                    return BadRequest(new
                    {
                        mensaje =
                            "El id_cliente recibido es 0 o inválido."
                    });
                }

                if (request.detalles == null ||
                    request.detalles.Count == 0)
                {
                    return BadRequest(new
                    {
                        mensaje =
                            "La factura debe tener al menos un detalle."
                    });
                }

                if (string.IsNullOrWhiteSpace(
                        request.forma_pago))
                {
                    return BadRequest(new
                    {
                        mensaje =
                            "La forma de pago es obligatoria."
                    });
                }

                Console.WriteLine(
                    "===================================="
                );

                Console.WriteLine(
                    "POST api/Facturas/completa"
                );

                Console.WriteLine(
                    $"Cliente: {request.id_cliente}"
                );

                Console.WriteLine(
                    $"Detalles: {request.detalles.Count}"
                );

                Console.WriteLine(
                    $"Forma de pago: {request.forma_pago}"
                );

                Console.WriteLine(
                    $"Total: {request.importe_total}"
                );

                Console.WriteLine(
                    "===================================="
                );

                var facturaCreada =
                    _service.CrearFacturaCompleta(
                        request
                    );

                // IMPORTANTE:
                //
                // NO devolver directamente facturaCreada.
                //
                // La entidad de Entity Framework contiene
                // relaciones navegacionales que pueden generar
                // referencias circulares:
                //
                // Factura
                //   -> FacturaDetalles
                //      -> IdFacturaNavigation
                //         -> FacturaDetalles
                //            -> IdFacturaNavigation
                //               -> ...
                //
                // Por eso devolvemos solamente los campos simples.

                return Ok(new
                {
                    idFactura =
                        facturaCreada.IdFactura,

                    establecimiento =
                        facturaCreada.Establecimiento,

                    puntoEmision =
                        facturaCreada.PuntoEmision,

                    secuencial =
                        facturaCreada.Secuencial,

                    claveAcceso =
                        facturaCreada.ClaveAcceso,

                    fechaEmision =
                        facturaCreada.FechaEmision,

                    idCliente =
                        facturaCreada.IdCliente,

                    subtotalSinImpuestos =
                        facturaCreada.SubtotalSinImpuestos,

                    totalDescuento =
                        facturaCreada.TotalDescuento,

                    subtotalIva =
                        facturaCreada.SubtotalIva,

                    propina =
                        facturaCreada.Propina,

                    importeTotal =
                        facturaCreada.ImporteTotal,

                    estadoSri =
                        facturaCreada.EstadoSri
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    mensaje =
                        ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje =
                        "No se pudo crear la factura.",

                    detalle =
                        ex.Message,

                    inner =
                        ex.InnerException?.Message
                });
            }
        }

        // ============================================
        // PUT: api/Facturas/5/estado/AUTORIZADA
        // Administrador y Facturación
        // ============================================

        [Authorize(Roles = "Administrador,Facturacion")]
        [HttpPut("{id}/estado/{estado}")]
        public IActionResult ActualizarEstado(
            int id,
            string estado)
        {
            _service.ActualizarEstado(
                id,
                estado
            );

            return Ok(new
            {
                mensaje =
                    "Estado actualizado correctamente"
            });
        }

        // ============================================
        // PUT: api/Facturas/5
        // Administrador y Facturación
        // ============================================

        [Authorize(Roles = "Administrador,Facturacion")]
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
                    _service.ObtenerFacturaPorId(
                        id
                    );

                if (factura == null)
                {
                    return NotFound(new
                    {
                        mensaje =
                            "Factura no encontrada"
                    });
                }

                if (!string.Equals(
                        factura.EstadoSri,
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
                    mensaje =
                        ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    mensaje =
                        ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje =
                        "No se pudo actualizar la factura.",

                    detalle =
                        ex.Message,

                    inner =
                        ex.InnerException?.Message
                });
            }
        }

        // ============================================
        // POST: api/Facturas/5/enviar-correo
        //
        // Recibe el PDF generado por Flutter
        // y lo envía al correo del cliente.
        //
        // Administrador y Facturación
        // ============================================

        [Authorize(Roles = "Administrador,Facturacion")]
        [HttpPost("{id}/enviar-correo")]
        public async Task<IActionResult> EnviarCorreo(
            int id,
            [FromForm] IFormFile pdf)
        {
            try
            {
                // ============================================
                // 1. Verificar que se recibió el archivo
                // ============================================

                if (pdf == null || pdf.Length == 0)
                {
                    return BadRequest(new
                    {
                        mensaje =
                            "No se recibió el PDF de la factura."
                    });
                }

                // ============================================
                // 2. Verificar extensión
                //
                // NO usamos ContentType porque Flutter/Android
                // puede enviar application/octet-stream aunque
                // el archivo sea realmente un PDF.
                // ============================================

                var extension =
                    Path.GetExtension(
                        pdf.FileName
                    );

                if (!string.Equals(
                        extension,
                        ".pdf",
                        StringComparison.OrdinalIgnoreCase))
                {
                    return BadRequest(new
                    {
                        mensaje =
                            "El archivo enviado debe tener extensión .pdf."
                    });
                }

                // ============================================
                // 3. Leer el archivo
                // ============================================

                byte[] pdfBytes;

                using (var memoryStream =
                       new MemoryStream())
                {
                    await pdf.CopyToAsync(
                        memoryStream
                    );

                    pdfBytes =
                        memoryStream.ToArray();
                }

                // ============================================
                // 4. Validar que realmente sea un PDF
                //
                // Un PDF comienza con:
                //
                // %PDF-
                //
                // En hexadecimal:
                //
                // 25 50 44 46 2D
                // ============================================

                if (pdfBytes.Length < 5 ||
                    pdfBytes[0] != 0x25 ||
                    pdfBytes[1] != 0x50 ||
                    pdfBytes[2] != 0x44 ||
                    pdfBytes[3] != 0x46 ||
                    pdfBytes[4] != 0x2D)
                {
                    return BadRequest(new
                    {
                        mensaje =
                            "El archivo recibido no contiene una estructura PDF válida."
                    });
                }

                Console.WriteLine(
                    "===================================="
                );

                Console.WriteLine(
                    "PDF RECIBIDO PARA ENVÍO POR CORREO"
                );

                Console.WriteLine(
                    $"Nombre: {pdf.FileName}"
                );

                Console.WriteLine(
                    $"Content-Type recibido: {pdf.ContentType}"
                );

                Console.WriteLine(
                    $"Tamaño: {pdfBytes.Length} bytes"
                );

                Console.WriteLine(
                    "PDF VALIDADO CORRECTAMENTE"
                );

                Console.WriteLine(
                    "===================================="
                );

                // ============================================
                // 5. Buscar factura
                // ============================================

                var factura =
                    _service.ObtenerFacturaPorId(
                        id
                    );

                if (factura == null)
                {
                    return NotFound(new
                    {
                        mensaje =
                            "Factura no encontrada."
                    });
                }

                // ============================================
                // 6. Obtener cliente
                // ============================================

                var cliente =
                    factura.IdClienteNavigation;

                if (cliente == null)
                {
                    return BadRequest(new
                    {
                        mensaje =
                            "La factura no tiene un cliente asociado."
                    });
                }

                // ============================================
                // 7. Verificar correo del cliente
                // ============================================

                if (string.IsNullOrWhiteSpace(
                        cliente.Email))
                {
                    return BadRequest(new
                    {
                        mensaje =
                            "El cliente no tiene un correo electrónico registrado."
                    });
                }

                // ============================================
                // 8. Datos del correo
                // ============================================

                var numeroFactura =
                    $"{factura.Establecimiento}-" +
                    $"{factura.PuntoEmision}-" +
                    $"{factura.Secuencial}";

                var nombreArchivo =
                    $"factura_{numeroFactura}.pdf";

                var asunto =
                    $"Factura {numeroFactura}";

                var mensaje =
                    $"Estimado/a {cliente.RazonSocial},\n\n" +
                    $"Adjuntamos la factura {numeroFactura} " +
                    $"correspondiente a su compra.\n\n" +
                    $"Valor total: " +
                    $"{factura.ImporteTotal:F2}\n\n" +
                    $"Gracias por su preferencia.\n\n" +
                    $"Sistema Académico de Facturación";

                // ============================================
                // 9. Enviar correo con PDF
                // ============================================

                await _emailService.EnviarCorreoConPdfAsync(
                    cliente.Email,
                    asunto,
                    mensaje,
                    pdfBytes,
                    nombreArchivo
                );

                // ============================================
                // 10. Respuesta exitosa
                // ============================================

                Console.WriteLine(
                    "===================================="
                );

                Console.WriteLine(
                    "CORREO ENVIADO CORRECTAMENTE"
                );

                Console.WriteLine(
                    $"Factura: {numeroFactura}"
                );

                Console.WriteLine(
                    $"Destinatario: {cliente.Email}"
                );

                Console.WriteLine(
                    "===================================="
                );

                return Ok(new
                {
                    mensaje =
                        "Factura enviada correctamente al correo del cliente.",

                    correo =
                        cliente.Email,

                    factura =
                        numeroFactura
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "===================================="
                );

                Console.WriteLine(
                    "ERROR AL ENVIAR FACTURA POR CORREO"
                );

                Console.WriteLine(
                    ex.Message
                );

                Console.WriteLine(
                    "===================================="
                );

                return BadRequest(new
                {
                    mensaje =
                        "No se pudo enviar la factura por correo.",

                    detalle =
                        ex.Message
                });
            }
        }
    }
}