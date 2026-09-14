using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticaClase.Models;
using PracticaClase.Services;

namespace PracticaClase.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProveedoreController : ControllerBase
{
    private readonly IProveedorService _service;

    public ProveedoreController(IProveedorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var proveedor = await _service.GetByIdAsync(id);
        return proveedor is null ? NotFound() : Ok(proveedor);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Proveedore proveedor)
    {
        var creado = await _service.CreateAsync(proveedor);
        return CreatedAtAction(nameof(GetById), new { id = creado.IdProveedor }, creado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Proveedore proveedor)
    {
        if (id != proveedor.IdProveedor) return BadRequest();
        await _service.UpdateAsync(proveedor);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Desactivar(int id)
    {
        try
        {
            await _service.DesactivarAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
    }
}