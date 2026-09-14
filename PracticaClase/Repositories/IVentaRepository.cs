namespace PracticaClase.Repositories;

using PracticaClase.Models;

public interface IVentaRepository
{
    Task RegistrarVentaAsync(Venta venta, List<DetalleVenta> detalles);
    Task<IEnumerable<VwProductosMasVendido>> ObtenerProductosMasVendidosAsync();
}