using PracticaClase.Data;
using PracticaClase.Models;
using PracticaClase.Repositories;

namespace PracticaClase.Repositories.Implements;

public class ProveedorRepository : GenericRepository<Proveedore>, IProveedorRepository
{
    public ProveedorRepository(InventarioContext context) : base(context) { }
}