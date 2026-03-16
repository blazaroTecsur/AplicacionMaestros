using AplicacionMaestro.Application.DependencyInjection;
using AplicacionMaestro.Infrastructure.DependencyInjection;
using AplicacionMaestro.Worker.Socios;
using AplicacionMaestro.Worker.Socios.Workers;
using Serilog;

try
{
    Log.Information("Iniciando Worker Aplicación para la Sincronización de Datos Maestros de Socios");

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

    builder.Services.AddHostedService<SocioSyncWorker>();
    builder.Services.Configure<SocioSyncWorkerOptions>(
        builder.Configuration.GetSection("WorkerSettings:SocioSync"));


    var host = builder.Build();
    await host.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "El Worker para Socios falló al iniciar");
}
finally
{
    Log.CloseAndFlush();
}
