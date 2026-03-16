using AplicacionMaestro.Application.Features.Certificados.Dtos;

namespace AplicacionMaestro.Application.ExternalServices.ERP
{
    public interface IErpCertificadoService
    {
        Task<IReadOnlyCollection<CertificadoSyncDto>> ObtenerCertificadosAsync(
            CancellationToken cancellationToken);
    }
}
