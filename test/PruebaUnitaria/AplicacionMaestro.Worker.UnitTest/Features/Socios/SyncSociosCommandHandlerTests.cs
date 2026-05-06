using AplicacionMaestro.Application.Features.Socios.Dtos;
using AplicacionMaestro.Application.Features.Socios.Handlers;
using AplicacionMaestro.Application.Mapping;
using AplicacionMaestro.Domain.Entities;
using AplicacionMaestro.Domain.ValueObject;
using AplicacionMaestro.Infrastructure;
using AplicacionMaestro.Infrastructure.Persistence.DbContext;
using AplicacionMaestro.Infrastructure.Persistence.Entities.Aptitudes;
using AplicacionMaestro.Infrastructure.Persistence.Entities.Certificaciones;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace AplicacionMaestro.Worker.UnitTest.Features.Socios;
public class SyncSociosCommandHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly PlataformaInternaDbContext _plataformaInternaDbContext;
    private readonly SocioRepository _repository;
    private readonly SocioLegacyRepository _repositoryLegacy;
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly SyncSociosHandler _handler;

    public SyncSociosCommandHandlerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TestSocioDb")
            .Options;

        var optionsLegacy = new DbContextOptionsBuilder<PlataformaInternaDbContext>()
            .UseInMemoryDatabase("TestSocioLegacyDb")
            .Options;

        _context = new ApplicationDbContext(options);
        _plataformaInternaDbContext = new PlataformaInternaDbContext(optionsLegacy);

        var loggerRepostory = new Mock<ILogger<SocioRepository>>();

        _repository = new SocioRepository(_context, loggerRepostory.Object);
        _repositoryLegacy = new SocioLegacyRepository(_plataformaInternaDbContext);

        _unitOfWork = new UnitOfWork(_context);

        // Mapper REAL (si usas AutoMapper)
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<SocioProfile>();
        });

        _mapper = config.CreateMapper();

        var loggerMock = new Mock<ILogger<SyncSociosHandler>>();

        _handler = new SyncSociosHandler(
            _repository,
            _repositoryLegacy,
            _mapper,
            _unitOfWork,
            loggerMock.Object);
    }

    private List<SocioSyncDto> CrearSocioDto()
    {
        return new List<SocioSyncDto>
        {
            new SocioSyncDto
            {
                IdSocio = 123,
                Socio = "JCAMPOSANO",
                TipoEmpleado = "Empleado",
                NroRef = "",
                Nombre = "Jhordan Nahum Camposano",
                Supervisor = "JALVAREZ",
                CodTrabajo = "001",
                TipoPago = "EOS",
                Activo = true,

                General = new SocioGeneralDto
                {
                    Email = "jcampo@encossa.com.pe",
                    DireccLocaliz = "Av. Campos 159",
                    DireccMensaje = "",
                    Almacen = "ENCOSSA",
                    Departamento = "ENC",
                    Usuario = "jcampo@encossa.com.pe"
                }

                //Certificaciones = new List<CertificadoSyncDto>
                //{
                //    new() { Codigo = "CRT0001" },
                //    new() { Codigo = "CRT0002" },
                //    new() { Codigo = "CRT0003" }
                //},

                //Aptitudes = new List<AptitudSyncDto>
                //{
                //    new() { Codigo = "APT0001" },
                //    new() { Codigo = "APT0002" },
                //    new() { Codigo = "APT0003" }
                //}
            }
        };

    }

    private Socio CrearSocioDomain(
        int id,
        string codigo,
        bool activo = true)
    {
        var general = new SocioGeneral(
            "correo@test.com",
            "Av Lima",
            "",
            "ENCOSSA",
            "ENC",
            "usuario@test.com");

        //var certificaciones = new List<Certificado>
        //{
        //    new Certificado("CRT0001", "CRT 1"),
        //    new Certificado("CRT0002", "CRT 2"),
        //    new Certificado("CRT0003", "CRT 3")
        //};

        //var aptitudes = new List<Aptitud>
        //{
        //    new Aptitud("APT0001","CAPATAZ"),
        //    new Aptitud("APT0002","SUPERVISOR"),
        //    new Aptitud("APT0003","JEFE")
        //};

        return Socio.Crear(
            id,
            codigo,
            "Empleado",
            "",
            "Juan Perez Perez",
            "SUP001",
            "001",
            "EOS",
            activo,
            general,
            DateTime.UtcNow);
    }

    [Fact]
    public async Task Caso1_Insertar_Socio_Nuevo()
    {
        //var dto = CrearSocioDto();

        //var command = new SyncSociosCommand(dto);

        //// Act
        //await _handler.Handle(command, CancellationToken.None);

        var socios = new List<Socio>
        {
            CrearSocioDomain(1,"SOC001")
        };

        await _repository.SincronizarAsync(socios, "test", CancellationToken.None);
        await _repositoryLegacy.SincronizarAsync(socios, CancellationToken.None);

        await _unitOfWork.CommitAsync(CancellationToken.None);

        var totalSocios = await _context.Socios.CountAsync();
        var totalSociosLegacy = await _plataformaInternaDbContext.Socios.CountAsync();

        Assert.Equal(1, totalSocios);
    }

    [Fact]
    public async Task Caso2_Insertar_Aptitudes_Relacionadas()
    {

        // ARRANGE

        // 1️⃣ Insertar aptitudes maestras
        _context.Aptitudes.AddRange(
            new AptitudEntity
            {
                IdAptitud = 1,
                Codigo = "APT0001",
                Descripcion = "APTITUD 1"
            },
            new AptitudEntity
            {
                IdAptitud = 2,
                Codigo = "APT0002",
                Descripcion = "APTITUD 2"
            }
        );

        await _context.SaveChangesAsync();

        var socios = new List<Socio>
        {
            CrearSocioDomain(2,"SOC002")
        };

        await _repository.SincronizarAsync(socios, "test", CancellationToken.None);
        await _repositoryLegacy.SincronizarAsync(socios, CancellationToken.None);

        await _unitOfWork.CommitAsync(CancellationToken.None);

        //var relaciones = await _context.SocioAptitudes.ToListAsync ();

        //Assert.Equal(2, relaciones.Count);
        //Assert.Contains(relaciones, r => r.IdAptitud == 1);
        //Assert.Contains(relaciones, r => r.IdAptitud == 2);
    }

    [Fact]
    public async Task Caso3_Insertar_Certificaciones_Relacionadas()
    {
        _context.Certificaciones.AddRange(
            new CertificadoEntity
            {
                IdCertificacion = 1,
                Codigo = "CRT0001",
                Descripcion = "APTITUD 1"
            },
            new CertificadoEntity
            {
                IdCertificacion = 2,
                Codigo = "CRT0002",
                Descripcion = "APTITUD 2"
            }
        );

        await _context.SaveChangesAsync();

        var socios = new List<Socio>
        {
            CrearSocioDomain(3,"SOC003")
        };

        await _repository.SincronizarAsync(socios, "test", CancellationToken.None);
        await _repositoryLegacy.SincronizarAsync(socios, CancellationToken.None);

        await _unitOfWork.CommitAsync(CancellationToken.None);

        //var relaciones = await _context.SocioCertificaciones.CountAsync();

        //Assert.Equal(2, relaciones);
    }

    [Fact]
    public async Task Caso4_Actualizar_Socio_Existente()
    {
        var socio = CrearSocioDomain(4, "SOC004");

        await _repository.SincronizarAsync(new List<Socio> { socio }, "test", CancellationToken.None);
        await _repositoryLegacy.SincronizarAsync(new List<Socio> { socio }, CancellationToken.None);
        await _unitOfWork.CommitAsync(CancellationToken.None);

        socio.Actualizar(
            "Nuevo Nombre Completo",
            "Empleado",
            "SUP002",
            true,
            DateTime.UtcNow);

        await _repository.SincronizarAsync(new List<Socio> { socio }, "test", CancellationToken.None);
        await _repositoryLegacy.SincronizarAsync(new List<Socio> { socio }, CancellationToken.None);
        await _unitOfWork.CommitAsync(CancellationToken.None);

        var socioDb = await _context.Socios
            .FirstAsync(x => x.IdSocioExternal == 4);

        var socioLegacyDb = await _plataformaInternaDbContext.Socios
            .FirstAsync(x => x.IdPersonal == 1);

        Assert.Equal("Nuevo Nombre Completo", socioDb.NombreCompleto);
        Assert.Equal("Nuevo", socioLegacyDb.Nombres);
    }

    [Fact]
    public async Task Caso5_Idempotencia_No_Duplicar()
    {
        var socio = CrearSocioDomain(5, "SOC005");

        await _repository.SincronizarAsync(new List<Socio> { socio }, "test", CancellationToken.None);
        await _repository.SincronizarAsync(new List<Socio> { socio }, "test", CancellationToken.None);

        await _unitOfWork.CommitAsync(CancellationToken.None);

        var total = await _context.Socios.CountAsync();

        Assert.Equal(1, total);
    }

    [Fact]
    public async Task Caso6_Mezcla_Insertar_Y_Actualizar()
    {
        var socio1 = CrearSocioDomain(6, "SOC006");
        var socio2 = CrearSocioDomain(7, "SOC007");

        await _repository.SincronizarAsync(new List<Socio> { socio1 }, "test", CancellationToken.None);
        await _unitOfWork.CommitAsync(CancellationToken.None);

        socio1.Actualizar("Cambio Nombre Completo", "Empleado", "SUP003", true, DateTime.UtcNow);

        await _repository.SincronizarAsync(
            new List<Socio> { socio1, socio2 },
            "test",
            CancellationToken.None);

        await _unitOfWork.CommitAsync(CancellationToken.None);

        var total = await _context.Socios.CountAsync();

        Assert.Equal(2, total);
    }

    [Fact]
    public async Task Caso7_Cambio_Estado_Activo_Inactivo()
    {
        var socio = CrearSocioDomain(8, "SOC008", true);

        await _repository.SincronizarAsync(new List<Socio> { socio }, "test", CancellationToken.None);
        await _unitOfWork.CommitAsync(CancellationToken.None);

        socio.Actualizar(
            "Juan Perez Perez",
            "Empleado",
            "SUP001",
            false,
            DateTime.UtcNow);

        await _repository.SincronizarAsync(new List<Socio> { socio }, "test", CancellationToken.None);
        await _unitOfWork.CommitAsync(CancellationToken.None);

        var socioDb = await _context.Socios
            .FirstAsync(x => x.IdSocioExternal == 8);

        Assert.False(socioDb.Activo);
    }
}