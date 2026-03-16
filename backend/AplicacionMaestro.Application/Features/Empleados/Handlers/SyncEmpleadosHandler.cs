using AplicacionMaestro.Application.Features.Empleados.Commands;
using AplicacionMaestro.Application.Interfaces;
using AplicacionMaestro.Application.Interfaces.Empleados;
using AplicacionMaestro.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AplicacionMaestro.Application.Features.Empleados.Handlers
{
    public class SyncEmpleadosHandler : IRequestHandler<SyncEmpleadosCommand, Unit>
    {
        private readonly IEmpleadoRepository _empleadoRepository;          // BD NUEVA
        private readonly IEmpleadoLegacyRepository _empleadoLegacyRepository; // BD LEGACY
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SyncEmpleadosHandler> _logger;

        public SyncEmpleadosHandler(
            IEmpleadoRepository empleadoRepository,
            IEmpleadoLegacyRepository empleadoLegacyRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<SyncEmpleadosHandler> logger)
        {
            _empleadoRepository = empleadoRepository;
            _empleadoLegacyRepository = empleadoLegacyRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Unit> Handle(
            SyncEmpleadosCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Inicio de sincronización de empleados. Total={total}",
                request.Empleados?.Count ?? 0);

            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var legacyListEmpleados = new List<Empleado>();

            try
            {
                if (request.Empleados == null || !request.Empleados.Any())
                {
                    _logger.LogWarning("No se recibieron empleados para sincronizar.");
                    return Unit.Value;
                }

                var empleadosDomain = _mapper
                    .Map<List<Empleado>>(request.Empleados);

                empleadosDomain = empleadosDomain
                    .DistinctBy(x => x.IdEmpleadoExternal)
                    .ToList();

                await _empleadoRepository.SincronizarAsync(
                    empleadosDomain,
                    "SyncEmpleadosHandler",
                    cancellationToken);

                legacyListEmpleados.AddRange(empleadosDomain);

                await _unitOfWork.CommitAsync(cancellationToken);

                _logger.LogInformation(
                    "Fin sincronización de empleados. Procesados={total}",
                    empleadosDomain.Count);

            }
            catch (Exception ex)
            {

                await _unitOfWork.RollbackAsync(cancellationToken);

                _logger.LogError(
                   ex,
                   "Error crítico en sincronización empleados");
                throw;
            }
            try
            {
                // 🔥 Fase 2 - Legacy (fuera de la transacción)
                await _empleadoLegacyRepository.SincronizarAsync(legacyListEmpleados, cancellationToken);

                _logger.LogInformation("Fin sincronización legacy de empleados.");

            }
            catch (Exception ex)
            {
                _logger.LogError(
                   ex,
                   "Error inesperado sincronizando empleados legacy");
            }


            _logger.LogInformation("Fin sincronización empleados OK");

            return Unit.Value;
        }

        #region Métodos privados 

        #endregion
    }
}
