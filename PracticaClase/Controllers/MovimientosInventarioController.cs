using Microsoft.AspNetCore.Mvc;
using PracticaClase.Models;
using PracticaClase.Services;

namespace PracticaClase.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovimientosInventarioController : ControllerBase
{
    private readonly IMovimientosInventarioService _service;

    public MovimientosInventarioController(IMovimientosInventarioService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var movimientos = await _service.GetAllAsync();
        return Ok(movimientos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var movimiento = await _service.GetByIdAsync(id);

        if (movimiento == null)
            return NotFound();

        return Ok(movimiento);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] MovimientosInventario movimiento)
    {
        var result = await _service.CreateAsync(movimiento);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.IdMovimiento },
            result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] MovimientosInventario movimiento)
    {
        var result = await _service.UpdateAsync(id, movimiento);

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