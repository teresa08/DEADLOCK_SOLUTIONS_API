using DEADLOCK_SOLUTIONS.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace DEADLOCK_SOLUTIONS.Infraestructura.Data;

public class DeadlockContext(DbContextOptions<DeadlockContext> options) : DbContext(options)
{
    public DbSet<Cliente> Clientes => Set<Cliente>();

    public DbSet<Producto> Productos => Set<Producto>();

    public DbSet<Orden> Ordenes => Set<Orden>();

    public DbSet<OrdenItem> OrdenItems => Set<OrdenItem>();

    public DbSet<Factura> Facturas => Set<Factura>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.Property(cliente => cliente.Nombre).HasMaxLength(50);
            entity.Property(cliente => cliente.Apellido).HasMaxLength(100);
            entity.Property(cliente => cliente.Telefono).HasMaxLength(20);
            entity.Property(cliente => cliente.Correo).HasMaxLength(100);
            entity.Property(cliente => cliente.Direccion).HasMaxLength(100);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.Property(producto => producto.Nombre).HasMaxLength(50);
            entity.Property(producto => producto.Precio).HasColumnType("decimal(18, 2)");
            entity.HasData(
                new Producto { Id = 1, Nombre = "Teclado", Precio = 45.00m, Stock = 50 },
                new Producto { Id = 2, Nombre = "Mouse", Precio = 20.00m, Stock = 80 },
                new Producto { Id = 3, Nombre = "Monitor", Precio = 180.00m, Stock = 25 },
                new Producto { Id = 4, Nombre = "Audifonos", Precio = 35.50m, Stock = 40 });
        });

        modelBuilder.Entity<Orden>(entity =>
        {
            entity.ToTable("Ordenes");
            entity.HasOne(orden => orden.Cliente)
                .WithMany(cliente => cliente.Ordenes)
                .HasForeignKey(orden => orden.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasMany(orden => orden.Items)
                .WithOne(item => item.Orden)
                .HasForeignKey(item => item.OrdenId);
            entity.HasOne(orden => orden.Factura)
                .WithOne(factura => factura.Orden)
                .HasForeignKey<Factura>(factura => factura.OrdenId);
        });

        modelBuilder.Entity<OrdenItem>(entity =>
        {
            entity.Property(item => item.PrecioUnitario).HasColumnType("decimal(18, 2)");
            entity.HasOne(item => item.Producto)
                .WithMany()
                .HasForeignKey(item => item.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.Property(factura => factura.Numero).HasMaxLength(50);
            entity.Property(factura => factura.Total).HasColumnType("decimal(18, 2)");
            entity.HasIndex(factura => factura.Numero).IsUnique();
        });
    }
}
