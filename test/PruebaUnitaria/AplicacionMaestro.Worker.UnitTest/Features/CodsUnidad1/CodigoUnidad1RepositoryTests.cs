using AplicacionMaestro.Domain.Entities;
using AplicacionMaestro.Infrastructure.Persistence.DbContext;
using AplicacionMaestro.Infrastructure.Persistence.Entities.CodsUnidad1;
using AplicacionMaestro.Infrastructure.Persistence.Repositories.CodsUnidad1;
using Microsoft.EntityFrameworkCore;

namespace AplicacionMaestro.Worker.UnitTest.Features.CodsUnidad1
{
    public class CodigoUnidad1RepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly CodUnidad1Repository _repository;

        public CodigoUnidad1RepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CodUnidad1DbTest")
                .Options;
            _context = new ApplicationDbContext(options);
            _repository = new CodUnidad1Repository(_context);
        }

        private CodUnidad1 CrearCodigoUnidad1Domain(string codigo, string descripcion)
        {
            // Act
            var codigoUnidad1 = new CodUnidad1(
                codigo,
                descripcion
                );

            return codigoUnidad1;
        }

        [Fact]
        public async Task Caso1_Insertar_Registros_Nuevos()
        {
            // Arrange
            var codigoUnidad1s = new List<CodUnidad1>
            {
                CrearCodigoUnidad1Domain(
                    "1000",
                    "Gerencia General")
             };

            // Act
            await _repository.SincronizarAsync(
                codigoUnidad1s,
                "test-user",
                CancellationToken.None);


            // Assert
            var codigoUnidad1Db = await _context.CodsUnidad1.FirstOrDefaultAsync();

            Assert.NotNull(codigoUnidad1Db);
            Assert.Equal("1000", codigoUnidad1Db.Codigo);
        }

        [Fact]
        public async Task SincronizarAsync_CuandoCodigoUnidad1Existe_DebeActualizar()
        {
            // Arrange
            var entity = new CodUnidad1Entity
            {
                Codigo = "1000",
                Descripcion = "Gerencia General"
            };

            _context.CodsUnidad1.Add(entity);
            await _context.SaveChangesAsync();

            var codigoUnidad1s = new List<CodUnidad1>
            {
                CrearCodigoUnidad1Domain(
                    "1000",
                    "Gerencia General Actualizado")
            };

            // Act
            await _repository.SincronizarAsync(
                codigoUnidad1s,
                "test-user",
                CancellationToken.None);


            // Assert
            var codigoUnidad1Db = await _context.CodsUnidad1.FirstAsync();

            Assert.Equal(
                "Gerencia General Actualizado",
                codigoUnidad1Db.Descripcion);
        }
    }
}
