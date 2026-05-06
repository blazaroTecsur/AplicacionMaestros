using AplicacionMaestro.Domain.Entities;

namespace AplicacionMaestro.Application;
public interface ISocioLegacyRepository
{
    Task SincronizarAsync(List<Socio> socios, CancellationToken cancellationToken);
}
