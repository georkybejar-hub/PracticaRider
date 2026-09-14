using PracticaClase.Models;

namespace PracticaClase.Repositories;

public interface IPedidoProveedorRepository : IRepository<PedidosProveedor>
{
    Task RecibirPedidoAsync(int idPedido);
}