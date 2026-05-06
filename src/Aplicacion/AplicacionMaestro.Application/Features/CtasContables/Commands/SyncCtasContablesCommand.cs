using AplicacionMaestro.Application.Features.CtasContables.Dtos;
using MediatR;

namespace AplicacionMaestro.Application.Features.CtasContables.Commands
{
    public record SyncCtasContablesCommand(
        IReadOnlyCollection<CuentaContableSyncDto> CuentasContables
    ) : IRequest<Unit>;
}
