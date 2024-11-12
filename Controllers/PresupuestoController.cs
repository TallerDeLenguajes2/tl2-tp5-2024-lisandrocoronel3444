using Microsoft.AspNetCore.Mvc;
using productos.Models;
using repositorio.interfaz;
using System.Collections.Generic;

namespace productos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresupuestoController : ControllerBase
    {
        private readonly IPresupuestoRepositorio _presupuestoRepositorio;

        public PresupuestoController(IPresupuestoRepositorio presupuestoRepositorio)
        {
            _presupuestoRepositorio = presupuestoRepositorio;
        }

        // POST: api/Presupuesto
        [HttpPost]
        public IActionResult CrearPresupuesto([FromBody] Presupuesto presupuesto)
        {
            if (presupuesto == null || presupuesto.Detalle == null)
                return BadRequest("Datos del presupuesto incompletos.");

            _presupuestoRepositorio.CrearPresupuesto(presupuesto);
            return CreatedAtAction(nameof(ObtenerPresupuestoPorId), new { id = presupuesto.IdPresupuesto }, presupuesto);
        }

        // GET: api/Presupuesto/{id}
        [HttpGet("{id}")]
        public IActionResult ObtenerPresupuestoPorId(int id)
        {
            var presupuesto = _presupuestoRepositorio.ObtenerPresupuestoPorId(id);
            if (presupuesto == null)
                return NotFound("Presupuesto no encontrado.");

            return Ok(presupuesto);
        }

        // POST: api/Presupuesto/{id}/AgregarProducto
        [HttpPost("{id}/AgregarProducto")]
        public IActionResult AgregarProducto(int id, [FromBody] ProductoCantidadDto productoDto)
        {
            if (productoDto == null || productoDto.Cantidad <= 0)
                return BadRequest("Producto o cantidad no válidos.");

            var producto = new Producto
            {
                IdProducto = productoDto.IdProducto
            };

            _presupuestoRepositorio.AgregarProducto(id, producto, productoDto.Cantidad);
            return Ok("Producto agregado al presupuesto.");
        }

        // GET: api/Presupuesto
        [HttpGet]
        public IActionResult ListarPresupuestos()
        {
            var presupuestos = _presupuestoRepositorio.ListarPresupuestos();
            return Ok(presupuestos);
        }

        // DELETE: api/Presupuesto/{id}
        [HttpDelete("{id}")]
        public IActionResult EliminarPresupuesto(int id)
        {
            var presupuestoExistente = _presupuestoRepositorio.ObtenerPresupuestoPorId(id);
            if (presupuestoExistente == null)
                return NotFound("Presupuesto no encontrado.");

            _presupuestoRepositorio.EliminarPresupuesto(id);
            return Ok("Presupuesto eliminado.");
        }
    }

    // Clase auxiliar para manejar el envío de datos en AgregarProducto
    public class ProductoCantidadDto
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
    }
}