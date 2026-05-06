using AplicacionMaestro.Domain.Entities;

namespace AplicacionMaestro.Application;
    public interface ISocioRepository
    {
        Task SincronizarAsync(List<Socio> socios, string usuario, CancellationToken cancellationToken);
    }
