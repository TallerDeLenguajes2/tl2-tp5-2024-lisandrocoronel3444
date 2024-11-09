using System.Collections.Generic;
using productos.Models;

public interface IPresupuestoRepositorio
{
    void CrearPresupuesto(Presupuesto presupuesto);

    List<Presupuesto> ListarPresupuestos();

    Presupuesto ObtenerPresupuestoPorId(int id);

    void AgregarProducto(int presupuestoId, Producto producto, int cantidad);

    void EliminarPresupuesto(int id);
}