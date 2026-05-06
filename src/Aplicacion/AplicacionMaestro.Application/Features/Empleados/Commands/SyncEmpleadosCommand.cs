using AplicacionMaestro.Application.Features.Empleados.Dtos;
using MediatR;

namespace AplicacionMaestro.Application.Features.Empleados.Commands
{
    public record SyncEmpleadosCommand(
        IReadOnlyCollection<EmpleadoSyncDto> Empleados
    ) : IRequest<Unit>;
}
