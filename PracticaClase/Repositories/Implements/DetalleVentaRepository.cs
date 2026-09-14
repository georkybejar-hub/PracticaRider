using Microsoft.EntityFrameworkCore;
using PracticaClase.Data;
using PracticaClase.Models;
using PracticaClase.Repositories;

namespace PracticaClase.Repositories.Implements;

public class DetalleVentaRepository : GenericRepository<DetalleVenta>, IDetalleVentaRepository
{
    public DetalleVentaRepository(InventarioContext context) : base(context) { }

    public async Task<IEnumerable<DetalleVenta>> GetByVentaAsync(int idVenta) =>
        await _context.DetalleVentas.Where(x => x.IdVenta == idVenta).Include(x => x.Producto).ToListAsync();
}