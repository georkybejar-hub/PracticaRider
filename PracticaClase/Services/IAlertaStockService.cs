using PracticaClase.Models;

namespace PracticaClase.Services;

public interface IAlertaStockService
{
    Task<IEnumerable<AlertasStock>> GetAllAsync();
    Task<AlertasStock?> GetByIdAsync(int id);
    Task<IEnumerable<AlertasStock>> GetActivasAsync();
    Task MarcarAtendidaAsync(int idAlerta);
    Task DeleteAsync(int id);
}