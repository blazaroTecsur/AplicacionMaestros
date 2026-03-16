using AplicacionMaestro.Application.ExternalServices.ERP;
using AplicacionMaestro.Application.ExternalServices.LDS;
using AplicacionMaestro.Application.Features.Aptitudes.Commands;
using AplicacionMaestro.Domain.Exceptions;
using AplicacionMaestro.Worker.Resilience;
using MediatR;
using Microsoft.Extensions.Options;
using Polly;

namespace AplicacionMaestro.Worker.Aptitudes.Workers
{
    public class AptitudSyncWorker : BackgroundService
    {
        private readonly ILogger<AptitudSyncWorker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly AptitudSyncWorkerOptions _options;
        public AptitudSyncWorker(
            ILogger<AptitudSyncWorker> logger,
            IServiceScopeFactory scopeFactory,
            IOptions<AptitudSyncWorkerOptions> options)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _options = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var retryPolicy = RetryPolicies.CreateDefaultRetryPolicy(_logger);

            _logger.LogInformation(
                "AptitudSyncWorker iniciado. Intervalo: {Interval} minutos",
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
                    _logger.LogCritical(ex, "Error crítico no controlado en AptitudSyncWorker");
                }

                await Task.Delay(
                    TimeSpan.FromMinutes(_options.IntervalMinutes),
                    stoppingToken);
            }

            _logger.LogInformation("AptitudSyncWorker finalizado");
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
                    .GetRequiredService<IErpAptitudService>();

                var ldsService = scope.ServiceProvider
                    .GetRequiredService<ILdsAptitudService>();

                _logger.LogInformation("Ejecutando sincronización de aptitudes");

                var aptitudesErp = await erpService.ObtenerAptitudesAsync(stoppingToken);
                var aptitudesLds = await ldsService.ObtenerAptitudesAsync(stoppingToken);

                var aptitudes = aptitudesErp
                    .Concat(aptitudesLds)
                    .ToList();

                await mediator.Send(
                    new SyncAptitudesCommand(aptitudes),
                    stoppingToken);

                _logger.LogInformation(
                    "Sincronización completada. Total aptitudes: {Total}",
                    aptitudes.Count);
            });
        }
    }
}
