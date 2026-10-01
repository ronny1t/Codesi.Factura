using Codesi.Factura.Api.Services;
using Codesi.Factura.Persistencia.Models.Universidad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Codesi.Factura.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriasController : ControllerBase
    {
        private readonly CategoriaService _service;

        public CategoriasController(CategoriaService service)
        {
            _service = service;
        }

        // GET: api/Categorias
        // Administrador, Facturación, Bodega y Consulta pueden consultar
        [HttpGet]
        public IActionResult ObtenerCategorias()
        {
            var categorias = _service.ObtenerCategorias();

            return Ok(categorias);
        }

        // POST: api/Categorias
        // Solo Administrador
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public IActionResult CrearCategoria(
            [FromBody] Categoria categoria)
        {
            _service.CrearCategoria(categoria);

            return Ok(categoria);
        }

        // PUT: api/Categorias/5/desactivar
        // Solo Administrador
        [Authorize(Roles = "Administrador")]
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
        // Solo Administrador
        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}/activar")]
        public IActionResult ActivarCategoria(int id)
        {
            _service.ActivarCategoria(id);

            return Ok(new
            {
                mensaje = "Categoría activada correctamente"
            });
        }

        // PUT: api/Categorias/5
        // Solo Administrador
        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public IActionResult ActualizarCategoria(
            int id,
            [FromBody] Categoria categoria)
        {
            categoria.IdCategoria = id;

            _service.ActualizarCategoria(categoria);

            return Ok(categoria);
        }
    }
}