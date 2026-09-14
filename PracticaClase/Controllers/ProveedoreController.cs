using Microsoft.AspNetCore.Mvc;
using PracticaClase.Services;

namespace PracticaClase.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProveedoreController : ControllerBase
{
    private readonly IProveedoreService _service;

    public ProveedoreController(IProveedoreService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var proveedores = await _service.GetAllAsync();
        return Ok(proveedores);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var proveedor = await _service.GetByIdAsync(id);

        if (proveedor == null)
            return NotFound();

        return Ok(proveedor);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] Models.Proveedore proveedor)
    {
        var result = await _service.CreateAsync(proveedor);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.IdProveedor },
            result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] Models.Proveedore proveedor)
    {
        var result = await _service.UpdateAsync(id, proveedor);

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