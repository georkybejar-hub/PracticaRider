using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticaClase.Models;
using PracticaClase.Services;

namespace PracticaClase.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductosProveedoreController : ControllerBase
{
    private readonly IProductoProveedorService _service;

    public ProductosProveedoreController(IProductoProveedorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{idProducto}/{idProveedor}")]
    public async Task<IActionResult> Get(int idProducto, int idProveedor)
    {
        var relacion = await _service.GetAsync(idProducto, idProveedor);
        return relacion is null ? NotFound() : Ok(relacion);
    }

    [HttpPost]
    public async Task<IActionResult> Asociar(ProductosProveedore relacion)
    {
        try
        {
            var creada = await _service.AsociarAsync(relacion);
            return CreatedAtAction(nameof(Get), new { idProducto = creada.IdProducto, idProveedor = creada.IdProveedor }, creada);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{idProducto}/{idProveedor}")]
    public async Task<IActionResult> Desasociar(int idProducto, int idProveedor)
    {
        try
        {
            await _service.DesasociarAsync(idProducto, idProveedor);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
    }
}