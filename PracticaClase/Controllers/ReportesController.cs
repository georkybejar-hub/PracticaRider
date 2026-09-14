using Microsoft.AspNetCore.Mvc;
using PracticaClase.Services;

namespace PracticaClase.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportesController : ControllerBase
{
    private readonly IReportesService _service;

    public ReportesController(IReportesService service)
    {
        _service = service;
    }

    [HttpGet("alertas-activas")]
    public async Task<IActionResult> GetAlertasActivas()
    {
        var alertas = await _service.GetAlertasActivasAsync();
        return Ok(alertas);
    }

    [HttpGet("estado-inventario")]
    public async Task<IActionResult> GetEstadoInventario()
    {
        var inventario = await _service.GetEstadoInventarioAsync();
        return Ok(inventario);
    }

    [HttpGet("productos-mas-vendidos")]
    public async Task<IActionResult> GetProductosMasVendidos()
    {
        var productos = await _service.GetProductosMasVendidosAsync();
        return Ok(productos);
    }
}