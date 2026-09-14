namespace PracticaClase.Repositories;

using PracticaClase.Models;

public interface IProductoRepository
{
    Task<IEnumerable<Producto>> ObtenerTodosAsync();
    Task<Producto?> ObtenerPorIdAsync(int id);
    Task AgregarAsync(Producto producto);
    Task ActualizarAsync(Producto producto);
    Task<IEnumerable<VwEstadoInventario>> ObtenerEstadoInventarioAsync();
    Task<IEnumerable<VwAlertasActiva>> ObtenerAlertasActivasAsync();
}