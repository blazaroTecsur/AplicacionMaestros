using AplicacionMaestro.Application.Features.CodsUnidad1.Dtos;

namespace AplicacionMaestro.Application.ExternalServices.ERP
{
    public interface IErpCodUnidad1Service
    {
        Task<IReadOnlyCollection<CodigoUnidad1SyncDto>> ObtenerCodsUnidad1Async(
            CancellationToken cancellationToken);
    }
}
