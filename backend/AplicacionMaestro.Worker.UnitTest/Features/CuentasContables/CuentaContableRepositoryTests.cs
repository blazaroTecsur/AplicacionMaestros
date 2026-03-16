using AplicacionMaestro.Domain.Entities;
using AplicacionMaestro.Infrastructure.Persistence.DbContext;
using AplicacionMaestro.Infrastructure.Persistence.Entities.CuentasContables;
using AplicacionMaestro.Infrastructure.Persistence.Repositories.CuentasContables;
using Microsoft.EntityFrameworkCore;

namespace AplicacionMaestro.Worker.UnitTest.Features.CuentasContables
{
    public class CuentaContableRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly CuentaContableRepository _repository;

        public CuentaContableRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CuentaContableDbTest")
                .Options;
            _context = new ApplicationDbContext(options);
            _repository = new CuentaContableRepository(_context);
        }

        private CuentaContable CrearCuentaContableDomain(string cuenta, string descripcion, string tipo)
        {
            // Act
            var cuentaContable = new CuentaContable(
                cuenta,
                descripcion,
                tipo
                );

            return cuentaContable;
        }

        [Fact]
        public async Task Caso1_Insertar_Registros_Nuevos()
        {
            // Arrange
            var cuentaContables = new List<CuentaContable>
            {
                CrearCuentaContableDomain(
                    "0112110",
                    "Bienes en Custodia",
                    "Activo")
             };

            // Act
            await _repository.SincronizarAsync(
                cuentaContables,
                "test-user",
                CancellationToken.None);

            await _context.SaveChangesAsync();
            // Assert
            var cuentaContableDb = await _context.CtasContables.FirstOrDefaultAsync();

            Assert.NotNull(cuentaContableDb);
            Assert.Equal("0112110", cuentaContableDb.Cuenta);
        }

        [Fact]
        public async Task SincronizarAsync_CuandoCuentaContableExiste_DebeActualizar()
        {
            // Arrange
            var entity = new CuentaContableEntity
            {
                Cuenta = "0112110",
                Descripcion = "Bienes en Custodia",
                Tipo = "Activo"
            };

            _context.CtasContables.Add(entity);
            await _context.SaveChangesAsync();

            var cuentaContables = new List<CuentaContable>
            {
                CrearCuentaContableDomain(
                    "0112110",
                    "Bienes en Custodia Actualizado",
                    "Activo")
            };

            // Act
            await _repository.SincronizarAsync(
                cuentaContables,
                "test-user",
                CancellationToken.None);

            await _context.SaveChangesAsync();

            // Assert
            var cuentaContableDb = await _context.CtasContables.FirstAsync();

            Assert.Equal(
                "Bienes en Custodia Actualizado",
                cuentaContableDb.Descripcion);
        }
    }
}
