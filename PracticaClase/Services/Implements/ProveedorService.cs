using PracticaClase.Models;
using PracticaClase.Repositories;
using PracticaClase.Services;

namespace PracticaClase.Services.Implements;

public class ProveedorService : IProveedorService
{
    private readonly IProveedorRepository _repository;

    public ProveedorService(IProveedorRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Proveedore>> GetAllAsync() => _repository.GetAllAsync();

    public Task<Proveedore?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

    public async Task<Proveedore> CreateAsync(Proveedore entity)
    {
        entity.Activo = true;
        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(Proveedore entity)
    {
        _repository.Update(entity);
        await _repository.SaveChangesAsync();
    }

    public async Task DesactivarAsync(int idProveedor)
    {
        var proveedor = await _repository.GetByIdAsync(idProveedor)
                        ?? throw new KeyNotFoundException($"No existe el proveedor {idProveedor}.");

        proveedor.Activo = false;
        _repository.Update(proveedor);
        await _repository.SaveChangesAsync();
    }
}