using PracticaClase.Models;

namespace PracticaClase.Services;

public interface IPedidoProveedorService
{
    Task<IEnumerable<PedidosProveedor>> GetAllAsync();
    Task<PedidosProveedor?> GetByIdAsync(int id);
    Task<IEnumerable<DetallePedido>> GetDetalleAsync(int idPedido);
    Task<PedidosProveedor> CrearPedidoAsync(PedidosProveedor pedido, List<DetallePedido> lineas);
    Task RecibirPedidoAsync(int idPedido);
    Task CancelarPedidoAsync(int idPedido);
}