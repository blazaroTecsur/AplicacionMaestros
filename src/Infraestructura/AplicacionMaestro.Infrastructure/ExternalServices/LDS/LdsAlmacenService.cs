using AplicacionMaestro.Application.ExternalServices.LDS;
using AplicacionMaestro.Application.Features.Almacenes.Dtos;
using System.Net.Http.Json;

namespace AplicacionMaestro.Infrastructure.ExternalServices.LDS
{
    public class LdsAlmacenService : ILdsAlmacenService
    {
        private readonly HttpClient _httpClient;

        public LdsAlmacenService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<AlmacenSyncDto>> ObtenerAlmacenesAsync(
            CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync(
                "/api/lds/almacenes",
                cancellationToken);

            response.EnsureSuccessStatusCode();

            var almacenes = await response.Content
                .ReadFromJsonAsync<List<AlmacenSyncDto>>(cancellationToken: cancellationToken);

            return almacenes ?? new List<AlmacenSyncDto>();
        }
    }
}
