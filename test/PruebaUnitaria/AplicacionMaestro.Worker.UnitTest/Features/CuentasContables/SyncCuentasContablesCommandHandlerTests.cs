using AplicacionMaestro.Application.Features.CtasContables.Commands;
using AplicacionMaestro.Application.Features.CtasContables.Dtos;
using AplicacionMaestro.Application.Features.CtasContables.Handlers;
using AplicacionMaestro.Application.Interfaces;
using AplicacionMaestro.Application.Interfaces.CtasContables;
using AplicacionMaestro.Application.Mapping;
using AplicacionMaestro.Domain.Entities;
using AplicacionMaestro.Infrastructure;
using AplicacionMaestro.Infrastructure.Persistence.DbContext;
using AplicacionMaestro.Infrastructure.Persistence.Entities.CuentasContables;
using AplicacionMaestro.Infrastructure.Persistence.Repositories.CuentasContables;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace AplicacionMaestro.Worker.UnitTest.Features.CuentasContables
{
    public class SyncCuentasContablesCommandHandlerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly ICuentaContableRepository _repositoryMock;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly Mock<ILogger<SyncCtasContablesHandler>> _loggerMock;

        private readonly SyncCtasContablesHandler _handler;

        public SyncCuentasContablesCommandHandlerTests() { 

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(options);

            _repositoryMock = new CuentaContableRepository(_context);

            _unitOfWorkMock = new UnitOfWork(_context);
            _loggerMock = new Mock<ILogger<SyncCtasContablesHandler>>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CuentaContableProfile>();
            });
            _mapper = config.CreateMapper();

            _handler = new SyncCtasContablesHandler(
                _repositoryMock,
                _mapper,
                _unitOfWorkMock,
                _loggerMock.Object
            );
        }

        private CuentaContableSyncDto CrearCuentaContableDto(string cuenta, string descripcion, string tipo)
        {
            // Act
            var cuentaContable = new CuentaContableSyncDto
            {
                Cuenta = cuenta,
                Descripcion = descripcion,
                TipoCuenta = tipo
            };

            return cuentaContable;
        }

        [Fact]
        public async Task Caso1_Handle_CuandoListaVacia_NoDebeEjecutarSincronizacion()
        {
            // Arrange
            var command = new SyncCtasContablesCommand(new List<CuentaContableSyncDto>());

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            var cuentasDB = await _context.CtasContables.CountAsync();

            Assert.Equal(0, cuentasDB);
        }

        [Fact]
        public async Task Caso2_Insertar_Registros_Nuevos()
        {
            // Arrange
            var ctasContables = new List<CuentaContableSyncDto>
        {
            CrearCuentaContableDto(
                "0112110",
                "Bienes en Custodia",
                "Activo"),
            CrearCuentaContableDto(
                "0121110",
                "Carta Fza Continental M.N.",
                "Activo")

        };

            var command = new SyncCtasContablesCommand(ctasContables);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert

            var ctasContablesDB = await _context.CtasContables.ToListAsync();

            Assert.Equal(2, ctasContablesDB.Count);
            Assert.Contains(ctasContablesDB, e => e.Cuenta == "0112110" && e.Descripcion == "Bienes en Custodia" && e.Tipo == "Activo");
            Assert.Contains(ctasContablesDB, e => e.Cuenta == "0121110" && e.Descripcion == "Carta Fza Continental M.N." && e.Tipo == "Activo");

        }

        [Fact]
        public async Task Caso3_Actualizar_Registro()
        {
            _context.CtasContables.Add(new CuentaContableEntity
            {
                Cuenta = "0112110",
                Descripcion = "Cuenta Nueva",
                Tipo = "Activo"
            });

            await _context.SaveChangesAsync();

            var ctasContables = new List<CuentaContableSyncDto>
        {
            CrearCuentaContableDto(
                "0112110",
                "Cuenta Actualizada",
                "Activo")

        };

            var command = new SyncCtasContablesCommand(ctasContables);

            await _handler.Handle(command, CancellationToken.None);

            var result = await _context.CtasContables.FirstAsync();

            Assert.Equal("Cuenta Actualizada", result.Descripcion);
        }

        [Fact]
        public async Task Caso4_Sin_Cambios_No_Actualiza()
        {
            _context.CtasContables.Add(new CuentaContableEntity
            {
                Cuenta = "0112110",
                Descripcion = "Cuenta Nueva",
                Tipo = "Activo"
            });

            await _context.SaveChangesAsync();

            var ctasContables = new List<CuentaContableSyncDto>
        {
            CrearCuentaContableDto(
                "0112110",
                "Cuenta Nueva",
                "Activo")

        };

            var command = new SyncCtasContablesCommand(ctasContables);

            await _handler.Handle(command, CancellationToken.None);

            var count = await _context.CtasContables.CountAsync();

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task Caso5_Idempotencia()
        {
            var ctasContables = new List<CuentaContableSyncDto>
        {
            CrearCuentaContableDto(
                "0112110",
                "Cuenta Nueva",
                "Activo")

        };

            var command = new SyncCtasContablesCommand(ctasContables);

            await _handler.Handle(command, CancellationToken.None);
            await _handler.Handle(command, CancellationToken.None);

            var count = await _context.CtasContables.CountAsync();

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task Caso6_Mezcla_Insert_Update()
        {
            _context.CtasContables.Add(new CuentaContableEntity
            {
                Cuenta = "0112110",
                Descripcion = "Cuenta Nueva",
                Tipo = "Activo"
            });

            await _context.SaveChangesAsync();

            var ctasContables = new List<CuentaContableSyncDto>
            {
            CrearCuentaContableDto(
                "0112110",
                "Bienes en Custodia",
                "Activo"),
            CrearCuentaContableDto(
                "0121110",
                "Carta Fza Continental M.N.",
                "Activo")
            };

            var command = new SyncCtasContablesCommand(ctasContables);

            await _handler.Handle(command, CancellationToken.None);

            var list = await _context.CtasContables.ToListAsync();

            Assert.Equal(2, list.Count);
        }

        [Fact]
        public async Task Caso7_Duplicados_No_Insertar_Duplicados()
        {
            var ctasContables = new List<CuentaContableSyncDto>
            {
            CrearCuentaContableDto(
                "0112110",
                "Bienes en Custodia",
                "Activo"),
            CrearCuentaContableDto(
                "0112110",
                "Bienes en Custodia",
                "Activo")
            };

            var command = new SyncCtasContablesCommand(ctasContables);

            await _handler.Handle(command, CancellationToken.None);

            var count = await _context.CtasContables.CountAsync();

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task Caso8_Trama_Vacia()
        {
            var ctasContables = new List<CuentaContableSyncDto>();

            var command = new SyncCtasContablesCommand(ctasContables);

            await _handler.Handle(command, CancellationToken.None);

            var count = await _context.CtasContables.CountAsync();

            Assert.Equal(0, count);
        }

        [Fact]
        public async Task Caso9_Codigo_Invalido()
        {
            var ctasContables = new List<CuentaContableSyncDto>
        {
            CrearCuentaContableDto(
                "",
                "Bienes en Custodia",
                "Activo"),
        };

            var command = new SyncCtasContablesCommand(ctasContables);

            var ex = await Assert.ThrowsAsync<AutoMapperMappingException>(
                () => _handler.Handle(command, CancellationToken.None));

            Assert.IsType<ArgumentException>(ex.InnerException);

            Assert.Contains(
                "Número de Cuenta es obligatorio",
                ex.InnerException.Message);
        }
    }
}
