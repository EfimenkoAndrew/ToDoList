using ToDoList.Core.Domain.Tasks.Common;
using Task = ToDoList.Core.Domain.Timers.Models.Task;

namespace ToDoList.Infrastructure.Domain.Tasks;

public class TasksRepository : ITasksRepository
{
    public Task<Task> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Task?> FindOrDefaultAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Add(Task task)
    {
        throw new NotImplementedException();
    }

    public void Delete(Task task)
    {
        throw new NotImplementedException();
    }
}