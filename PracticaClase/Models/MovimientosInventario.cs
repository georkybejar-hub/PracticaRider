namespace PracticaClase.Models;

public partial class MovimientosInventario
{
    public int IdMovimiento { get; set; }
    public int IdProducto { get; set; }
    public string TipoMovimiento { get; set; } = null!; // "ENTRADA" | "SALIDA"
    public int Cantidad { get; set; }
    public string Motivo { get; set; } = null!;
    public string? ReferenciaTipo { get; set; }
    public int? ReferenciaId { get; set; }
    public int StockResultante { get; set; }
    public DateTime FechaMovimiento { get; set; }

    public virtual Producto Producto { get; set; } = null!;
}