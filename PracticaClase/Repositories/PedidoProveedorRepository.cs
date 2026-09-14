namespace PracticaClase.Repositories;

using Microsoft.EntityFrameworkCore;
using PracticaClase.Data;
using PracticaClase.Models;

public class PedidoProveedorRepository : IPedidoProveedorRepository
{
    private readonly InventarioContext _context;

    public PedidoProveedorRepository(InventarioContext context)
    {
        _context = context;
    }

    public async Task CrearPedidoAsync(PedidosProveedor pedido, List<DetallePedido> detalles)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            _context.PedidosProveedors.Add(pedido);
            await _context.SaveChangesAsync();

            foreach (var detalle in detalles)
            {
                detalle.IdPedido = pedido.IdPedido;
                _context.DetallePedidos.Add(detalle);
            }
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task RecibirPedidoAsync(int idPedido)
    {
        await _context.Database.ExecuteSqlRawAsync("CALL sp_recibir_pedido({0})", idPedido);
    }
}