using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticaClase.Models;
using PracticaClase.Services;

namespace PracticaClase.Controllers;

public record CrearPedidoRequest(PedidosProveedor Pedido, List<DetallePedido> Lineas);

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PedidosProveedorController : ControllerBase
{
    private readonly IPedidoProveedorService _service;

    public PedidosProveedorController(IPedidoProveedorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var pedido = await _service.GetByIdAsync(id);
        return pedido is null ? NotFound() : Ok(pedido);
    }

    [HttpGet("{id}/detalle")]
    public async Task<IActionResult> GetDetalle(int id) => Ok(await _service.GetDetalleAsync(id));

    [HttpPost]
    public async Task<IActionResult> Crear(CrearPedidoRequest request)
    {
        try
        {
            var pedido = await _service.CrearPedidoAsync(request.Pedido, request.Lineas);
            return CreatedAtAction(nameof(GetById), new { id = pedido.IdPedido }, pedido);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPost("{id}/recibir")]
    public async Task<IActionResult> Recibir(int id)
    {
        try
        {
            await _service.RecibirPedidoAsync(id);
            return NoContent();
        }
        catch (Exception ex) when (ex is KeyNotFoundException or InvalidOperationException)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("{id}/cancelar")]
    public async Task<IActionResult> Cancelar(int id)
    {
        try
        {
            await _service.CancelarPedidoAsync(id);
            return NoContent();
        }
        catch (Exception ex) when (ex is KeyNotFoundException or InvalidOperationException)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
}