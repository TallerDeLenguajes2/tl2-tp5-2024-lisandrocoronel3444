namespace repositorio;
using System;
using System.Collections.Generic;
using repositorio.interfaz;
using productos.Models;


public class ProductoRepositorio : IProductoRepositorio
{
    
    
}

 /*   public Producto GetById(int id)
    {
        Producto producto = null;
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT Id, Nombre, Precio FROM Productos WHERE Id = @id";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        producto = new Producto
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre = reader["Nombre"].ToString(),
                            Precio = Convert.ToDecimal(reader["Precio"])
                        };
                    }
                }
            }
            connection.Close();

        }
        return producto;
    }

    public void Create(Producto producto)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "INSERT INTO Productos (Nombre, Precio) VALUES (@nombre, @precio)";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nombre", producto.Nombre);
                command.Parameters.AddWithValue("@precio", producto.Precio);
                command.ExecuteNonQuery();
            }
            connection.Close();

        }
    }

    public void Update(Producto producto)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "UPDATE Productos SET Nombre = @nombre, Precio = @precio WHERE Id = @id";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", producto.Id);
                command.Parameters.AddWithValue("@nombre", producto.Nombre);
                command.Parameters.AddWithValue("@precio", producto.Precio);
                command.ExecuteNonQuery();
            }
            connection.Close();

        }
    }

    public void Delete(int id)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "DELETE FROM Productos WHERE Id = @id";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
            connection.Close();

        }
    }
}
*/