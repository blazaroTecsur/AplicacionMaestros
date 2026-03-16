using AplicacionMaestro.Application.Interfaces.CodsUnidad1;
using AplicacionMaestro.Domain.Entities;
using AplicacionMaestro.Infrastructure.Persistence.DbContext;
using AplicacionMaestro.Infrastructure.Persistence.Entities.CodsUnidad1;
using Microsoft.EntityFrameworkCore;

namespace AplicacionMaestro.Infrastructure.Persistence.Repositories.CodsUnidad1
{
    public class CodUnidad1Repository : ICodUnidad1Repository
    {

        private readonly ApplicationDbContext _context;
    
        public CodUnidad1Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SincronizarAsync(
            List<CodUnidad1> codsUnidad1,
            string usuario,
            CancellationToken ct)
        {
            if (codsUnidad1 == null || codsUnidad1.Count == 0)
                return;

            _context.ChangeTracker.AutoDetectChangesEnabled = false;

            try
            {
                var codigos = codsUnidad1
                    .Select(x => x.Codigo)
                    .ToList();

                var existentes = await _context.CodsUnidad1
                    .Where(x => codigos.Contains(x.Codigo))
                    .ToDictionaryAsync(x => x.Codigo, ct);

                var nuevos = new List<CodUnidad1Entity>();

                foreach (var item in codsUnidad1)
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
                        nuevos.Add(new CodUnidad1Entity
                        {
                            Codigo = item.Codigo,
                            Descripcion = item.Descripcion,
                            UsuarioRegistro = usuario,
                            FechaCreacion = DateTime.UtcNow
                        });
                    }
                }
                if (nuevos.Count > 0)
                    await _context.CodsUnidad1.AddRangeAsync(nuevos, ct);
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
