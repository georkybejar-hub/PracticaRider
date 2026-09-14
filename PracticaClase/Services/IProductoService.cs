using PracticaClase.Models;

namespace PracticaClase.Services;

public interface IProductoService
{
    Task<IEnumerable<Producto>> GetAllAsync();
    Task<Producto?> GetByIdAsync(int id);
    Task<IEnumerable<Producto>> GetActivosAsync();
    Task<IEnumerable<Producto>> GetPorCategoriaAsync(int idCategoria);
    Task<Producto> CreateAsync(Producto entity);
    Task UpdateAsync(Producto entity);
    Task DesactivarAsync(int idProducto);
}