using AplicacionMaestro.Application.Interfaces.Certificados;
using AplicacionMaestro.Domain.Entities;
using AplicacionMaestro.Infrastructure.Persistence.DbContext;
using AplicacionMaestro.Infrastructure.Persistence.Entities.Certificaciones;
using Microsoft.EntityFrameworkCore;
namespace AplicacionMaestro.Infrastructure.Persistence.Repositories.Certificados
{
    public class CertificadoRepository : ICertificadoRepository
    {
        private readonly ApplicationDbContext _context;

        public CertificadoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SincronizarAsync(
            List<Certificado> certificados,
            string usuario,
            CancellationToken ct)
        {
            if (certificados == null || certificados.Count == 0)
                return;

            _context.ChangeTracker.AutoDetectChangesEnabled = false;
            try
            {
                var codigos = certificados
                    .Select(x => x.Codigo)
                    .ToList();

                var existentes = await _context.Certificaciones
                    .Where(x => codigos.Contains(x.Codigo))
                    .ToDictionaryAsync(x => x.Codigo, ct);

                var nuevos = new List<CertificadoEntity>();

                foreach (var item in certificados)
                {
                    if (existentes.TryGetValue(item.Codigo, out var entity))
                    {
                        entity.Descripcion = item.Descripcion;
                        entity.UsuarioModificacion = usuario;
                        entity.FechaModificacion = DateTime.UtcNow;
                    }
                    else
                    {
                        nuevos.Add(new CertificadoEntity
                        {
                            Codigo = item.Codigo,
                            Descripcion = item.Descripcion,
                            UsuarioRegistro = usuario,
                            FechaCreacion = DateTime.UtcNow
                        });
                    }
                }
                if (nuevos.Count > 0)
                    await _context.Certificaciones.AddRangeAsync(nuevos, ct);
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
