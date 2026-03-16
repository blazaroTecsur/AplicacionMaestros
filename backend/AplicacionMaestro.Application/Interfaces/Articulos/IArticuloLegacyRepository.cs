using AplicacionMaestro.Application.Features.Articulos.Models;

namespace AplicacionMaestro.Application;

public interface IArticuloLegacyRepository
{
    Task SincronizarAsync(
        List<ArticuloLegacySyncModel> articulos,
        CancellationToken cancellationToken);
}
