using System;
using System.Collections.Generic;

namespace PracticaClase.Models;

public partial class AlertasStock
{
    public int IdAlerta { get; set; }

    public int IdProducto { get; set; }

    public int StockAlMomento { get; set; }

    public int StockMinimo { get; set; }

    public DateTime FechaAlerta { get; set; }

    public bool Atendida { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
