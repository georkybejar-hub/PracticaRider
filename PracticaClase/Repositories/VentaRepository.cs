namespace PracticaClase.Repositories;

using Microsoft.EntityFrameworkCore;
using PracticaClase.Data;
using PracticaClase.Models;

public class VentaRepository : IVentaRepository
{
    private readonly InventarioContext _context;

    public VentaRepository(InventarioContext context)
    {
        _context = context;
    }

    public async Task RegistrarVentaAsync(Venta venta, List<DetalleVenta> detalles)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();

            foreach (var detalle in detalles)
            {
                detalle.IdVenta = venta.IdVenta;
                _context.DetalleVentas.Add(detalle);
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

    public async Task<IEnumerable<VwProductosMasVendido>> ObtenerProductosMasVendidosAsync()
    {
        return await _context.VwProductosMasVendidos.ToListAsync();
    }
}