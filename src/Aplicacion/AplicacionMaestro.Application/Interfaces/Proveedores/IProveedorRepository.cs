using AplicacionMaestro.Domain.Entities;

namespace AplicacionMaestro.Application;
public interface IProveedorRepository
{

    Task SincronizarAsync(List<Proveedor> proveedores,string usuario, CancellationToken cancellationToken);
}

