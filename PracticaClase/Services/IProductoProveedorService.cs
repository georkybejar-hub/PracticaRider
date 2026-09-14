using PracticaClase.Models;

namespace PracticaClase.Services;

public interface IProductoProveedorService
{
    Task<IEnumerable<ProductosProveedore>> GetAllAsync();
    Task<ProductosProveedore?> GetAsync(int idProducto, int idProveedor);
    Task<ProductosProveedore> AsociarAsync(ProductosProveedore relacion);
    Task DesasociarAsync(int idProducto, int idProveedor);
}