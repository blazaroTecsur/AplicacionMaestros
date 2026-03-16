using AplicacionMaestro.Application.Features.CodsUnidad1.Commands;
using AplicacionMaestro.Application.Interfaces;
using AplicacionMaestro.Application.Interfaces.CodsUnidad1;
using AplicacionMaestro.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AplicacionMaestro.Application.Features.CodsUnidad1.Handlers
{
    public class SyncCodsUnidad1Handler
        : IRequestHandler<SyncCodsUnidad1Command, Unit>
    {
        private readonly ICodUnidad1Repository _codUnidad1repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SyncCodsUnidad1Handler> _logger;

        public SyncCodsUnidad1Handler(
            ICodUnidad1Repository codUnidad1repository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<SyncCodsUnidad1Handler> logger)
        {
            _codUnidad1repository = codUnidad1repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Unit> Handle(
                SyncCodsUnidad1Command request,
                CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Inicio sincronización de códigos de unidad 1. Total={total}",
                request.CodsUnidad1?.Count ?? 0);

            try
            {
                if (request.CodsUnidad1 == null || !request.CodsUnidad1.Any())
                {
                    _logger.LogWarning("No se recibieron códigos de unidad 1 para sincronizar.");
                    return Unit.Value;
                }

                // DTO → Dominio
                var codsUnidad1Domain = _mapper
                    .Map<List<CodUnidad1>>(request.CodsUnidad1);

                codsUnidad1Domain = codsUnidad1Domain
                    .DistinctBy(x => x.Codigo)
                    .ToList();

                await _codUnidad1repository.SincronizarAsync(
                    codsUnidad1Domain,
                    "SyncCodsUnidad1Handler",
                    cancellationToken);

                await _unitOfWork.CommitAsync(cancellationToken);

                _logger.LogInformation(
                    "Fin sincronización de códigos de unidad 1. Procesados={total}",
                    codsUnidad1Domain.Count);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error inesperado en sincronización de códigos de unidad1");

                throw; // dejamos que el middleware global maneje la excepción
            }

        }
    }
}
