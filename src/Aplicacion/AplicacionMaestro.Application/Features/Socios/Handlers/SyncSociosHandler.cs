using AplicacionMaestro.Application.Features.Proveedores.Models;
using AplicacionMaestro.Application.Features.Socios.Commands;
using AplicacionMaestro.Application.Interfaces;
using AplicacionMaestro.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AplicacionMaestro.Application.Features.Socios.Handlers
{
    public class SyncSociosHandler : IRequestHandler<SyncSociosCommand, Unit>
    {
        private readonly ISocioRepository _socioRepository;          // BD NUEVA
        private readonly ISocioLegacyRepository _socioLegacyRepository; // BD LEGACY
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SyncSociosHandler> _logger;

        public SyncSociosHandler(
            ISocioRepository socioRepository,
            ISocioLegacyRepository socioLegacyRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<SyncSociosHandler> logger)
        {
            _socioRepository = socioRepository;
            _socioLegacyRepository = socioLegacyRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Unit> Handle(
            SyncSociosCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Inicio de sincronización de socios. Total={total}",
                request.socios?.Count ?? 0);

            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var legacyListSocios = new List<Socio>();

            try
            {
                var sociosDomain = _mapper
                    .Map<List<Socio>>(request.socios);

                await _socioRepository.SincronizarAsync(
                    sociosDomain,
                    "SyncSociosHandler",
                    cancellationToken);

                legacyListSocios.AddRange(sociosDomain);

                await _unitOfWork.CommitAsync(cancellationToken);

                _logger.LogInformation(
                    "Fin sincronización de socios. Procesados={total}",
                    sociosDomain.Count);

            }
            catch (Exception ex)
            {

                await _unitOfWork.RollbackAsync(cancellationToken);

                _logger.LogError(
                   ex,
                   "Error crítico en sincronización socios");
                throw;
            }
            try
            {
                // 🔥 Fase 2 - Legacy (fuera de la transacción)
                await _socioLegacyRepository.SincronizarAsync(legacyListSocios, cancellationToken);

                _logger.LogInformation("Fin sincronización legacy de socios.");

            }
            catch (Exception ex)
            {
                _logger.LogError(
                   ex,
                   "Error inesperado sincronizando socios legacy");
            }

            _logger.LogInformation("Fin sincronización socios OK");

            return Unit.Value;
        }

        #region Métodos privados 

        #endregion
    }
}
