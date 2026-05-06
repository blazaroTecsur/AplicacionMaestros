namespace AplicacionMaestro.Application;

public interface IErpProveedorService
{
    Task<IReadOnlyCollection<ProveedorSyncDto>> ObtenerProveedoresAsync(
        CancellationToken cancellationToken);
}