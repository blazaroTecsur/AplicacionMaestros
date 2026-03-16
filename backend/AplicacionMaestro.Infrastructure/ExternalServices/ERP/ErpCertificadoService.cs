using AplicacionMaestro.Application.ExternalServices.ERP;
using AplicacionMaestro.Application.Features.Certificados.Dtos;
using System.Net.Http.Json;

namespace AplicacionMaestro.Infrastructure.ExternalServices.ERP
{
    public class ErpCertificadoService : IErpCertificadoService
    {
        private readonly HttpClient _httpClient;

        public ErpCertificadoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<CertificadoSyncDto>> ObtenerCertificadosAsync(
            CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync(
                "/api/erp/certificados",
                cancellationToken);

            response.EnsureSuccessStatusCode();

            var certificados = await response.Content
                .ReadFromJsonAsync<List<CertificadoSyncDto>>(cancellationToken: cancellationToken);

            return certificados ?? new List<CertificadoSyncDto>();
        }
    }
}
