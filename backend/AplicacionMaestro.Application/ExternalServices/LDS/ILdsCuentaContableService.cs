using AplicacionMaestro.Application.Features.CtasContables.Dtos;

namespace AplicacionMaestro.Application.ExternalServices.LDS
{
    public interface ILdsCuentaContableService
    {
        Task<IReadOnlyCollection<CuentaContableSyncDto>> ObtenerCuentasContablesSAsync(
            CancellationToken cancellationToken);
    }
}
