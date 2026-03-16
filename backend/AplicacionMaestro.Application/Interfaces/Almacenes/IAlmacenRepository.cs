using AplicacionMaestro.Domain.Entities;

namespace AplicacionMaestro.Application.Interfaces.Almacenes
{
    public interface IAlmacenRepository
    {
        Task SincronizarAsync(List<Almacen> almacenes, string usuario, CancellationToken cancellationToken);
    }
}
