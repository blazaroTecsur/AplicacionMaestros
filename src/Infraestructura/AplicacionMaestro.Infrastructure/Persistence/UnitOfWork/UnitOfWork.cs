using AplicacionMaestro.Application.Interfaces;
using AplicacionMaestro.Infrastructure.Persistence.DbContext;
using Microsoft.EntityFrameworkCore.Storage;

namespace AplicacionMaestro.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _newDbContext;
    private IDbContextTransaction? _transaction;
    // private readonly PlataformaInternaDbContext _legacyDbContext;

    public UnitOfWork(
        ApplicationDbContext newDbContext)
    {
        _newDbContext = newDbContext;
        // _legacyDbContext = legacyDbContext;
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken)
    {
        if (IsInMemoryDatabase(_newDbContext))
            return;

        _transaction = await _newDbContext.Database
            .BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await _newDbContext.SaveChangesAsync(cancellationToken);

        if (_transaction != null)
            await _transaction.CommitAsync(cancellationToken);
    }

    public async Task RollbackAsync(CancellationToken cancellationToken)
    {
        if (_transaction != null)
            await _transaction.RollbackAsync(cancellationToken);
    }

    public static bool IsInMemoryDatabase(ApplicationDbContext context)
    {
        return context.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
    }
}