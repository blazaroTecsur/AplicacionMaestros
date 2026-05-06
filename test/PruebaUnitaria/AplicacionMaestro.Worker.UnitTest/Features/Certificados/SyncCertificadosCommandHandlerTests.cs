using AplicacionMaestro.Application.Features.Certificados.Commands;
using AplicacionMaestro.Application.Features.Certificados.Dtos;
using AplicacionMaestro.Application.Features.Certificados.Handlers;
using AplicacionMaestro.Application.Interfaces;
using AplicacionMaestro.Application.Interfaces.Certificados;
using AplicacionMaestro.Application.Mapping;
using AplicacionMaestro.Domain.Entities;
using AplicacionMaestro.Infrastructure;
using AplicacionMaestro.Infrastructure.Persistence.DbContext;
using AplicacionMaestro.Infrastructure.Persistence.Entities.Certificaciones;
using AplicacionMaestro.Infrastructure.Persistence.Repositories.Certificados;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace AplicacionMaestro.Worker.UnitTest.Features.Certificados
{
    public class SyncCertificadosCommandHandlerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly ICertificadoRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SyncCertificadosHandler _handler;

        public SyncCertificadosCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);

            // Repository REAL
            _repository = new CertificadoRepository(_context);

            // UnitOfWork REAL
            _unitOfWork = new UnitOfWork(_context);

            // Mapper REAL (si usas AutoMapper)
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CertificadoProfile>();
            });

            _mapper = config.CreateMapper();

            var loggerMock = new Mock<ILogger<SyncCertificadosHandler>>();

            _handler = new SyncCertificadosHandler(
                _repository,
                _mapper,
                _unitOfWork,
                loggerMock.Object);
        }

        //CASO 1: Verificar que se guarden las certificados en la base de datos
        [Fact]
        public async Task Caso1_Insertar_Certificados_Nuevas()
        {
            // Arrange
            var dtoList = new List<CertificadoSyncDto>
            {
                new CertificadoSyncDto
                {
                    Codigo = "CRT0001",
                    Descripcion = "CAPATAZ ALUMBRADO"
                },
                new CertificadoSyncDto
                {
                    Codigo = "CRT0002",
                    Descripcion = "CAPATAZ DE EMERGENCIA"
                }
            };

            var command = new SyncCertificadosCommand(dtoList);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert (VALIDA BD REAL)
            var certificadosDb = await _context.Certificaciones.ToListAsync();

            Assert.Equal(2, certificadosDb.Count);
            Assert.Contains(certificadosDb, a => a.Codigo == "CRT0001");
            Assert.Contains(certificadosDb, a => a.Codigo == "CRT0002");
        }


        // CASO 2
        [Fact]
        public async Task Caso2_Actualizar_Descripcion()
        {
            _context.Certificaciones.Add(new CertificadoEntity
            {
                Codigo = "CRT0001",
                Descripcion = "ANTIGUA"
            });

            await _context.SaveChangesAsync();

            var dto = new List<CertificadoSyncDto>
            {
                    new CertificadoSyncDto
                    {
                        Codigo = "CRT0001",
                        Descripcion = "NUEVA DESCRIPCION"
                    }
            };

            var domain = dto.Select(x => new Certificado(x.Codigo, x.Descripcion)).ToList();

            var command = new SyncCertificadosCommand(dto);

            await _handler.Handle(command, CancellationToken.None);

            var certificado = await _context.Certificaciones.FirstAsync();

            Assert.Equal("NUEVA DESCRIPCION", certificado.Descripcion);
        }

        // CASO 3
        [Fact]
        public async Task Caso3_No_Hay_Cambios_No_Actualiza()
        {
            _context.Certificaciones.Add(new CertificadoEntity
            {
                Codigo = "CRT0001",
                Descripcion = "CAPATAZ"
            });

            await _context.SaveChangesAsync();

            var dto = new List<CertificadoSyncDto>
            {
                    new CertificadoSyncDto
                    {
                        Codigo = "CRT0001",
                        Descripcion = "CAPATAZ"
                    }
            };

            var domain = dto.Select(x => new Certificado(x.Codigo, x.Descripcion)).ToList();

            var command = new SyncCertificadosCommand(dto);

            await _handler.Handle(command, CancellationToken.None);

            var count = await _context.Certificaciones.CountAsync();

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task Caso5_Codigo_Null_Debe_Fallar()
        {
            var dto = new List<CertificadoSyncDto>
            {
                    new CertificadoSyncDto
                    {
                        Codigo = "",
                        Descripcion = "CAPATAZ"
                    }
            };

            var command = new SyncCertificadosCommand(dto);

            var act = await Assert.ThrowsAsync<AutoMapperMappingException>(
                () => _handler.Handle(command, CancellationToken.None));


            Assert.IsType<ArgumentException>(act.InnerException);
            Assert.Contains("Código es obligatorio", act.InnerException.Message);
        }

        // CASO 6
        [Fact]
        public async Task Caso6_Mezcla_Insertar_Y_Actualizar()
        {
            _context.Certificaciones.Add(new CertificadoEntity
            {
                Codigo = "CRT0001",
                Descripcion = "ANTIGUA"
            });

            await _context.SaveChangesAsync();

            var dto = new List<CertificadoSyncDto>
            {
                new CertificadoSyncDto
                {
                    Codigo = "CRT0001",
                    Descripcion = "NUEVA"
                },
                new CertificadoSyncDto
                {
                    Codigo = "CRT0002",
                    Descripcion = "EMERGENCIA"
                }
            };

            var domain = dto.Select(x => new Certificado(x.Codigo, x.Descripcion)).ToList();

            var command = new SyncCertificadosCommand(dto);

            await _handler.Handle(command, CancellationToken.None);

            var list = await _context.Certificaciones.ToListAsync();

            Assert.Equal(2, list.Count);
        }

        // CASO 7
        [Fact]
        public async Task Caso7_Idempotencia()
        {
            var dto = new List<CertificadoSyncDto>
            {
                    new CertificadoSyncDto
                    {
                        Codigo = "CRT0001",
                        Descripcion = "CAPATAZ"
                    }
            };

            var domain = dto.Select(x => new Certificado(x.Codigo, x.Descripcion)).ToList();

            var command = new SyncCertificadosCommand(dto);

            await _handler.Handle(command, CancellationToken.None);
            await _handler.Handle(command, CancellationToken.None);

            var count = await _context.Certificaciones.CountAsync();

            Assert.Equal(1, count);
        }

        // CASO 8 — Duplicados en la trama
        [Fact]
        public async Task Caso8_Trama_Con_Codigos_Duplicados_No_Debe_Insertar_Duplicados()
        {
            var dto = new List<CertificadoSyncDto>
            {
                new CertificadoSyncDto
                {
                    Codigo = "CRT0001",
                    Descripcion = "CAPATAZ ALUMBRADO"
                },
                new CertificadoSyncDto
                {
                    Codigo = "CRT0001",
                    Descripcion = "CAPATAZ ALUMBRADO"
                }
            };

            var domain = new List<Certificado>
        {
            new Certificado("CRT0001","CAPATAZ")
        };

            var command = new SyncCertificadosCommand(dto);

            await _handler.Handle(command, CancellationToken.None);

            var count = await _context.Certificaciones.CountAsync();

            Assert.Equal(1, count);
        }

        // CASO 9 — Trama vacía
        [Fact]
        public async Task Caso9_Trama_Vacia_No_Debe_Insertar_Nada()
        {
            var dto = new List<CertificadoSyncDto>();

            var domain = new List<Certificado>();

            var command = new SyncCertificadosCommand(dto);

            await _handler.Handle(command, CancellationToken.None);

            var count = await _context.Certificaciones.CountAsync();

            Assert.Equal(0, count);
        }

    }
}
