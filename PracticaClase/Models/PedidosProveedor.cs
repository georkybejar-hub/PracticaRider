using System;
using System.Collections.Generic;

namespace PracticaClase.Models;

public partial class PedidosProveedor
{
    public int IdPedido { get; set; }

    public int IdProveedor { get; set; }

    public DateTime FechaPedido { get; set; }

    public DateOnly? FechaEntregaEstimada { get; set; }

    public DateOnly? FechaEntregaReal { get; set; }

    public string Estado { get; set; } = null!;

    public decimal Total { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual Proveedore IdProveedorNavigation { get; set; } = null!;
}
