using Microsoft.EntityFrameworkCore;
using PracticaClase.Data;
using PracticaClase.Repositories;

namespace PracticaClase.Repositories.Implements;

public class ReadOnlyRepository<T> : IReadOnlyRepository<T> where T : class
{
    private readonly InventarioContext _context;

    public ReadOnlyRepository(InventarioContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().AsNoTracking().ToListAsync();
}