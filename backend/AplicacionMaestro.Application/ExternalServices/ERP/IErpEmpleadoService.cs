using AplicacionMaestro.Application.Features.Empleados.Dtos;

namespace AplicacionMaestro.Application.ExternalServices.ERP
{
    public interface IErpEmpleadoService
    {
        Task<IReadOnlyCollection<EmpleadoSyncDto>> ObtenerEmpleadosAsync(
            CancellationToken cancellationToken);
    }
}
