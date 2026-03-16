using AplicacionMaestro.Application;
using AplicacionMaestro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace AplicacionMaestro.Infrastructure;
    public class SocioLegacyRepository : ISocioLegacyRepository
    {
        private readonly PlataformaInternaDbContext _context;

        public SocioLegacyRepository(PlataformaInternaDbContext context)
        {
            _context = context;
        }
        public async Task SincronizarAsync(
            List<Socio> socios,
            CancellationToken ct)
        {
            if (socios == null || socios.Count == 0)
                return;

            _context.ChangeTracker.AutoDetectChangesEnabled = false;

            try
            {
                var ids = socios
                    .Select(x => x.CodigoSocio)
                    .Distinct()
                    .ToList();

                // Traer existentes
                var existentes = await _context.Socios
                    .Where(x => ids.Contains(x.Dni))
                    .ToListAsync(ct);

                var dictExistentes = existentes
                    .ToDictionary(x => x.Dni);

                var nuevos = new List<SocioLegacyEntity>();

                foreach (var socio in socios)
                {
                    if (!dictExistentes.TryGetValue(socio.CodigoSocio, out var entity))
                    {
                        // INSERT
                        entity = MapToLegacyEntity(socio);
                        nuevos.Add(entity);

                }
                    else
                    {
                        // UPDATE
                        MapToExistingEntity(entity, socio);
                    }
                }
                if (nuevos.Count > 0)
                    await _context.Socios.AddRangeAsync(nuevos, ct);

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
        private static SocioLegacyEntity MapToLegacyEntity(Socio socio)
        {
            return new SocioLegacyEntity
            {
                Dni = socio.CodigoSocio,
                Nombres = socio.Nombre.Nombres,
                ApePaterno = socio.Nombre.ApePaterno,
                ApeMaterno = socio.Nombre.ApeMaterno,
                Email = socio.General.Email,
                Estado = socio.Activo,
                UsuarioReg = "System", // O asignar un usuario específico si se tiene esa información
                FechaReg = socio.FechaCreacion
            };
        }

        private static void MapToExistingEntity(
            SocioLegacyEntity entity,
            Socio socio)
        {
            entity.Nombres = socio.Nombre.Nombres;
            entity.ApePaterno = socio.Nombre.ApePaterno;
            entity.ApeMaterno = socio.Nombre.ApeMaterno;
            entity.Email = socio.General.Email;
            entity.Estado = socio.Activo;
            entity.UsuarioAct = "System"; // O asignar un usuario específico si se tiene esa información
            entity.FechaAct = socio.FechaModificacion;
        }

        #endregion

    }
