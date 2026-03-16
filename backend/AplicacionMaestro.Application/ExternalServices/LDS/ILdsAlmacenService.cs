using AplicacionMaestro.Application.Features.Almacenes.Dtos;

namespace AplicacionMaestro.Application.ExternalServices.LDS
{
    public interface ILdsAlmacenService
    {
        Task<IReadOnlyCollection<AlmacenSyncDto>> ObtenerAlmacenesAsync(
            CancellationToken cancellationToken);
    }
}
