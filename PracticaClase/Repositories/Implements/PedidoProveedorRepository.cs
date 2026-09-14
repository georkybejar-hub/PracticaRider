using Microsoft.EntityFrameworkCore;
using PracticaClase.Data;
using PracticaClase.Models;
using PracticaClase.Repositories;

namespace PracticaClase.Repositories.Implements;

public class PedidoProveedorRepository : GenericRepository<PedidosProveedor>, IPedidoProveedorRepository
{
    public PedidoProveedorRepository(InventarioContext context) : base(context) { }

    public async Task RecibirPedidoAsync(int idPedido)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync($"CALL sp_recibir_pedido({idPedido})");
    }
}