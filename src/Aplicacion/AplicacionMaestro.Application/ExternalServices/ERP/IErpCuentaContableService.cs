using AplicacionMaestro.Application.Features.CtasContables.Dtos;

namespace AplicacionMaestro.Application.ExternalServices.ERP
{
    public interface IErpCuentaContableService
    {
        Task<IReadOnlyCollection<CuentaContableSyncDto>> ObtenerCuentasContablesSAsync(
            CancellationToken cancellationToken);
    }
}
