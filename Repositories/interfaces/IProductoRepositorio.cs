namespace repositorio.interfaz;
using productos.Models;
public interface IProductoRepositorio
{
    void CrearProducto(Producto producto);
    void ModificarProducto(int id, Producto producto);
    List<Producto> ListarProductos();
    Producto ObtenerProductoPorId(int id);
    void EliminarProducto(int id);
}