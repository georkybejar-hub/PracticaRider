using Microsoft.AspNetCore.Mvc;
using PracticaClase.Models;
using PracticaClase.Services;

namespace PracticaClase.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosProveedorController : ControllerBase
{
    private readonly IPedidosProveedorService _service;

    public PedidosProveedorController(IPedidosProveedorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var pedidos = await _service.GetAllAsync();
        return Ok(pedidos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var pedido = await _service.GetByIdAsync(id);

        if (pedido == null)
            return NotFound();

        return Ok(pedido);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] PedidosProveedor pedido)
    {
        var result = await _service.CreateAsync(pedido);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.IdPedido },
            result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] PedidosProveedor pedido)
    {
        var result = await _service.UpdateAsync(id, pedido);

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