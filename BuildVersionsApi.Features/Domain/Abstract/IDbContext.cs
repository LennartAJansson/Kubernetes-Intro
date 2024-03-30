namespace BuildVersionsApi.Features.Domain.Abstract;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public interface IDbContext
{
    int SaveChanges();

    int SaveChanges(bool acceptAllChangesOnSuccess);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;

    EntityEntry<TEntity> Attach<TEntity>(TEntity entity) where TEntity : class;

    EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;

    EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;

    EntityEntry Add(object entity);

    EntityEntry Attach(object entity);

    EntityEntry Update(object entity);

    EntityEntry Remove(object entity);
}