using Microsoft.AspNetCore.Mvc;
using PracticaClase.Models;
using PracticaClase.Services;

namespace PracticaClase.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VentaController : ControllerBase
{
    private readonly IVentaService _service;

    public VentaController(IVentaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var ventas = await _service.GetAllAsync();
        return Ok(ventas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var venta = await _service.GetByIdAsync(id);

        if (venta == null)
            return NotFound();

        return Ok(venta);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] Venta venta)
    {
        var result = await _service.CreateAsync(venta);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.IdVenta },
            result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] Venta venta)
    {
        var result = await _service.UpdateAsync(id, venta);

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