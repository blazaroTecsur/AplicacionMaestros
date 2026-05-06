using AplicacionMaestro.Application.Features.Aptitudes.Dtos;

namespace AplicacionMaestro.Application.ExternalServices.LDS
{
    public interface ILdsAptitudService
    {
        Task<IReadOnlyCollection<AptitudSyncDto>> ObtenerAptitudesAsync(
            CancellationToken cancellationToken);
    }
}
