using AplicacionMaestro.Application;
using AplicacionMaestro.Domain.Entities;
using AplicacionMaestro.Infrastructure.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AplicacionMaestro.Infrastructure;

public class ProveedorRepository : IProveedorRepository
{
    private readonly ApplicationDbContext _context;

    public ProveedorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SincronizarAsync(
        List<Proveedor> proveedores,
        string usuario,
        CancellationToken cancellationToken)
    {
        if (proveedores == null || proveedores.Count == 0)
            return;

        // 🔥 PERFORMANCE
        _context.ChangeTracker.AutoDetectChangesEnabled = false;

        try
        {
            var externalIds = proveedores
                .Select(x => Convert.ToInt32(x.IdExternal))
                .ToList();

            var existentes = await _context.Proveedores
                .Where(x => externalIds.Contains(x.IdExternal))
                .ToListAsync(cancellationToken);

            var dictExistentes = existentes
                .ToDictionary(x => x.IdExternal);

            var nuevos = new List<ProveedorEntity>();

            foreach (var proveedor in proveedores)
            {
                if (!dictExistentes.TryGetValue(Convert.ToInt32(proveedor.IdExternal), out var entity))
                {
                    entity = CrearEntity(proveedor);
                    nuevos.Add(entity);
                }
                else
                {
                    ActualizarEntity(entity, proveedor);
                }
            }
            if (nuevos.Count > 0)
                await _context.Proveedores.AddRangeAsync(nuevos, cancellationToken);
        }
        catch (Exception)
        {

            throw;
        }
        finally
        {
            _context.ChangeTracker.AutoDetectChangesEnabled = true;
        }
    }

    #region Métodos de mapeo
    private static ProveedorEntity CrearEntity(Proveedor proveedor)
    {
        return new ProveedorEntity
        {
            IdExternal = Convert.ToInt32(proveedor.IdExternal),
            Ruc = proveedor.Ruc,
            RazonSocial = proveedor.RazonSocial,
            TipoPersona = proveedor.TipoPersona.ToString(),
            Direccion1 = proveedor.Direccion1,
            Direccion2 = proveedor.Direccion2,
            Direccion3 = proveedor.Direccion3,
            Direccion4 = proveedor.Direccion4,
            Contacto = proveedor.Contacto.Nombre,
            Telefono = proveedor.Contacto.Telefono,
            CorreoExterno = proveedor.Contacto.CorreoExterno,
            CorreoInterno = proveedor.Contacto.CorreoInterno,
            Comprador = proveedor.Comprador,
            Estado = proveedor.Estado.ToString(),
            UsuarioRegistro = "SynProveedoresHandler",
            FechaCreacion = proveedor.FechaCreacion,
        };
    }

    private static void ActualizarEntity(
        ProveedorEntity entity,
        Proveedor proveedor)
    {
        entity.RazonSocial = proveedor.RazonSocial;
        entity.TipoPersona = proveedor.TipoPersona.ToString();

        entity.Direccion1 = proveedor.Direccion1;
        entity.Direccion2 = proveedor.Direccion2;
        entity.Direccion3 = proveedor.Direccion3;
        entity.Direccion4 = proveedor.Direccion4;

        entity.Comprador = proveedor.Comprador;

        entity.Contacto = proveedor.Contacto.Nombre;
        entity.Telefono = proveedor.Contacto.Telefono;
        entity.CorreoExterno = proveedor.Contacto.CorreoExterno;
        entity.CorreoInterno = proveedor.Contacto.CorreoInterno;

        entity.Estado = proveedor.Estado.ToString();

        entity.UsuarioModificacion = "SyncProveedoresHandler";
        entity.FechaModificacion = DateTime.UtcNow;
    }
    #endregion
}
