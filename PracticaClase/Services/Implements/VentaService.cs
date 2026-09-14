using PracticaClase.Models;
using PracticaClase.Repositories;
using PracticaClase.Services;

namespace PracticaClase.Services.Implements;

public class VentaService : IVentaService
{
    private readonly IVentaRepository _ventaRepository;
    private readonly IDetalleVentaRepository _detalleRepository;

    public VentaService(
        IVentaRepository ventaRepository,
        IDetalleVentaRepository detalleRepository)
    {
        _ventaRepository = ventaRepository;
        _detalleRepository = detalleRepository;
    }

    public Task<IEnumerable<Venta>> GetAllAsync() => _ventaRepository.GetAllAsync();

    public Task<Venta?> GetByIdAsync(int id) => _ventaRepository.GetByIdAsync(id);

    public Task<IEnumerable<DetalleVenta>> GetDetalleAsync(int idVenta) =>
        _detalleRepository.GetByVentaAsync(idVenta);

    public async Task<Venta> RegistrarVentaAsync(Venta venta, List<DetalleVenta> lineas)
    {
        if (lineas is null || lineas.Count == 0)
            throw new ArgumentException("La venta debe tener al menos un producto.");

        if (lineas.Any(l => l.Cantidad <= 0))
            throw new ArgumentException("Todas las cantidades deben ser mayores a cero.");

        await _ventaRepository.AddAsync(venta);
        await _ventaRepository.SaveChangesAsync();

        try
        {
            foreach (var linea in lineas)
            {
                linea.IdVenta = venta.IdVenta;
                await _detalleRepository.AddAsync(linea);
            }
            await _detalleRepository.SaveChangesAsync();
        }
        catch (Exception ex) when (ex.Message.Contains("Stock insuficiente"))
        {
            throw new InvalidOperationException(
                "No se pudo completar la venta: stock insuficiente para uno de los productos.", ex);
        }

        return venta;
    }
}