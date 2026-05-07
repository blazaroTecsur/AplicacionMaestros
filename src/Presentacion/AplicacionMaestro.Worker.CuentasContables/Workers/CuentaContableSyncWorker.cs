using AplicacionMaestro.Application.ExternalServices.ERP;
using AplicacionMaestro.Application.ExternalServices.LDS;
using AplicacionMaestro.Application.Features.CtasContables.Commands;
using AplicacionMaestro.Domain.Exceptions;
using AplicacionMaestro.Worker.CuentasContables.Resilience;
using MediatR;
using Microsoft.Extensions.Options;
using Polly;

namespace AplicacionMaestro.Worker.CuentasContables.Workers
{
    public class CuentaContableSyncWorker : BackgroundService
    {
        private readonly ILogger<CuentaContableSyncWorker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly CuentaContableSyncWorkerOptions _options;
        public CuentaContableSyncWorker(
            ILogger<CuentaContableSyncWorker> logger,
            IServiceScopeFactory scopeFactory,
            IOptions<CuentaContableSyncWorkerOptions> options)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _options = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var retryPolicy = RetryPolicies.CreateDefaultRetryPolicy(_logger);

            _logger.LogInformation(
                "CuentaContableSyncWorker iniciado. Intervalo: {Interval} minutos",
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
                    _logger.LogCritical(ex, "Error crítico no controlado en CuentaContableSyncWorker");
                }

                await Task.Delay(
                    TimeSpan.FromMinutes(_options.IntervalMinutes),
                    stoppingToken);
            }

            _logger.LogInformation("CuentaContableSyncWorker finalizado");
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
                    .GetRequiredService<IErpCuentaContableService>();

                var ldsService = scope.ServiceProvider
                    .GetRequiredService<ILdsCuentaContableService>();

                _logger.LogInformation("Ejecutando sincronización de cuentas contables");

                var ctasContableErp = await erpService.ObtenerCuentasContablesSAsync(stoppingToken);
                var ctasContableLds = await ldsService.ObtenerCuentasContablesSAsync(stoppingToken);

                var data = ctasContableErp
                    .Concat(ctasContableLds)
                    .ToList();

                await mediator.Send(
                    new SyncCtasContablesCommand(data),
                    stoppingToken);

                _logger.LogInformation(
                    "Sincronización completada. Total cuentas contables: {Total}",
                    data.Count);
            });
        }

    }
}
