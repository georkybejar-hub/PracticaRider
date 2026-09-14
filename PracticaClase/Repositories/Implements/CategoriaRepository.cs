using PracticaClase.Data;
using PracticaClase.Models;
using PracticaClase.Repositories;

namespace PracticaClase.Repositories.Implements;

public class CategoriaRepository : GenericRepository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(InventarioContext context) : base(context) { }
}