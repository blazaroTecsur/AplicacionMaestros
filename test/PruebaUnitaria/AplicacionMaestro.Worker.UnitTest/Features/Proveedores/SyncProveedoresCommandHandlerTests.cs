using AplicacionMaestro.Application;
using AplicacionMaestro.Application.Features.Proveedores.Models;
using AplicacionMaestro.Domain.Entities;
using AplicacionMaestro.Domain.Enums;
using AplicacionMaestro.Domain.ValueObject;
using AplicacionMaestro.Infrastructure;
using AplicacionMaestro.Infrastructure.Persistence.DbContext;
using AplicacionMaestro.Infrastructure.Persistence.Mappers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace AplicacionMaestro.Worker.UnitTest.Features.Proveedores
{
    public class SyncProveedoresCommandHandlerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly PlataformaInternaDbContext _legacyContext;

        private readonly ProveedorRepository _repository;
        private readonly ProveedorLegacyRepository _repositoryLegacy;

        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly SyncProveedoresHandler _handler;

        public SyncProveedoresCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var optionsLegacy = new DbContextOptionsBuilder<PlataformaInternaDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _legacyContext = new PlataformaInternaDbContext(optionsLegacy);

            var loggerRepository = new Mock<ILogger<ProveedorRepository>>();
            var loggerLegacyRepository = new Mock<ILogger<ProveedorLegacyRepository>>();

            _repository = new ProveedorRepository(_context);
            _repositoryLegacy = new ProveedorLegacyRepository(_legacyContext, loggerLegacyRepository.Object);

            _unitOfWork = new UnitOfWork(_context);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProveedorProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            var loggerHandler = new Mock<ILogger<SyncProveedoresHandler>>();

            _handler = new SyncProveedoresHandler(
                _repository,
                _repositoryLegacy,
                _mapper,
                _unitOfWork,
                loggerHandler.Object);
        }

        private async Task EjecutarSync(List<Proveedor> proveedores)
        {
            await _repository.SincronizarAsync(proveedores, "test", CancellationToken.None);

            await _repositoryLegacy.SincronizarAsync(
                _mapper.Map<List<ProveedorLegacySyncModel>>(proveedores),
                CancellationToken.None);

            await _unitOfWork.CommitAsync(CancellationToken.None);
        }

        private Proveedor CrearProveedorDomain(
            string id,
            string ruc,
            string razonSocial,
            bool activo = true)
        {
            return Proveedor.Create(
                id,
                ruc,
                razonSocial,
                TipoPersona.Natural,
                "Direccion 1",
                "",
                "",
                "",
                "COMPRADOR1",
                new ContactoProveedor(
                    "Contacto Test",
                    "999999999",
                    "correo@ext.com",
                    "correo@int.com"),
                Estado.ACTIVO);
        }

        [Fact]
        public async Task Caso1_Insertar_Proveedor_Nuevo()
        {
            var proveedores = new List<Proveedor>
            {
            CrearProveedorDomain("1","20111111111","Proveedor Test")
            };


            await EjecutarSync(proveedores);

            var total = await _context.Proveedores.CountAsync();
            var totalLegacy = await _legacyContext.Proveedores.CountAsync();

            Assert.Equal(1, total);
            Assert.Equal(1, totalLegacy);
        }

        [Fact]
        public async Task Caso2_Actualizar_Proveedor_Existente()
        {
            var proveedor = CrearProveedorDomain("2", "20222222222", "Proveedor Antiguo");

            await EjecutarSync(new List<Proveedor> { proveedor });

            proveedor.Actualizar(
                "Proveedor Actualizado",
                TipoPersona.Juridica,
                "Dirección Actualizada",
                "",
                "",
                "",
                "COMPRADOR1",
                new ContactoProveedor(
                    "Contacto Actualizado",
                    "988888888",
                    "",
                    ""),
                Estado.INACTIVO);

            await EjecutarSync(new List<Proveedor> { proveedor });

            var proveedorDb = await _context.Proveedores
                .FirstAsync(x => x.IdExternal == 2);

            Assert.Equal("Proveedor Actualizado", proveedorDb.RazonSocial);
        }

        [Fact]
        public async Task Caso3_Idempotencia_No_Duplicar()
        {
            var proveedor = CrearProveedorDomain("3", "20333333333", "Proveedor Idempotente");

            await EjecutarSync(new List<Proveedor> { proveedor });
            await EjecutarSync(new List<Proveedor> { proveedor });

            var total = await _context.Proveedores.CountAsync();

            Assert.Equal(1, total);
        }

        [Fact]
        public async Task Caso4_Insertar_Y_Actualizar_Mezcla()
        {
            var proveedor1 = CrearProveedorDomain("4", "20444444444", "Proveedor 1");
            var proveedor2 = CrearProveedorDomain("5", "20555555555", "Proveedor 2");

            await EjecutarSync(new List<Proveedor> { proveedor1 });

            proveedor1.Actualizar(
                "Proveedor 1 Actualizado",
                TipoPersona.Juridica,
                "Dirección Actualizada",
                "",
                "",
                "",
                "COMPRADOR1",
                new ContactoProveedor(
                    "Contacto Actualizado",
                    "988888888",
                    "",
                    ""),
                Estado.INACTIVO);

            await EjecutarSync(new List<Proveedor> { proveedor1, proveedor2 });

            var total = await _context.Proveedores.CountAsync();

            Assert.Equal(2, total);
        }

        [Fact]
        public async Task Caso5_Cambio_Estado_Activo_Inactivo()
        {
            var proveedor = CrearProveedorDomain("6", "20666666666", "Proveedor Estado", true);

            await EjecutarSync(new List<Proveedor> { proveedor });

            proveedor.Actualizar(
                "Proveedor Estado",
                TipoPersona.Juridica,
                "Dirección Actualizada",
                "",
                "",
                "",
                "COMPRADOR1",
                new ContactoProveedor(
                    "Contacto Actualizado",
                    "988888888",
                    "",
                    ""),
                Estado.INACTIVO);

            await EjecutarSync(new List<Proveedor> { proveedor });

            var proveedorDb = await _context.Proveedores
                .FirstAsync(x => x.IdExternal == 6);

            Assert.Equal("INACTIVO", proveedorDb.Estado);
        }
    }
}
