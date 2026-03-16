using AplicacionMaestro.Domain.Entities;

namespace AplicacionMaestro.Application.Interfaces.Aptitudes
{
    public interface IAptitudRepository
    {
        Task SincronizarAsync(
            List<Aptitud> aptitudes,
            string usuario,
            CancellationToken ct);
    }
}
