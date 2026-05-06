using AplicacionMaestro.Application.Features.CodsUnidad1.Dtos;

namespace AplicacionMaestro.Application.ExternalServices.LDS
{
    public interface ILdsCodUnidad1Service
    {
        Task<IReadOnlyCollection<CodigoUnidad1SyncDto>> ObtenerCodsUnidad1Async(
            CancellationToken cancellationToken);
    }
}
