using AplicacionMaestro.Application.Features.CodsUnidad1.Handlers;
using AplicacionMaestro.Application.Features.CodsUnidad1.Commands;
using AplicacionMaestro.Application.Features.CodsUnidad1.Dtos;
using AplicacionMaestro.Application.Interfaces;
using AplicacionMaestro.Application.Interfaces.CodsUnidad1;
using AplicacionMaestro.Application.Mapping;
using AplicacionMaestro.Infrastructure.Persistence.DbContext;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using AplicacionMaestro.Infrastructure.Persistence.Entities.CodsUnidad1;
using AplicacionMaestro.Infrastructure.Persistence.Repositories.CodsUnidad1;

namespace AplicacionMaestro.Worker.UnitTest.Features.CodsUnidad1
{
    public class SyncCodsUnidad1CommandHandlerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly ICodUnidad1Repository _repositoryMock;
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ILogger<SyncCodsUnidad1Handler>> _loggerMock;

        private readonly SyncCodsUnidad1Handler _handler;

        public SyncCodsUnidad1CommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);
            _repositoryMock = new CodUnidad1Repository(_context);
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _loggerMock = new Mock<ILogger<SyncCodsUnidad1Handler>>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CodigoUnidad1Profile>();
            });
            _mapper = config.CreateMapper();
            _handler = new SyncCodsUnidad1Handler(
                _repositoryMock,
                _mapper,
                _unitOfWorkMock.Object,
                _loggerMock.Object
            );
        }

        private CodigoUnidad1SyncDto CrearCodigoUnidad1Dto(string codigo, string descripcion)
        {
            // Act
            var codigoUnidad1 = new CodigoUnidad1SyncDto
            {
                Codigo = codigo,
                Descripcion = descripcion
            };

            return codigoUnidad1;
        }

        [Fact]
        public async Task Caso1_Handle_CuandoListaVacia_NoDebeEjecutarSincronizacion()
        {
            // Arrange
            var command = new SyncCodsUnidad1Command(new List<CodigoUnidad1SyncDto>());

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            var codigos = await _context.Almacenes.CountAsync();

            Assert.Equal(0, codigos);
        }

        [Fact]
        public async Task Caso2_Insertar_Registros_Nuevos()
        {
            // Arrange
            var codsUnidad1 = new List<CodigoUnidad1SyncDto>
            {
                CrearCodigoUnidad1Dto(
                    "1000",
                    "Gerencia General"),
                CrearCodigoUnidad1Dto(
                    "1103",
                    "Taller Flota Pesada")

            };

            var command = new SyncCodsUnidad1Command(codsUnidad1);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert

            var codsUnidad1DB = await _context.CodsUnidad1.ToListAsync();

            Assert.Equal(2, codsUnidad1DB.Count);
            Assert.Contains(codsUnidad1DB, e => e.Codigo == "1000" && e.Descripcion == "Gerencia General");
            Assert.Contains(codsUnidad1DB, e => e.Codigo == "1103" && e.Descripcion == "Taller Flota Pesada");

        }

        [Fact]
        public async Task Caso3_Actualizar_Registro()
        {
            _context.CodsUnidad1.Add(new CodUnidad1Entity
            {
                Codigo = "1000",
                Descripcion = "Codigo Unidad 1 Nuevo"
            });

            await _context.SaveChangesAsync();

            var codsUnidad1 = new List<CodigoUnidad1SyncDto>
            {
                CrearCodigoUnidad1Dto(
                    "1000",
                    "Codigo Unidad 1 Actualizado")

            };

            var command = new SyncCodsUnidad1Command(codsUnidad1);

            await _handler.Handle(command, CancellationToken.None);

            var result = await _context.CodsUnidad1.FirstAsync();

            Assert.Equal("Codigo Unidad 1 Actualizado", result.Descripcion);
        }

        [Fact]
        public async Task Caso4_Sin_Cambios_No_Actualiza()
        {
            _context.CodsUnidad1.Add(new CodUnidad1Entity
            {
                Codigo = "1000",
                Descripcion = "Codigo Unidad 1 Nuevo"
            });

            await _context.SaveChangesAsync();

            var codsUnidad1 = new List<CodigoUnidad1SyncDto>
            {
                CrearCodigoUnidad1Dto(
                    "1000",
                    "Codigo Unidad 1 Nuevo")

            };

            var command = new SyncCodsUnidad1Command(codsUnidad1);

            await _handler.Handle(command, CancellationToken.None);

            var count = await _context.CodsUnidad1.CountAsync();

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task Caso5_Idempotencia()
        {
            var codsUnidad1 = new List<CodigoUnidad1SyncDto>
            {
                CrearCodigoUnidad1Dto(
                        "1103",
                        "Taller Flota Pesada")

            };

            var command = new SyncCodsUnidad1Command(codsUnidad1);

            await _handler.Handle(command, CancellationToken.None);
            await _handler.Handle(command, CancellationToken.None);

            var count = await _context.CodsUnidad1.CountAsync();

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task Caso6_Mezcla_Insert_Update()
        {
            _context.CodsUnidad1.Add(new CodUnidad1Entity
            {
                Codigo = "1000",
                Descripcion = "Codigo Unidad 1 Nuevo"
            });

            await _context.SaveChangesAsync();

            var codsUnidad1 = new List<CodigoUnidad1SyncDto>
            {
                CrearCodigoUnidad1Dto(
                    "1000",
                    "Gerencia General"),
                CrearCodigoUnidad1Dto(
                    "1103",
                    "Taller Flota Pesada")
            };

            var command = new SyncCodsUnidad1Command(codsUnidad1);

            await _handler.Handle(command, CancellationToken.None);

            var list = await _context.CodsUnidad1.ToListAsync();

            Assert.Equal(2, list.Count);
        }

        [Fact]
        public async Task Caso7_Duplicados_No_Insertar_Duplicados()
        {
            var codsUnidad1 = new List<CodigoUnidad1SyncDto>
            {
                CrearCodigoUnidad1Dto(
                    "1000",
                    "Gerencia General"),
                CrearCodigoUnidad1Dto(
                    "1000",
                    "Gerencia General")
            };

            var command = new SyncCodsUnidad1Command(codsUnidad1);

            await _handler.Handle(command, CancellationToken.None);

            var count = await _context.CodsUnidad1.CountAsync();

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task Caso8_Trama_Vacia()
        {
            var codsUnidad1 = new List<CodigoUnidad1SyncDto>();

            var command = new SyncCodsUnidad1Command(codsUnidad1);

            await _handler.Handle(command, CancellationToken.None);

            var count = await _context.CodsUnidad1.CountAsync();

            Assert.Equal(0, count);
        }

        [Fact]
        public async Task Caso9_Codigo_Invalido()
        {
            var codsUnidad1 = new List<CodigoUnidad1SyncDto>
            {
                CrearCodigoUnidad1Dto(
                        "",
                        "Gerencia General")
            };

            var command = new SyncCodsUnidad1Command(codsUnidad1);

            var ex = await Assert.ThrowsAsync<AutoMapperMappingException>(
                () => _handler.Handle(command, CancellationToken.None));

            Assert.IsType<ArgumentException>(ex.InnerException);

            Assert.Contains(
                "Código es obligatorio",
                ex.InnerException.Message);
        }

    }
}
