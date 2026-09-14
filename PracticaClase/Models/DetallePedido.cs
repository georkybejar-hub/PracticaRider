namespace PracticaClase.Models;

public partial class DetallePedido
{
    public int IdDetalle { get; set; }
    public int IdPedido { get; set; }
    public int IdProducto { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; } // columna generada en MySQL, solo lectura

    public virtual PedidosProveedor Pedido { get; set; } = null!;
    public virtual Producto Producto { get; set; } = null!;
}