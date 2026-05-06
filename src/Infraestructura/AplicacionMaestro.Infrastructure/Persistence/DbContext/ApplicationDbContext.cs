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
            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");
            entity.Property(e => e.IdExternal).HasColumnName("id_proveedor_external");
            entity.Property(e => e.TipoPersona).HasColumnName("tipo_persona");
            entity.Property(e => e.Ruc).HasColumnName("ruc");
            entity.Property(e => e.RazonSocial).HasColumnName("nombre_proveedor");
            entity.Property(e => e.Direccion1).HasColumnName("direccion1");
            entity.Property(e => e.Direccion2).HasColumnName("direccion2");
            entity.Property(e => e.Direccion3).HasColumnName("direccion3");
            entity.Property(e => e.Direccion4).HasColumnName("direccion4");
            entity.Property(e => e.Comprador).HasColumnName("comprador");
            entity.Property(e => e.Contacto).HasColumnName("contacto");
            entity.Property(e => e.Telefono).HasColumnName("telefono_contacto");
            entity.Property(e => e.CorreoExterno).HasColumnName("correo_externo_contacto");
            entity.Property(e => e.CorreoInterno).HasColumnName("correo_interno_contacto");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.UsuarioRegistro).HasColumnName("usuario_reg");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_reg");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuario_act");
            entity.Property(e => e.FechaModificacion).HasColumnName("fecha_act");
            entity.HasIndex(e => e.IdExternal)
                .IsUnique()
                .HasDatabaseName("idx_tmaproveedores_id_proveedor_external");
        });

        modelBuilder.Entity<ArticuloEntity>(entity =>
        {
            entity.ToTable("tmaarticulos");
            entity.HasKey(e => e.IdArticulo);
            entity.Property(e => e.IdArticulo).HasColumnName("id_articulo");
            entity.Property(e => e.IdExternal).HasColumnName("id_articulo_external");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.UnidadMedida).HasColumnName("unidad_medida");
            entity.Property(e => e.Tipo).HasColumnName("tipo");
            entity.Property(e => e.Origen).HasColumnName("origen");
            entity.Property(e => e.CodigoProducto).HasColumnName("codigo_producto");
            entity.Property(e => e.CodigoAbc).HasColumnName("codigo_abc");
            entity.Property(e => e.SegLote).HasColumnName("seg_lote");
            entity.Property(e => e.EstadoMaterial).HasColumnName("estado_material").HasConversion<string>();
            entity.Property(e => e.UsuarioRegistro).HasColumnName("usuario_reg");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_reg");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuario_act");
            entity.Property(e => e.FechaModificacion).HasColumnName("fecha_act");
            entity.HasIndex(e => e.IdExternal)
                .IsUnique()
                .HasDatabaseName("idx_tmaarticulos_id_articulo_external");
        });

        modelBuilder.Entity<SocioEntity>(entity =>
        {
            entity.ToTable("tmasocios");
            entity.HasKey(e => e.IdSocio);
            entity.Property(e => e.IdSocio).HasColumnName("id_socio");
            entity.Property(e => e.IdSocioExternal).HasColumnName("id_socio_external");
            entity.Property(e => e.CodigoSocio).HasColumnName("codigo_socio");
            entity.Property(e => e.TipoEmpleado).HasColumnName("tipo_empleado");
            entity.Property(e => e.NroReferencia).HasColumnName("nro_referencia");
            entity.Property(e => e.NombreCompleto).HasColumnName("nombre_completo");
            entity.Property(e => e.Supervisor).HasColumnName("supervisor");
            entity.Property(e => e.CodTrabajo).HasColumnName("cod_trabajo");
            entity.Property(e => e.TipoPago).HasColumnName("tipo_pago");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.DireccionLocaliz).HasColumnName("direccion_localiz");
            entity.Property(e => e.DireccionMensaje).HasColumnName("direccion_mensaje");
            entity.Property(e => e.Almacen).HasColumnName("almacen");
            entity.Property(e => e.Departamento).HasColumnName("departamento");
            entity.Property(e => e.Usuario).HasColumnName("usuario");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.UsuarioRegistro).HasColumnName("usuario_reg");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_reg");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuario_act");
            entity.Property(e => e.FechaModificacion).HasColumnName("fecha_act");
            entity.HasIndex(e => e.IdSocioExternal)
                .IsUnique()
                .HasDatabaseName("idx_tmasocios_id_socio_external");
        });

        modelBuilder.Entity<AptitudEntity>(entity =>
        {
            entity.ToTable("tmaaptitudes");
            entity.HasKey(e => e.IdAptitud);
            entity.Property(e => e.IdAptitud).HasColumnName("id_aptitud");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.UsuarioRegistro).HasColumnName("usuario_reg");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_reg");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuario_act");
            entity.Property(e => e.FechaModificacion).HasColumnName("fecha_act");
            entity.HasIndex(e => e.Codigo)
                .IsUnique()
                .HasDatabaseName("idx_tmaaptitudes_codigo");
        });

        modelBuilder.Entity<CertificadoEntity>(entity =>
        {
            entity.ToTable("tmacertificaciones");
            entity.HasKey(e => e.IdCertificacion);
            entity.Property(e => e.IdCertificacion).HasColumnName("id_certificacion");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.UsuarioRegistro).HasColumnName("usuario_reg");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_reg");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuario_act");
            entity.Property(e => e.FechaModificacion).HasColumnName("fecha_act");
            entity.HasIndex(e => e.Codigo)
                .IsUnique()
                .HasDatabaseName("idx_tmacertificaciones_codigo");
        });

        modelBuilder.Entity<AlmacenEntity>(entity =>
        {
            entity.ToTable("tmaalmacenes");
            entity.HasKey(e => e.IdAlmacen);
            entity.Property(e => e.IdAlmacen).HasColumnName("id_almacen");
            entity.Property(e => e.IdAlmacenExternal).HasColumnName("id_almacen_external");
            entity.Property(e => e.CodigoAlmacen).HasColumnName("codigo_almacen");
            entity.Property(e => e.NombreAlmacen).HasColumnName("nombre_almacen");
            entity.Property(e => e.Direccion1).HasColumnName("direccion1");
            entity.Property(e => e.Direccion2).HasColumnName("direccion2");
            entity.Property(e => e.Direccion3).HasColumnName("direccion3");
            entity.Property(e => e.Direccion4).HasColumnName("direccion4");
            entity.Property(e => e.Ciudad).HasColumnName("ciudad");
            entity.Property(e => e.CodigoProvincia).HasColumnName("codigo_provincia");
            entity.Property(e => e.CodigoPostal).HasColumnName("codigo_postal");
            entity.Property(e => e.Contacto).HasColumnName("contacto");
            entity.Property(e => e.Telefono).HasColumnName("telefono");
            entity.Property(e => e.Fax).HasColumnName("fax");
            entity.Property(e => e.Ruc).HasColumnName("ruc");
            entity.Property(e => e.UsuarioRegistro).HasColumnName("usuario_reg");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_reg");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuario_act");
            entity.Property(e => e.FechaModificacion).HasColumnName("fecha_act");
            entity.HasIndex(e => e.IdAlmacenExternal)
                .IsUnique()
                .HasDatabaseName("idx_tmaalmacenes_id_almacen_external");
        });

        modelBuilder.Entity<EmpleadoEntity>(entity =>
        {
            entity.ToTable("tmaempleados");
            entity.HasKey(e => e.IdEmpleado);
            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");
            entity.Property(e => e.IdEmpleadoExternal).HasColumnName("id_empleado_external");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.NombreCompleto).HasColumnName("nombre_completo");
            entity.Property(e => e.Apellido).HasColumnName("apellido");
            entity.Property(e => e.Nombre).HasColumnName("nombre");
            entity.Property(e => e.Alias).HasColumnName("alias");
            entity.Property(e => e.Cargo).HasColumnName("cargo");
            entity.Property(e => e.Departamento).HasColumnName("dpto");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Turno).HasColumnName("turno");
            entity.Property(e => e.Categoria).HasColumnName("categoria");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.FrecuenciaPago).HasColumnName("frec_pago");
            entity.Property(e => e.TipoEmpleado).HasColumnName("tip_emp");
            entity.Property(e => e.GeneraNomina).HasColumnName("gen_nomina");
            entity.Property(e => e.CuentaSueldo).HasColumnName("cta_sueldo");
            entity.Property(e => e.PrimerNombre).HasColumnName("primer_nombre");
            entity.Property(e => e.SegundoNombre).HasColumnName("segundo_nombre");
            entity.Property(e => e.PrimerApellido).HasColumnName("primer_apellido");
            entity.Property(e => e.SegundoApellido).HasColumnName("segundo_apellido");
            entity.Property(e => e.Direccion1).HasColumnName("direccion1");
            entity.Property(e => e.Direccion2).HasColumnName("direccion2");
            entity.Property(e => e.Direccion3).HasColumnName("direccion3");
            entity.Property(e => e.Direccion4).HasColumnName("direccion4");
            entity.Property(e => e.Ciudad).HasColumnName("ciudad");
            entity.Property(e => e.CodProvincia).HasColumnName("cod_provincia");
            entity.Property(e => e.CP).HasColumnName("cp");
            entity.Property(e => e.Municipio).HasColumnName("municipio");
            entity.Property(e => e.Telefono).HasColumnName("telefono");
            entity.Property(e => e.TelComercial).HasColumnName("tel_comercial");
            entity.Property(e => e.ExtensionTel).HasColumnName("extension_tel");
            entity.Property(e => e.CorreoElect).HasColumnName("correo_elect");
            entity.Property(e => e.Correo).HasColumnName("correo");
            entity.Property(e => e.FechaContratacion).HasColumnName("fecha_contr");
            entity.Property(e => e.FechaRevision).HasColumnName("fecha_revis");
            entity.Property(e => e.FechaRescision).HasColumnName("fecha_rescis");
            entity.Property(e => e.UsuarioRegistro).HasColumnName("usuario_reg");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_reg");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuario_act");
            entity.Property(e => e.FechaModificacion).HasColumnName("fecha_act");
            entity.HasIndex(e => e.IdEmpleadoExternal)
                .IsUnique()
                .HasDatabaseName("idx_tmaempleados_id_empleado_external");
        });

        modelBuilder.Entity<CodUnidad1Entity>(entity =>
        {
            entity.ToTable("tmacodsunidad1");
            entity.HasKey(e => e.IdCodUnidad1);
            entity.Property(e => e.IdCodUnidad1).HasColumnName("id_cod_unidad1");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.UsuarioRegistro).HasColumnName("usuario_reg");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_reg");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuario_act");
            entity.Property(e => e.FechaModificacion).HasColumnName("fecha_act");
            entity.HasIndex(e => e.Codigo)
                .IsUnique()
                .HasDatabaseName("idx_tmacodsunidad1_codigo");
        });

        modelBuilder.Entity<CuentaContableEntity>(entity =>
        {
            entity.ToTable("tmacuentascontables");
            entity.HasKey(e => e.IdCuentaContable);
            entity.Property(e => e.IdCuentaContable).HasColumnName("id_cuenta_contable");
            entity.Property(e => e.Cuenta).HasColumnName("cuenta");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Tipo).HasColumnName("tipo");
            entity.Property(e => e.UsuarioRegistro).HasColumnName("usuario_reg");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_reg");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuario_act");
            entity.Property(e => e.FechaModificacion).HasColumnName("fecha_act");
            entity.HasIndex(e => e.Cuenta)
                .IsUnique()
                .HasDatabaseName("idx_tmacuentascontables_cuenta");
        });
    }
}
