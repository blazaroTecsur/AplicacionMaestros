using AplicacionMaestro.Application.Interfaces.CtasContables;
using AplicacionMaestro.Domain.Entities;
using AplicacionMaestro.Infrastructure.Persistence.DbContext;
using AplicacionMaestro.Infrastructure.Persistence.Entities.CuentasContables;
using Microsoft.EntityFrameworkCore;

namespace AplicacionMaestro.Infrastructure.Persistence.Repositories.CuentasContables
{
    public class CuentaContableRepository : ICuentaContableRepository
    {
        private readonly ApplicationDbContext _context;
        public CuentaContableRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SincronizarAsync(
            List<CuentaContable> cuentasContables,
            string usuario,
            CancellationToken ct)
        {
            if (cuentasContables == null || cuentasContables.Count == 0)
                return;

            _context.ChangeTracker.AutoDetectChangesEnabled = false;

            try
            {
                var codigos = cuentasContables
                    .Select(x => x.Cuenta)
                    .ToList();

                var existentes = await _context.CtasContables
                    .Where(x => codigos.Contains(x.Cuenta))
                    .ToDictionaryAsync(x => x.Cuenta, ct);

                var nuevos = new List<CuentaContableEntity>();

                foreach (var item in cuentasContables)
                {
                    if (existentes.TryGetValue(item.Cuenta, out var entity))
                    {
                        // 🔥 Update
                        entity.Descripcion = item.Descripcion;
                        entity.Tipo = item.Tipo;
                        entity.UsuarioModificacion = usuario;
                        entity.FechaModificacion = DateTime.UtcNow;
                    }
                    else
                    {
                        // 🔥 Insert
                        nuevos.Add(new CuentaContableEntity
                        {
                            Cuenta = item.Cuenta,
                            Descripcion = item.Descripcion,
                            Tipo = item.Tipo,
                            UsuarioRegistro = usuario,
                            FechaCreacion = DateTime.UtcNow
                        });
                    }
                }
                if (nuevos.Count > 0)
                    await _context.CtasContables.AddRangeAsync(nuevos, ct);
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
