using AplicacionMaestro.Application.Features.Empleados.Commands;
using AplicacionMaestro.Application.Features.Empleados.Dtos;
using AplicacionMaestro.Application.Features.Empleados.Handlers;
using AplicacionMaestro.Application.Interfaces;
using AplicacionMaestro.Application.Interfaces.Empleados;
using AplicacionMaestro.Application.Mapping;
using AplicacionMaestro.Infrastructure;
using AplicacionMaestro.Infrastructure.Persistence.DbContext;
using AplicacionMaestro.Infrastructure.Persistence.Entities.Empleados;
using AplicacionMaestro.Infrastructure.Persistence.Repositories.Empleados;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace AplicacionMaestro.Worker.UnitTest.Features.Empleados;
public class SyncEmpleadosCommandHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly PlataformaInternaDbContext _plataformaInternaDbContext;
    private readonly IEmpleadoRepository _repositoryMock;
    private readonly IEmpleadoLegacyRepository _legacyRepositoryMock;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly Mock<ILogger<SyncEmpleadosHandler>> _loggerMock;

    private readonly SyncEmpleadosHandler _handler;

    public SyncEmpleadosCommandHandlerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);

        var optionsLegacy = new DbContextOptionsBuilder<PlataformaInternaDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _plataformaInternaDbContext = new PlataformaInternaDbContext(optionsLegacy);

        _repositoryMock = new EmpleadoRepository(_context);
        _legacyRepositoryMock = new EmpleadoLegacyRepository(_plataformaInternaDbContext);

        _unitOfWorkMock = new UnitOfWork(_context);
        _loggerMock = new Mock<ILogger<SyncEmpleadosHandler>>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<EmpleadoProfile>();
        });

        _mapper = config.CreateMapper();

        _handler = new SyncEmpleadosHandler(
            _repositoryMock,
            _legacyRepositoryMock,
            _mapper,
            _unitOfWorkMock,
            _loggerMock.Object
        );
    }

    private EmpleadoSyncDto CrearEmpleadoDto(string idExternal, string codigo, string nombreCompleto)
    {
        // Arrange
        var principal = new PrincipalDto {
            Apellido = "",
            Nombre = "",
            Alias = "",
            Cargo = "",
            Dpto = "4000",
            Estado = "",
            Turno = "1",
            Categoria = "A66",
            IdUsuario = "",
            FrecPago = "Diar",
            TipEmp = "X Sueldo",
            GenNomina = "Punt/asist",
            CtaSueldo = "1041120" };

        var nameTax = new NameTaxDto { 
            PrNombre = "MANUEL",
            SgNombre = "PORFIRIO",
            PrApellido = "ARROYO",
            SgApellido = "CHAVEZ"};

        var contacto = new ContactoEmpleadoDto {
            Direccion1 = "Psje Calango 158",
            Direccion2 = "SJM",
            Direccion3 = "",
            Direccion4 = "",
            Ciudad = "LIMA",
            CodProvincia = "LIM",
            CP = "",
            Municipio = "SJM",
            Telefono = "",
            TelComercial = "",
            ExtensionTel = "",
            CorreoElect = "marroyo@tecsur.com.pe",
            Correo = ""};

        var rh = new RecursosHumanosDto {
            FechContr = "2025/03/01",
            FechRevis = null,
            FechRescis = null
        };

        // Act
        var empleado = new EmpleadoSyncDto{
            IdEmpleado = idExternal,
            Codigo = codigo,
            Empleado = nombreCompleto,
            Principal = principal,
            NameTax = nameTax,
            Contacto = contacto,
            Rh= rh };

        return empleado;
    }

    [Fact]
    public async Task Caso1_Handle_CuandoListaVacia_NoDebeEjecutarSincronizacion()
    {
        // Arrange
        var command = new SyncEmpleadosCommand(new List<EmpleadoSyncDto>());

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        var empleados = await _context.Empleados.CountAsync();

        Assert.Equal(0, empleados);
    }

    [Fact]
    public async Task Caso2_Insertar_Registros_Nuevos()
    {
        // Arrange
        var empleados = new List<EmpleadoSyncDto>
        {
            CrearEmpleadoDto(
                "1001",
                "A0001",
                "JUAN PEREZ"),
            CrearEmpleadoDto(
                "1002",
                "A0002",
                "JUAN RAMOS")

        };

        var command = new SyncEmpleadosCommand(empleados);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert

        var empleadosDB = await _context.Empleados.ToListAsync();
        var empleadoLegacyDb = await _plataformaInternaDbContext.Empleados.FirstOrDefaultAsync();

        Assert.Equal(2, empleadosDB.Count);
        Assert.Contains(empleadosDB, e => e.IdEmpleadoExternal == "1001" && e.Codigo == "A0001" && e.NombreCompleto == "JUAN PEREZ");
        Assert.Contains(empleadosDB, e => e.IdEmpleadoExternal == "1002" && e.Codigo == "A0002" && e.NombreCompleto == "JUAN RAMOS");

        Assert.NotNull(empleadoLegacyDb);
        Assert.Equal("A0001", empleadoLegacyDb.Dni);
    }

    [Fact]
    public async Task Caso3_Actualizar_Registro()
    {
        _context.Empleados.Add(new EmpleadoEntity
        {
            IdEmpleadoExternal = "1001",
            Codigo = "001",
            NombreCompleto = "ANTIGUO"
        });

        await _context.SaveChangesAsync();

        var empleados = new List<EmpleadoSyncDto>
        {
            CrearEmpleadoDto(
                "1001",
                "001",
                "NUEVO")

        };

        var command = new SyncEmpleadosCommand(empleados);

        await _handler.Handle(command, CancellationToken.None);

        var result = await _context.Empleados.FirstAsync();

        Assert.Equal("NUEVO", result.NombreCompleto);
    }

    [Fact]
    public async Task Caso4_Sin_Cambios_No_Actualiza()
    {
        _context.Empleados.Add(new EmpleadoEntity
        {
            IdEmpleadoExternal = "1001",
            Codigo = "001",
            NombreCompleto = "EMPLEADO NUEVO"
        });

        await _context.SaveChangesAsync();

        var empleados = new List<EmpleadoSyncDto>
        {
            CrearEmpleadoDto(
                "1001",
                "001",
                "EMPLEADO NUEVO")

        };

        var command = new SyncEmpleadosCommand(empleados);

        await _handler.Handle(command, CancellationToken.None);

        var count = await _context.Empleados.CountAsync();

        Assert.Equal(1, count);
    }

    [Fact]
    public async Task Caso5_Idempotencia()
    {
        var empleados = new List<EmpleadoSyncDto>
        {
            CrearEmpleadoDto(
                "1001",
                "001",
                "NUEVO")

        };

        var command = new SyncEmpleadosCommand(empleados);

        await _handler.Handle(command, CancellationToken.None);
        await _handler.Handle(command, CancellationToken.None);

        var count = await _context.Empleados.CountAsync();

        Assert.Equal(1, count);
    }

    [Fact]
    public async Task Caso6_Mezcla_Insert_Update()
    {
        _context.Empleados.Add(new EmpleadoEntity
        {
            IdEmpleadoExternal = "1001",
            Codigo = "001",
            NombreCompleto = "ANTIGUO"
        });

        await _context.SaveChangesAsync();

        var empleados = new List<EmpleadoSyncDto>
        {
            CrearEmpleadoDto(
                "1001",
                "001",
                "NUEVO"),
            CrearEmpleadoDto(
                "1002",
                "002",
                "JUAN RAMOS")
        };

        var command = new SyncEmpleadosCommand(empleados);

        await _handler.Handle(command, CancellationToken.None);

        var list = await _context.Empleados.ToListAsync();

        Assert.Equal(2, list.Count);
    }

    [Fact]
    public async Task Caso7_Duplicados_No_Insertar_Duplicados()
    {
        var empleados = new List<EmpleadoSyncDto>
        {
            CrearEmpleadoDto(
                "1001",
                "001",
                "NUEVO"),
            CrearEmpleadoDto(
                "1001",
                "001",
                "NUEVO")
        };

        var command = new SyncEmpleadosCommand(empleados);

        await _handler.Handle(command, CancellationToken.None);

        var count = await _context.Empleados.CountAsync();

        Assert.Equal(1, count);
    }

    [Fact]
    public async Task Caso8_Trama_Vacia()
    {
        var empleados = new List<EmpleadoSyncDto>();

        var command = new SyncEmpleadosCommand(empleados);

        await _handler.Handle(command, CancellationToken.None);

        var count = await _context.Empleados.CountAsync();

        Assert.Equal(0, count);
    }

    [Fact]
    public async Task Caso9_Codigo_Invalido()
    {
        var empleados = new List<EmpleadoSyncDto>
        {
            CrearEmpleadoDto(
                "1001",
                "",
                "NUEVO")
        };

        var command = new SyncEmpleadosCommand(empleados);

        var ex = await Assert.ThrowsAsync<AutoMapperMappingException>(
            () => _handler.Handle(command, CancellationToken.None));

        Assert.IsType<ArgumentException>(ex.InnerException);

        Assert.Contains(
            "El código del empleado es obligatorio",
            ex.InnerException.Message);
    }
}