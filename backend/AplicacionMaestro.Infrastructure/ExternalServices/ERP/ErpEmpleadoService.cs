using AplicacionMaestro.Application.ExternalServices.ERP;
using AplicacionMaestro.Application.Features.Empleados.Dtos;
using System.Net.Http.Json;

namespace AplicacionMaestro.Infrastructure.ExternalServices.ERP
{
    public class ErpEmpleadoService : IErpEmpleadoService
    {
        private readonly HttpClient _httpClient;

        public ErpEmpleadoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<EmpleadoSyncDto>> ObtenerEmpleadosAsync(
            CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync(
                "/api/erp/empleados",
                cancellationToken);

            response.EnsureSuccessStatusCode();

            var empleados = await response.Content
                .ReadFromJsonAsync<List<EmpleadoSyncDto>>(cancellationToken: cancellationToken);

            return empleados ?? new List<EmpleadoSyncDto>();
        }
    }
}
