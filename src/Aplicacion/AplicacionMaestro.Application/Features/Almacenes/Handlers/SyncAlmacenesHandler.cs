using AplicacionMaestro.Application.Features.Almacenes.Commands;
using AplicacionMaestro.Application.Interfaces;
using AplicacionMaestro.Application.Interfaces.Almacenes;
using AplicacionMaestro.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AplicacionMaestro.Application.Features.Almacenes.Handlers
{
    public class SyncAlmacenesHandler : IRequestHandler<SyncAlmacenesCommand, Unit>
    {
        private readonly IAlmacenRepository _almacenRepository;         
        private readonly IAlmacenLegacyRepository _almacenLegacyRepository; 
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SyncAlmacenesHandler> _logger;
        public SyncAlmacenesHandler(
            IAlmacenRepository almacenRepository,
            IAlmacenLegacyRepository almacenLegacyRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<SyncAlmacenesHandler> logger)
        {
            _almacenRepository = almacenRepository;
            _almacenLegacyRepository = almacenLegacyRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Unit> Handle(
            SyncAlmacenesCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Inicio de sincronización de almacenes. Total={total}",
                request.Almacenes?.Count ?? 0);

            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var legacyListAlmacenes = new List<Almacen>();

            try
            {
                var almacenesDomain = _mapper
                    .Map<List<Almacen>>(request.Almacenes);

                almacenesDomain = almacenesDomain
                    .DistinctBy(x => x.IdAlmacenExternal) 
                    .ToList();

                await _almacenRepository.SincronizarAsync(
                    almacenesDomain,
                    "SyncAlmacenesHandler",
                    cancellationToken);

                legacyListAlmacenes.AddRange(almacenesDomain);

                await _unitOfWork.CommitAsync(cancellationToken);

                _logger.LogInformation(
                    "Fin sincronización de almacenes. Procesados={total}",
                    almacenesDomain.Count);

            }
            catch (Exception ex)
            {

                await _unitOfWork.RollbackAsync(cancellationToken);

                _logger.LogError(
                   ex,
                   "Error crítico en sincronización almacenes");
                throw;
            }
            try
            {
                // 🔥 Fase 2 - Legacy (fuera de la transacción)
                await _almacenLegacyRepository.SincronizarAsync(legacyListAlmacenes, cancellationToken);

                _logger.LogInformation("Fin sincronización legacy de almacenes.");

            }
            catch (Exception ex)
            {
                _logger.LogError(
                   ex,
                   "Error inesperado sincronizando almacenes legacy");
            }


            _logger.LogInformation("Fin sincronización almacenes OK");

            return Unit.Value;
        }
    }
}
