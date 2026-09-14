using System;
using System.Collections.Generic;

namespace PracticaClase.Models;

public partial class VwProductosMasVendido
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal? UnidadesVendidas { get; set; }

    public decimal? IngresosTotales { get; set; }
}
