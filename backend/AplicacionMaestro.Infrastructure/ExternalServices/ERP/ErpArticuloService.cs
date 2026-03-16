using AplicacionMaestro.Application;
using System.Net.Http.Json;

namespace AplicacionMaestro.Infrastructure;
public class ErpArticuloService : IErpArticuloService
{
    private readonly HttpClient _httpClient;

    public ErpArticuloService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyCollection<ArticuloSyncDto>> ObtenerArticulosAsync(
        CancellationToken cancellationToken)
    {
        // Endpoint de ejemplo
        var response = await _httpClient.GetAsync(
            "/api/erp/articulos",
            cancellationToken);

        response.EnsureSuccessStatusCode();

        var data = await response.Content
            .ReadFromJsonAsync<List<ArticuloSyncDto>>(cancellationToken: cancellationToken);

        return data ?? new List<ArticuloSyncDto>();
    }
}
