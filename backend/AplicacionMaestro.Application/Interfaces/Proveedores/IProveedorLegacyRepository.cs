using AplicacionMaestro.Application.Features.Proveedores.Models;

namespace AplicacionMaestro.Application;

public interface IProveedorLegacyRepository
{
    Task SincronizarAsync(
        List<ProveedorLegacySyncModel> proveedores,
        CancellationToken cancellationToken);
}
