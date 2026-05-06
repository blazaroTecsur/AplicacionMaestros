using AplicacionMaestro.Application.ExternalServices.LDS;
using AplicacionMaestro.Application.Features.CtasContables.Dtos;
using System.Net.Http.Json;

namespace AplicacionMaestro.Infrastructure.ExternalServices.LDS
{
    public class LdsCuentaContableService : ILdsCuentaContableService
    {
        private readonly HttpClient _httpClient;

        public LdsCuentaContableService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<CuentaContableSyncDto>> ObtenerCuentasContablesSAsync(CancellationToken ct)
        {
            var response = await _httpClient.GetAsync("/api/lds/cuentascontables", ct);
            response.EnsureSuccessStatusCode();

            var data = await response.Content
                .ReadFromJsonAsync<List<CuentaContableSyncDto>>(cancellationToken: ct);

            return data ?? new List<CuentaContableSyncDto>();
        }
    }
}
