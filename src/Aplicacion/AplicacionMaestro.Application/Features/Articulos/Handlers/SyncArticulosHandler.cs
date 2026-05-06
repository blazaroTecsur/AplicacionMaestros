using AplicacionMaestro.Application.Features.Articulos.Commands;
using AplicacionMaestro.Application.Features.Articulos.Models;
using AplicacionMaestro.Application.Interfaces;
using AplicacionMaestro.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AplicacionMaestro.Application
{
    public class SyncArticulosHandler
        : IRequestHandler<SyncArticulosCommand, Unit>
    {
        private readonly IArticuloRepository _articuloRepository;
        private readonly IArticuloLegacyRepository _articuloLegacyRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SyncArticulosHandler> _logger;

        public SyncArticulosHandler(
            IArticuloRepository articuloRepository,
            IArticuloLegacyRepository legacyRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<SyncArticulosHandler> logger)
        {
            _articuloRepository = articuloRepository;
            _articuloLegacyRepository = legacyRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Unit> Handle(
               SyncArticulosCommand request,
               CancellationToken cancellationToken)
        {
            _logger.LogInformation("Inicio de sincronización de artículos. Total={total}",
                request.Articulos?.Count ?? 0);

            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var legacyList = new List<ArticuloLegacySyncModel>();

            try
            {
                var articulosDomain = _mapper
                    .Map<List<Articulo>>(request.Articulos);

                await _articuloRepository.SincronizarAsync(
                    articulosDomain,
                    "SyncSociosHandler",
                    cancellationToken);

                legacyList.AddRange(MapToLegacy(request.Articulos));

                await _unitOfWork.CommitAsync(cancellationToken);

                _logger.LogInformation(
                    "Fin sincronización de artículos. Procesados={total}",
                    articulosDomain.Count);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync(cancellationToken);

                _logger.LogError(ex, "Error crítico en sincronización artículos");

                throw;
            }
            try
            {
                // 🔥 Fase 2 - Legacy (fuera de la transacción)
                await _articuloLegacyRepository.SincronizarAsync(legacyList, cancellationToken);

                _logger.LogInformation("Fin sincronización legacy de artículos.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                   ex,
                   "Error inesperado sincronizando artículos legacy");
            }
            _logger.LogInformation("Fin sincronización artículos OK");

            return Unit.Value;
        }

        #region Metodos privados
        private List<ArticuloLegacySyncModel> MapToLegacy(
            IEnumerable<ArticuloSyncDto> dtos)
        {
            return dtos.Select(dto => new ArticuloLegacySyncModel
            {
                IdExternal = dto.IdArticulo.ToString(),
                Codigo = dto.Codigo,
                Descripcion = dto.Descripcion,
                TipoArticulo = dto.Tipo,

                Origen= dto.Origen,
                CodigoProducto = dto.CodProducto,
                CodigoAbc = dto.CodAbc,
                SegLote = dto.SegLote,

                Estado = dto.EstadoMaterial,
            }).ToList();
        }
        #endregion
    }
}