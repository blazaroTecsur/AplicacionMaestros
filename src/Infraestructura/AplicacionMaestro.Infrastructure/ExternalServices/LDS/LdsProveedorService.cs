using AplicacionMaestro.Application;
using System.Net.Http.Json;

namespace AplicacionMaestro.Infrastructure;

public class LdsProveedorService : ILdsProveedorService
{
    private readonly HttpClient _httpClient;

    public LdsProveedorService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyCollection<ProveedorSyncDto>> ObtenerProveedoresAsync(CancellationToken ct)
    {
        var response = await _httpClient.GetAsync("/api/lds/proveedores", ct);
        response.EnsureSuccessStatusCode();

        var data = await response.Content
            .ReadFromJsonAsync<List<ProveedorSyncDto>>(cancellationToken: ct);

        return data ?? new List<ProveedorSyncDto>();
    }
}