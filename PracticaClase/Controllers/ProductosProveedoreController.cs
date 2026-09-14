using Microsoft.AspNetCore.Mvc;
using PracticaClase.Models;
using PracticaClase.Services;

namespace PracticaClase.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosProveedoreController : ControllerBase
{
    private readonly IProductosProveedoreService _service;

    public ProductosProveedoreController(IProductosProveedoreService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var productosProveedores = await _service.GetAllAsync();
        return Ok(productosProveedores);
    }

    [HttpGet("{idProducto}/{idProveedor}")]
    public async Task<IActionResult> GetById(
        int idProducto,
        int idProveedor)
    {
        var productoProveedor = await _service.GetByIdAsync(
            idProducto,
            idProveedor);

        if (productoProveedor == null)
            return NotFound();

        return Ok(productoProveedor);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] ProductosProveedore productoProveedor)
    {
        var result = await _service.CreateAsync(productoProveedor);

        return CreatedAtAction(
            nameof(GetById),
            new
            {
                idProducto = result.IdProducto,
                idProveedor = result.IdProveedor
            },
            result);
    }

    [HttpPut("{idProducto}/{idProveedor}")]
    public async Task<IActionResult> Update(
        int idProducto,
        int idProveedor,
        [FromBody] ProductosProveedore productoProveedor)
    {
        var result = await _service.UpdateAsync(
            idProducto,
            idProveedor,
            productoProveedor);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{idProducto}/{idProveedor}")]
    public async Task<IActionResult> Delete(
        int idProducto,
        int idProveedor)
    {
        var result = await _service.DeleteAsync(
            idProducto,
            idProveedor);

        if (!result)
            return NotFound();

        return NoContent();
    }
}