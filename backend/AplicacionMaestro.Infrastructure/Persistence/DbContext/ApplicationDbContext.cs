using AplicacionMaestro.Infrastructure.Persistence.Entities.Almacenes;
using AplicacionMaestro.Infrastructure.Persistence.Entities.Aptitudes;
using AplicacionMaestro.Infrastructure.Persistence.Entities.Certificaciones;
using AplicacionMaestro.Infrastructure.Persistence.Entities.CodsUnidad1;
using AplicacionMaestro.Infrastructure.Persistence.Entities.CuentasContables;
using AplicacionMaestro.Infrastructure.Persistence.Entities.Empleados;
using AplicacionMaestro.Infrastructure.Persistence.Entities.Socios;
using Microsoft.EntityFrameworkCore;

namespace AplicacionMaestro.Infrastructure.Persistence.DbContext;

public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ProveedorEntity> Proveedores => Set<ProveedorEntity>();
    public DbSet<ArticuloEntity> Articulos => Set<ArticuloEntity>();
    public DbSet<SocioEntity> Socios => Set<SocioEntity>();
    //public DbSet<SocioAptitudEntity> SocioAptitudes => Set<SocioAptitudEntity>();
    //public DbSet<SocioCertificacionEntity> SocioCertificaciones => Set<SocioCertificacionEntity>();
    public DbSet<AptitudEntity> Aptitudes => Set<AptitudEntity>();
    public DbSet<CertificadoEntity> Certificaciones => Set<CertificadoEntity>();
    public DbSet<AlmacenEntity> Almacenes => Set<AlmacenEntity>();
    public DbSet<EmpleadoEntity> Empleados => Set<EmpleadoEntity>();
    //public DbSet<EmpleadoAptitudEntity> EmpleadoAptitudes => Set<EmpleadoAptitudEntity>();
    //public DbSet<EmpleadoCertificacionEntity> EmpleadoCertificaciones => Set<EmpleadoCertificacionEntity>();
    public DbSet<CodUnidad1Entity> CodsUnidad1 => Set<CodUnidad1Entity>();
    public DbSet<CuentaContableEntity> CtasContables => Set<CuentaContableEntity>();



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProveedorEntity>(entity =>
        {
            entity.HasKey(e => e.IdProveedor);
            entity.HasIndex(p => p.IdExternal)
            .IsUnique();
        });


        modelBuilder.Entity<ArticuloEntity>(entity =>
        {
            entity.HasKey(e => e.IdArticulo);

            entity.HasIndex(e => e.IdExternal)
                  .IsUnique();

            entity.Property(e => e.EstadoMaterial)
                    .HasConversion<string>();
        });

        modelBuilder.Entity<SocioEntity>(entity =>
        {
            entity.HasKey(e => e.IdSocio);
            entity.HasIndex(e => e.IdSocioExternal)
                  .IsUnique();
        });

        modelBuilder.Entity<AptitudEntity>()
            .HasIndex(a => a.Codigo)
            .IsUnique();

        modelBuilder.Entity<CertificadoEntity>()   
            .HasIndex(c => c.Codigo)
                .IsUnique();

        /*modelBuilder.Entity<SocioAptitudEntity>(entity =>
        {
            entity.HasKey(e => new { e.IdSocio, e.IdAptitud });

            entity.HasOne(e => e.Socio)
                  .WithMany(s => s.SocioAptitudes)
                  .HasForeignKey(e => e.IdSocio)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Aptitud)
                  .WithMany() // sin navegación en Aptitud
                  .HasForeignKey(e => e.IdAptitud)
                  .OnDelete(DeleteBehavior.Restrict);
        });*/

        /*modelBuilder.Entity<SocioCertificacionEntity>(entity =>
        {
            entity.HasKey(e => new { e.IdSocio, e.IdCertificacion });

            entity.HasOne(e => e.Socio)
                  .WithMany(s => s.SocioCertificaciones)
                  .HasForeignKey(e => e.IdSocio)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Certificacion)
                  .WithMany()
                  .HasForeignKey(e => e.IdCertificacion)
                  .OnDelete(DeleteBehavior.Restrict);
        });*/

        modelBuilder.Entity<AlmacenEntity>(entity =>
        {
            entity.HasKey(e => e.IdAlmacen);
            entity.HasIndex(p => p.IdAlmacenExternal)
            .IsUnique();
        });

        modelBuilder.Entity<EmpleadoEntity>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado);
            entity.HasIndex(e => e.IdEmpleadoExternal)
                  .IsUnique();
        });

        /*modelBuilder.Entity<EmpleadoAptitudEntity>(entity =>
        {
            entity.HasKey(e => new { e.IdEmpleado, e.IdAptitud });

            entity.HasOne(e => e.Empleado)
                  .WithMany(s => s.EmpleadoAptitudes)
                  .HasForeignKey(e => e.IdEmpleado)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Aptitud)
                  .WithMany() // sin navegación en Aptitud
                  .HasForeignKey(e => e.IdAptitud)
                  .OnDelete(DeleteBehavior.Restrict);
        });*/

        /*modelBuilder.Entity<EmpleadoCertificacionEntity>(entity =>
        {
            entity.HasKey(e => new { e.IdEmpleado, e.IdCertificacion });

            entity.HasOne(e => e.Empleado)
                  .WithMany(s => s.EmpleadoCertificaciones)
                  .HasForeignKey(e => e.IdEmpleado)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Certificacion)
                  .WithMany()
                  .HasForeignKey(e => e.IdCertificacion)
                  .OnDelete(DeleteBehavior.Restrict);
        });*/

        modelBuilder.Entity<CodUnidad1Entity>()
            .HasIndex(a => a.Codigo)
            .IsUnique();

        modelBuilder.Entity<CuentaContableEntity>()
            .HasIndex(c => c.Cuenta)
                .IsUnique();
    }
}
