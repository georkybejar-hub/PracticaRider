using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PracticaClase.Models;

namespace PracticaClase.Data;

public class InventarioContext : IdentityDbContext<ApplicationUser>
{
    public InventarioContext(DbContextOptions<InventarioContext> options) : base(options) { }

    public virtual DbSet<Categoria> Categorias { get; set; } = null!;
    public virtual DbSet<Proveedore> Proveedores { get; set; } = null!;
    public virtual DbSet<Producto> Productos { get; set; } = null!;
    public virtual DbSet<ProductosProveedore> ProductosProveedores { get; set; } = null!;
    public virtual DbSet<PedidosProveedor> PedidosProveedors { get; set; } = null!;
    public virtual DbSet<DetallePedido> DetallePedidos { get; set; } = null!;
    public virtual DbSet<Venta> Ventas { get; set; } = null!;
    public virtual DbSet<DetalleVenta> DetalleVentas { get; set; } = null!;
    public virtual DbSet<MovimientosInventario> MovimientosInventarios { get; set; } = null!;
    public virtual DbSet<AlertasStock> AlertasStocks { get; set; } = null!;
    public virtual DbSet<VwEstadoInventario> VwEstadoInventarios { get; set; } = null!;
    public virtual DbSet<VwProductosMasVendido> VwProductosMasVendidos { get; set; } = null!;
    public virtual DbSet<VwAlertasActiva> VwAlertasActivas { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // tablas de Identity (AspNetUsers, AspNetRoles, etc.)

        modelBuilder.Entity<Categoria>(e =>
        {
            e.ToTable("categorias");
            e.HasKey(x => x.IdCategoria);
            e.Property(x => x.IdCategoria).HasColumnName("id_categoria");
            e.Property(x => x.Nombre).HasColumnName("nombre");
            e.Property(x => x.Descripcion).HasColumnName("descripcion");
            e.Property(x => x.FechaCreacion).HasColumnName("fecha_creacion");
        });

        modelBuilder.Entity<Proveedore>(e =>
        {
            e.ToTable("proveedores");
            e.HasKey(x => x.IdProveedor);
            e.Property(x => x.IdProveedor).HasColumnName("id_proveedor");
            e.Property(x => x.Nombre).HasColumnName("nombre");
            e.Property(x => x.ContactoNombre).HasColumnName("contacto_nombre");
            e.Property(x => x.Telefono).HasColumnName("telefono");
            e.Property(x => x.Email).HasColumnName("email");
            e.Property(x => x.Direccion).HasColumnName("direccion");
            e.Property(x => x.Activo).HasColumnName("activo");
            e.Property(x => x.FechaRegistro).HasColumnName("fecha_registro");
        });

        modelBuilder.Entity<Producto>(e =>
        {
            e.ToTable("productos");
            e.HasKey(x => x.IdProducto);
            e.Property(x => x.IdProducto).HasColumnName("id_producto");
            e.Property(x => x.Nombre).HasColumnName("nombre");
            e.Property(x => x.Descripcion).HasColumnName("descripcion");
            e.Property(x => x.Precio).HasColumnName("precio").HasColumnType("decimal(10,2)");
            e.Property(x => x.IdCategoria).HasColumnName("id_categoria");
            e.Property(x => x.StockActual).HasColumnName("stock_actual");
            e.Property(x => x.StockMinimo).HasColumnName("stock_minimo");
            e.Property(x => x.Activo).HasColumnName("activo");
            e.Property(x => x.FechaCreacion).HasColumnName("fecha_creacion");
            e.Property(x => x.FechaModificacion).HasColumnName("fecha_modificacion");
            e.HasOne(x => x.Categoria).WithMany(c => c.Productos).HasForeignKey(x => x.IdCategoria);
        });

        modelBuilder.Entity<ProductosProveedore>(e =>
        {
            e.ToTable("productos_proveedores");
            e.HasKey(x => new { x.IdProducto, x.IdProveedor });
            e.Property(x => x.IdProducto).HasColumnName("id_producto");
            e.Property(x => x.IdProveedor).HasColumnName("id_proveedor");
            e.Property(x => x.PrecioProveedor).HasColumnName("precio_proveedor").HasColumnType("decimal(10,2)");
            e.Property(x => x.TiempoEntregaDias).HasColumnName("tiempo_entrega_dias");
            e.HasOne(x => x.Producto).WithMany(p => p.ProductosProveedores).HasForeignKey(x => x.IdProducto);
            e.HasOne(x => x.Proveedor).WithMany(p => p.ProductosProveedores).HasForeignKey(x => x.IdProveedor);
        });

        modelBuilder.Entity<PedidosProveedor>(e =>
        {
            e.ToTable("pedidos_proveedor");
            e.HasKey(x => x.IdPedido);
            e.Property(x => x.IdPedido).HasColumnName("id_pedido");
            e.Property(x => x.IdProveedor).HasColumnName("id_proveedor");
            e.Property(x => x.FechaPedido).HasColumnName("fecha_pedido");
            e.Property(x => x.FechaEntregaEstimada).HasColumnName("fecha_entrega_estimada");
            e.Property(x => x.FechaEntregaReal).HasColumnName("fecha_entrega_real");
            e.Property(x => x.Estado).HasColumnName("estado");
            e.Property(x => x.Total).HasColumnName("total").HasColumnType("decimal(12,2)");
            e.HasOne(x => x.Proveedor).WithMany(p => p.PedidosProveedors).HasForeignKey(x => x.IdProveedor);
        });

        modelBuilder.Entity<DetallePedido>(e =>
        {
            e.ToTable("detalle_pedido");
            e.HasKey(x => x.IdDetalle);
            e.Property(x => x.IdDetalle).HasColumnName("id_detalle");
            e.Property(x => x.IdPedido).HasColumnName("id_pedido");
            e.Property(x => x.IdProducto).HasColumnName("id_producto");
            e.Property(x => x.Cantidad).HasColumnName("cantidad");
            e.Property(x => x.PrecioUnitario).HasColumnName("precio_unitario").HasColumnType("decimal(10,2)");
            e.Property(x => x.Subtotal).HasColumnName("subtotal").HasColumnType("decimal(12,2)").ValueGeneratedOnAddOrUpdate();
            e.HasOne(x => x.Pedido).WithMany(p => p.DetallePedidos).HasForeignKey(x => x.IdPedido);
            e.HasOne(x => x.Producto).WithMany(p => p.DetallePedidos).HasForeignKey(x => x.IdProducto);
        });

        modelBuilder.Entity<Venta>(e =>
        {
            e.ToTable("ventas");
            e.HasKey(x => x.IdVenta);
            e.Property(x => x.IdVenta).HasColumnName("id_venta");
            e.Property(x => x.FechaVenta).HasColumnName("fecha_venta");
            e.Property(x => x.Cliente).HasColumnName("cliente");
            e.Property(x => x.Total).HasColumnName("total").HasColumnType("decimal(12,2)");
        });

        modelBuilder.Entity<DetalleVenta>(e =>
        {
            e.ToTable("detalle_ventas");
            e.HasKey(x => x.IdDetalleVenta);
            e.Property(x => x.IdDetalleVenta).HasColumnName("id_detalle_venta");
            e.Property(x => x.IdVenta).HasColumnName("id_venta");
            e.Property(x => x.IdProducto).HasColumnName("id_producto");
            e.Property(x => x.Cantidad).HasColumnName("cantidad");
            e.Property(x => x.PrecioUnitario).HasColumnName("precio_unitario").HasColumnType("decimal(10,2)");
            e.Property(x => x.Subtotal).HasColumnName("subtotal").HasColumnType("decimal(12,2)").ValueGeneratedOnAddOrUpdate();
            e.HasOne(x => x.Venta).WithMany(v => v.DetalleVentas).HasForeignKey(x => x.IdVenta);
            e.HasOne(x => x.Producto).WithMany(p => p.DetalleVentas).HasForeignKey(x => x.IdProducto);
        });

        modelBuilder.Entity<MovimientosInventario>(e =>
        {
            e.ToTable("movimientos_inventario");
            e.HasKey(x => x.IdMovimiento);
            e.Property(x => x.IdMovimiento).HasColumnName("id_movimiento");
            e.Property(x => x.IdProducto).HasColumnName("id_producto");
            e.Property(x => x.TipoMovimiento).HasColumnName("tipo_movimiento");
            e.Property(x => x.Cantidad).HasColumnName("cantidad");
            e.Property(x => x.Motivo).HasColumnName("motivo");
            e.Property(x => x.ReferenciaTipo).HasColumnName("referencia_tipo");
            e.Property(x => x.ReferenciaId).HasColumnName("referencia_id");
            e.Property(x => x.StockResultante).HasColumnName("stock_resultante").ValueGeneratedOnAddOrUpdate();
            e.Property(x => x.FechaMovimiento).HasColumnName("fecha_movimiento");
            e.HasOne(x => x.Producto).WithMany(p => p.MovimientosInventarios).HasForeignKey(x => x.IdProducto);
        });

        modelBuilder.Entity<AlertasStock>(e =>
        {
            e.ToTable("alertas_stock");
            e.HasKey(x => x.IdAlerta);
            e.Property(x => x.IdAlerta).HasColumnName("id_alerta");
            e.Property(x => x.IdProducto).HasColumnName("id_producto");
            e.Property(x => x.StockAlMomento).HasColumnName("stock_al_momento");
            e.Property(x => x.StockMinimo).HasColumnName("stock_minimo");
            e.Property(x => x.FechaAlerta).HasColumnName("fecha_alerta");
            e.Property(x => x.Atendida).HasColumnName("atendida");
            e.HasOne(x => x.Producto).WithMany(p => p.AlertasStocks).HasForeignKey(x => x.IdProducto);
        });

        modelBuilder.Entity<VwEstadoInventario>(e =>
        {
            e.HasNoKey();
            e.ToView("vw_estado_inventario");
            e.Property(x => x.IdProducto).HasColumnName("id_producto");
            e.Property(x => x.Nombre).HasColumnName("nombre");
            e.Property(x => x.Categoria).HasColumnName("categoria");
            e.Property(x => x.StockActual).HasColumnName("stock_actual");
            e.Property(x => x.StockMinimo).HasColumnName("stock_minimo");
            e.Property(x => x.Precio).HasColumnName("precio");
            e.Property(x => x.ValorInventario).HasColumnName("valor_inventario");
            e.Property(x => x.EstadoStock).HasColumnName("estado_stock");
        });

        modelBuilder.Entity<VwProductosMasVendido>(e =>
        {
            e.HasNoKey();
            e.ToView("vw_productos_mas_vendidos");
            e.Property(x => x.IdProducto).HasColumnName("id_producto");
            e.Property(x => x.Nombre).HasColumnName("nombre");
            e.Property(x => x.UnidadesVendidas).HasColumnName("unidades_vendidas");
            e.Property(x => x.IngresosTotales).HasColumnName("ingresos_totales");
        });

        modelBuilder.Entity<VwAlertasActiva>(e =>
        {
            e.HasNoKey();
            e.ToView("vw_alertas_activas");
            e.Property(x => x.IdAlerta).HasColumnName("id_alerta");
            e.Property(x => x.IdProducto).HasColumnName("id_producto");
            e.Property(x => x.Producto).HasColumnName("producto");
            e.Property(x => x.StockAlMomento).HasColumnName("stock_al_momento");
            e.Property(x => x.StockMinimo).HasColumnName("stock_minimo");
            e.Property(x => x.FechaAlerta).HasColumnName("fecha_alerta");
        });
    }
}