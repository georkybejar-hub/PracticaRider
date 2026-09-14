using PracticaClase.Models;

namespace PracticaClase.Services;

public interface IReportesService
{
    Task<IEnumerable<VwEstadoInventario>> GetEstadoInventarioAsync();
    Task<IEnumerable<VwEstadoInventario>> GetProductosBajoStockAsync();
    Task<IEnumerable<VwProductosMasVendido>> GetProductosMasVendidosAsync(int top);
    Task<IEnumerable<VwAlertasActiva>> GetAlertasActivasAsync();
}