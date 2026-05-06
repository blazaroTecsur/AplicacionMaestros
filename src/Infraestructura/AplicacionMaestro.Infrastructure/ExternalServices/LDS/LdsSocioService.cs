using AplicacionMaestro.Application.ExternalServices.LDS;
using AplicacionMaestro.Application.Features.Socios.Dtos;
using System.Net.Http.Json;

namespace AplicacionMaestro.Infrastructure.ExternalServices.LDS
{
    public class LdsSocioService : ILdsSocioService
    {
        private readonly HttpClient _httpClient;

        public LdsSocioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<SocioSyncDto>> ObtenerSociosAsync(CancellationToken ct)
        {
            var response = await _httpClient.GetAsync("/api/lds/socios", ct);
            response.EnsureSuccessStatusCode();

            var data = await response.Content
                .ReadFromJsonAsync<List<SocioSyncDto>>(cancellationToken: ct);

            return data ?? new List<SocioSyncDto>();
        }
    }

}
