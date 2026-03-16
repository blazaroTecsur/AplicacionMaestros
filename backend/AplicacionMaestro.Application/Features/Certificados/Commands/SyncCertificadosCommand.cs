using AplicacionMaestro.Application.Features.Certificados.Dtos;
using MediatR;

namespace AplicacionMaestro.Application.Features.Certificados.Commands
{
    public record SyncCertificadosCommand(
        IReadOnlyCollection<CertificadoSyncDto> Certificados
    ) : IRequest<Unit>;
}
