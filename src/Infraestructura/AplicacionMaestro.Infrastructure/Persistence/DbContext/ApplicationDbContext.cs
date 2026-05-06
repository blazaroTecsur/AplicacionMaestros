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
            entity.ToTable("tmaproveedor");
            entity.HasKey(e => e.IdProveedor);
            entity.Property(e => e.IdProveedor).HasColumnName("IdProveedor");
            entity.Property(e => e.IdExternal).HasColumnName("IdProveedorExternal");
            entity.Property(e => e.TipoPersona).HasColumnName("TipoPersona");
            entity.Property(e => e.Ruc).HasColumnName("Ruc");
            entity.Property(e => e.RazonSocial).HasColumnName("NombreProveedor");
            entity.Property(e => e.Direccion1).HasColumnName("Direccion1");
            entity.Property(e => e.Direccion2).HasColumnName("Direccion2");
            entity.Property(e => e.Direccion3).HasColumnName("Direccion3");
            entity.Property(e => e.Direccion4).HasColumnName("Direccion4");
            entity.Property(e => e.Comprador).HasColumnName("Comprador");
            entity.Property(e => e.Contacto).HasColumnName("Contacto");
            entity.Property(e => e.Telefono).HasColumnName("TelefonoContacto");
            entity.Property(e => e.CorreoExterno).HasColumnName("CorreoExternoContacto");
            entity.Property(e => e.CorreoInterno).HasColumnName("CorreoInternoContacto");
            entity.Property(e => e.Estado).HasColumnName("Estado");
            entity.Property(e => e.UsuarioRegistro).HasColumnName("UsuarioReg");
            entity.Property(e => e.FechaCreacion).HasColumnName("FechaReg");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("UsuarioAct");
            entity.Property(e => e.FechaModificacion).HasColumnName("FechaAct");
            entity.HasIndex(e => e.IdExternal)
                .IsUnique()
                .HasDatabaseName("idx_tmaproveedor_IdProveedorExternal");
        });

        modelBuilder.Entity<ArticuloEntity>(entity =>
        {
            entity.ToTable("tmaarticulo");
            entity.HasKey(e => e.IdArticulo);
            entity.Property(e => e.IdArticulo).HasColumnName("IdArticulo");
            entity.Property(e => e.IdExternal).HasColumnName("IdArticuloExternal");
            entity.Property(e => e.Codigo).HasColumnName("Codigo");
            entity.Property(e => e.Descripcion).HasColumnName("Descripcion");
            entity.Property(e => e.UnidadMedida).HasColumnName("UnidadMedida");
            entity.Property(e => e.Tipo).HasColumnName("Tipo");
            entity.Property(e => e.Origen).HasColumnName("Origen");
            entity.Property(e => e.CodigoProducto).HasColumnName("CodigoProducto");
            entity.Property(e => e.CodigoAbc).HasColumnName("CodigoAbc");
            entity.Property(e => e.SegLote).HasColumnName("SegLote");
            entity.Property(e => e.EstadoMaterial).HasColumnName("EstadoMaterial").HasConversion<string>();
            entity.Property(e => e.UsuarioRegistro).HasColumnName("UsuarioReg");
            entity.Property(e => e.FechaCreacion).HasColumnName("FechaReg");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("UsuarioAct");
            entity.Property(e => e.FechaModificacion).HasColumnName("FechaAct");
            entity.HasIndex(e => e.IdExternal)
                .IsUnique()
                .HasDatabaseName("idx_tmaarticulo_IdArticuloExternal");
        });

        modelBuilder.Entity<SocioEntity>(entity =>
        {
            entity.ToTable("tmasocio");
            entity.HasKey(e => e.IdSocio);
            entity.Property(e => e.IdSocio).HasColumnName("IdSocio");
            entity.Property(e => e.IdSocioExternal).HasColumnName("IdSocioExternal");
            entity.Property(e => e.CodigoSocio).HasColumnName("CodigoSocio");
            entity.Property(e => e.TipoEmpleado).HasColumnName("TipoEmpleado");
            entity.Property(e => e.NroReferencia).HasColumnName("NroReferencia");
            entity.Property(e => e.NombreCompleto).HasColumnName("NombreCompleto");
            entity.Property(e => e.Supervisor).HasColumnName("Supervisor");
            entity.Property(e => e.CodTrabajo).HasColumnName("CodTrabajo");
            entity.Property(e => e.TipoPago).HasColumnName("TipoPago");
            entity.Property(e => e.Email).HasColumnName("Email");
            entity.Property(e => e.DireccionLocaliz).HasColumnName("DireccionLocaliz");
            entity.Property(e => e.DireccionMensaje).HasColumnName("DireccionMensaje");
            entity.Property(e => e.Almacen).HasColumnName("Almacen");
            entity.Property(e => e.Departamento).HasColumnName("Departamento");
            entity.Property(e => e.Usuario).HasColumnName("Usuario");
            entity.Property(e => e.Activo).HasColumnName("Activo");
            entity.Property(e => e.UsuarioRegistro).HasColumnName("UsuarioReg");
            entity.Property(e => e.FechaCreacion).HasColumnName("FechaReg");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("UsuarioAct");
            entity.Property(e => e.FechaModificacion).HasColumnName("FechaAct");
            entity.HasIndex(e => e.IdSocioExternal)
                .IsUnique()
                .HasDatabaseName("idx_tmasocio_IdSocioExternal");
        });

        modelBuilder.Entity<AptitudEntity>(entity =>
        {
            entity.ToTable("tmaaptitud");
            entity.HasKey(e => e.IdAptitud);
            entity.Property(e => e.IdAptitud).HasColumnName("IdAptitud");
            entity.Property(e => e.Codigo).HasColumnName("Codigo");
            entity.Property(e => e.Descripcion).HasColumnName("Descripcion");
            entity.Property(e => e.UsuarioRegistro).HasColumnName("UsuarioReg");
            entity.Property(e => e.FechaCreacion).HasColumnName("FechaReg");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("UsuarioAct");
            entity.Property(e => e.FechaModificacion).HasColumnName("FechaAct");
            entity.HasIndex(e => e.Codigo)
                .IsUnique()
                .HasDatabaseName("idx_tmaaptitud_Codigo");
        });

        modelBuilder.Entity<CertificadoEntity>(entity =>
        {
            entity.ToTable("tmacertificacion");
            entity.HasKey(e => e.IdCertificacion);
            entity.Property(e => e.IdCertificacion).HasColumnName("IdCertificacion");
            entity.Property(e => e.Codigo).HasColumnName("Codigo");
            entity.Property(e => e.Descripcion).HasColumnName("Descripcion");
            entity.Property(e => e.UsuarioRegistro).HasColumnName("UsuarioReg");
            entity.Property(e => e.FechaCreacion).HasColumnName("FechaReg");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("UsuarioAct");
            entity.Property(e => e.FechaModificacion).HasColumnName("FechaAct");
            entity.HasIndex(e => e.Codigo)
                .IsUnique()
                .HasDatabaseName("idx_tmacertificacion_Codigo");
        });

        modelBuilder.Entity<AlmacenEntity>(entity =>
        {
            entity.ToTable("tmaalmacen");
            entity.HasKey(e => e.IdAlmacen);
            entity.Property(e => e.IdAlmacen).HasColumnName("IdAlmacen");
            entity.Property(e => e.IdAlmacenExternal).HasColumnName("IdAlmacenExternal");
            entity.Property(e => e.CodigoAlmacen).HasColumnName("CodigoAlmacen");
            entity.Property(e => e.NombreAlmacen).HasColumnName("NombreAlmacen");
            entity.Property(e => e.Direccion1).HasColumnName("Direccion1");
            entity.Property(e => e.Direccion2).HasColumnName("Direccion2");
            entity.Property(e => e.Direccion3).HasColumnName("Direccion3");
            entity.Property(e => e.Direccion4).HasColumnName("Direccion4");
            entity.Property(e => e.Ciudad).HasColumnName("Ciudad");
            entity.Property(e => e.CodigoProvincia).HasColumnName("CodigoProvincia");
            entity.Property(e => e.CodigoPostal).HasColumnName("CodigoPostal");
            entity.Property(e => e.Contacto).HasColumnName("Contacto");
            entity.Property(e => e.Telefono).HasColumnName("Telefono");
            entity.Property(e => e.Fax).HasColumnName("Fax");
            entity.Property(e => e.Ruc).HasColumnName("Ruc");
            entity.Property(e => e.UsuarioRegistro).HasColumnName("UsuarioReg");
            entity.Property(e => e.FechaCreacion).HasColumnName("FechaReg");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("UsuarioAct");
            entity.Property(e => e.FechaModificacion).HasColumnName("FechaAct");
            entity.HasIndex(e => e.IdAlmacenExternal)
                .IsUnique()
                .HasDatabaseName("idx_tmaalmacen_IdAlmacenExternal");
        });

        modelBuilder.Entity<EmpleadoEntity>(entity =>
        {
            entity.ToTable("tmaempleado");
            entity.HasKey(e => e.IdEmpleado);
            entity.Property(e => e.IdEmpleado).HasColumnName("IdEmpleado");
            entity.Property(e => e.IdEmpleadoExternal).HasColumnName("IdEmpleadoExternal");
            entity.Property(e => e.Codigo).HasColumnName("Codigo");
            entity.Property(e => e.NombreCompleto).HasColumnName("NombreCompleto");
            entity.Property(e => e.Apellido).HasColumnName("Apellido");
            entity.Property(e => e.Nombre).HasColumnName("Nombre");
            entity.Property(e => e.Alias).HasColumnName("Alias");
            entity.Property(e => e.Cargo).HasColumnName("Cargo");
            entity.Property(e => e.Departamento).HasColumnName("Dpto");
            entity.Property(e => e.Estado).HasColumnName("Estado");
            entity.Property(e => e.Turno).HasColumnName("Turno");
            entity.Property(e => e.Categoria).HasColumnName("Categoria");
            entity.Property(e => e.IdUsuario).HasColumnName("IdUsuario");
            entity.Property(e => e.FrecuenciaPago).HasColumnName("FrecPago");
            entity.Property(e => e.TipoEmpleado).HasColumnName("TipEmp");
            entity.Property(e => e.GeneraNomina).HasColumnName("GenNomina");
            entity.Property(e => e.CuentaSueldo).HasColumnName("CtaSueldo");
            entity.Property(e => e.PrimerNombre).HasColumnName("PrimerNombre");
            entity.Property(e => e.SegundoNombre).HasColumnName("SegundoNombre");
            entity.Property(e => e.PrimerApellido).HasColumnName("PrimerApellido");
            entity.Property(e => e.SegundoApellido).HasColumnName("SegundoApellido");
            entity.Property(e => e.Direccion1).HasColumnName("Direccion1");
            entity.Property(e => e.Direccion2).HasColumnName("Direccion2");
            entity.Property(e => e.Direccion3).HasColumnName("Direccion3");
            entity.Property(e => e.Direccion4).HasColumnName("Direccion4");
            entity.Property(e => e.Ciudad).HasColumnName("Ciudad");
            entity.Property(e => e.CodProvincia).HasColumnName("CodProvincia");
            entity.Property(e => e.CP).HasColumnName("Cp");
            entity.Property(e => e.Municipio).HasColumnName("Municipio");
            entity.Property(e => e.Telefono).HasColumnName("Telefono");
            entity.Property(e => e.TelComercial).HasColumnName("TelComercial");
            entity.Property(e => e.ExtensionTel).HasColumnName("ExtensionTel");
            entity.Property(e => e.CorreoElect).HasColumnName("CorreoElect");
            entity.Property(e => e.Correo).HasColumnName("Correo");
            entity.Property(e => e.FechaContratacion).HasColumnName("FechaContr");
            entity.Property(e => e.FechaRevision).HasColumnName("FechaRevis");
            entity.Property(e => e.FechaRescision).HasColumnName("FechaRescis");
            entity.Property(e => e.UsuarioRegistro).HasColumnName("UsuarioReg");
            entity.Property(e => e.FechaCreacion).HasColumnName("FechaReg");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("UsuarioAct");
            entity.Property(e => e.FechaModificacion).HasColumnName("FechaAct");
            entity.HasIndex(e => e.IdEmpleadoExternal)
                .IsUnique()
                .HasDatabaseName("idx_tmaempleado_IdEmpleadoExternal");
        });

        modelBuilder.Entity<CodUnidad1Entity>(entity =>
        {
            entity.ToTable("tmacodunidad1");
            entity.HasKey(e => e.IdCodUnidad1);
            entity.Property(e => e.IdCodUnidad1).HasColumnName("IdCodUnidad1");
            entity.Property(e => e.Codigo).HasColumnName("Codigo");
            entity.Property(e => e.Descripcion).HasColumnName("Descripcion");
            entity.Property(e => e.UsuarioRegistro).HasColumnName("UsuarioReg");
            entity.Property(e => e.FechaCreacion).HasColumnName("FechaReg");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("UsuarioAct");
            entity.Property(e => e.FechaModificacion).HasColumnName("FechaAct");
            entity.HasIndex(e => e.Codigo)
                .IsUnique()
                .HasDatabaseName("idx_tmacodunidad1_Codigo");
        });

        modelBuilder.Entity<CuentaContableEntity>(entity =>
        {
            entity.ToTable("tmacuentacontable");
            entity.HasKey(e => e.IdCuentaContable);
            entity.Property(e => e.IdCuentaContable).HasColumnName("IdCuentaContable");
            entity.Property(e => e.Cuenta).HasColumnName("Cuenta");
            entity.Property(e => e.Descripcion).HasColumnName("Descripcion");
            entity.Property(e => e.Tipo).HasColumnName("Tipo");
            entity.Property(e => e.UsuarioRegistro).HasColumnName("UsuarioReg");
            entity.Property(e => e.FechaCreacion).HasColumnName("FechaReg");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("UsuarioAct");
            entity.Property(e => e.FechaModificacion).HasColumnName("FechaAct");
            entity.HasIndex(e => e.Cuenta)
                .IsUnique()
                .HasDatabaseName("idx_tmacuentacontable_Cuenta");
        });
    }
}
