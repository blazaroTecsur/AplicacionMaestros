using AplicacionMaestro.Application.Features.Almacenes.Commands;
using AplicacionMaestro.Application.Features.Almacenes.Dtos;
using AplicacionMaestro.Application.Features.Almacenes.Handlers;
using AplicacionMaestro.Application.Interfaces;
using AplicacionMaestro.Application.Interfaces.Almacenes;
using AplicacionMaestro.Application.Mapping;
using AplicacionMaestro.Infrastructure;
using AplicacionMaestro.Infrastructure.Persistence.DbContext;
using AplicacionMaestro.Infrastructure.Persistence.Entities.Almacenes;
using AplicacionMaestro.Infrastructure.Persistence.Repositories.Almacenes;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace AplicacionMaestro.Worker.UnitTest.Features.Almacenes
{
    public class SyncAlmacenesCommandHandlerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly PlataformaInternaDbContext _plataformaInternaDbContext;
        private readonly IAlmacenRepository _repositoryMock;
        private readonly IAlmacenLegacyRepository _legacyRepositoryMock;
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ILogger<SyncAlmacenesHandler>> _loggerMock;

        private readonly SyncAlmacenesHandler _handler;

        public SyncAlmacenesCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new ApplicationDbContext(options);

            var optionsLegacy = new DbContextOptionsBuilder<PlataformaInternaDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _plataformaInternaDbContext = new PlataformaInternaDbContext(optionsLegacy);

            _repositoryMock = new AlmacenRepository(_context);
            _legacyRepositoryMock = new AlmacenLegacyRepository(_plataformaInternaDbContext);

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _loggerMock = new Mock<ILogger<SyncAlmacenesHandler>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AlmacenProfile>();
            });

            _mapper = config.CreateMapper();

            _handler = new SyncAlmacenesHandler(
                _repositoryMock,
                _legacyRepositoryMock,
                _mapper,
                _unitOfWorkMock.Object,
                _loggerMock.Object
            );
        }

        private AlmacenSyncDto CrearAlmacenDto(string idExternal, string codigo, string nombreAlmacen)
        {
            // Arrange

            var ivaUe = new IvaUeDto
            {
                VatId = "20601088186"
            };

            // Act
            var almacen = new AlmacenSyncDto
            {
                IdAlmacen = idExternal,
                Codigo = codigo,
                Almacen = nombreAlmacen,
                Direccion1 = "AV LOS CONQUISTADORES SN - SJM",
                Direccion2 = String.Empty,
                Direccion3 = String.Empty,
                Direccion4 = String.Empty,
                Ciudad = "LIMA",
                CodProvincia = "LIMA",
                CP = "15036",
                Contacto = "JUAN PEREZ",
                Telefono = "987654321",
                Fax = String.Empty,
                IvaUe = ivaUe
            };

            return almacen;
        }

        [Fact]
        public async Task Caso1_Handle_CuandoListaVacia_NoDebeEjecutarSincronizacion()
        {
            // Arrange
            var command = new SyncAlmacenesCommand(new List<AlmacenSyncDto>());

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            var almacenesDB = await _context.Almacenes.CountAsync();

            Assert.Equal(0, almacenesDB);
        }

        [Fact]
        public async Task Caso2_Insertar_Registros_Nuevos()
        {
            // Arrange
            var almacenes = new List<AlmacenSyncDto>
        {
            CrearAlmacenDto(
                "321",
                "001",
                "ALMACEN PRINCIPAL"),
            CrearAlmacenDto(
                "421",
                "CARH",
                "CESAR CARHUAVILCA")

        };

            var command = new SyncAlmacenesCommand(almacenes);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert

            var almacenesDB = await _context.Almacenes.ToListAsync();
            var almacenLegacyDb = await _plataformaInternaDbContext.Almacenes.FirstOrDefaultAsync();

            Assert.Equal(2, almacenesDB.Count);
            Assert.Contains(almacenesDB, e => e.IdAlmacenExternal == "321");
            Assert.Contains(almacenesDB, e => e.IdAlmacenExternal == "421");

            Assert.NotNull(almacenLegacyDb);
            Assert.Equal("001", almacenLegacyDb.CodigoAlmacen);
        }

        [Fact]
        public async Task Caso3_Actualizar_Registro()
        {
            _context.Almacenes.Add(new AlmacenEntity
            {
                IdAlmacenExternal = "001",
                CodigoAlmacen = "001",
                NombreAlmacen = "ANTIGUO"
            });

            await _context.SaveChangesAsync();

            var almacenes = new List<AlmacenSyncDto>
        {
            CrearAlmacenDto(
                "001",
                "001",
                "NUEVO")

        };

            var command = new SyncAlmacenesCommand(almacenes);

            await _handler.Handle(command, CancellationToken.None);

            var result = await _context.Almacenes.FirstAsync();

            Assert.Equal("NUEVO", result.NombreAlmacen);
        }

        [Fact]
        public async Task Caso4_Sin_Cambios_No_Actualiza()
        {
            _context.Almacenes.Add(new AlmacenEntity
            {
                IdAlmacenExternal = "001",
                CodigoAlmacen = "001",
                NombreAlmacen = "ANTIGUO"
            });

            await _context.SaveChangesAsync();

            var almacenes = new List<AlmacenSyncDto>
        {
            CrearAlmacenDto(
                "001",
                "001",
                "ANTIGUO")

        };

            var command = new SyncAlmacenesCommand(almacenes);

            await _handler.Handle(command, CancellationToken.None);

            var count = await _context.Almacenes.CountAsync();

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task Caso5_Idempotencia()
        {
            var almacenes = new List<AlmacenSyncDto>
        {
            CrearAlmacenDto(
                "1001",
                "001",
                "NUEVO")

        };

            var command = new SyncAlmacenesCommand(almacenes);

            await _handler.Handle(command, CancellationToken.None);
            await _handler.Handle(command, CancellationToken.None);

            var count = await _context.Almacenes.CountAsync();

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task Caso6_Mezcla_Insert_Update()
        {
            _context.Almacenes.Add(new AlmacenEntity
            {
                IdAlmacenExternal = "321",
                CodigoAlmacen = "001",
                NombreAlmacen = "ANTIGUO"
            });

            await _context.SaveChangesAsync();

            var almacenes = new List<AlmacenSyncDto>
        {
            CrearAlmacenDto(
                "321",
                "001",
                "ALMACEN PRINCIPAL"),
            CrearAlmacenDto(
                "421",
                "CARH",
                "CESAR CARHUAVILCA")
        };

            var command = new SyncAlmacenesCommand(almacenes);

            await _handler.Handle(command, CancellationToken.None);

            var list = await _context.Almacenes.ToListAsync();

            Assert.Equal(2, list.Count);
        }

        [Fact]
        public async Task Caso7_Duplicados_No_Insertar_Duplicados()
        {
            var almacenes = new List<AlmacenSyncDto>
        {
            CrearAlmacenDto(
                "321",
                "001",
                "ALMACEN PRINCIPAL"),
            CrearAlmacenDto(
                "321",
                "001",
                "CESAR CARHUAVILCA")
        };

            var command = new SyncAlmacenesCommand(almacenes);

            await _handler.Handle(command, CancellationToken.None);

            var count = await _context.Almacenes.CountAsync();

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task Caso8_Trama_Vacia()
        {
            var almacenes = new List<AlmacenSyncDto>();

            var command = new SyncAlmacenesCommand(almacenes);

            await _handler.Handle(command, CancellationToken.None);

            var count = await _context.Almacenes.CountAsync();

            Assert.Equal(0, count);
        }

        [Fact]
        public async Task Caso9_Codigo_Invalido()
        {
            var almacenes = new List<AlmacenSyncDto>
        {
            CrearAlmacenDto(
                "1001",
                "",
                "NUEVO")
        };

            var command = new SyncAlmacenesCommand(almacenes);

            var ex = await Assert.ThrowsAsync<AutoMapperMappingException>(
                () => _handler.Handle(command, CancellationToken.None));

            Assert.IsType<ArgumentException>(ex.InnerException);

            Assert.Contains(
                "El código del almacen es obligatorio",
                ex.InnerException.Message);
        }
    }
}
