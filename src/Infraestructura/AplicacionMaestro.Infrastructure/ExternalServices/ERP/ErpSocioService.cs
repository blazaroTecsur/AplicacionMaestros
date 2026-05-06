using AplicacionMaestro.Application.ExternalServices.ERP;
using AplicacionMaestro.Application.Features.Socios.Dtos;
using System.Net.Http.Json;

namespace AplicacionMaestro.Infrastructure.ExternalServices.ERP
{
    public class ErpSocioService : IErpSocioService
    {
        private readonly HttpClient _httpClient;

        public ErpSocioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<SocioSyncDto>> ObtenerSociosAsync(
            CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync(
                "/api/erp/socios",
                cancellationToken);

            response.EnsureSuccessStatusCode();

            var socios = await response.Content
                .ReadFromJsonAsync<List<SocioSyncDto>>(cancellationToken: cancellationToken);

            return socios ?? new List<SocioSyncDto>();
        }
    }
}
