using PracticaClase.Models;

namespace PracticaClase.Repositories;

public interface IDetalleVentaRepository : IRepository<DetalleVenta>
{
    Task<IEnumerable<DetalleVenta>> GetByVentaAsync(int idVenta);
}