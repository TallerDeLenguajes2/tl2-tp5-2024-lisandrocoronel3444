using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using productos.Models;
using repositorio.interfaz;

public class PresupuestoRepositorio : IPresupuestoRepositorio
{
    private readonly string cadenaDeConexion = "Data Source=Tienda.db;Cache=Shared";

    // Método para crear un presupuesto
    public void CrearPresupuesto(Presupuesto presupuesto)
    {
        string consultaPresupuesto = "INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) VALUES (@NombreDestinatario, @FechaCreacion)";
        string consultaObtenerId = "SELECT last_insert_rowid()";

        using (var conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();
            int idPresupuestoCreado;

            // Insertar el presupuesto
            using (var comandoPresupuesto = new SqliteCommand(consultaPresupuesto, conexion))
            {
                comandoPresupuesto.Parameters.AddWithValue("@NombreDestinatario", presupuesto.NombreDestinatario);
                comandoPresupuesto.Parameters.AddWithValue("@FechaCreacion", DateTime.Now);
                comandoPresupuesto.ExecuteNonQuery();
            }

            // Obtener el ID del presupuesto recién insertado
            using (var comandoObtenerId = new SqliteCommand(consultaObtenerId, conexion))
            {
                idPresupuestoCreado = Convert.ToInt32(comandoObtenerId.ExecuteScalar());
            }

            // Insertar los detalles del presupuesto
            foreach (var detalle in presupuesto.Detalle)
            {
                string consultaDetalle = "INSERT INTO PresupuestoDetalle (idPresupuesto, idProducto, Cantidad) VALUES (@idPresupuesto, @idProducto, @cantidad)";
                using (var comandoDetalle = new SqliteCommand(consultaDetalle, conexion))
                {
                    comandoDetalle.Parameters.AddWithValue("@idPresupuesto", idPresupuestoCreado);
                    comandoDetalle.Parameters.AddWithValue("@idProducto", detalle.Producto.IdProducto);
                    comandoDetalle.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                    comandoDetalle.ExecuteNonQuery();
                }
            }
        }
    }

    // Método para obtener un presupuesto por su ID
    public Presupuesto ObtenerPresupuestoPorId(int id)
    {
        Presupuesto presupuesto = null;

        using (var conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();

            // Obtener el presupuesto principal
            string consultaPresupuesto = "SELECT idPresupuesto, NombreDestinatario, FechaCreacion FROM Presupuestos WHERE idPresupuesto = @idPresupuesto";
            using (var comandoPresupuesto = new SqliteCommand(consultaPresupuesto, conexion))
            {
                comandoPresupuesto.Parameters.AddWithValue("@idPresupuesto", id);
                using (var reader = comandoPresupuesto.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        presupuesto = new Presupuesto
                        {
                            IdPresupuesto = reader.GetInt32(0),
                            NombreDestinatario = reader.GetString(1),
                            FechaCreacion = reader.GetDateTime(2),
                            Detalle = new List<PresupuestoDetalle>()
                        };

                        // Obtener los productos (detalles) asociados al presupuesto
                        string consultaDetalles = "SELECT pd.idProducto, pd.Cantidad, p.Descripcion FROM PresupuestoDetalle pd " +
                                                  "JOIN Productos p ON pd.idProducto = p.IdProducto WHERE pd.idPresupuesto = @idPresupuesto";
                        using (var comandoDetalles = new SqliteCommand(consultaDetalles, conexion))
                        {
                            comandoDetalles.Parameters.AddWithValue("@idPresupuesto", id);
                            using (var readerDetalle = comandoDetalles.ExecuteReader())
                            {
                                while (readerDetalle.Read())
                                {
                                    presupuesto.Detalle.Add(new PresupuestoDetalle
                                    {
                                        Producto = new Producto
                                        {
                                            IdProducto = readerDetalle.GetInt32(0),
                                            Descripcion = readerDetalle.GetString(2)
                                        },
                                        Cantidad = readerDetalle.GetInt32(1)
                                    });
                                }
                            }
                        }
                    }
                }
            }
        }
        return presupuesto;
    }

    // Método para agregar un producto al presupuesto
    public void AgregarProducto(int presupuestoId, Producto producto, int cantidad)
    {
        string consulta = "INSERT INTO PresupuestoDetalle (idPresupuesto, idProducto, Cantidad) VALUES (@idPresupuesto, @idProducto, @Cantidad)";

        using (var conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();
            using (var comando = new SqliteCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@idPresupuesto", presupuestoId);
                comando.Parameters.AddWithValue("@idProducto", producto.IdProducto);
                comando.Parameters.AddWithValue("@Cantidad", cantidad);
                comando.ExecuteNonQuery();
            }
        }
    }

    // Método para listar todos los presupuestos
    public List<Presupuesto> ListarPresupuestos()
    {
        List<Presupuesto> presupuestos = new List<Presupuesto>();

        using (var conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();
            string consulta = "SELECT idPresupuesto, NombreDestinatario, FechaCreacion FROM Presupuestos";

            using (var comando = new SqliteCommand(consulta, conexion))
            {
                using (var reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var presupuesto = new Presupuesto
                        {
                            IdPresupuesto = reader.GetInt32(0),
                            NombreDestinatario = reader.GetString(1),
                            FechaCreacion = reader.GetDateTime(2),
                            Detalle = new List<PresupuestoDetalle>()
                        };

                        presupuestos.Add(presupuesto);
                    }
                }
            }
        }
        return presupuestos;
    }

    // Método para eliminar un presupuesto
    public void EliminarPresupuesto(int id)
    {
        using (var conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();
            string consulta = "DELETE FROM Presupuestos WHERE idPresupuesto = @idPresupuesto";

            using (var comando = new SqliteCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@idPresupuesto", id);
                comando.ExecuteNonQuery();
            }
        }
    }
}