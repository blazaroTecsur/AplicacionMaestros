using AplicacionMaestro.Domain.Entities;

namespace AplicacionMaestro.Application.Interfaces.Almacenes
{
    public interface IAlmacenLegacyRepository
    {
        Task SincronizarAsync(
            List<Almacen> almacenes,
            CancellationToken cancellationToken);
    }
}
