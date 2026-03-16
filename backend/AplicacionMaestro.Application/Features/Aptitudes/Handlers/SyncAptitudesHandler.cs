using AplicacionMaestro.Application.Features.Aptitudes.Commands;
using AplicacionMaestro.Application.Interfaces;
using AplicacionMaestro.Application.Interfaces.Aptitudes;
using AplicacionMaestro.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AplicacionMaestro.Application.Features.Aptitudes.Handlers
{
    public class SyncAptitudesHandler
                : IRequestHandler<SyncAptitudesCommand, Unit>
    {
        private readonly IAptitudRepository _aptitudRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SyncAptitudesHandler> _logger;

        public SyncAptitudesHandler(
            IAptitudRepository aptitudRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<SyncAptitudesHandler> logger)
        {
            _aptitudRepository = aptitudRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Unit> Handle(
                SyncAptitudesCommand request,
                CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Inicio sincronización de aptitudes. Total={total}",
                request.Aptitudes?.Count ?? 0);

            try
            {
                if (request.Aptitudes == null || !request.Aptitudes.Any())
                {
                    _logger.LogWarning("No se recibieron aptitudes para sincronizar.");
                    return Unit.Value;
                }

                // DTO → Dominio
                var aptitudesDomain = _mapper
                    .Map<List<Aptitud>>(request.Aptitudes);

                aptitudesDomain = aptitudesDomain
                    .DistinctBy(x => x.Codigo) // eliminar duplicados por código, si existieran
                    .ToList();

                await _aptitudRepository.SincronizarAsync(
                    aptitudesDomain,
                    "SyncAptitudesHandler",
                    cancellationToken);

                await _unitOfWork.CommitAsync(cancellationToken);

                _logger.LogInformation(
                    "Fin sincronización de aptitudes. Procesados={total}",
                    aptitudesDomain.Count);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error inesperado en sincronización de aptitudes");

                throw; // dejamos que el middleware global maneje la excepción
            }

        }
    }
}
