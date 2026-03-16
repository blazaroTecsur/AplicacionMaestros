using AplicacionMaestro.Application;
using AplicacionMaestro.Application.Common.Mappings;
using AplicacionMaestro.Application.Features.Articulos.Models;
using AplicacionMaestro.Domain.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AplicacionMaestro.Infrastructure
{
    public class ArticuloLegacyRepository : IArticuloLegacyRepository
    {
        private readonly PlataformaInternaDbContext _context;
        private readonly ILogger<ArticuloLegacyRepository> _logger;

        public ArticuloLegacyRepository(PlataformaInternaDbContext context, ILogger<ArticuloLegacyRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SincronizarAsync(
            List<ArticuloLegacySyncModel> articulos,
            CancellationToken cancellationToken)
        {
            if (articulos == null || articulos.Count == 0)
                return;

            var codigos = articulos
                .Select(x => x.Codigo)
                .Distinct()
                .ToList();

            // 1️⃣ Traer existentes sin tracking (más rápido)
            var existentes = await _context.Articulos
                .Where(x => codigos.Contains(x.Codigo))
                //.AsNoTracking()
                .ToListAsync(cancellationToken);

            var dictExistentes = existentes.ToDictionary(x => x.Codigo);

            var inserts = new List<ArticuloLegacyEntity>();
            var updates = new List<ArticuloLegacyEntity>();

            foreach (var articulo in articulos)
            {
                var codigo = ArticuloCodigo.Crear(articulo.Codigo);

                if (dictExistentes.TryGetValue(articulo.Codigo, out var existente))
                {
                    // UPDATE
                    existente.Codigo = codigo.Valor;
                    existente.CodigoCorto = codigo.CodigoCorto;
                    existente.Marca = codigo.Marca;
                    existente.Descripcion = articulo.Descripcion;
                    existente.DescripcionLarga = articulo.Descripcion;
                    existente.Unidad = articulo.UnidadMedida;
                    existente.EstadoMatricula = String.Empty; // No se mapea, se deja vacío o con un valor por defecto
                    existente.Estado = EstadoArticuloMapper.ToBool(
                        EstadoArticuloMapper.FromExternal(articulo.Estado));

                    existente.UsuarioModifiacion = "SyncArticulosHandler";
                    existente.FechaModificacion = DateTime.UtcNow;

                    updates.Add(existente);
                }
                else
                {
                    // INSERT
                    inserts.Add(new ArticuloLegacyEntity
                    {
                        Codigo = codigo.Valor,
                        CodigoCorto = codigo.CodigoCorto,
                        Marca = codigo.Marca,
                        Descripcion = articulo.Descripcion,
                        DescripcionLarga = articulo.Descripcion,
                        EstadoMatricula = String.Empty, // No se mapea, se deja vacío o con un valor por defecto
                        Unidad = articulo.UnidadMedida,
                        Estado = EstadoArticuloMapper.ToBool(
                            EstadoArticuloMapper.FromExternal(articulo.Estado)),

                        UsuarioRegistro = "SyncArticulosHandler",
                        FechaCreacion = DateTime.UtcNow
                    });
                }
            }

            // 3️⃣ Batch operations
            if (inserts.Count > 0)
                await _context.Articulos.AddRangeAsync(inserts, cancellationToken);

            if (updates.Count > 0)
                _context.Articulos.UpdateRange(updates);

            // 4️⃣ Un solo SaveChanges
            await _context.SaveChangesAsync(cancellationToken);
        }

        #region Mapping

        #endregion
    }
}
