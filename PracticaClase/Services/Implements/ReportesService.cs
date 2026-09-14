using PracticaClase.Models;
using PracticaClase.Repositories;
using PracticaClase.Services;

namespace PracticaClase.Services.Implements;

public class ReportesService : IReportesService
{
    private readonly IReadOnlyRepository<VwEstadoInventario> _estadoInventarioRepository;
    private readonly IReadOnlyRepository<VwProductosMasVendido> _masVendidosRepository;
    private readonly IReadOnlyRepository<VwAlertasActiva> _alertasActivasRepository;

    public ReportesService(
        IReadOnlyRepository<VwEstadoInventario> estadoInventarioRepository,
        IReadOnlyRepository<VwProductosMasVendido> masVendidosRepository,
        IReadOnlyRepository<VwAlertasActiva> alertasActivasRepository)
    {
        _estadoInventarioRepository = estadoInventarioRepository;
        _masVendidosRepository = masVendidosRepository;
        _alertasActivasRepository = alertasActivasRepository;
    }

    public Task<IEnumerable<VwEstadoInventario>> GetEstadoInventarioAsync() =>
        _estadoInventarioRepository.GetAllAsync();

    public async Task<IEnumerable<VwEstadoInventario>> GetProductosBajoStockAsync()
    {
        var estado = await _estadoInventarioRepository.GetAllAsync();
        return estado.Where(p => p.EstadoStock == "BAJO");
    }

    public async Task<IEnumerable<VwProductosMasVendido>> GetProductosMasVendidosAsync(int top = 10)
    {
        var vendidos = await _masVendidosRepository.GetAllAsync();
        return vendidos.Take(top);
    }

    public Task<IEnumerable<VwAlertasActiva>> GetAlertasActivasAsync() =>
        _alertasActivasRepository.GetAllAsync();
}