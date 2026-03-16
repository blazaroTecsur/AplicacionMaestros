using AplicacionMaestro.Application.Interfaces.Aptitudes;
using AplicacionMaestro.Domain.Entities;
using AplicacionMaestro.Infrastructure.Persistence.DbContext;
using AplicacionMaestro.Infrastructure.Persistence.Entities.Aptitudes;
using Microsoft.EntityFrameworkCore;

namespace AplicacionMaestro.Infrastructure.Persistence.Repositories.Aptitudes
{
    public class AptitudRepository : IAptitudRepository
    {
        private readonly ApplicationDbContext _context;

        public AptitudRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SincronizarAsync(
            List<Aptitud> aptitudes,
            string usuario,
            CancellationToken ct)
        {

            if (aptitudes == null || aptitudes.Count == 0)
                return;

            _context.ChangeTracker.AutoDetectChangesEnabled = false;

            try
            {
                var codigos = aptitudes
                    .Select(x => x.Codigo)
                    .ToList();

                var existentes = await _context.Aptitudes
                    .Where(x => codigos.Contains(x.Codigo))
                    .ToDictionaryAsync(x => x.Codigo, ct);

                var nuevos = new List<AptitudEntity>();

                foreach (var item in aptitudes)
                {
                    if (existentes.TryGetValue(item.Codigo, out var entity))
                    {
                        // 🔥 Update
                        entity.Descripcion = item.Descripcion;
                        entity.UsuarioModificacion = usuario;
                        entity.FechaModificacion = DateTime.UtcNow;
                    }
                    else
                    {
                        // 🔥 Insert
                        nuevos.Add(new AptitudEntity
                        {
                            Codigo = item.Codigo,
                            Descripcion = item.Descripcion,
                            UsuarioRegistro = usuario,
                            FechaCreacion = DateTime.UtcNow
                        });
                    }
                }
                if (nuevos.Count > 0)
                    await _context.Aptitudes.AddRangeAsync(nuevos, ct);

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
    }
}
