namespace PracticaClase.Models;

// Mapea la vista vw_productos_mas_vendidos (solo lectura)
public partial class VwProductosMasVendido
{
    public int IdProducto { get; set; }
    public string Nombre { get; set; } = null!;
    public long? UnidadesVendidas { get; set; }
    public decimal? IngresosTotales { get; set; }
}