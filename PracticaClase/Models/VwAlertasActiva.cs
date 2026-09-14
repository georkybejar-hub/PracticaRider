namespace PracticaClase.Models;

// Mapea la vista vw_alertas_activas (solo lectura)
public partial class VwAlertasActiva
{
    public int IdAlerta { get; set; }
    public int IdProducto { get; set; }
    public string Producto { get; set; } = null!;
    public int StockAlMomento { get; set; }
    public int StockMinimo { get; set; }
    public DateTime FechaAlerta { get; set; }
}