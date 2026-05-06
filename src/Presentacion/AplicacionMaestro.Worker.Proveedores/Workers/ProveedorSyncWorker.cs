using AplicacionMaestro.Application;
using AplicacionMaestro.Worker.Resilience;
using MediatR;
using Microsoft.Extensions.Options;

namespace AplicacionMaestro.Worker.Proveedores.Workers;

public class ProveedorSyncWorker : BackgroundService
{
    private readonly ILogger<ProveedorSyncWorker> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ProveedorSyncWorkerOptions _options;

    public ProveedorSyncWorker(
        ILogger<ProveedorSyncWorker> logger,
        IServiceScopeFactory scopeFactory,
        IOptions<ProveedorSyncWorkerOptions> options)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        var retryPolicy = RetryPolicies.CreateDefaultRetryPolicy(_logger);

        _logger.LogInformation(
            "ProveedorSyncWorker iniciado. Intervalo: {Interval} minutos",
            _options.IntervalMinutes);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await retryPolicy.ExecuteAsync(async () =>
                {
                    using var scope = _scopeFactory.CreateScope();

                    var mediator = scope.ServiceProvider
                        .GetRequiredService<IMediator>();

                    var erpService = scope.ServiceProvider
                        .GetRequiredService<IErpProveedorService>();

                    var ldsService = scope.ServiceProvider
                        .GetRequiredService<ILdsProveedorService>();

                    _logger.LogInformation("Ejecutando sincronización de proveedores");


                    var proveedoresErp = await erpService.ObtenerProveedoresAsync(stoppingToken);
                    var proveedoresLds = await ldsService.ObtenerProveedoresAsync(stoppingToken);

                    var proveedores = proveedoresErp
                        .Concat(proveedoresLds)
                        .ToList();

                    await mediator.Send(
                        new SyncProveedoresCommand(proveedores),
                        stoppingToken);
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en ProveedorSyncWorker");
            }

            await Task.Delay(
                TimeSpan.FromMinutes(_options.IntervalMinutes),
                stoppingToken);
        }
    }
}
