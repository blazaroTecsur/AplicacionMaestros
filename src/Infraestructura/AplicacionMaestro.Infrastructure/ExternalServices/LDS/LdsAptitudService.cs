using AplicacionMaestro.Application.ExternalServices.LDS;
using AplicacionMaestro.Application.Features.Aptitudes.Dtos;
using System.Net.Http.Json;

namespace AplicacionMaestro.Infrastructure.ExternalServices.LDS
{
    public class LdsAptitudService : ILdsAptitudService
    {
        private readonly HttpClient _httpClient;

        public LdsAptitudService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<AptitudSyncDto>> ObtenerAptitudesAsync(
            CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync(
                "/api/lds/aptitudes",
                cancellationToken);

            response.EnsureSuccessStatusCode();

            var aptitudes = await response.Content
                .ReadFromJsonAsync<List<AptitudSyncDto>>(cancellationToken: cancellationToken);

            return aptitudes ?? new List<AptitudSyncDto>();
        }
    }
}
