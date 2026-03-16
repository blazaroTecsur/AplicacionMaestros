using AplicacionMaestro.Application.ExternalServices.ERP;
using AplicacionMaestro.Application.ExternalServices.LDS;
using AplicacionMaestro.Application.Features.Certificados.Commands;
using AplicacionMaestro.Domain.Exceptions;
using AplicacionMaestro.Worker.Certificados.Resilience;
using MediatR;
using Microsoft.Extensions.Options;
using Polly;

namespace AplicacionMaestro.Worker.Certificados.Workers
{
    public class CertificadoSyncWorker : BackgroundService
    {
        private readonly ILogger<CertificadoSyncWorker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly CertificadoSyncWorkerOptions _options;

        public CertificadoSyncWorker(
            ILogger<CertificadoSyncWorker> logger,
            IServiceScopeFactory scopeFactory,
            IOptions<CertificadoSyncWorkerOptions> options)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _options = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var retryPolicy = RetryPolicies.CreateDefaultRetryPolicy(_logger);

            _logger.LogInformation(
                "CertificadoSyncWorker iniciado. Intervalo: {Interval} minutos",
            _options.IntervalMinutes);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await EjecutarSincronizacionAsync(retryPolicy, stoppingToken);
                }
                catch (DomainException ex)
                {
                    _logger.LogWarning(ex, "Error de dominio en sincronización");
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("Worker cancelado correctamente");
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex, "Error crítico no controlado en CertificadoSyncWorker");
                }

                await Task.Delay(
                    TimeSpan.FromMinutes(_options.IntervalMinutes),
                    stoppingToken);
            }

            _logger.LogInformation("CertificadoSyncWorker finalizado");
        }

        private async Task EjecutarSincronizacionAsync(
            IAsyncPolicy retryPolicy,
            CancellationToken stoppingToken)
        {
            await retryPolicy.ExecuteAsync(async () =>
            {
                using var scope = _scopeFactory.CreateScope();

                var mediator = scope.ServiceProvider
                    .GetRequiredService<IMediator>();

                var erpService = scope.ServiceProvider
                    .GetRequiredService<IErpCertificadoService>();

                var ldsService = scope.ServiceProvider
                    .GetRequiredService<ILdsCertificadoService>();

                _logger.LogInformation("Ejecutando sincronización de aptitudes");

                var certificadosErp = await erpService.ObtenerCertificadosAsync(stoppingToken);
                var certificadosLds = await ldsService.ObtenerCertificadosAsync(stoppingToken);

                var certificados = certificadosErp
                    .Concat(certificadosLds)
                    .ToList();

                await mediator.Send(
                    new SyncCertificadosCommand(certificados),
                    stoppingToken);

                _logger.LogInformation(
                    "Sincronización completada. Total aptitudes: {Total}",
                    certificados.Count);
            });
        }
    }
}
