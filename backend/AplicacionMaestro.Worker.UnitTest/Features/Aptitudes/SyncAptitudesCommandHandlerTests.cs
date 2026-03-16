using AplicacionMaestro.Application.Features.Aptitudes.Commands;
using AplicacionMaestro.Application.Features.Aptitudes.Dtos;
using AplicacionMaestro.Application.Features.Aptitudes.Handlers;
using AplicacionMaestro.Application.Interfaces;
using AplicacionMaestro.Application.Interfaces.Aptitudes;
using AplicacionMaestro.Application.Mapping;
using AplicacionMaestro.Domain.Entities;
using AplicacionMaestro.Infrastructure;
using AplicacionMaestro.Infrastructure.Persistence.DbContext;
using AplicacionMaestro.Infrastructure.Persistence.Entities.Aptitudes;
using AplicacionMaestro.Infrastructure.Persistence.Repositories.Aptitudes;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace AplicacionMaestro.Worker.UnitTest.Features.Aptitudes
{
    public class SyncAptitudesCommandHandlerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly IAptitudRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SyncAptitudesHandler _handler;

        public SyncAptitudesCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                //.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);

            // Repository REAL
            _repository = new AptitudRepository(_context);

            // UnitOfWork REAL
            _unitOfWork = new UnitOfWork(_context);

            // Mapper REAL (si usas AutoMapper)
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AptitudProfile>();
            });

            _mapper = config.CreateMapper();

            var loggerMock = new Mock<ILogger<SyncAptitudesHandler>>();

            _handler = new SyncAptitudesHandler(
                _repository,
                _mapper,
                _unitOfWork,
                loggerMock.Object);
        }

        //CASO 1: Verificar que se guarden las aptitudes en la base de datos
        [Fact]
        public async Task Caso1_Insertar_Aptitudes_Nuevas()
        {
            // Arrange
            var dtoList = new List<AptitudSyncDto>
            {
                new AptitudSyncDto
                {
                    Codigo = "APT0001",
                    Descripcion = "CAPATAZ ALUMBRADO"
                },
                new AptitudSyncDto
                {
                    Codigo = "APT0002",
                    Descripcion = "CAPATAZ DE EMERGENCIA"
                }
            };

            var command = new SyncAptitudesCommand(dtoList);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert (VALIDA BD REAL)
            var aptitudesDb = await _context.Aptitudes.ToListAsync();

            Assert.Equal(2, aptitudesDb.Count);
            Assert.Contains(aptitudesDb, a => a.Codigo == "APT0001");
            Assert.Contains(aptitudesDb, a => a.Codigo == "APT0002");
        }


        // CASO 2
        [Fact]
        public async Task Caso2_Actualizar_Descripcion()
        {
            _context.Aptitudes.Add(new AptitudEntity
            {
                Codigo = "APT0001",
                Descripcion = "ANTIGUA"
            });

            await _context.SaveChangesAsync();

            var dto = new List<AptitudSyncDto>
            {
                    new AptitudSyncDto
                    {
                        Codigo = "APT0001",
                        Descripcion = "NUEVA DESCRIPCION"
                    }
            };

            var domain = dto.Select(x => new Aptitud(x.Codigo, x.Descripcion)).ToList();

            var command = new SyncAptitudesCommand(dto);

            await _handler.Handle(command, CancellationToken.None);

            var aptitud = await _context.Aptitudes.FirstAsync();

            Assert.Equal("NUEVA DESCRIPCION", aptitud.Descripcion);
        }

        // CASO 3
        [Fact]
        public async Task Caso3_No_Hay_Cambios_No_Actualiza()
        {
            _context.Aptitudes.Add(new AptitudEntity
            {
                Codigo = "APT0001",
                Descripcion = "CAPATAZ"
            });

            await _context.SaveChangesAsync();

            var dto = new List<AptitudSyncDto>
            {
                    new AptitudSyncDto
                    {
                        Codigo = "APT0001",
                        Descripcion = "CAPATAZ"
                    }
            };

            var domain = dto.Select(x => new Aptitud(x.Codigo, x.Descripcion)).ToList();

            var command = new SyncAptitudesCommand(dto);

            await _handler.Handle(command, CancellationToken.None);

            var count = await _context.Aptitudes.CountAsync();

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task Caso5_Codigo_Null_Debe_Fallar()
        {
            var dto = new List<AptitudSyncDto>
            {
                    new AptitudSyncDto
                    {
                        Codigo = "",
                        Descripcion = "CAPATAZ"
                    }
            };

            var command = new SyncAptitudesCommand(dto);

            var act = await Assert.ThrowsAsync<AutoMapperMappingException>(
                () => _handler.Handle(command, CancellationToken.None));


            Assert.IsType<ArgumentException>(act.InnerException);
            Assert.Contains("Código es obligatorio", act.InnerException.Message);
        }

        // CASO 6
        [Fact]
        public async Task Caso6_Mezcla_Insertar_Y_Actualizar()
        {
            _context.Aptitudes.Add(new AptitudEntity
            {
                Codigo = "APT0001",
                Descripcion = "ANTIGUA"
            });

            await _context.SaveChangesAsync();

            var dto = new List<AptitudSyncDto>
            {
                new AptitudSyncDto
                {
                    Codigo = "APT0001",
                    Descripcion = "NUEVA"
                },
                new AptitudSyncDto
                {
                    Codigo = "APT0002",
                    Descripcion = "EMERGENCIA"
                }
            };

            var domain = dto.Select(x => new Aptitud(x.Codigo, x.Descripcion)).ToList();

            var command = new SyncAptitudesCommand(dto);

            await _handler.Handle(command, CancellationToken.None);

            var list = await _context.Aptitudes.ToListAsync();

            Assert.Equal(2, list.Count);
        }

        // CASO 7
        [Fact]
        public async Task Caso7_Idempotencia()
        {
            var dto = new List<AptitudSyncDto>
            {
                    new AptitudSyncDto
                    {
                        Codigo = "APT0001",
                        Descripcion = "CAPATAZ"
                    }
            };

            var domain = dto.Select(x => new Aptitud(x.Codigo, x.Descripcion)).ToList();

            var command = new SyncAptitudesCommand(dto);

            await _handler.Handle(command, CancellationToken.None);
            await _handler.Handle(command, CancellationToken.None);

            var count = await _context.Aptitudes.CountAsync();

            Assert.Equal(1, count);
        }

        // CASO 8 — Duplicados en la trama
        [Fact]
        public async Task Caso8_Trama_Con_Codigos_Duplicados_No_Debe_Insertar_Duplicados()
        {
            var dto = new List<AptitudSyncDto>
            {
                new AptitudSyncDto
                {
                    Codigo = "APT0001",
                    Descripcion = "CAPATAZ ALUMBRADO"
                },
                new AptitudSyncDto
                {
                    Codigo = "APT0001",
                    Descripcion = "CAPATAZ ALUMBRADO"
                }
            };

            var domain = new List<Aptitud>
        {
            new Aptitud("APT0001","CAPATAZ")
        };

            var command = new SyncAptitudesCommand(dto);

            await _handler.Handle(command, CancellationToken.None);

            var count = await _context.Aptitudes.CountAsync();

            Assert.Equal(1, count);
        }

        // CASO 9 — Trama vacía
        [Fact]
        public async Task Caso9_Trama_Vacia_No_Debe_Insertar_Nada()
        {
            var dto = new List<AptitudSyncDto>();

            var domain = new List<Aptitud>();

            var command = new SyncAptitudesCommand(dto);

            await _handler.Handle(command, CancellationToken.None);

            var count = await _context.Aptitudes.CountAsync();

            Assert.Equal(0, count);
        }

    }
}
