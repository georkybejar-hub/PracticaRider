using PracticaClase.Models;
using PracticaClase.Repositories;
using PracticaClase.Services;

namespace PracticaClase.Services.Implements;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _repository;

    public CategoriaService(ICategoriaRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Categoria>> GetAllAsync() => _repository.GetAllAsync();

    public Task<Categoria?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

    public async Task<Categoria> CreateAsync(Categoria entity)
    {
        var existentes = await _repository.GetAllAsync();
        if (existentes.Any(c => c.Nombre.Equals(entity.Nombre, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException($"Ya existe una categoría llamada '{entity.Nombre}'.");

        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(Categoria entity)
    {
        _repository.Update(entity);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var categoria = await _repository.GetByIdAsync(id)
                        ?? throw new KeyNotFoundException($"No existe la categoría {id}.");

        _repository.Delete(categoria);
        await _repository.SaveChangesAsync();
    }
}