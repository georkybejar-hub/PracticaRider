using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PracticaClase.Models;

namespace PracticaClase.Data;

public partial class InventarioContext : DbContext
{
    public InventarioContext(DbContextOptions<InventarioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AlertasStock> AlertasStocks { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<DetallePedido> DetallePedidos { get; set; }

    public virtual DbSet<DetalleVenta> DetalleVentas { get; set; }

    public virtual DbSet<MovimientosInventario> MovimientosInventarios { get; set; }

    public virtual DbSet<PedidosProveedor> PedidosProveedors { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductosProveedore> ProductosProveedores { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    public virtual DbSet<VwAlertasActiva> VwAlertasActivas { get; set; }

    public virtual DbSet<VwEstadoInventario> VwEstadoInventarios { get; set; }

    public virtual DbSet<VwProductosMasVendido> VwProductosMasVendidos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<AlertasStock>(entity =>
        {
            entity.HasKey(e => e.IdAlerta).HasName("PRIMARY");

            entity.ToTable("alertas_stock");

            entity.HasIndex(e => e.IdProducto, "fk_alerta_producto");

            entity.Property(e => e.IdAlerta).HasColumnName("id_alerta");
            entity.Property(e => e.Atendida).HasColumnName("atendida");
            entity.Property(e => e.FechaAlerta)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecha_alerta");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.StockAlMomento).HasColumnName("stock_al_momento");
            entity.Property(e => e.StockMinimo).HasColumnName("stock_minimo");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.AlertasStocks)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("fk_alerta_producto");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PRIMARY");

            entity.ToTable("categorias");

            entity.HasIndex(e => e.Nombre, "nombre").IsUnique();

            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PRIMARY");

            entity.ToTable("detalle_pedido");

            entity.HasIndex(e => e.IdPedido, "fk_dp_pedido");

            entity.HasIndex(e => e.IdProducto, "fk_dp_producto");

            entity.Property(e => e.IdDetalle).HasColumnName("id_detalle");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdPedido).HasColumnName("id_pedido");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.PrecioUnitario)
                .HasPrecision(10, 2)
                .HasColumnName("precio_unitario");
            entity.Property(e => e.Subtotal)
                .HasPrecision(12, 2)
                .HasComputedColumnSql("`cantidad` * `precio_unitario`", true)
                .HasColumnName("subtotal");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.IdPedido)
                .HasConstraintName("fk_dp_pedido");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_dp_producto");
        });

        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("PRIMARY");

            entity.ToTable("detalle_ventas");

            entity.HasIndex(e => e.IdProducto, "fk_dv_producto");

            entity.HasIndex(e => e.IdVenta, "fk_dv_venta");

            entity.Property(e => e.IdDetalleVenta).HasColumnName("id_detalle_venta");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.PrecioUnitario)
                .HasPrecision(10, 2)
                .HasColumnName("precio_unitario");
            entity.Property(e => e.Subtotal)
                .HasPrecision(12, 2)
                .HasComputedColumnSql("`cantidad` * `precio_unitario`", true)
                .HasColumnName("subtotal");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_dv_producto");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("fk_dv_venta");
        });

        modelBuilder.Entity<MovimientosInventario>(entity =>
        {
            entity.HasKey(e => e.IdMovimiento).HasName("PRIMARY");

            entity.ToTable("movimientos_inventario");

            entity.HasIndex(e => new { e.IdProducto, e.FechaMovimiento }, "idx_mov_producto_fecha");

            entity.Property(e => e.IdMovimiento).HasColumnName("id_movimiento");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.FechaMovimiento)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecha_movimiento");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.Motivo)
                .HasMaxLength(100)
                .HasColumnName("motivo");
            entity.Property(e => e.ReferenciaId).HasColumnName("referencia_id");
            entity.Property(e => e.ReferenciaTipo)
                .HasMaxLength(30)
                .HasColumnName("referencia_tipo");
            entity.Property(e => e.StockResultante).HasColumnName("stock_resultante");
            entity.Property(e => e.TipoMovimiento)
                .HasColumnType("enum('ENTRADA','SALIDA')")
                .HasColumnName("tipo_movimiento");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.MovimientosInventarios)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_mov_producto");
        });

        modelBuilder.Entity<PedidosProveedor>(entity =>
        {
            entity.HasKey(e => e.IdPedido).HasName("PRIMARY");

            entity.ToTable("pedidos_proveedor");

            entity.HasIndex(e => e.IdProveedor, "fk_pedido_proveedor");

            entity.HasIndex(e => e.FechaPedido, "idx_pedido_fecha");

            entity.Property(e => e.IdPedido).HasColumnName("id_pedido");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'Pendiente'")
                .HasColumnType("enum('Pendiente','Enviado','Recibido','Cancelado')")
                .HasColumnName("estado");
            entity.Property(e => e.FechaEntregaEstimada).HasColumnName("fecha_entrega_estimada");
            entity.Property(e => e.FechaEntregaReal).HasColumnName("fecha_entrega_real");
            entity.Property(e => e.FechaPedido)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecha_pedido");
            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");
            entity.Property(e => e.Total)
                .HasPrecision(12, 2)
                .HasColumnName("total");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.PedidosProveedors)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pedido_proveedor");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PRIMARY");

            entity.ToTable("productos");

            entity.HasIndex(e => e.IdCategoria, "idx_producto_categoria");

            entity.HasIndex(e => e.Nombre, "idx_producto_nombre");

            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("activo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasPrecision(10, 2)
                .HasColumnName("precio");
            entity.Property(e => e.StockActual).HasColumnName("stock_actual");
            entity.Property(e => e.StockMinimo)
                .HasDefaultValueSql("'5'")
                .HasColumnName("stock_minimo");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_producto_categoria");
        });

        modelBuilder.Entity<ProductosProveedore>(entity =>
        {
            entity.HasKey(e => new { e.IdProducto, e.IdProveedor })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("productos_proveedores");

            entity.HasIndex(e => e.IdProveedor, "fk_pp_proveedor");

            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");
            entity.Property(e => e.PrecioProveedor)
                .HasPrecision(10, 2)
                .HasColumnName("precio_proveedor");
            entity.Property(e => e.TiempoEntregaDias).HasColumnName("tiempo_entrega_dias");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductosProveedores)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("fk_pp_producto");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.ProductosProveedores)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("fk_pp_proveedor");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PRIMARY");

            entity.ToTable("proveedores");

            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");
            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("activo");
            entity.Property(e => e.ContactoNombre)
                .HasMaxLength(100)
                .HasColumnName("contacto_nombre");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PRIMARY");

            entity.ToTable("ventas");

            entity.HasIndex(e => e.FechaVenta, "idx_venta_fecha");

            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.Cliente)
                .HasMaxLength(150)
                .HasColumnName("cliente");
            entity.Property(e => e.FechaVenta)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecha_venta");
            entity.Property(e => e.Total)
                .HasPrecision(12, 2)
                .HasColumnName("total");
        });

        modelBuilder.Entity<VwAlertasActiva>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_alertas_activas");

            entity.Property(e => e.FechaAlerta)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecha_alerta");
            entity.Property(e => e.IdAlerta).HasColumnName("id_alerta");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.Producto)
                .HasMaxLength(150)
                .HasColumnName("producto");
            entity.Property(e => e.StockAlMomento).HasColumnName("stock_al_momento");
            entity.Property(e => e.StockMinimo).HasColumnName("stock_minimo");
        });

        modelBuilder.Entity<VwEstadoInventario>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_estado_inventario");

            entity.Property(e => e.Categoria)
                .HasMaxLength(100)
                .HasColumnName("categoria");
            entity.Property(e => e.EstadoStock)
                .HasMaxLength(4)
                .HasDefaultValueSql("''")
                .HasColumnName("estado_stock")
                .UseCollation("utf8mb4_0900_ai_ci");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasPrecision(10, 2)
                .HasColumnName("precio");
            entity.Property(e => e.StockActual).HasColumnName("stock_actual");
            entity.Property(e => e.StockMinimo)
                .HasDefaultValueSql("'5'")
                .HasColumnName("stock_minimo");
            entity.Property(e => e.ValorInventario)
                .HasPrecision(20, 2)
                .HasColumnName("valor_inventario");
        });

        modelBuilder.Entity<VwProductosMasVendido>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_productos_mas_vendidos");

            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IngresosTotales)
                .HasPrecision(34, 2)
                .HasColumnName("ingresos_totales");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");
            entity.Property(e => e.UnidadesVendidas)
                .HasPrecision(32)
                .HasColumnName("unidades_vendidas");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
