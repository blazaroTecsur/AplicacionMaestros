using AplicacionMaestro.Application.Features.Almacenes.Dtos;
using AplicacionMaestro.Application.Features.Aptitudes.Dtos;

namespace AplicacionMaestro.Application.ExternalServices.ERP
{
    public interface IErpAlmacenService
    {
        Task<IReadOnlyCollection<AlmacenSyncDto>> ObtenerAlmacenesAsync(
            CancellationToken cancellationToken);
    }
}
