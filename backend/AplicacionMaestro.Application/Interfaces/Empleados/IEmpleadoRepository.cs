using AplicacionMaestro.Domain.Entities;

namespace AplicacionMaestro.Application.Interfaces.Empleados
{
    public interface IEmpleadoRepository
    {
        Task SincronizarAsync(List<Empleado> empleados, string usuario, CancellationToken cancellationToken);
    }
}
