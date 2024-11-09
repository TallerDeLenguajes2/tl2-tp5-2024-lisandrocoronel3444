namespace productos.Models;
public class Presupuesto{
    public int IdPresupuesto {get; set;} 

    public string NombreDestinatario = string.Empty;

    public List<PresupuestoDetalle> Detalle {get; set;} 


    public void MontoPresupuesto(){

    }

    public void MontoPresupuestoConIva(){

    }

    public void CantidadProductos(){

    }
}