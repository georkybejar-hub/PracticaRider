using PracticaClase.Data;
using PracticaClase.Models;
using PracticaClase.Repositories;

namespace PracticaClase.Repositories.Implements;

public class AlertaStockRepository : GenericRepository<AlertasStock>, IAlertaStockRepository
{
    public AlertaStockRepository(InventarioContext context) : base(context) { }
}