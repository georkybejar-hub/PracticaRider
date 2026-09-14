using PracticaClase.Data;
using PracticaClase.Models;
using PracticaClase.Repositories;

namespace PracticaClase.Repositories.Implements;

public class ProductoRepository : GenericRepository<Producto>, IProductoRepository
{
    public ProductoRepository(InventarioContext context) : base(context) { }
}