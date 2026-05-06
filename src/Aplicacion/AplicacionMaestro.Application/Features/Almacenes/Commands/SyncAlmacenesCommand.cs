using AplicacionMaestro.Application.Features.Almacenes.Dtos;
using MediatR;

namespace AplicacionMaestro.Application.Features.Almacenes.Commands
{
    public record SyncAlmacenesCommand(
        IReadOnlyCollection<AlmacenSyncDto> Almacenes
    ) : IRequest<Unit>;
}
