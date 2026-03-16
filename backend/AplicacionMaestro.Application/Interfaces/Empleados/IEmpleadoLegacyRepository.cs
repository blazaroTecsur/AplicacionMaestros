using AplicacionMaestro.Domain.Entities;

namespace AplicacionMaestro.Application.Interfaces.Empleados
{
    public interface IEmpleadoLegacyRepository
    {
        Task SincronizarAsync(List<Empleado> empleados, CancellationToken cancellationToken);
    }
}
