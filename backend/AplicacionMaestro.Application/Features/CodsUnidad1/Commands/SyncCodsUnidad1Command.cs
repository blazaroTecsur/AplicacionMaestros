using AplicacionMaestro.Application.Features.CodsUnidad1.Dtos;
using MediatR;

namespace AplicacionMaestro.Application.Features.CodsUnidad1.Commands
{
    public record SyncCodsUnidad1Command(
        IReadOnlyCollection<CodigoUnidad1SyncDto> CodsUnidad1
    ) : IRequest<Unit>;
}
