using AplicacionMaestro.Application;
using System.Net.Http.Json;

namespace AplicacionMaestro.Infrastructure;

public class ErpProveedorService : IErpProveedorService
{
    private readonly HttpClient _httpClient;

    public ErpProveedorService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyCollection<ProveedorSyncDto>> ObtenerProveedoresAsync(
        CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(
            "/api/erp/proveedores",
            cancellationToken);

        response.EnsureSuccessStatusCode();

        var proveedores = await response.Content
            .ReadFromJsonAsync<List<ProveedorSyncDto>>(cancellationToken: cancellationToken);

        return proveedores ?? new List<ProveedorSyncDto>();
    }
}