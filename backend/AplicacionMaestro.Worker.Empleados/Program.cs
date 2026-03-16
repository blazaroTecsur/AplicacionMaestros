using AplicacionMaestro.Application.DependencyInjection;
using AplicacionMaestro.Infrastructure.DependencyInjection;
using AplicacionMaestro.Worker.Empleados;
using AplicacionMaestro.Worker.Empleados.Workers;
using Serilog;

try
{
    Log.Information("Iniciando Worker Aplicación para la Sincronización de Datos Maestros de Empleados");

    var builder = Host.CreateApplicationBuilder(args);

    // Configurar Serilog desde appsettings
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

    // ? SE REGISTRA ASÍ

    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Services.AddHostedService<EmpleadoSyncWorker>();
    builder.Services.Configure<EmpleadoSyncWorkerOptions>(
        builder.Configuration.GetSection("WorkerSettings:EmpleadoSync"));


    var host = builder.Build();
    await host.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "El Worker para Empleados falló al iniciar");
}
finally
{
    Log.CloseAndFlush();
}
