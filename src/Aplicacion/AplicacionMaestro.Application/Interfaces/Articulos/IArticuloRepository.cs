using AplicacionMaestro.Domain.Entities;

namespace AplicacionMaestro.Application;
public interface IArticuloRepository
{
    Task SincronizarAsync(List<Articulo> articulos, string usuario, CancellationToken cancellationToken);
}