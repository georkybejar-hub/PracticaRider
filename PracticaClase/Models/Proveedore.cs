namespace PracticaClase.Models;

public partial class Proveedore
{
    public int IdProveedor { get; set; }
    public string Nombre { get; set; } = null!;
    public string? ContactoNombre { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public string? Direccion { get; set; }
    public bool? Activo { get; set; }
    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<PedidosProveedor> PedidosProveedors { get; set; } = new List<PedidosProveedor>();
    public virtual ICollection<ProductosProveedore> ProductosProveedores { get; set; } = new List<ProductosProveedore>();
}