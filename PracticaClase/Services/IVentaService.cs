using PracticaClase.Models;

namespace PracticaClase.Services;

public interface IVentaService
{
    Task<IEnumerable<Venta>> GetAllAsync();
    Task<Venta?> GetByIdAsync(int id);
    Task<IEnumerable<DetalleVenta>> GetDetalleAsync(int idVenta);
    Task<Venta> RegistrarVentaAsync(Venta venta, List<DetalleVenta> lineas);
}