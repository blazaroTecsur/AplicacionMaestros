using AplicacionMaestro.Application.ExternalServices.ERP;
using AplicacionMaestro.Application.Features.CtasContables.Dtos;
using System.Net.Http.Json;

namespace AplicacionMaestro.Infrastructure.ExternalServices.ERP
{
    public class ErpCuentaContableService : IErpCuentaContableService
    {
        private readonly HttpClient _httpClient;

        public ErpCuentaContableService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<CuentaContableSyncDto>> ObtenerCuentasContablesSAsync(
            CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync(
                "/api/erp/cuentascontables",
                cancellationToken);

            response.EnsureSuccessStatusCode();

            var ctasContables = await response.Content
                .ReadFromJsonAsync<List<CuentaContableSyncDto>>(cancellationToken: cancellationToken);

            return ctasContables ?? new List<CuentaContableSyncDto>();
        }
    }
}
