using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticaClase.Models;
using PracticaClase.Services;

namespace PracticaClase.Controllers;

public record RegistrarVentaRequest(Venta Venta, List<DetalleVenta> Lineas);

[Authorize]
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
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var venta = await _service.GetByIdAsync(id);
        return venta is null ? NotFound() : Ok(venta);
    }

    [HttpGet("{id}/detalle")]
    public async Task<IActionResult> GetDetalle(int id) => Ok(await _service.GetDetalleAsync(id));

    [HttpPost]
    public async Task<IActionResult> Registrar(RegistrarVentaRequest request)
    {
        try
        {
            var venta = await _service.RegistrarVentaAsync(request.Venta, request.Lineas);
            return CreatedAtAction(nameof(GetById), new { id = venta.IdVenta }, venta);
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
}