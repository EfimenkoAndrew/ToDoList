using MediatR;
using ToDoList.Core.Common;
using ToDoList.Persistence.ToDoListDb;

namespace ToDoList.Infrastructure.Processing;

//todo: cover it with unit tests when inbox-outbox pattern will be implemented
internal class DomainEventsDispatcher(
    ToDoListDbContext dbContext,
    IMediator mediator) : IDomainEventsDispatcher
{
    public async Task DispatchEventsAsync(CancellationToken cancellationToken)
    {
        var domainEntities = dbContext.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents.Any()).ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            if (cancellationToken.IsCancellationRequested) break;

            await mediator.Publish(domainEvent, cancellationToken);
        }
    }
}
