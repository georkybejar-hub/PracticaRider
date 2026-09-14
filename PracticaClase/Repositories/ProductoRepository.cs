namespace PracticaClase.Repositories;

using Microsoft.EntityFrameworkCore;
using PracticaClase.Data;
using PracticaClase.Models;

public class ProductoRepository : IProductoRepository
{
    private readonly InventarioContext _context;

    public ProductoRepository(InventarioContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Producto>> ObtenerTodosAsync()
    {
        return await _context.Productos.Where(p => p.Activo == true).Include(p => p.IdCategoriaNavigation).ToListAsync();
    }

    public async Task<Producto?> ObtenerPorIdAsync(int id)
    {
        return await _context.Productos.Include(p => p.IdCategoriaNavigation).FirstOrDefaultAsync(p => p.IdProducto == id);
    }

    public async Task AgregarAsync(Producto producto)
    {
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Producto producto)
    {
        _context.Productos.Update(producto);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<VwEstadoInventario>> ObtenerEstadoInventarioAsync()
    {
        return await _context.VwEstadoInventarios.ToListAsync();
    }

    public async Task<IEnumerable<VwAlertasActiva>> ObtenerAlertasActivasAsync()
    {
        return await _context.VwAlertasActivas.ToListAsync();
    }
}