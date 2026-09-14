using PracticaClase.Models;

namespace PracticaClase.Repositories;

public interface IDetallePedidoRepository : IRepository<DetallePedido>
{
    Task<IEnumerable<DetallePedido>> GetByPedidoAsync(int idPedido);
}