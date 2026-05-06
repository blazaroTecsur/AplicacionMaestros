using AplicacionMaestro.Application;
using AplicacionMaestro.Application.Features.Articulos.Models;
using AplicacionMaestro.Domain.Entities;
using AplicacionMaestro.Domain.Enums;
using AplicacionMaestro.Infrastructure;
using AplicacionMaestro.Infrastructure.Persistence.DbContext;
using AplicacionMaestro.Infrastructure.Persistence.Mappers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace AplicacionMaestro.Worker.UnitTest.Features.Articulos
{
    public class SyncArticulosCommandHandlerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly PlataformaInternaDbContext _legacyContext;

        private readonly ArticuloRepository _repository;
        private readonly ArticuloLegacyRepository _repositoryLegacy;

        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly SyncArticulosHandler _handler;

        public SyncArticulosCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var optionsLegacy = new DbContextOptionsBuilder<PlataformaInternaDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _legacyContext = new PlataformaInternaDbContext(optionsLegacy);

            var loggerRepository = new Mock<ILogger<ArticuloRepository>>();
            var loggerLegacyRepository = new Mock<ILogger<ArticuloLegacyRepository>>();

            _repository = new ArticuloRepository(_context);
            _repositoryLegacy = new ArticuloLegacyRepository(_legacyContext, loggerLegacyRepository.Object);

            _unitOfWork = new UnitOfWork(_context);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ArticuloProfile());
            });

            _mapper = mapperConfig.CreateMapper();

            var loggerHandler = new Mock<ILogger<SyncArticulosHandler>>();

            _handler = new SyncArticulosHandler(
                _repository,
                _repositoryLegacy,
                _mapper,
                _unitOfWork,
                loggerHandler.Object);
        }

        private async Task EjecutarSync(List<Articulo> articulos)
        {
            await _repository.SincronizarAsync(articulos, "test", CancellationToken.None);

            await _repositoryLegacy.SincronizarAsync(
                _mapper.Map<List<ArticuloLegacySyncModel>>(articulos),
                CancellationToken.None);

            await _unitOfWork.CommitAsync(CancellationToken.None);
        }

        private Articulo CrearArticuloDomain(
            int id,
            string codigo,
            string descripcion,
            bool activo = true)
        {
            return Articulo.Create(
                id,
                codigo,
                descripcion,
                "UND",
                "TIPO1",
                "ORIGEN1",
                "CODPROD1",
                "CODABC1",
                false,
                Estado.ACTIVO,
                DateTime.UtcNow,
                DateTime.UtcNow);
        }

        [Fact]
        public async Task Caso1_Insertar_Articulo_Nuevo()
        {
            var articulos = new List<Articulo>
        {
            CrearArticuloDomain(1,"9595959IND","Articulo Test")
        };

            await EjecutarSync(articulos);

            var total = await _context.Articulos.CountAsync();
            var totalLegacy = await _legacyContext.Articulos.CountAsync();

            Assert.Equal(1, total);
            Assert.Equal(1, totalLegacy);
        }

        [Fact]
        public async Task Caso2_Actualizar_Articulo_Existente()
        {
            var articulo = CrearArticuloDomain(2, "9595959IND", "Articulo Antiguo");

            await EjecutarSync(new List<Articulo> { articulo });

            articulo.Actualizar(
                "Articulo Actualizado",
                "UND",
                "",
                "",
                "",
                "",
                false,
                Estado.ACTIVO);

            await EjecutarSync(new List<Articulo> { articulo });

            var articuloDb = await _context.Articulos
                .FirstAsync(x => x.IdExternal == 2);

            Assert.Equal("Articulo Actualizado", articuloDb.Descripcion);
        }

        [Fact]
        public async Task Caso3_Idempotencia_No_Duplicar()
        {
            var articulo = CrearArticuloDomain(3, "ART003", "Articulo Idempotente");

            await EjecutarSync(new List<Articulo> { articulo });
            await EjecutarSync(new List<Articulo> { articulo });

            var total = await _context.Articulos.CountAsync();

            Assert.Equal(1, total);
        }

        [Fact]
        public async Task Caso4_Insertar_Y_Actualizar_Mezcla()
        {
            var articulo1 = CrearArticuloDomain(4, "ART004", "Articulo 1");
            var articulo2 = CrearArticuloDomain(5, "ART005", "Articulo 2");

            await EjecutarSync(new List<Articulo> { articulo1 });

            articulo1.Actualizar(
                "Articulo 1 Actualizado",
                "UND",
                "",
                "",
                "",
                "",
                false,
                Estado.ACTIVO);

            await EjecutarSync(new List<Articulo> { articulo1, articulo2 });

            var total = await _context.Articulos.CountAsync();

            Assert.Equal(2, total);
        }

        [Fact]
        public async Task Caso5_Cambio_Estado_Activo_Inactivo()
        {
            var articulo = CrearArticuloDomain(6, "ART006", "Articulo Estado", true);

            await EjecutarSync(new List<Articulo> { articulo });

            articulo.Actualizar(
                "Articulo Estado",
                "UND",
                "",
                "",
                "",
                "",
                false,
                Estado.INACTIVO);

            await EjecutarSync(new List<Articulo> { articulo });

            var articuloDb = await _context.Articulos
                .FirstAsync(x => x.IdExternal == 6);

            Assert.Equal("INACTIVO", articuloDb.EstadoMaterial.ToString());
        }
    }
}
