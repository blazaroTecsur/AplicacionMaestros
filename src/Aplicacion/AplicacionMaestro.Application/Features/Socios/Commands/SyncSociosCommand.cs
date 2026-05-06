using AplicacionMaestro.Application.Features.Socios.Dtos;
using MediatR;

namespace AplicacionMaestro.Application.Features.Socios.Commands
{
    public record SyncSociosCommand(
        IReadOnlyCollection<SocioSyncDto> socios
    ) : IRequest<Unit>;
}
