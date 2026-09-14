using Codesi.Factura.Api.Services;
using Codesi.Factura.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace Codesi.Factura.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly CategoriaService _service;

        public CategoriasController(CategoriaService service)
        {
            _service = service;
        }

        // GET: api/Categorias
        [HttpGet]
        public IActionResult ObtenerCategorias()
        {
            var categorias = _service.ObtenerCategorias();

            return Ok(categorias);
        }

        // POST: api/Categorias
        [HttpPost]
        public IActionResult CrearCategoria([FromBody] categorias categoria)
        {
            _service.CrearCategoria(categoria);

            return Ok(categoria);
        }

        // PUT: api/Categorias/5/desactivar
        [HttpPut("{id}/desactivar")]
        public IActionResult DesactivarCategoria(int id)
        {
            _service.DesactivarCategoria(id);

            return Ok(new
            {
                mensaje = "Categoría desactivada correctamente"
            });
        }

        // PUT: api/Categorias/5/activar
        [HttpPut("{id}/activar")]
        public IActionResult ActivarCategoria(int id)
        {
            _service.ActivarCategoria(id);

            return Ok(new
            {
                mensaje = "Categoría activada correctamente"
            });
        }
    }
}