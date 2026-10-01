using Codesi.Factura.Api.Services;
using Codesi.Factura.Persistencia.Models.Universidad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Codesi.Factura.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductosController : ControllerBase
    {
        private readonly ProductoService _service;

        public ProductosController(
            ProductoService service)
        {
            _service = service;
        }

        // ============================================================
        // CONSULTAR PRODUCTOS ACTIVOS
        // Cualquier usuario autenticado
        // ============================================================

        // GET: api/Productos
        [HttpGet]
        public IActionResult ObtenerProductos()
        {
            return Ok(
                _service.ObtenerProductos()
            );
        }

        // ============================================================
        // CONSULTAR PRODUCTOS DESACTIVADOS
        // Administrador y Bodega
        // ============================================================

        // GET: api/Productos/desactivados
        [Authorize(Roles = "Administrador,Bodega")]
        [HttpGet("desactivados")]
        public IActionResult ObtenerProductosDesactivados()
        {
            return Ok(
                _service.ObtenerProductosDesactivados()
            );
        }

        // ============================================================
        // CONSULTAR PRODUCTO POR ID
        // Cualquier usuario autenticado
        // ============================================================

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public IActionResult ObtenerProducto(int id)
        {
            var producto =
                _service.ObtenerProductoPorId(id);

            if (producto == null)
            {
                return NotFound(new
                {
                    mensaje = "Producto no encontrado"
                });
            }

            return Ok(producto);
        }

        // ============================================================
        // BUSCAR PRODUCTOS ACTIVOS
        // Cualquier usuario autenticado
        // ============================================================

        // GET: api/Productos/buscar/whisky
        [HttpGet("buscar/{texto}")]
        public IActionResult Buscar(string texto)
        {
            return Ok(
                _service.Buscar(texto)
            );
        }

        // ============================================================
        // CREAR PRODUCTO
        // Administrador y Bodega
        // ============================================================

        // POST: api/Productos
        [Authorize(Roles = "Administrador,Bodega")]
        [HttpPost]
        public IActionResult CrearProducto(
            [FromBody] Producto producto)
        {
            try
            {
                _service.CrearProducto(producto);

                return Ok(producto);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // ============================================================
        // ACTUALIZAR PRODUCTO
        // Administrador y Bodega
        // ============================================================

        // PUT: api/Productos/5
        [Authorize(Roles = "Administrador,Bodega")]
        [HttpPut("{id}")]
        public IActionResult ActualizarProducto(
            int id,
            [FromBody] Producto producto)
        {
            try
            {
                producto.IdProducto = id;

                _service.ActualizarProducto(producto);

                return Ok(producto);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // ============================================================
        // DESACTIVAR PRODUCTO
        // Administrador y Bodega
        // ============================================================

        // PUT: api/Productos/5/desactivar
        [Authorize(Roles = "Administrador,Bodega")]
        [HttpPut("{id}/desactivar")]
        public IActionResult DesactivarProducto(int id)
        {
            try
            {
                _service.DesactivarProducto(id);

                return Ok(new
                {
                    mensaje =
                        "Producto desactivado correctamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // ============================================================
        // ACTIVAR PRODUCTO
        // Administrador y Bodega
        // ============================================================

        // PUT: api/Productos/5/activar
        [Authorize(Roles = "Administrador,Bodega")]
        [HttpPut("{id}/activar")]
        public IActionResult ActivarProducto(int id)
        {
            try
            {
                _service.ActivarProducto(id);

                return Ok(new
                {
                    mensaje =
                        "Producto activado correctamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }
    }
}