namespace PracticaClase.Repositories;

using PracticaClase.Models;

public interface IProveedorRepository
{
    Task<IEnumerable<Proveedore>> ObtenerProveedoresAsync();
    Task<Proveedore?> ObtenerPorIdAsync(int id);
    Task AgregarAsync(Proveedore proveedor);
    Task ActualizarAsync(Proveedore proveedor);
}