using System;
using System.Collections.Generic;

namespace PracticaClase.Models;

public partial class ProductosProveedore
{
    public int IdProducto { get; set; }

    public int IdProveedor { get; set; }

    public decimal PrecioProveedor { get; set; }

    public int TiempoEntregaDias { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Proveedore IdProveedorNavigation { get; set; } = null!;
}
