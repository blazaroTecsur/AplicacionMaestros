namespace AplicacionMaestro.Application;

public interface ILdsProveedorService
{
    Task<IReadOnlyCollection<ProveedorSyncDto>> ObtenerProveedoresAsync(
        CancellationToken cancellationToken);
}
