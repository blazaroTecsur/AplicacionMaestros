using AplicacionMaestro.Application.Features.Aptitudes.Dtos;

namespace AplicacionMaestro.Application.ExternalServices.ERP
{
    public interface IErpAptitudService
    {
        Task<IReadOnlyCollection<AptitudSyncDto>> ObtenerAptitudesAsync(
            CancellationToken cancellationToken);
    }
}
