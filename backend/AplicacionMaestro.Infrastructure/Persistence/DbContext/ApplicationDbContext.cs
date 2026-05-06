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
            entity.ToTable("tmaproveedores");
            entity.HasKey(e => e.IdProveedor);
            entity.Property(e => e.IdExternal).HasColumnName("IdProveedorExternal");
            entity.Property(e => e.RazonSocial).HasColumnName("NombreProveedor");
            entity.Property(e => e.Telefono).HasColumnName("TelefonoContacto");
            entity.Property(e => e.CorreoExterno).HasColumnName("CorreoExternoContacto");
            entity.Property(e => e.CorreoInterno).HasColumnName("CorreoInternoContacto");
            entity.Property(e => e.UsuarioRegistro).HasColumnName("UsuarioReg");
            entity.Property(e => e.FechaCreacion).HasColumnName("FechaReg");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("UsuarioAct");
            entity.Property(e => e.FechaModificacion).HasColumnName("FechaAct");
            entity.HasIndex(e => e.IdExternal).IsUnique();
        });

        modelBuilder.Entity<ArticuloEntity>(entity =>
        {
            entity.ToTable("tmaarticulos");
            entity.HasKey(e => e.IdArticulo);
            entity.Property(e => e.IdExternal).HasColumnName("IdArticuloExternal");
            entity.Property(e => e.UsuarioRegistro).HasColumnName("UsuarioReg");
            entity.Property(e => e.FechaCreacion).HasColumnName("FechaReg");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("UsuarioAct");
            entity.Property(e => e.FechaModificacion).HasColumnName("FechaAct");
            entity.HasIndex(e => e.IdExternal).IsUnique();
            entity.Property(e => e.EstadoMaterial).HasConversion<string>();
        });

        modelBuilder.Entity<SocioEntity>(entity =>
        {
            entity.ToTable("tmasocios");
            entity.HasKey(e => e.IdSocio);
            entity.Property(e => e.UsuarioRegistro).HasColumnName("UsuarioReg");
            entity.Property(e => e.FechaCreacion).HasColumnName("FechaReg");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("UsuarioAct");
            entity.Property(e => e.FechaModificacion).HasColumnName("FechaAct");
            entity.HasIndex(e => e.IdSocioExternal).IsUnique();
        });

        modelBuilder.Entity<AptitudEntity>(entity =>
        {
            entity.ToTable("tmaaptitudes");
            entity.HasKey(e => e.IdAptitud);
            entity.Property(e => e.UsuarioRegistro).HasColumnName("UsuarioReg");
            entity.Property(e => e.FechaCreacion).HasColumnName("FechaReg");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("UsuarioAct");
            entity.Property(e => e.FechaModificacion).HasColumnName("FechaAct");
            entity.HasIndex(e => e.Codigo).IsUnique();
        });

        modelBuilder.Entity<CertificadoEntity>(entity =>
        {
            entity.ToTable("tmacertificaciones");
            entity.HasKey(e => e.IdCertificacion);
            entity.Property(e => e.UsuarioRegistro).HasColumnName("UsuarioReg");
            entity.Property(e => e.FechaCreacion).HasColumnName("FechaReg");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("UsuarioAct");
            entity.Property(e => e.FechaModificacion).HasColumnName("FechaAct");
            entity.HasIndex(e => e.Codigo).IsUnique();
        });

        modelBuilder.Entity<AlmacenEntity>(entity =>
        {
            entity.ToTable("tmaalmacenes");
            entity.HasKey(e => e.IdAlmacen);
            entity.Property(e => e.UsuarioRegistro).HasColumnName("UsuarioReg");
            entity.Property(e => e.FechaCreacion).HasColumnName("FechaReg");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("UsuarioAct");
            entity.Property(e => e.FechaModificacion).HasColumnName("FechaAct");
            entity.HasIndex(e => e.IdAlmacenExternal).IsUnique();
        });

        modelBuilder.Entity<EmpleadoEntity>(entity =>
        {
            entity.ToTable("tmaempleados");
            entity.HasKey(e => e.IdEmpleado);
            entity.Property(e => e.Departamento).HasColumnName("Dpto");
            entity.Property(e => e.FrecuenciaPago).HasColumnName("FrecPago");
            entity.Property(e => e.TipoEmpleado).HasColumnName("TipEmp");
            entity.Property(e => e.GeneraNomina).HasColumnName("GenNomina");
            entity.Property(e => e.CuentaSueldo).HasColumnName("CtaSueldo");
            entity.Property(e => e.FechaContratacion).HasColumnName("FechaContr");
            entity.Property(e => e.FechaRevision).HasColumnName("FechaRevis");
            entity.Property(e => e.FechaRescision).HasColumnName("FechaRescis");
            entity.Property(e => e.UsuarioRegistro).HasColumnName("UsuarioReg");
            entity.Property(e => e.FechaCreacion).HasColumnName("FechaReg");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("UsuarioAct");
            entity.Property(e => e.FechaModificacion).HasColumnName("FechaAct");
            entity.HasIndex(e => e.IdEmpleadoExternal).IsUnique();
        });

        /*modelBuilder.Entity<SocioAptitudEntity>(entity =>
        {
            entity.HasKey(e => new { e.IdSocio, e.IdAptitud });

            entity.HasOne(e => e.Socio)
                  .WithMany(s => s.SocioAptitudes)
                  .HasForeignKey(e => e.IdSocio)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Aptitud)
                  .WithMany()
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

        /*modelBuilder.Entity<EmpleadoAptitudEntity>(entity =>
        {
            entity.HasKey(e => new { e.IdEmpleado, e.IdAptitud });

            entity.HasOne(e => e.Empleado)
                  .WithMany(s => s.EmpleadoAptitudes)
                  .HasForeignKey(e => e.IdEmpleado)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Aptitud)
                  .WithMany()
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

        modelBuilder.Entity<CodUnidad1Entity>(entity =>
        {
            entity.ToTable("tmacodsunidad1");
            entity.HasKey(e => e.IdCodUnidad1);
            entity.Property(e => e.UsuarioRegistro).HasColumnName("UsuarioReg");
            entity.Property(e => e.FechaCreacion).HasColumnName("FechaReg");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("UsuarioAct");
            entity.Property(e => e.FechaModificacion).HasColumnName("FechaAct");
            entity.HasIndex(e => e.Codigo).IsUnique();
        });

        modelBuilder.Entity<CuentaContableEntity>(entity =>
        {
            entity.ToTable("tmacuentascontables");
            entity.HasKey(e => e.IdCuentaContable);
            entity.Property(e => e.UsuarioRegistro).HasColumnName("UsuarioReg");
            entity.Property(e => e.FechaCreacion).HasColumnName("FechaReg");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("UsuarioAct");
            entity.Property(e => e.FechaModificacion).HasColumnName("FechaAct");
            entity.HasIndex(e => e.Cuenta).IsUnique();
        });
    }
}
