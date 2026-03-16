using AplicacionMaestro.Application.ExternalServices.ERP;
using AplicacionMaestro.Application.Features.CodsUnidad1.Dtos;
using System.Net.Http.Json;

namespace AplicacionMaestro.Infrastructure.ExternalServices.ERP
{
    public class ErpCodUnidad1Service : IErpCodUnidad1Service
    {
        private readonly HttpClient _httpClient;

        public ErpCodUnidad1Service(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<CodigoUnidad1SyncDto>> ObtenerCodsUnidad1Async(
            CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync(
                "/api/erp/codsunidad1",
                cancellationToken);

            response.EnsureSuccessStatusCode();

            var codsUnidad1 = await response.Content
                .ReadFromJsonAsync<List<CodigoUnidad1SyncDto>>(cancellationToken: cancellationToken);

            return codsUnidad1 ?? new List<CodigoUnidad1SyncDto>();
        }
    }
}
