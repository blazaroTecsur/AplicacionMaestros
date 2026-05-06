using AplicacionMaestro.Application;
using AplicacionMaestro.Application.Common.Mappings;
using AplicacionMaestro.Application.Features.Proveedores.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AplicacionMaestro.Infrastructure;
    public class ProveedorLegacyRepository
        : IProveedorLegacyRepository
    {
        private readonly PlataformaInternaDbContext _context;
        private readonly ILogger<ProveedorLegacyRepository> _logger;

        public ProveedorLegacyRepository(
            PlataformaInternaDbContext context,
            ILogger<ProveedorLegacyRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

    public async Task SincronizarAsync(
            List<ProveedorLegacySyncModel> proveedores,
            CancellationToken cancellationToken)
    {
        if (!proveedores.Any())
            return;

        _context.ChangeTracker.AutoDetectChangesEnabled = false; // Desactivamos el seguimiento para mejorar el rendimiento en operaciones masivas

        try
        {
            var externalIds = proveedores
                .Select(x => x.Ruc)
                .ToList();

            var existentes = await _context.Proveedores
                .Where(x => externalIds.Contains(x.Ruc))
                .ToListAsync(cancellationToken);

            var dict = existentes.ToDictionary(x => x.Ruc);

            var inserts = new List<ProveedorLegacyEntity>();
            var updates = new List<ProveedorLegacyEntity>();

            foreach (var proveedor in proveedores)
            {
                if (dict.TryGetValue(proveedor.Ruc, out var existente))
                {
                    // UPDATE
                    existente.RazonSocial = proveedor.RazonSocial;
                    // existente.Ruc = proveedor.Ruc;
                    existente.Direccion = proveedor.Direccion1; // Solo mapeamos la primera dirección por simplicidad
                    existente.Telefono = proveedor.ContactoTelefono;
                    existente.Correo = proveedor.CorreoInt; // Mapeamos el correo interno
                    existente.Estado = EstadoProveedorMapper.MapEstadoToLegacyBool(proveedor.Estado);
                    existente.UsuarioModifiacion = "SyncProveedoresHandler"; // Usuario fijo para auditoría
                    existente.FechaModificacion = DateTime.UtcNow;

                    updates.Add(existente);
                }
                else
                {
                    // INSERT
                    inserts.Add(new ProveedorLegacyEntity
                    {
                        //IdEntidad = Convert.ToInt32(proveedor.IdExternal),
                        RazonSocial = proveedor.RazonSocial,
                        Ruc = proveedor.Ruc,
                        Direccion = proveedor.Direccion1, // Solo mapeamos la primera dirección por simplicidad
                        Telefono = proveedor.ContactoTelefono,
                        Correo = proveedor.CorreoExt, // Mapeamos el correo interno
                        EsProveedor = true, // Marcamos como proveedor
                        Estado = EstadoProveedorMapper.MapEstadoToLegacyBool(proveedor.Estado),
                        UsuarioRegistro = "SyncProveedorHandler",
                        FechaCreacion = DateTime.UtcNow
                    });

                }
            }

            if (inserts.Count > 0)
                await _context.Proveedores.AddRangeAsync(inserts, cancellationToken);

            if (updates.Count > 0)
                _context.Proveedores.UpdateRange(updates);

            // 4️⃣ Un solo SaveChanges
            await _context.SaveChangesAsync(cancellationToken);

        }
        catch (Exception)
        {

            throw;
        }
        finally
        {
            _context.ChangeTracker.AutoDetectChangesEnabled = true; // Volvemos a activar el seguimiento al finalizar
        }
    }
}


