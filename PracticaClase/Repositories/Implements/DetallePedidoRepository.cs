using Microsoft.EntityFrameworkCore;
using PracticaClase.Data;
using PracticaClase.Models;
using PracticaClase.Repositories;

namespace PracticaClase.Repositories.Implements;

public class DetallePedidoRepository : GenericRepository<DetallePedido>, IDetallePedidoRepository
{
    public DetallePedidoRepository(InventarioContext context) : base(context) { }

    public async Task<IEnumerable<DetallePedido>> GetByPedidoAsync(int idPedido) =>
        await _context.DetallePedidos.Where(x => x.IdPedido == idPedido).Include(x => x.Producto).ToListAsync();
}