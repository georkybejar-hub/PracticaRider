using PracticaClase.Models;
using PracticaClase.Repositories;
using PracticaClase.Services;

namespace PracticaClase.Services.Implements;

public class ProductoProveedorService : IProductoProveedorService
{
    private readonly IProductoProveedorRepository _repository;

    public ProductoProveedorService(IProductoProveedorRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<ProductosProveedore>> GetAllAsync() => _repository.GetAllAsync();

    public Task<ProductosProveedore?> GetAsync(int idProducto, int idProveedor) =>
        _repository.GetByIdsAsync(idProducto, idProveedor);

    public async Task<ProductosProveedore> AsociarAsync(ProductosProveedore relacion)
    {
        var existente = await _repository.GetByIdsAsync(relacion.IdProducto, relacion.IdProveedor);
        if (existente is not null)
            throw new InvalidOperationException("Ese producto ya está asociado a ese proveedor.");

        await _repository.AddAsync(relacion);
        await _repository.SaveChangesAsync();
        return relacion;
    }

    public async Task DesasociarAsync(int idProducto, int idProveedor)
    {
        var relacion = await _repository.GetByIdsAsync(idProducto, idProveedor)
                       ?? throw new KeyNotFoundException("Esa relación producto-proveedor no existe.");

        _repository.Delete(relacion);
        await _repository.SaveChangesAsync();
    }
}