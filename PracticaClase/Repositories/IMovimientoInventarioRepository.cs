using PracticaClase.Models;

namespace PracticaClase.Repositories;

public interface IMovimientoInventarioRepository
{
    Task<IEnumerable<MovimientosInventario>> GetAllAsync();
    Task<IEnumerable<MovimientosInventario>> GetByProductoAsync(int idProducto);
}