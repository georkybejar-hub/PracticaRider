using PracticaClase.Models;

namespace PracticaClase.Services;

public interface IProveedorService
{
    Task<IEnumerable<Proveedore>> GetAllAsync();
    Task<Proveedore?> GetByIdAsync(int id);
    Task<Proveedore> CreateAsync(Proveedore entity);
    Task UpdateAsync(Proveedore entity);
    Task DesactivarAsync(int idProveedor);
}