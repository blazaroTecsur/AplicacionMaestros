namespace AplicacionMaestro.Application;
public interface ILdsArticuloService
{
    Task<IReadOnlyCollection<ArticuloSyncDto>> ObtenerArticulosAsync(
        CancellationToken cancellationToken);
}