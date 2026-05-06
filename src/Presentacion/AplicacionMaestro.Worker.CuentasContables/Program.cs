using AplicacionMaestro.Application.DependencyInjection;
using AplicacionMaestro.Infrastructure.DependencyInjection;
using AplicacionMaestro.Worker.CuentasContables;
using AplicacionMaestro.Worker.CuentasContables.Workers;
using Serilog;

try
{
    Log.Information("Iniciando Worker Aplicación para la Sincronización de Datos Maestros de Cuentas Contables");

    var builder = Host.CreateApplicationBuilder(args);

    // ?? leer serilog desde appsettings

    // Configurar Serilog desde appsettings
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

    // ? SE REGISTRA ASÍ

    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Services.AddHostedService<CuentaContableSyncWorker>();
    builder.Services.Configure<CuentaContableSyncWorkerOptions>(
        builder.Configuration.GetSection("WorkerSettings:CuentaContableSync"));


    var host = builder.Build();
    await host.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "El Worker para cuentas contables falló al iniciar");
}
finally
{
    Log.CloseAndFlush();
}
