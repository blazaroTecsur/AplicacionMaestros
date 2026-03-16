using AplicacionMaestro.Application.ExternalServices.ERP;
using AplicacionMaestro.Application.ExternalServices.LDS;
using AplicacionMaestro.Application.Features.Socios.Commands;
using AplicacionMaestro.Domain.Exceptions;
using AplicacionMaestro.Worker.Socios.Resilence;
using MediatR;
using Microsoft.Extensions.Options;
using Polly;

namespace AplicacionMaestro.Worker.Socios.Workers
{
    public class SocioSyncWorker : BackgroundService
    {
        private readonly ILogger<SocioSyncWorker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly SocioSyncWorkerOptions _options;
        public SocioSyncWorker(
            ILogger<SocioSyncWorker> logger,
            IServiceScopeFactory scopeFactory,
            IOptions<SocioSyncWorkerOptions> options)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _options = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var retryPolicy = RetryPolicies.CreateDefaultRetryPolicy(_logger);

            _logger.LogInformation(
                "SocioSyncWorker iniciado. Intervalo: {Interval} minutos",
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
                    _logger.LogCritical(ex, "Error crítico no controlado en SocioSyncWorker");
                }

                await Task.Delay(
                    TimeSpan.FromMinutes(_options.IntervalMinutes),
                    stoppingToken);
            }

            _logger.LogInformation("SocioSyncWorker finalizado");
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
                    .GetRequiredService<IErpSocioService>();

                var ldsService = scope.ServiceProvider
                    .GetRequiredService<ILdsSocioService>();

                _logger.LogInformation("Ejecutando sincronización de socios");

                var sociosErp = await erpService.ObtenerSociosAsync(stoppingToken);
                var sociosLds = await ldsService.ObtenerSociosAsync(stoppingToken);

                var socios = sociosErp
                    .Concat(sociosLds)
                    .ToList();

                await mediator.Send(
                    new SyncSociosCommand(socios),
                    stoppingToken);

                _logger.LogInformation(
                    "Sincronización completada. Total socios: {Total}",
                    socios.Count);
            });
        }

    }
}
