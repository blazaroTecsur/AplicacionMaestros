using AplicacionMaestro.Application;
using AplicacionMaestro.Domain.Entities;
using AplicacionMaestro.Infrastructure.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AplicacionMaestro.Infrastructure;

public class ArticuloRepository : IArticuloRepository
{
    private readonly ApplicationDbContext _context;

    public ArticuloRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SincronizarAsync(
        List<Articulo> articulos,
        string usuario,
        CancellationToken cancellationToken)
    {
        if (articulos == null || articulos.Count == 0)
            return;

        // 🔥 PERFORMANCE
        _context.ChangeTracker.AutoDetectChangesEnabled = false;
        try
        {
            var externalIds = articulos
                .Select(x => Convert.ToInt32(x.IdExternal))
                .ToList();

            var existentes = await _context.Articulos
                .Where(x => externalIds.Contains(x.IdExternal))
                .ToListAsync(cancellationToken);

            var dictExistentes = existentes
                .ToDictionary(x => x.IdExternal);

            var nuevos = new List<ArticuloEntity>();

            foreach (var articulo in articulos)
            {
                if (!dictExistentes.TryGetValue(Convert.ToInt32(articulo.IdExternal), out var entity))
                {
                    // INSERT
                    entity = CrearEntity(articulo);
                    nuevos.Add(entity);
                }
                else
                {
                    // UPDATE
                    ActualizarEntity(entity, articulo);
                }
            }
            if (nuevos.Count > 0)
                await _context.Articulos.AddRangeAsync(nuevos, cancellationToken);
        }
        finally
        {
            _context.ChangeTracker.AutoDetectChangesEnabled = true;
        }
    }

    #region Métodos de mapeo
    private static ArticuloEntity CrearEntity(Articulo articulo)
    {
        return new ArticuloEntity
        {
            IdExternal = articulo.IdExternal,
            Codigo = articulo.Codigo.Valor,
            Descripcion = articulo.Descripcion,
            UnidadMedida = articulo.UnidadMedida,
            Tipo = articulo.Tipo,
            Origen = articulo.Origen,
            CodigoProducto = articulo.CodigoProducto,
            CodigoAbc = articulo.CodigoAbc,
            SegLote = articulo.SegLote,
            EstadoMaterial = articulo.EstadoMaterial,
            UsuarioRegistro = "SyncArticulosHandel",
            FechaCreacion = articulo.FechaCreacion
        };
    }

    private static void ActualizarEntity(
        ArticuloEntity entity,
        Articulo articulo)
    {
        entity.Codigo = articulo.Codigo.Valor;
        entity.Descripcion = articulo.Descripcion;

        entity.UnidadMedida = articulo.UnidadMedida;
        entity.Tipo = articulo.Tipo;
        entity.Origen = articulo.Origen;
        entity.CodigoProducto = articulo.CodigoProducto;
        entity.CodigoAbc = articulo.CodigoAbc;

        entity.SegLote = articulo.SegLote;

        entity.EstadoMaterial = articulo.EstadoMaterial;

        entity.UsuarioModificacion = "SyncArticulosHandler";
        entity.FechaModificacion = DateTime.UtcNow;
    }
    #endregion
}
