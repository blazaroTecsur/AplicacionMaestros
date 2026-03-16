using AplicacionMaestro.Application.Features.Socios.Dtos;

namespace AplicacionMaestro.Application.ExternalServices.LDS
{
    public interface ILdsSocioService
    {
        Task<IReadOnlyCollection<SocioSyncDto>> ObtenerSociosAsync(
            CancellationToken cancellationToken);
    }
}
