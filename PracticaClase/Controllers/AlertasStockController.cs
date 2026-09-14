using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticaClase.Services;

namespace PracticaClase.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AlertasStockController : ControllerBase
{
    private readonly IAlertaStockService _service;

    public AlertasStockController(IAlertaStockService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool? soloActivas)
    {
        return Ok(soloActivas == true ? await _service.GetActivasAsync() : await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var alerta = await _service.GetByIdAsync(id);
        return alerta is null ? NotFound() : Ok(alerta);
    }

    [HttpPost("{id}/atender")]
    public async Task<IActionResult> MarcarAtendida(int id)
    {
        try
        {
            await _service.MarcarAtendidaAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
    }
}