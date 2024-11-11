namespace repositorio;
using System;
using System.Collections.Generic;
using repositorio.interfaz;
using productos.Models;
using Microsoft.Data.Sqlite;

public class ProductoRepositorio : IProductoRepositorio
{
    public void CrearProducto(Producto producto)
    {
        string consulta = "INSERT INTO Productos (Descripcion, Precio) VALUES (@Descripcion, @Precio)";
        string cadenaDeConexion = "Data Source=Tienda.db;Cache=Shared";

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();
            SqliteCommand command = new SqliteCommand(consulta, conexion);

            // Agregar parámetros de forma segura
            command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
            command.Parameters.AddWithValue("@Precio", producto.Precio);

            // Ejecutar la consulta para insertar el producto
            command.ExecuteNonQuery();
            conexion.Close();
        }
    }
    public void ModificarProducto(int id, Producto producto)
    {
        string consulta = "UPDATE Productos SET Descripcion = @descripcion, Precio = @precio WHERE idProducto = @idProducto";
        string cadenaDeConexion = "Data Source=Tienda.db;Cache=Shared";

        using (var conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();

            // Crear el comando SQL y asignar los valores a los parámetros
            using (var comando = new SqliteCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                comando.Parameters.AddWithValue("@precio", producto.Precio);
                comando.Parameters.AddWithValue("@idProducto", id);

                // Ejecutar la consulta
                int filasAfectadas = comando.ExecuteNonQuery();
                Console.WriteLine($"Filas actualizadas: {filasAfectadas}");
            }
            conexion.Close();
        }
    }
    public Producto ObtenerProductoPorId(int idProducto)
    {
        string consulta = "SELECT idProducto, descripcion, precio FROM Productos WHERE idProducto = @idProducto";
        string cadenaDeConexion = "Data Source=Tienda.db;Cache=Shared";

        using (var conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();

            using (var comando = new SqliteCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@idProducto", idProducto);

                using (var reader = comando.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Producto
                        {
                            IdProducto = reader.GetInt32(0),
                            Descripcion = reader.GetString(1),
                            Precio = reader.GetInt32(2)
                        };
                    }
                    else
                    {
                        return null; // Si no se encuentra el producto, retorna null
                    }
                }
            }
        }
    }

    public List<Producto> ListarProductos()
    {
        List<Producto> productos = new List<Producto>();
        string cadenaDeConexion = "Data Source=Tienda.db;Cache=Shared";

        using (var conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();

            string consulta = "SELECT idProducto, descripcion, precio FROM Tienda";

            using (var comando = new SqliteCommand(consulta, conexion))
            {
                using (var reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Producto producto = new Producto
                        {
                            IdProducto = reader.GetInt32(0),
                            Descripcion = reader.GetString(1),
                            Precio = reader.GetInt32(2)
                        };

                        productos.Add(producto);
                    }
                }
            }
        }

        return productos;
    }
    public void EliminarProducto(int idProducto)
{
    string cadenaDeConexion = "Data Source=Tienda.db;Cache=Shared";

    using (var conexion = new SqliteConnection(cadenaDeConexion))
    {
        conexion.Open();

        // Consulta SQL para eliminar el producto con el idProducto dado
        string consulta = "DELETE FROM Productos WHERE idProducto = @idProducto";

        using (var comando = new SqliteCommand(consulta, conexion))
        {
            // Agregar el parámetro para evitar inyecciones SQL
            comando.Parameters.AddWithValue("@idProducto", idProducto);

            // Ejecutar la consulta
            int filasAfectadas = comando.ExecuteNonQuery();

            if (filasAfectadas == 0)
            {
                // Si no se eliminó ninguna fila, el producto no existe
                throw new Exception("No se encontró el producto con el ID proporcionado.");
            }
        }
    }
}

}

