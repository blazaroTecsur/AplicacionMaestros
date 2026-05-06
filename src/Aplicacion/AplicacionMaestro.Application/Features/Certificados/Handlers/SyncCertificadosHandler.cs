using AplicacionMaestro.Application.Features.Aptitudes.Commands;
using AplicacionMaestro.Application.Features.Aptitudes.Handlers;
using AplicacionMaestro.Application.Features.Certificados.Commands;
using AplicacionMaestro.Application.Interfaces;
using AplicacionMaestro.Application.Interfaces.Aptitudes;
using AplicacionMaestro.Application.Interfaces.Certificados;
using AplicacionMaestro.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AplicacionMaestro.Application.Features.Certificados.Handlers
{
    public class SyncCertificadosHandler
        : IRequestHandler<SyncCertificadosCommand, Unit>
    {
        private readonly ICertificadoRepository _certificadoRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SyncCertificadosHandler> _logger;

        public SyncCertificadosHandler(
            ICertificadoRepository certificadoRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<SyncCertificadosHandler> logger)
        {
            _certificadoRepository = certificadoRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }


        public async Task<Unit> Handle(
                SyncCertificadosCommand request,
                CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Inicio sincronización de certificados. Total={total}",
                request.Certificados?.Count ?? 0);

            try
            {
                if (request.Certificados == null || !request.Certificados.Any())
                {
                    _logger.LogWarning("No se recibieron certificados para sincronizar.");
                    return Unit.Value;
                }

                // DTO → Dominio
                var certificadosDomain = _mapper
                    .Map<List<Certificado>>(request.Certificados);

                certificadosDomain = certificadosDomain
                    .DistinctBy(x => x.Codigo) 
                    .ToList();

                await _certificadoRepository.SincronizarAsync(
                    certificadosDomain,
                    "SyncCertificadosHandler",
                    cancellationToken);

                await _unitOfWork.CommitAsync(cancellationToken);

                _logger.LogInformation(
                    "Fin sincronización de certificados. Procesados={total}",
                    certificadosDomain.Count);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error inesperado en sincronización de certificados");

                throw; 
            }
        }
    }
}
