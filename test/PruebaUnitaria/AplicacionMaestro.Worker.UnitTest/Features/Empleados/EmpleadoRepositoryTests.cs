using AplicacionMaestro.Domain.Entities;
using AplicacionMaestro.Domain.Models;
using AplicacionMaestro.Infrastructure;
using AplicacionMaestro.Infrastructure.Persistence.DbContext;
using AplicacionMaestro.Infrastructure.Persistence.Entities.Empleados;
using AplicacionMaestro.Infrastructure.Persistence.Repositories.Empleados;
using Microsoft.EntityFrameworkCore;

namespace AplicacionMaestro.Worker.UnitTest.Features.Empleados;
public class EmpleadoRepositoryTests
{
    private readonly ApplicationDbContext _context;
    private readonly PlataformaInternaDbContext _plataformaInternaDbContext;
    private readonly EmpleadoRepository _repository;
    private readonly EmpleadoLegacyRepository _legacyRepository;

    public EmpleadoRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "EmpleadoDbTest")
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new EmpleadoRepository(_context);

        var optionsLegacy = new DbContextOptionsBuilder<PlataformaInternaDbContext>()
            .UseInMemoryDatabase("TestSocioLegacyDb")
            .Options;

        _plataformaInternaDbContext = new PlataformaInternaDbContext(optionsLegacy);
        _legacyRepository = new EmpleadoLegacyRepository(_plataformaInternaDbContext);
    }

    private Empleado CrearEmpleadoDomain(string idExternal, string codigo, string nombreCompleto)
    {
        // Arrange
        var principal = new PrincipalInfo(
            "ARROYO CHAVEZ",
            "Manuel",
            "",
            "",
            "4000",
            "",
            "1",
            "A66",
            "",
            "Diar",
            "X Sueldo",
            "Punt/asist",
            "1041120");

        var nameTax = new NameTaxInfo(
            "MANUEL",
            "PORFIRIO",
            "ARROYO",
            "CHAVEZ");

        var contacto = new ContactoInfo(
            "Psje Calango 158",
            "SJM",
            "",
            "",
            "LIMA",
            "LIM",
            "",
            "SJM",
            "",
            "",
            "",
            "marroyo@tecsur.com.pe",
            "");

        var rh = new RecursosHumanosInfo(
            new DateTime(2005, 02, 01),
            null,
            null);

        // Act
        var empleado = Empleado.Crear(
            "0001",
            "A000013",
            "ARROYO CHAVEZ, Manuel",
            principal,
            nameTax,
            contacto,
            rh);

        return empleado;
    }

    [Fact]
    public async Task Caso1_Insertar_Registros_Nuevos()
    {
        // Arrange
        var empleados = new List<Empleado>
        {
            CrearEmpleadoDomain(
                "2001",
                "EMP001",
                "CARLOS RAMIREZ")
        };

        // Act
        await _repository.SincronizarAsync(
            empleados,
            "test-user",
            CancellationToken.None);

        await _legacyRepository.SincronizarAsync(empleados, CancellationToken.None);

        // Assert
        var empleadoDb = await _context.Empleados.FirstOrDefaultAsync();
        var empleadoLegacyDb = await _plataformaInternaDbContext.Empleados.FirstOrDefaultAsync();

        Assert.NotNull(empleadoDb);
        Assert.Equal("2001", empleadoDb.IdEmpleadoExternal);

        Assert.NotNull(empleadoLegacyDb);
        Assert.Equal("EMP001", empleadoLegacyDb.Dni);
    }

    [Fact]
    public async Task SincronizarAsync_CuandoEmpleadoExiste_DebeActualizar()
    {
        // Arrange
        var entity = new EmpleadoEntity
        {
            IdEmpleadoExternal = "3001",
            Codigo = "EMP002",
            NombreCompleto = "PEDRO DIAZ",
            FechaCreacion = System.DateTime.UtcNow
        };

        _context.Empleados.Add(entity);
        await _context.SaveChangesAsync();

        var empleados = new List<Empleado>
        {
            CrearEmpleadoDomain(
                "3001",
                "EMP002",
                "PEDRO DIAZ ACTUALIZADO")
        };

        // Act
        await _repository.SincronizarAsync(
            empleados,
            "test-user",
            CancellationToken.None);

        await _legacyRepository.SincronizarAsync(empleados, CancellationToken.None);

        // Assert
        var empleadoDb = await _context.Empleados.FirstAsync();

        Assert.Equal(
            "PEDRO DIAZ ACTUALIZADO",
            empleadoDb.NombreCompleto);
    }
}