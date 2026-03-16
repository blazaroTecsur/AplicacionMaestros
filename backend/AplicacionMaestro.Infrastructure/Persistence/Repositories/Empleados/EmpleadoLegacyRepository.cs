using AplicacionMaestro.Application.Interfaces.Empleados;
using AplicacionMaestro.Domain.Entities;
using AplicacionMaestro.Infrastructure.Persistence.Entities.Empleados;
using Microsoft.EntityFrameworkCore;

namespace AplicacionMaestro.Infrastructure.Persistence.Repositories.Empleados
{
    public class EmpleadoLegacyRepository : IEmpleadoLegacyRepository
    {
        private readonly PlataformaInternaDbContext _context;

        public EmpleadoLegacyRepository(PlataformaInternaDbContext context)
        {
            _context = context;
        }

        public async Task SincronizarAsync(
            List<Empleado> empleados,
            CancellationToken ct)
        {
            if (empleados == null || empleados.Count == 0)
                return;

            _context.ChangeTracker.AutoDetectChangesEnabled = false;

            try
            {
                var ids = empleados
                    .Select(x => x.Codigo)
                    .Distinct()
                    .ToList();

                // Traer existentes
                var existentes = await _context.Empleados
                    .Where(x => ids.Contains(x.Dni))
                    .ToListAsync(ct);

                var dictExistentes = existentes
                    .ToDictionary(x => x.Dni);

                var nuevos = new List<EmpleadoLegacyEntity>();

                foreach (var empleado in empleados)
                {
                    if (!dictExistentes.TryGetValue(empleado.Codigo, out var entity))
                    {
                        // INSERT
                        entity = MapToLegacyEntity(empleado);
                        nuevos.Add(entity);

                    }
                    else
                    {
                        // UPDATE
                        MapToExistingEntity(entity, empleado);
                    }
                }
                if(nuevos.Count > 0)
                    await _context.Empleados.AddRangeAsync(nuevos, ct);

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
        private static EmpleadoLegacyEntity MapToLegacyEntity(Empleado empleado)
        {
            return new EmpleadoLegacyEntity
            {
                Dni = empleado.Codigo,
                Nombres = empleado.NombreCompleto,
                ApePaterno = empleado.NameTax.PrimerApellido,
                ApeMaterno = empleado.NameTax.SegundoApellido,
                Email = empleado.Contacto.Correo,
                Estado = true,
                UsuarioReg = "System", // O asignar un usuario específico si se tiene esa información
                FechaReg = DateTime.UtcNow
            };
        }

        private static void MapToExistingEntity(
            EmpleadoLegacyEntity entity,
            Empleado empleado)
        {
            entity.Nombres = empleado.NombreCompleto;
            entity.ApePaterno = empleado.NameTax.PrimerApellido;
            entity.ApeMaterno = empleado.NameTax.SegundoApellido;
            entity.Email = empleado.Contacto.Correo;
            entity.Estado = true;
            entity.UsuarioAct = "System"; 
            entity.FechaAct = DateTime.UtcNow;
        }

        #endregion
    }
}
