using AplicacionMaestro.Infrastructure.Persistence.Entities.Almacenes;
using AplicacionMaestro.Infrastructure.Persistence.Entities.Empleados;
using Microsoft.EntityFrameworkCore;

namespace AplicacionMaestro.Infrastructure
{
    public class PlataformaInternaDbContext : DbContext
    {
        public PlataformaInternaDbContext(
            DbContextOptions<PlataformaInternaDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProveedorLegacyEntity> Proveedores => Set<ProveedorLegacyEntity>();
        public DbSet<ArticuloLegacyEntity> Articulos => Set<ArticuloLegacyEntity>();
        public DbSet<SocioLegacyEntity> Socios => Set<SocioLegacyEntity>();
        public DbSet<AlmacenLegacyEntity> Almacenes => Set<AlmacenLegacyEntity>();
        public DbSet<EmpleadoLegacyEntity> Empleados => Set<EmpleadoLegacyEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProveedorLegacyEntity>(entity =>
            {
                entity.HasKey(e => e.IdEntidad);
                entity.HasIndex(e => e.Ruc)
                      .IsUnique();
            });

            modelBuilder.Entity<ArticuloLegacyEntity>(entity =>
            {
                entity.HasKey(e => e.IdMatricula);

                entity.HasIndex(e => e.Codigo)
                      .IsUnique();
            });

            modelBuilder.Entity<SocioLegacyEntity>(entity =>
            {
                entity.HasKey(e => e.IdPersonal);

                entity.HasIndex(e => e.Dni)
                      .IsUnique();
            });

            modelBuilder.Entity<AlmacenLegacyEntity>(entity =>
            {
                entity.HasKey(e => e.IdAlmacen);
                entity.HasIndex(e => e.CodigoAlmacen)
                      .IsUnique();
            });

            modelBuilder.Entity<EmpleadoLegacyEntity>(entity =>
            {
                entity.HasKey(e => e.IdPersonal);

                entity.HasIndex(e => e.Dni)
                      .IsUnique();
            });

        }
    }

}
