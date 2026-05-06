using AplicacionMaestro.Application.ExternalServices.LDS;
using AplicacionMaestro.Application.Features.Certificados.Dtos;
using System.Net.Http.Json;

namespace AplicacionMaestro.Infrastructure.ExternalServices.LDS
{
    public class LdsCertificadoService : ILdsCertificadoService
    {
        private readonly HttpClient _httpClient;

        public LdsCertificadoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<CertificadoSyncDto>> ObtenerCertificadosAsync(
            CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync(
                "/api/lds/certificados",
                cancellationToken);

            response.EnsureSuccessStatusCode();

            var certificados = await response.Content
                .ReadFromJsonAsync<List<CertificadoSyncDto>>(cancellationToken: cancellationToken);

            return certificados ?? new List<CertificadoSyncDto>();
        }
    }
}
