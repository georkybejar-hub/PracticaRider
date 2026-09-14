using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticaClase.Services;

namespace PracticaClase.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ReportesController : ControllerBase
{
    private readonly IReportesService _service;

    public ReportesController(IReportesService service)
    {
        _service = service;
    }

    [HttpGet("estado-inventario")]
    public async Task<IActionResult> EstadoInventario() => Ok(await _service.GetEstadoInventarioAsync());

    [HttpGet("bajo-stock")]
    public async Task<IActionResult> BajoStock() => Ok(await _service.GetProductosBajoStockAsync());

    [HttpGet("mas-vendidos")]
    public async Task<IActionResult> MasVendidos([FromQuery] int top = 10) =>
        Ok(await _service.GetProductosMasVendidosAsync(top));

    [HttpGet("alertas-activas")]
    public async Task<IActionResult> AlertasActivas() => Ok(await _service.GetAlertasActivasAsync());
}