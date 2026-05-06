using AplicacionMaestro.Application.Features.Aptitudes.Dtos;
using MediatR;

namespace AplicacionMaestro.Application.Features.Aptitudes.Commands
{
    public record SyncAptitudesCommand(
        IReadOnlyCollection<AptitudSyncDto> Aptitudes
    ) : IRequest<Unit>;

}
