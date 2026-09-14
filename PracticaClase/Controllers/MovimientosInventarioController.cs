using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticaClase.Services;

namespace PracticaClase.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MovimientosInventarioController : ControllerBase
{
    private readonly IMovimientoInventarioService _service;

    public MovimientosInventarioController(IMovimientoInventarioService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? idProducto)
    {
        return Ok(idProducto.HasValue
            ? await _service.GetByProductoAsync(idProducto.Value)
            : await _service.GetAllAsync());
    }
}