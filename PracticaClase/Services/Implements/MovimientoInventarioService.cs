using PracticaClase.Models;
using PracticaClase.Repositories;
using PracticaClase.Services;

namespace PracticaClase.Services.Implements;

public class MovimientoInventarioService : IMovimientoInventarioService
{
    private readonly IMovimientoInventarioRepository _repository;

    public MovimientoInventarioService(IMovimientoInventarioRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<MovimientosInventario>> GetAllAsync() => _repository.GetAllAsync();

    public Task<IEnumerable<MovimientosInventario>> GetByProductoAsync(int idProducto) =>
        _repository.GetByProductoAsync(idProducto);
}