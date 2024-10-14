namespace ToDoList.Core.Common;

public interface IDomainEventsDispatcher
{
    Task DispatchEventsAsync(CancellationToken cancellationToken);
}
