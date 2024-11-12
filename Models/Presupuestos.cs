namespace productos.Models;
public class Presupuesto
{
    public int IdPresupuesto { get; set; }

    public string NombreDestinatario = string.Empty;

    public List<PresupuestoDetalle> Detalle { get; set; }

    public DateTime FechaCreacion { get; set; }

    public decimal MontoPresupuesto()
    {
        return Detalle.Sum(d => d.Producto.Precio * d.Cantidad);
    }

    public decimal MontoPresupuestoConIva()
    {
        const decimal iva = 0.21m; // 21% IVA, puedes cambiarlo según tus necesidades
        return MontoPresupuesto() * (1 + iva);
    }

    public int CantidadProductos()
    {
        return Detalle.Sum(d => d.Cantidad);
    }
}