using Microsoft.AspNetCore.Mvc;
using repositorio;
using productos.Models;
namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductosController : ControllerBase
{
    private readonly ProductoRepositorio _productoRepositorio;

    public ProductosController()
    {
        _productoRepositorio = new ProductoRepositorio();
    }

    [HttpPost]
    [Route("crear")]
    public IActionResult CrearProducto([FromBody] Producto producto)
    {
        if (producto == null)
        {
            return BadRequest("El producto no puede ser nulo.");
        }

        _productoRepositorio.CrearProducto(producto);
        return Ok("Producto creado con éxito.");
    }

    [HttpPut]
    [Route("actualizar/{idProducto}")]
    public IActionResult ActualizarProducto(int idProducto, [FromBody] Producto producto)
    {
        if (producto == null)
        {
            return BadRequest("El producto no puede ser nulo.");
        }

        // Verificar si el producto con el idProducto existe, si no, retornar error.
        var productoExistente = _productoRepositorio.ObtenerProductoPorId(idProducto);
        if (productoExistente == null)
        {
            return NotFound($"Producto con ID {idProducto} no encontrado.");
        }

        // Actualizar el producto usando los nuevos datos
        _productoRepositorio.ModificarProducto(idProducto, producto);

        return Ok("Producto actualizado con éxito.");
    }

    [HttpGet]
    [Route("listar")]
    public IActionResult ListarProductos()
    {
        var productos = _productoRepositorio.ListarProductos();

        if (productos == null || productos.Count == 0)
        {
            return NotFound("No se encontraron productos.");
        }

        return Ok(productos);
    }
    [HttpDelete]
    [Route("eliminar/{idProducto}")]
    public IActionResult EliminarProducto(int idProducto)
    {
        try
        {
            // Llamar al método EliminarProducto en el repositorio
            _productoRepositorio.EliminarProducto(idProducto);

            // Retornar un mensaje de éxito si el producto fue eliminado
            return Ok($"Producto con ID {idProducto} eliminado con éxito.");
        }
        catch (Exception ex)
        {
            // En caso de error (producto no encontrado o cualquier otro problema)
            return NotFound(ex.Message);
        }
    }


    // Puedes agregar otros métodos si es necesario (como para obtener productos)
}
