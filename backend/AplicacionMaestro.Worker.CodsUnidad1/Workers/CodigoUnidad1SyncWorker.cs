using AplicacionMaestro.Application.ExternalServices.ERP;
using AplicacionMaestro.Application.ExternalServices.LDS;
using AplicacionMaestro.Application.Features.CodsUnidad1.Commands;
using AplicacionMaestro.Domain.Exceptions;
using AplicacionMaestro.Worker.CodsUnidad1.Resilience;
using MediatR;
using Microsoft.Extensions.Options;
using Polly;

namespace AplicacionMaestro.Worker.CodsUnidad1.Workers
{
    public class CodigoUnidad1SyncWorker : BackgroundService
    {
        private readonly ILogger<CodigoUnidad1SyncWorker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly CodigoUnidad1SyncWorkerOptions _options;
        public CodigoUnidad1SyncWorker(
            ILogger<CodigoUnidad1SyncWorker> logger,
            IServiceScopeFactory scopeFactory,
            IOptions<CodigoUnidad1SyncWorkerOptions> options)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _options = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var retryPolicy = RetryPolicies.CreateDefaultRetryPolicy(_logger);

            _logger.LogInformation(
                "CodigoUnidad1SyncWorker iniciado. Intervalo: {Interval} minutos",
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
                    _logger.LogCritical(ex, "Error crítico no controlado en CodigoUnidad1SyncWorker");
                }

                await Task.Delay(
                    TimeSpan.FromMinutes(_options.IntervalMinutes),
                    stoppingToken);
            }

            _logger.LogInformation("CodigoUnidad1SyncWorker finalizado");
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
                    .GetRequiredService<IErpCodUnidad1Service>();

                var ldsService = scope.ServiceProvider
                    .GetRequiredService<ILdsCodUnidad1Service>();

                _logger.LogInformation("Ejecutando sincronización de códigos de unidad 1");

                var codsUnidad1Erp = await erpService.ObtenerCodsUnidad1Async(stoppingToken);
                var codsUnidad1Lds = await ldsService.ObtenerCodsUnidad1Async(stoppingToken);

                var data = codsUnidad1Erp
                    .Concat(codsUnidad1Lds)
                    .ToList();

                await mediator.Send(
                    new SyncCodsUnidad1Command(data),
                    stoppingToken);

                _logger.LogInformation(
                    "Sincronización completada. Total códigos de unidad 1: {Total}",
                    data.Count);
            });
        }

    }
}
