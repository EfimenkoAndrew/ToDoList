using ToDoList.Core.Common;
using ToDoList.Persistence.ToDoListDb;

namespace ToDoList.Infrastructure.Common;

public class UnitOfWork(ToDoListDbContext dbContext, IDomainEventsDispatcher domainEventsDispatcher) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await domainEventsDispatcher.DispatchEventsAsync(cancellationToken);
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}
