using AplicacionMaestro.Application.ExternalServices.LDS;
using AplicacionMaestro.Application.Features.Empleados.Dtos;
using System.Net.Http.Json;

namespace AplicacionMaestro.Infrastructure.ExternalServices.LDS
{
    public class LdsEmpleadoService : ILdsEmpleadoService
    {
        private readonly HttpClient _httpClient;

        public LdsEmpleadoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<EmpleadoSyncDto>> ObtenerEmpleadosAsync(CancellationToken ct)
        {
            var response = await _httpClient.GetAsync("/api/lds/empleados", ct);
            response.EnsureSuccessStatusCode();

            var data = await response.Content
                .ReadFromJsonAsync<List<EmpleadoSyncDto>>(cancellationToken: ct);

            return data ?? new List<EmpleadoSyncDto>();
        }
    }
}
