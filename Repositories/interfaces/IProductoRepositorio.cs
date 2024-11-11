namespace repositorio.interfaz;
using productos.Models;
public interface IProductoRepositorio
{
    void CrearProducto(Producto producto);
    void ModificarProducto(int id, Producto producto);
    Producto ObtenerProductoPorId(int id);
   List<Producto> ListarProductos();
    void EliminarProducto(int id);
}