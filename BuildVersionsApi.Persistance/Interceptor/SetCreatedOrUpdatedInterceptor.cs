namespace BuildVersionsApi.Persistance.Interceptor;

using System;
using System.Threading.Tasks;

using BuildVersionsApi.Domain.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

public class SetCreatedOrUpdatedInterceptor : SaveChangesInterceptor
{
  public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
          DbContextEventData eventData,
          InterceptionResult<int> result,
          CancellationToken cancellationToken = default)
  {
    if (eventData.Context is null)
    {
      return base.SavingChangesAsync(
          eventData, result, cancellationToken);
    }

    foreach (EntityEntry<BaseLoggedEntity> entry in eventData.Context!.ChangeTracker.Entries<BaseLoggedEntity>())
    {
      entry.Entity.Changed = DateTime.Now;
      if (entry.State == EntityState.Added)
      {
        entry.Entity.Created = DateTime.Now;
      }
    }

    return base.SavingChangesAsync(eventData, result, cancellationToken);
  }
}