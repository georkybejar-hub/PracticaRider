using Microsoft.AspNetCore.Mvc;
using PracticaClase.Services;

namespace PracticaClase.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlertasStockController : ControllerBase
{
    private readonly IAlertasStockService _service;

    public AlertasStockController(IAlertasStockService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var alertas = await _service.GetAllAsync();
        return Ok(alertas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var alerta = await _service.GetByIdAsync(id);

        if (alerta == null)
            return NotFound();

        return Ok(alerta);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] Models.AlertasStock alerta)
    {
        var result = await _service.CreateAsync(alerta);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.IdAlerta },
            result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] Models.AlertasStock alerta)
    {
        var result = await _service.UpdateAsync(id, alerta);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);

        if (!result)
            return NotFound();

        return NoContent();
    }
}   