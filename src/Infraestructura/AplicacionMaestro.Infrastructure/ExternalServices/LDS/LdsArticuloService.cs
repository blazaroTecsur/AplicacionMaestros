using AplicacionMaestro.Application;
using System.Net.Http.Json;

namespace AplicacionMaestro.Infrastructure;

public class LdsArticuloService : ILdsArticuloService
{
    private readonly HttpClient _httpClient;

    public LdsArticuloService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyCollection<ArticuloSyncDto>> ObtenerArticulosAsync(
        CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(
            "/api/lds/articulos",
            cancellationToken);

        response.EnsureSuccessStatusCode();

        var articulos = await response.Content
            .ReadFromJsonAsync<List<ArticuloSyncDto>>(cancellationToken: cancellationToken);

        return articulos ?? new List<ArticuloSyncDto>();
    }
}
