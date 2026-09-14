using Codesi.Factura.Api.Services;
using Codesi.Factura.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace Codesi.Factura.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly ProductoService _service;

        public ProductosController(ProductoService service)
        {
            _service = service;
        }

        // GET: api/Productos
        [HttpGet]
        public IActionResult ObtenerProductos()
        {
            return Ok(_service.ObtenerProductos());
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public IActionResult ObtenerProducto(int id)
        {
            var producto = _service.ObtenerProductoPorId(id);

            if (producto == null)
                return NotFound(new
                {
                    mensaje = "Producto no encontrado"
                });

            return Ok(producto);
        }

        // GET: api/Productos/codigo/ABC123
        [HttpGet("codigo/{codigo}")]
        public IActionResult ObtenerPorCodigo(string codigo)
        {
            var producto = _service.ObtenerPorCodigo(codigo);

            if (producto == null)
                return NotFound(new
                {
                    mensaje = "Producto no encontrado"
                });

            return Ok(producto);
        }

        // GET: api/Productos/buscar/whisky
        [HttpGet("buscar/{nombre}")]
        public IActionResult BuscarPorNombre(string nombre)
        {
            return Ok(_service.BuscarPorNombre(nombre));
        }

        // POST: api/Productos
        [HttpPost]
        public IActionResult CrearProducto([FromBody] productos producto)
        {
            _service.CrearProducto(producto);

            return Ok(producto);
        }

        // PUT: api/Productos/5
        [HttpPut("{id}")]
        public IActionResult ActualizarProducto(
            int id,
            [FromBody] productos producto)
        {
            producto.id_producto = id;

            _service.ActualizarProducto(producto);

            return Ok(producto);
        }

        // PUT: api/Productos/5/desactivar
        [HttpPut("{id}/desactivar")]
        public IActionResult DesactivarProducto(int id)
        {
            _service.DesactivarProducto(id);

            return Ok(new
            {
                mensaje = "Producto desactivado correctamente"
            });
        }
    }
}