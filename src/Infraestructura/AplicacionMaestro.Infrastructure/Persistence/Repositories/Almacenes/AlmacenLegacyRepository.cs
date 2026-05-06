using AplicacionMaestro.Application.Interfaces.Almacenes;
using AplicacionMaestro.Domain.Entities;
using AplicacionMaestro.Infrastructure.Persistence.Entities.Almacenes;
using Microsoft.EntityFrameworkCore;

namespace AplicacionMaestro.Infrastructure.Persistence.Repositories.Almacenes
{
    public class AlmacenLegacyRepository : IAlmacenLegacyRepository
    {
        private readonly PlataformaInternaDbContext _context;

        public AlmacenLegacyRepository(PlataformaInternaDbContext context)
        {
            _context = context;
        }

        public async Task SincronizarAsync(
            List<Almacen> almacenes,
            CancellationToken ct)
        {
            if (almacenes == null || almacenes.Count == 0)
                return;

            _context.ChangeTracker.AutoDetectChangesEnabled = false;

            try
            {
                var ids = almacenes
                    .Select(x => x.Codigo)
                    .Distinct()
                    .ToList();

                // Traer existentes
                var existentes = await _context.Almacenes
                    .Where(x => ids.Contains(x.CodigoAlmacen))
                    .ToListAsync(ct);

                var dictExistentes = existentes
                    .ToDictionary(x => x.CodigoAlmacen);

                var nuevos = new List<AlmacenLegacyEntity>();

                foreach (var almacen in almacenes)
                {
                    if (!dictExistentes.TryGetValue(almacen.Codigo, out var entity))
                    {
                        entity = CrearEntity(almacen);
                        nuevos.Add(entity);

                    }
                    else
                    {
                        ActualizarEntity(entity, almacen);
                    }
                }
                if(nuevos.Count > 0)
                    await _context.Almacenes.AddRangeAsync(nuevos, ct);

                await _context.SaveChangesAsync(ct);

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _context.ChangeTracker.AutoDetectChangesEnabled = true;
            }

        }

        #region Metódos de mapeo
        private static AlmacenLegacyEntity CrearEntity(Almacen almacen)
        {
            return new AlmacenLegacyEntity
            {
                CodigoAlmacen = almacen.Codigo,
                NombreAlmacen = almacen.Nombre,
                Direccion = almacen.Direccion1,
                Correo = string.Empty,
                Responsable = almacen.Contacto,
                Estado = true,
                UsuarioRegistro = "System", 
                FechaCreacion = DateTime.UtcNow
            };
        }

        private static void ActualizarEntity(
            AlmacenLegacyEntity entity,
            Almacen almacen)
        {
            entity.NombreAlmacen = almacen.Nombre;
            entity.Direccion = almacen.Direccion1;
            entity.Responsable = almacen.Contacto;
            entity.UsuarioModificacion = "System"; 
            entity.FechaModificacion = DateTime.UtcNow;
        }
        #endregion
    }
}
