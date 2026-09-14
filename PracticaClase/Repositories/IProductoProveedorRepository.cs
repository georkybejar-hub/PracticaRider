using PracticaClase.Models;

namespace PracticaClase.Repositories;

// Tabla productos_proveedores: llave compuesta (id_producto + id_proveedor),
// no encaja en el genérico IRepository<T>.
public interface IProductoProveedorRepository
{
    Task<IEnumerable<ProductosProveedore>> GetAllAsync();
    Task<ProductosProveedore?> GetByIdsAsync(int idProducto, int idProveedor);
    Task AddAsync(ProductosProveedore entity);
    void Delete(ProductosProveedore entity);
    Task<int> SaveChangesAsync();
}