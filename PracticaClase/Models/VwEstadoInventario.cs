namespace PracticaClase.Models;

// Mapea la vista vw_estado_inventario (solo lectura, sin llave primaria real)
public partial class VwEstadoInventario
{
    public int IdProducto { get; set; }
    public string Nombre { get; set; } = null!;
    public string Categoria { get; set; } = null!;
    public int StockActual { get; set; }
    public int StockMinimo { get; set; }
    public decimal Precio { get; set; }
    public decimal ValorInventario { get; set; }
    public string EstadoStock { get; set; } = null!; // "OK" | "BAJO"
}