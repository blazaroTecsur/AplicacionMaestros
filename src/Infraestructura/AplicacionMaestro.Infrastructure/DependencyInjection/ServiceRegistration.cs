using AplicacionMaestro.Application;
using AplicacionMaestro.Application.ExternalServices.ERP;
using AplicacionMaestro.Application.ExternalServices.LDS;
using AplicacionMaestro.Application.Interfaces;
using AplicacionMaestro.Application.Interfaces.Almacenes;
using AplicacionMaestro.Application.Interfaces.Aptitudes;
using AplicacionMaestro.Application.Interfaces.Certificados;
using AplicacionMaestro.Application.Interfaces.CodsUnidad1;
using AplicacionMaestro.Application.Interfaces.CtasContables;
using AplicacionMaestro.Application.Interfaces.Empleados;
using AplicacionMaestro.Infrastructure.ExternalServices.ERP;
using AplicacionMaestro.Infrastructure.ExternalServices.LDS;
using AplicacionMaestro.Infrastructure.Persistence.DbContext;
using AplicacionMaestro.Infrastructure.Persistence.Mappers;
using AplicacionMaestro.Infrastructure.Persistence.Repositories.Almacenes;
using AplicacionMaestro.Infrastructure.Persistence.Repositories.Aptitudes;
using AplicacionMaestro.Infrastructure.Persistence.Repositories.Certificados;
using AplicacionMaestro.Infrastructure.Persistence.Repositories.CodsUnidad1;
using AplicacionMaestro.Infrastructure.Persistence.Repositories.CuentasContables;
using AplicacionMaestro.Infrastructure.Persistence.Repositories.Empleados;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace AplicacionMaestro.Infrastructure.DependencyInjection;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var extranetConn = configuration.GetConnectionString("ErpExtranetDb");
        var pinternaConn = configuration.GetConnectionString("ErpPlataformaInternaDb");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(
                pinternaConn,
                ServerVersion.AutoDetect(pinternaConn)));

        services.AddDbContext<PlataformaInternaDbContext>(options =>
            options.UseMySql(
                extranetConn,
                ServerVersion.AutoDetect(extranetConn)));

        services.AddScoped<IProveedorRepository, ProveedorRepository>();
        services.AddScoped<IProveedorLegacyRepository, ProveedorLegacyRepository>();

        services.AddScoped<IArticuloRepository, ArticuloRepository>();
        services.AddScoped<IArticuloLegacyRepository, ArticuloLegacyRepository>();

        services.AddScoped<ISocioRepository, SocioRepository>();
        services.AddScoped<ISocioLegacyRepository, SocioLegacyRepository>();

        services.AddScoped<IAptitudRepository, AptitudRepository>();

        services.AddScoped<ICertificadoRepository, CertificadoRepository>();

        services.AddScoped<IAlmacenRepository, AlmacenRepository>();
        services.AddScoped<IAlmacenLegacyRepository, AlmacenLegacyRepository>();

        services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();
        services.AddScoped<IEmpleadoLegacyRepository, EmpleadoLegacyRepository>();

        services.AddScoped<ICodUnidad1Repository, CodUnidad1Repository>();

        services.AddScoped<ICuentaContableRepository, CuentaContableRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddAutoMapper(typeof(ArticuloProfile));
        services.AddAutoMapper(typeof(ProveedorProfile));

        services.AddHttpClient<IErpProveedorService, ErpProveedorService>((sp, client) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var baseUrl = configuration["ExternalServices:ERP:BaseUrl"]
                ?? throw new InvalidOperationException("ERP BaseUrl no configurado");
            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);

        });
        services.AddHttpClient<ILdsProveedorService, LdsProveedorService>((sp, client) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var baseUrl = configuration["ExternalServices:LDS:BaseUrl"]
                ?? throw new InvalidOperationException("LDS BaseUrl no configurado");

            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        services.AddHttpClient<IErpArticuloService, ErpArticuloService>((sp, client) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var baseUrl = configuration["ExternalServices:ERP:BaseUrl"]
                ?? throw new InvalidOperationException("ERP BaseUrl no configurado");
            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);

        });
        services.AddHttpClient<ILdsArticuloService, LdsArticuloService>((sp, client) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var baseUrl = configuration["ExternalServices:LDS:BaseUrl"]
                ?? throw new InvalidOperationException("LDS BaseUrl no configurado");

            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        services.AddHttpClient<IErpSocioService, ErpSocioService>((sp, client) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();

            var baseUrl = configuration["ExternalServices:ERP:BaseUrl"]
                ?? throw new InvalidOperationException("ERP BaseUrl no configurado");

            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        });
        services.AddHttpClient<ILdsSocioService, LdsSocioService>((sp, client) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();

            var baseUrl = configuration["ExternalServices:LDS:BaseUrl"]
                ?? throw new InvalidOperationException("LDS BaseUrl no configurado");

            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        services.AddHttpClient<IErpAptitudService, ErpAptitudService>((sp, client) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var baseUrl = configuration["ExternalServices:ERP:BaseUrl"]
                ?? throw new InvalidOperationException("ERP BaseUrl no configurado");

            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        });
        services.AddHttpClient<ILdsAptitudService, LdsAptitudService>((sp, client) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var baseUrl = configuration["ExternalServices:LDS:BaseUrl"]
                ?? throw new InvalidOperationException("LDS BaseUrl no configurado");

            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        services.AddHttpClient<IErpCertificadoService, ErpCertificadoService>((sp, client) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var baseUrl = configuration["ExternalServices:ERP:BaseUrl"]
                ?? throw new InvalidOperationException("ERP BaseUrl no configurado");
            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);

        });
        services.AddHttpClient<ILdsCertificadoService, LdsCertificadoService>((sp, client) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var baseUrl = configuration["ExternalServices:LDS:BaseUrl"]
                ?? throw new InvalidOperationException("LDS BaseUrl no configurado");

            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        services.AddHttpClient<IErpAlmacenService, ErpAlmacenService>((sp, client) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var baseUrl = configuration["ExternalServices:ERP:BaseUrl"]
                ?? throw new InvalidOperationException("ERP BaseUrl no configurado");
            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);

        });
        services.AddHttpClient<ILdsAlmacenService, LdsAlmacenService>((sp, client) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var baseUrl = configuration["ExternalServices:LDS:BaseUrl"]
                ?? throw new InvalidOperationException("LDS BaseUrl no configurado");

            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        services.AddHttpClient<IErpEmpleadoService, ErpEmpleadoService>((sp, client) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var baseUrl = configuration["ExternalServices:ERP:BaseUrl"]
                ?? throw new InvalidOperationException("ERP BaseUrl no configurado");
            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);

        });
        services.AddHttpClient<ILdsEmpleadoService, LdsEmpleadoService>((sp, client) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var baseUrl = configuration["ExternalServices:LDS:BaseUrl"]
                ?? throw new InvalidOperationException("LDS BaseUrl no configurado");

            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        services.AddHttpClient<IErpCodUnidad1Service, ErpCodUnidad1Service>((sp, client) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var baseUrl = configuration["ExternalServices:ERP:BaseUrl"]
                ?? throw new InvalidOperationException("ERP BaseUrl no configurado");
            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);

        });
        services.AddHttpClient<ILdsCodUnidad1Service, LdsCodUnidad1Service>((sp, client) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var baseUrl = configuration["ExternalServices:LDS:BaseUrl"]
                ?? throw new InvalidOperationException("LDS BaseUrl no configurado");

            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        services.AddHttpClient<IErpCuentaContableService, ErpCuentaContableService>((sp, client) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var baseUrl = configuration["ExternalServices:ERP:BaseUrl"]
                ?? throw new InvalidOperationException("ERP BaseUrl no configurado");
            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);

        });
        services.AddHttpClient<ILdsCuentaContableService, LdsCuentaContableService>((sp, client) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var baseUrl = configuration["ExternalServices:LDS:BaseUrl"]
                ?? throw new InvalidOperationException("LDS BaseUrl no configurado");

            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        return services;
    }


}
