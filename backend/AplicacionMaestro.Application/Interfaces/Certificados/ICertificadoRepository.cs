using AplicacionMaestro.Domain.Entities;

namespace AplicacionMaestro.Application.Interfaces.Certificados
{
    public interface ICertificadoRepository
    {
        Task SincronizarAsync(List<Certificado> certificados, string usuario, CancellationToken ct);
    }
}
