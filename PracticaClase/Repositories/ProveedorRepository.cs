namespace PracticaClase.Repositories;

using Microsoft.EntityFrameworkCore;
using PracticaClase.Data;
using PracticaClase.Models;

public class ProveedorRepository : IProveedorRepository
{
    private readonly InventarioContext _context;

    public ProveedorRepository(InventarioContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Proveedore>> ObtenerProveedoresAsync()
    {
        return await _context.Proveedores.Where(p => p.Activo == true).ToListAsync();
    }

    public async Task<Proveedore?> ObtenerPorIdAsync(int id)
    {
        return await _context.Proveedores.FindAsync(id);
    }

    public async Task AgregarAsync(Proveedore proveedor)
    {
        _context.Proveedores.Add(proveedor);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Proveedore proveedor)
    {
        _context.Proveedores.Update(proveedor);
        await _context.SaveChangesAsync();
    }
}