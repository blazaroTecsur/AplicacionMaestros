using AplicacionMaestro.Application.ExternalServices.ERP;
using AplicacionMaestro.Application.ExternalServices.LDS;
using AplicacionMaestro.Application.Features.Empleados.Commands;
using AplicacionMaestro.Domain.Exceptions;
using AplicacionMaestro.Worker.Socios.Resilence;
using MediatR;
using Microsoft.Extensions.Options;
using Polly;

namespace AplicacionMaestro.Worker.Empleados.Workers
{
    public class EmpleadoSyncWorker : BackgroundService
    {
        private readonly ILogger<EmpleadoSyncWorker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly EmpleadoSyncWorkerOptions _options;
        public EmpleadoSyncWorker(
            ILogger<EmpleadoSyncWorker> logger,
            IServiceScopeFactory scopeFactory,
            IOptions<EmpleadoSyncWorkerOptions> options)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _options = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var retryPolicy = RetryPolicies.CreateDefaultRetryPolicy(_logger);

            _logger.LogInformation(
                "EmpleadoSyncWorker iniciado. Intervalo: {Interval} minutos",
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
                    _logger.LogCritical(ex, "Error crítico no controlado en EmpleadoSyncWorker");
                }

                await Task.Delay(
                    TimeSpan.FromMinutes(_options.IntervalMinutes),
                    stoppingToken);
            }

            _logger.LogInformation("EmpleadoSyncWorker finalizado");
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
                    .GetRequiredService<IErpEmpleadoService>();

                var ldsService = scope.ServiceProvider
                    .GetRequiredService<ILdsEmpleadoService>();

                _logger.LogInformation("Ejecutando sincronización de empleados");

                var empleadosErp = await erpService.ObtenerEmpleadosAsync(stoppingToken);
                var empleadosLds = await ldsService.ObtenerEmpleadosAsync(stoppingToken);

                var empleados = empleadosErp
                    .Concat(empleadosLds)
                    .ToList();

                await mediator.Send(
                    new SyncEmpleadosCommand(empleados),
                    stoppingToken);

                _logger.LogInformation(
                    "Sincronización completada. Total empleados: {Total}",
                    empleados.Count);
            });
        }

    }
}
