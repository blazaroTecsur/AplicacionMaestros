using AplicacionMaestro.Application.Features.Empleados.Dtos;

namespace AplicacionMaestro.Application.ExternalServices.LDS
{
    public interface ILdsEmpleadoService
    {
        Task<IReadOnlyCollection<EmpleadoSyncDto>> ObtenerEmpleadosAsync(
            CancellationToken cancellationToken);
    }
}
