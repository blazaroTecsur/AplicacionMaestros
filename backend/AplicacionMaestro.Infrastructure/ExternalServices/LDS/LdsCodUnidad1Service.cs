using AplicacionMaestro.Application.ExternalServices.LDS;
using AplicacionMaestro.Application.Features.CodsUnidad1.Dtos;
using System.Net.Http.Json;

namespace AplicacionMaestro.Infrastructure.ExternalServices.LDS
{
    public class LdsCodUnidad1Service : ILdsCodUnidad1Service
    {
        private readonly HttpClient _httpClient;

        public LdsCodUnidad1Service(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<CodigoUnidad1SyncDto>> ObtenerCodsUnidad1Async(CancellationToken ct)
        {
            var response = await _httpClient.GetAsync("/api/lds/codigosunidad1", ct);
            response.EnsureSuccessStatusCode();

            var data = await response.Content
                .ReadFromJsonAsync<List<CodigoUnidad1SyncDto>>(cancellationToken: ct);

            return data ?? new List<CodigoUnidad1SyncDto>();
        }
    }
}
