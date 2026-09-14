using Microsoft.EntityFrameworkCore;
using PracticaClase.Data;
using PracticaClase.Models;
using PracticaClase.Repositories;

namespace PracticaClase.Repositories.Implements;

public class ProductoProveedorRepository : IProductoProveedorRepository
{
    private readonly InventarioContext _context;

    public ProductoProveedorRepository(InventarioContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductosProveedore>> GetAllAsync() =>
        await _context.ProductosProveedores.Include(x => x.Producto).Include(x => x.Proveedor).ToListAsync();

    public async Task<ProductosProveedore?> GetByIdsAsync(int idProducto, int idProveedor) =>
        await _context.ProductosProveedores.FindAsync(idProducto, idProveedor);

    public async Task AddAsync(ProductosProveedore entity) =>
        await _context.ProductosProveedores.AddAsync(entity);

    public void Delete(ProductosProveedore entity) => _context.ProductosProveedores.Remove(entity);

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
}