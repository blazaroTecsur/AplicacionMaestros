using AplicacionMaestro.Application.Interfaces.Almacenes;
using AplicacionMaestro.Domain.Entities;
using AplicacionMaestro.Infrastructure.Persistence.DbContext;
using AplicacionMaestro.Infrastructure.Persistence.Entities.Almacenes;
using Microsoft.EntityFrameworkCore;

namespace AplicacionMaestro.Infrastructure.Persistence.Repositories.Almacenes
{
    public class AlmacenRepository : IAlmacenRepository
    {
        private readonly ApplicationDbContext _context;
        public AlmacenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SincronizarAsync(
            List<Almacen> almacenes,
            string usuario,
            CancellationToken ct)
        {
            if (almacenes == null || almacenes.Count == 0)
                return;

            // 🔥 PERFORMANCE
            _context.ChangeTracker.AutoDetectChangesEnabled = false;

            try
            {
                // ========================================
                // 1 Obtener IDs externos
                // ========================================
                var externalIds = almacenes
                    .Select(x => x.IdAlmacenExternal)
                    .ToList();

                // ========================================
                // 2️ Traer almacenes existentes con relaciones (tracking activado)
                // ========================================
                var existentes = await _context.Almacenes
                    .Where(x => externalIds.Contains(x.IdAlmacenExternal))
                    .ToListAsync(ct);

                var almacenesDict = existentes.ToDictionary(x => x.IdAlmacenExternal);

                var nuevos = new List<AlmacenEntity>();

                foreach (var almacen in almacenes)
                {
                    if (!almacenesDict.TryGetValue(almacen.IdAlmacenExternal, out var entity))
                    {
                        entity = CrearEntity(almacen, usuario);
                        nuevos.Add(entity);

                    }
                    else
                    {
                        ActualizarEntity(entity, almacen, usuario);
                    }
                }

                if (nuevos.Count > 0)
                    await _context.Almacenes.AddRangeAsync(nuevos, ct);

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

        #region Métodos de mapeo
        // ===============================
        // CREAR NUEVO ALMACEN
        // ===============================
        private AlmacenEntity CrearEntity(Almacen almacen, string usuario)
        {
            return new AlmacenEntity
            {
                IdAlmacenExternal = almacen.IdAlmacenExternal,
                CodigoAlmacen = almacen.Codigo,
                NombreAlmacen = almacen.Nombre,
                Direccion1 = almacen.Direccion1,
                Direccion2 = almacen.Direccion2,
                Direccion3 = almacen.Direccion3,
                Direccion4 = almacen.Direccion4,
                Ciudad = almacen.Ciudad,
                CodigoProvincia = almacen.CodProvincia,
                CodigoPostal = almacen.CodigoPostal,

                Contacto= almacen.Contacto,
                Telefono = almacen.Telefono,
                Fax = almacen.Fax,
                Ruc = almacen.VatId,

                UsuarioRegistro = usuario,
                FechaCreacion = DateTime.UtcNow,
            };
        }

        // ===============================
        // ACTUALIZAR ALMACEN EXISTENTE
        // ===============================
        private void ActualizarEntity(
            AlmacenEntity entity,
            Almacen almacen,
            string usuario)
        {
            entity.CodigoAlmacen = almacen.Codigo;
            entity.NombreAlmacen = almacen.Nombre;

            entity.Direccion1 = almacen.Direccion1;
            entity.Direccion2 = almacen.Direccion2;
            entity.Direccion3 = almacen.Direccion3;
            entity.Direccion4 = almacen.Direccion4;
            entity.Ciudad = almacen.Ciudad;
            entity.CodigoProvincia = almacen.CodProvincia;
            entity.CodigoPostal = almacen.CodigoPostal;

            entity.Contacto= almacen.Contacto;
            entity.Telefono = almacen.Telefono;
            entity.Fax = almacen.Fax;

            entity.Ruc = almacen.VatId;

            entity.UsuarioModificacion = usuario;
            entity.FechaModificacion = DateTime.UtcNow;
        }

        #endregion
    }
}
