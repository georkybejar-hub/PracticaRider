using PracticaClase.Models;

namespace PracticaClase.Services;

public interface IMovimientoInventarioService
{
    Task<IEnumerable<MovimientosInventario>> GetAllAsync();
    Task<IEnumerable<MovimientosInventario>> GetByProductoAsync(int idProducto);
}