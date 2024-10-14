using Task = ToDoList.Core.Domain.Tasks.Models.Task;

namespace ToDoList.Core.Domain.Tasks.Common;

public interface ITasksRepository
{
    public Task<Task> FindAsync(Guid id, CancellationToken cancellationToken = default);

    public Task<Task?> FindOrDefaultAsync(Guid id, CancellationToken cancellationToken = default);

    public void Add(Task task);

    public void Delete(Task task);
}
