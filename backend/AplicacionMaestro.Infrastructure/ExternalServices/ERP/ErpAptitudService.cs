using AplicacionMaestro.Application.ExternalServices.ERP;
using AplicacionMaestro.Application.Features.Aptitudes.Dtos;
using System.Net.Http.Json;
using System.Text.Json;

namespace AplicacionMaestro.Infrastructure.ExternalServices.ERP
{
    public class ErpAptitudService : IErpAptitudService
    {
        private readonly HttpClient _httpClient;

        public ErpAptitudService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<AptitudSyncDto>> ObtenerAptitudesAsync(
            CancellationToken cancellationToken)
        {
            // Endpoint de ejemplo
            var response = await _httpClient.GetAsync(
                "/api/erp/aptitudes",
                cancellationToken);

            response.EnsureSuccessStatusCode();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var aptitudes = await response.Content
                .ReadFromJsonAsync<List<AptitudSyncDto>>(options, cancellationToken: cancellationToken);

            return aptitudes ?? new List<AptitudSyncDto>();
        }
    }
}
