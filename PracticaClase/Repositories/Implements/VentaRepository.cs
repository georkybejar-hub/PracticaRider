using PracticaClase.Data;
using PracticaClase.Models;
using PracticaClase.Repositories;

namespace PracticaClase.Repositories.Implements;

public class VentaRepository : GenericRepository<Venta>, IVentaRepository
{
    public VentaRepository(InventarioContext context) : base(context) { }
}