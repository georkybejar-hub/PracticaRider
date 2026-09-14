namespace PracticaClase.Models;

public partial class Producto
{
    public int IdProducto { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public decimal Precio { get; set; }
    public int IdCategoria { get; set; }
    public int StockActual { get; set; }
    public int StockMinimo { get; set; }
    public bool? Activo { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaModificacion { get; set; }

    public virtual Categoria Categoria { get; set; } = null!;
    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();
    public virtual ICollection<DetalleVenta> DetalleVentas { get; set; } = new List<DetalleVenta>();
    public virtual ICollection<MovimientosInventario> MovimientosInventarios { get; set; } = new List<MovimientosInventario>();
    public virtual ICollection<AlertasStock> AlertasStocks { get; set; } = new List<AlertasStock>();
    public virtual ICollection<ProductosProveedore> ProductosProveedores { get; set; } = new List<ProductosProveedore>();
}