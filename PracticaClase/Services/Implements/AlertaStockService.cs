using PracticaClase.Models;
using PracticaClase.Repositories;
using PracticaClase.Services;

namespace PracticaClase.Services.Implements;

public class AlertaStockService : IAlertaStockService
{
    private readonly IAlertaStockRepository _repository;

    public AlertaStockService(IAlertaStockRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<AlertasStock>> GetAllAsync() => _repository.GetAllAsync();

    public Task<AlertasStock?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

    public async Task<IEnumerable<AlertasStock>> GetActivasAsync()
    {
        var alertas = await _repository.GetAllAsync();
        return alertas.Where(a => a.Atendida == false);
    }

    public async Task MarcarAtendidaAsync(int idAlerta)
    {
        var alerta = await _repository.GetByIdAsync(idAlerta)
                     ?? throw new KeyNotFoundException($"No existe la alerta {idAlerta}.");

        alerta.Atendida = true;
        _repository.Update(alerta);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var alerta = await _repository.GetByIdAsync(id)
                     ?? throw new KeyNotFoundException($"No existe la alerta {id}.");

        _repository.Delete(alerta);
        await _repository.SaveChangesAsync();
    }
}