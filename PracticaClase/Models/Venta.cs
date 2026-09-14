namespace PracticaClase.Models;

public partial class Venta
{
    public int IdVenta { get; set; }
    public DateTime FechaVenta { get; set; }
    public string? Cliente { get; set; }
    public decimal Total { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVentas { get; set; } = new List<DetalleVenta>();
}