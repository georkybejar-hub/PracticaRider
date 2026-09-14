namespace PracticaClase.Repositories;

using PracticaClase.Models;

public interface IPedidoProveedorRepository
{
    Task CrearPedidoAsync(PedidosProveedor pedido, List<DetallePedido> detalles);
    Task RecibirPedidoAsync(int idPedido);
}