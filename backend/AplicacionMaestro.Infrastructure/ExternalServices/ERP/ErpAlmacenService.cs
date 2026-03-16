using AplicacionMaestro.Application.ExternalServices.ERP;
using AplicacionMaestro.Application.Features.Almacenes.Dtos;
using System.Net.Http.Json;

namespace AplicacionMaestro.Infrastructure.ExternalServices.ERP
{
    public class ErpAlmacenService : IErpAlmacenService
    {
        private readonly HttpClient _httpClient;

        public ErpAlmacenService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<AlmacenSyncDto>> ObtenerAlmacenesAsync(
            CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync(
                "/api/erp/almacenes",
                cancellationToken);

            response.EnsureSuccessStatusCode();

            var almacenes = await response.Content
                .ReadFromJsonAsync<List<AlmacenSyncDto>>(cancellationToken: cancellationToken);

            return almacenes ?? new List<AlmacenSyncDto>();
        }
    }
}
