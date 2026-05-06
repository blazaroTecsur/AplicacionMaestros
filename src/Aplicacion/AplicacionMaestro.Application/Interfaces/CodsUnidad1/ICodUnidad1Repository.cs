using AplicacionMaestro.Domain.Entities;

namespace AplicacionMaestro.Application.Interfaces.CodsUnidad1
{
    public interface ICodUnidad1Repository
    {
        Task SincronizarAsync(
            List<CodUnidad1> codsUnidad1,
            string usuario,
            CancellationToken ct);
    }
}
