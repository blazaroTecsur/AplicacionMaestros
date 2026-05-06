using AplicacionMaestro.Application.ExternalServices.ERP;
using AplicacionMaestro.Application.ExternalServices.LDS;
using AplicacionMaestro.Application.Features.Almacenes.Commands;
using AplicacionMaestro.Domain.Exceptions;
using AplicacionMaestro.Worker.Almacenes.Resilience;
using MediatR;
using Microsoft.Extensions.Options;
using Polly;

namespace AplicacionMaestro.Worker.Almacenes.Workers
{
    public class AlmacenSyncWorker : BackgroundService
    {
        private readonly ILogger<AlmacenSyncWorker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly AlmacenSyncWorkerOptions _options;
        public AlmacenSyncWorker(
            ILogger<AlmacenSyncWorker> logger,
            IServiceScopeFactory scopeFactory,
            IOptions<AlmacenSyncWorkerOptions> options)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _options = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var retryPolicy = RetryPolicies.CreateDefaultRetryPolicy(_logger);

            _logger.LogInformation(
                "AlmacenSyncWorker iniciado. Intervalo: {Interval} minutos",
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
                    _logger.LogCritical(ex, "Error crítico no controlado en AlmacenSyncWorker");
                }

                await Task.Delay(
                    TimeSpan.FromMinutes(_options.IntervalMinutes),
                    stoppingToken);
            }

            _logger.LogInformation("AlmacenSyncWorker finalizado");
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
                    .GetRequiredService<IErpAlmacenService>();

                var ldsService = scope.ServiceProvider
                    .GetRequiredService<ILdsAlmacenService>();

                _logger.LogInformation("Ejecutando sincronización de almacenes");

                var almacenesErp = await erpService.ObtenerAlmacenesAsync(stoppingToken);
                var almacenesLds = await ldsService.ObtenerAlmacenesAsync(stoppingToken);

                var almacenes = almacenesErp
                    .Concat(almacenesLds)
                    .ToList();

                await mediator.Send(
                    new SyncAlmacenesCommand(almacenes),
                    stoppingToken);

                _logger.LogInformation(
                    "Sincronización completada. Total almacenes: {Total}",
                    almacenes.Count);
            });
        }
    }
}
