using AplicacionMaestro.Application.Features.Certificados.Dtos;

namespace AplicacionMaestro.Application.ExternalServices.LDS
{
    public interface ILdsCertificadoService
    {
        Task<IReadOnlyCollection<CertificadoSyncDto>> ObtenerCertificadosAsync(
            CancellationToken cancellationToken);
    }
}
