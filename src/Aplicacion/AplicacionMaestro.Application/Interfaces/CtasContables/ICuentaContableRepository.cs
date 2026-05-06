using AplicacionMaestro.Domain.Entities;

namespace AplicacionMaestro.Application.Interfaces.CtasContables
{
    public interface ICuentaContableRepository
    {
        Task SincronizarAsync(
            List<CuentaContable> ctasContables,
            string usuario,
            CancellationToken ct);
    }
}
