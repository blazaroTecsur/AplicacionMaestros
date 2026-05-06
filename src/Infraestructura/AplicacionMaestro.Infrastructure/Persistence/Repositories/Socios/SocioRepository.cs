using AplicacionMaestro.Application;
using AplicacionMaestro.Domain.Entities;
using AplicacionMaestro.Infrastructure.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AplicacionMaestro.Infrastructure;

public class SocioRepository : ISocioRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<SocioRepository> _logger;

    public SocioRepository(ApplicationDbContext context, ILogger<SocioRepository> logger)
    {
        _context = context;
        _logger = logger;

    }

    public async Task SincronizarAsync(
        List<Socio> socios,
        string usuario,
        CancellationToken ct)
    {
        if (socios == null || socios.Count == 0)
            return;

        // 🔥 PERFORMANCE
        _context.ChangeTracker.AutoDetectChangesEnabled = false;

        try
        {
            // ========================================
            // 1 Obtener IDs externos
            // ========================================
            var externalIds = socios
                .Select(x => x.IdSocioExternal)
                .ToHashSet();

            // ========================================
            // 2️ Traer socios existentes con relaciones (tracking activado)
            // ========================================
            var existentes = await _context.Socios
                .Where(x => externalIds.Contains(x.IdSocioExternal))
                .ToListAsync(ct);

            var sociosDict = existentes.ToDictionary(x => x.IdSocioExternal);

            var nuevosSocios = new List<SocioEntity>();

            // ========================================
            // 3 Sincronización
            // ========================================

            foreach (var socio in socios)
            {
                if (!sociosDict.TryGetValue(socio.IdSocioExternal, out var entity))
                {
                    entity = CrearEntity(socio, usuario);
                    nuevosSocios.Add(entity);
                }
                else
                {
                    ActualizarEntity(entity, socio, usuario);
                }
            }
            if (nuevosSocios.Count > 0)
                await _context.Socios.AddRangeAsync(nuevosSocios, ct);

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
    // ===============================
    // CREAR NUEVO SOCIO
    // ===============================
    private SocioEntity CrearEntity(Socio socio, string usuario)
    {
        return new SocioEntity
        {
            IdSocioExternal = socio.IdSocioExternal,
            CodigoSocio = socio.CodigoSocio,
            TipoEmpleado = socio.TipoEmpleado,
            NroReferencia = socio.NroReferencia,
            NombreCompleto = socio.Nombre.Valor,
            Supervisor = socio.Supervisor,
            CodTrabajo = socio.CodigoTrabajo,
            TipoPago = socio.TipoPago,
            Activo = socio.Activo,

            Email = socio.General.Email,
            DireccionLocaliz = socio.General.DireccLocaliz,
            DireccionMensaje = socio.General.DireccMensaje,
            Almacen = socio.General.Almacen,
            Departamento = socio.General.Departamento,
            Usuario = socio.General.Usuario,

            UsuarioRegistro = usuario,
            FechaCreacion = DateTime.UtcNow,

            //SocioAptitudes = new List<SocioAptitudEntity>(),
            //SocioCertificaciones = new List<SocioCertificacionEntity>()
        };
    }

    // ===============================
    // ACTUALIZAR SOCIO EXISTENTE
    // ===============================
    private void ActualizarEntity(
        SocioEntity entity,
        Socio socio,
        string usuario)
    {
        entity.CodigoSocio = socio.CodigoSocio;
        entity.TipoEmpleado = socio.TipoEmpleado;
        entity.NroReferencia = socio.NroReferencia;
        entity.NombreCompleto = socio.Nombre.Valor;
        entity.Supervisor = socio.Supervisor;
        entity.CodTrabajo = socio.CodigoTrabajo;
        entity.TipoPago = socio.TipoPago;
        entity.Activo = socio.Activo;

        entity.Email = socio.General.Email;
        entity.DireccionLocaliz = socio.General.DireccLocaliz;
        entity.DireccionMensaje = socio.General.DireccMensaje;
        entity.Almacen = socio.General.Almacen;
        entity.Departamento = socio.General.Departamento;
        entity.Usuario = socio.General.Usuario;

        entity.UsuarioModificacion = usuario;
        entity.FechaModificacion = DateTime.UtcNow;
    }

    #endregion
}
