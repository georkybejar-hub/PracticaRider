using PracticaClase.Models;

namespace PracticaClase.Services;

public interface ICategoriaService
{
    Task<IEnumerable<Categoria>> GetAllAsync();
    Task<Categoria?> GetByIdAsync(int id);
    Task<Categoria> CreateAsync(Categoria entity);
    Task UpdateAsync(Categoria entity);
    Task DeleteAsync(int id);
}