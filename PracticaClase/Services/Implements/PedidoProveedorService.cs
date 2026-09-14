using PracticaClase.Models;
using PracticaClase.Repositories;
using PracticaClase.Services;

namespace PracticaClase.Services.Implements;

public class PedidoProveedorService : IPedidoProveedorService
{
    private readonly IPedidoProveedorRepository _pedidoRepository;
    private readonly IDetallePedidoRepository _detalleRepository;

    public PedidoProveedorService(
        IPedidoProveedorRepository pedidoRepository,
        IDetallePedidoRepository detalleRepository)
    {
        _pedidoRepository = pedidoRepository;
        _detalleRepository = detalleRepository;
    }

    public Task<IEnumerable<PedidosProveedor>> GetAllAsync() => _pedidoRepository.GetAllAsync();

    public Task<PedidosProveedor?> GetByIdAsync(int id) => _pedidoRepository.GetByIdAsync(id);

    public Task<IEnumerable<DetallePedido>> GetDetalleAsync(int idPedido) =>
        _detalleRepository.GetByPedidoAsync(idPedido);

    public async Task<PedidosProveedor> CrearPedidoAsync(PedidosProveedor pedido, List<DetallePedido> lineas)
    {
        if (lineas is null || lineas.Count == 0)
            throw new ArgumentException("El pedido debe tener al menos un producto.");

        if (lineas.Any(l => l.Cantidad <= 0))
            throw new ArgumentException("Todas las cantidades deben ser mayores a cero.");

        pedido.Estado = "Pendiente";
        await _pedidoRepository.AddAsync(pedido);
        await _pedidoRepository.SaveChangesAsync();

        foreach (var linea in lineas)
        {
            linea.IdPedido = pedido.IdPedido;
            await _detalleRepository.AddAsync(linea);
        }
        await _detalleRepository.SaveChangesAsync();

        return pedido;
    }

    public async Task RecibirPedidoAsync(int idPedido)
    {
        var pedido = await _pedidoRepository.GetByIdAsync(idPedido)
            ?? throw new KeyNotFoundException($"No existe el pedido {idPedido}.");

        if (pedido.Estado == "Recibido")
            throw new InvalidOperationException("Este pedido ya fue recibido.");
        if (pedido.Estado == "Cancelado")
            throw new InvalidOperationException("No se puede recibir un pedido cancelado.");

        await _pedidoRepository.RecibirPedidoAsync(idPedido);
    }

    public async Task CancelarPedidoAsync(int idPedido)
    {
        var pedido = await _pedidoRepository.GetByIdAsync(idPedido)
            ?? throw new KeyNotFoundException($"No existe el pedido {idPedido}.");

        if (pedido.Estado == "Recibido")
            throw new InvalidOperationException("No se puede cancelar un pedido ya recibido.");

        pedido.Estado = "Cancelado";
        _pedidoRepository.Update(pedido);
        await _pedidoRepository.SaveChangesAsync();
    }
}