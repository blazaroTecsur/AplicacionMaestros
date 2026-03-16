using MediatR;

namespace AplicacionMaestro.Application.Features.Articulos.Commands
{
    public record SyncArticulosCommand(
        IReadOnlyCollection<ArticuloSyncDto> Articulos
    ) : IRequest<Unit>;

}
