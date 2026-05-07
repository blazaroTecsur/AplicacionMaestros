using AplicacionMaestro.Application.ExternalServices.ERP;
using AplicacionMaestro.Application.Features.Aptitudes.Dtos;
using System.Net.Http.Json;
using System.Text.Json;

namespace AplicacionMaestro.Infrastructure.ExternalServices.ERP
{
    public class ErpAptitudService : IErpAptitudService
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private readonly HttpClient _httpClient;

        public ErpAptitudService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<AptitudSyncDto>> ObtenerAptitudesAsync(
            CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync(
                "/api/erp/aptitudes",
                cancellationToken);

            response.EnsureSuccessStatusCode();

            var aptitudes = await response.Content
                .ReadFromJsonAsync<List<AptitudSyncDto>>(JsonOptions, cancellationToken: cancellationToken);

            return aptitudes ?? new List<AptitudSyncDto>();
        }
    }
}
