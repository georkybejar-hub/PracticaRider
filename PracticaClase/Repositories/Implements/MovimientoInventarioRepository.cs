using Microsoft.EntityFrameworkCore;
using PracticaClase.Data;
using PracticaClase.Models;
using PracticaClase.Repositories;

namespace PracticaClase.Repositories.Implements;

public class MovimientoInventarioRepository : IMovimientoInventarioRepository
{
    private readonly InventarioContext _context;

    public MovimientoInventarioRepository(InventarioContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MovimientosInventario>> GetAllAsync() =>
        await _context.MovimientosInventarios.AsNoTracking().ToListAsync();

    public async Task<IEnumerable<MovimientosInventario>> GetByProductoAsync(int idProducto) =>
        await _context.MovimientosInventarios.Where(x => x.IdProducto == idProducto).AsNoTracking().ToListAsync();
}