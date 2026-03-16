namespace AplicacionMaestro.Application;
public interface IErpArticuloService
{
    Task<IReadOnlyCollection<ArticuloSyncDto>> ObtenerArticulosAsync(
        CancellationToken cancellationToken);
}

