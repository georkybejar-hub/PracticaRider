using PracticaClase.Models;
using PracticaClase.Repositories;
using PracticaClase.Services;

namespace PracticaClase.Services.Implements;

public class ProductoService : IProductoService
{
    private readonly IProductoRepository _repository;

    public ProductoService(IProductoRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Producto>> GetAllAsync() => _repository.GetAllAsync();

    public Task<Producto?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

    public async Task<IEnumerable<Producto>> GetActivosAsync()
    {
        var productos = await _repository.GetAllAsync();
        return productos.Where(p => p.Activo == true);
    }

    public async Task<IEnumerable<Producto>> GetPorCategoriaAsync(int idCategoria)
    {
        var productos = await _repository.GetAllAsync();
        return productos.Where(p => p.IdCategoria == idCategoria);
    }

    public async Task<Producto> CreateAsync(Producto entity)
    {
        if (entity.Precio < 0)
            throw new ArgumentException("El precio no puede ser negativo.");
        if (entity.StockMinimo < 0)
            throw new ArgumentException("El stock mínimo no puede ser negativo.");

        entity.Activo = true;
        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(Producto entity)
    {
        _repository.Update(entity);
        await _repository.SaveChangesAsync();
    }

    public async Task DesactivarAsync(int idProducto)
    {
        var producto = await _repository.GetByIdAsync(idProducto)
                       ?? throw new KeyNotFoundException($"No existe el producto {idProducto}.");

        producto.Activo = false;
        _repository.Update(producto);
        await _repository.SaveChangesAsync();
    }
}