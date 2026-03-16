using AplicacionMaestro.Application.Features.Socios.Dtos;

namespace AplicacionMaestro.Application.ExternalServices.ERP
{
    public interface IErpSocioService
    {
        Task<IReadOnlyCollection<SocioSyncDto>> ObtenerSociosAsync(
            CancellationToken cancellationToken);
    }
}
