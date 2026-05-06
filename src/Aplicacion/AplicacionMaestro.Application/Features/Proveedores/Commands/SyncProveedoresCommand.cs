using MediatR;
namespace AplicacionMaestro.Application;

public record SyncProveedoresCommand(
    IReadOnlyCollection<ProveedorSyncDto> Proveedores
) : IRequest<Unit>;

