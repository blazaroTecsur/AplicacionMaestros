using AplicacionMaestro.Application.Features.CtasContables.Commands;
using AplicacionMaestro.Application.Interfaces;
using AplicacionMaestro.Application.Interfaces.CtasContables;
using AplicacionMaestro.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AplicacionMaestro.Application.Features.CtasContables.Handlers
{
    public class SyncCtasContablesHandler
        : IRequestHandler<SyncCtasContablesCommand, Unit>
    {
        private readonly ICuentaContableRepository _cuentaContableRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SyncCtasContablesHandler> _logger;

        public SyncCtasContablesHandler(
            ICuentaContableRepository cuentaContableRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<SyncCtasContablesHandler> logger)
        {
            _cuentaContableRepository = cuentaContableRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Unit> Handle(
                SyncCtasContablesCommand request,
                CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Inicio sincronización de cuentas contables. Total={total}",
                request.CuentasContables?.Count ?? 0);

            try
            {
                if (request.CuentasContables == null || !request.CuentasContables.Any())
                {
                    _logger.LogWarning("No se recibieron cuentas contables para sincronizar.");
                    return Unit.Value;
                }

                // DTO → Dominio
                var ctasContablesDomain = _mapper
                    .Map<List<CuentaContable>>(request.CuentasContables);

                ctasContablesDomain = ctasContablesDomain
                    .DistinctBy(x => x.Cuenta) 
                    .ToList();

                await _cuentaContableRepository.SincronizarAsync(
                    ctasContablesDomain,
                    "SyncCtasContablesHandler",
                    cancellationToken);

                await _unitOfWork.CommitAsync(cancellationToken);

                _logger.LogInformation(
                    "Fin sincronización de cuentas contables. Procesados={total}",
                    ctasContablesDomain.Count);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error inesperado en sincronización de cuentas contables");

                throw;
            }
        }
    }
}
