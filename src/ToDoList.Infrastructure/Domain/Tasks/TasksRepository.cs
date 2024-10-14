using Microsoft.EntityFrameworkCore;
using ToDoList.Core.Domain.Tasks.Common;
using ToDoList.Core.Exceptions;
using ToDoList.Persistence.ToDoListDb;
using Task = ToDoList.Core.Domain.Timers.Models.Task;

namespace ToDoList.Infrastructure.Domain.Tasks;

public class TasksRepository(ToDoListDbContext dbContext) : ITasksRepository
{
    public async Task<Task> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext
            .Tasks
            .Include(x=>x.User)
            .Include(x=>x.SharedWithUsers)
            .ThenInclude(x=>x.User)
            .Include(x=>x.SharedWithUsers)
            .ThenInclude(x=>x.Task)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
            ?? throw new NotFoundException($"{nameof(Task)} with id: '{id}' was not found.");
    }

    public async Task<Task?> FindOrDefaultAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext
            .Tasks
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public void Add(Task task)
    {
        dbContext.Tasks.Add(task);
    }

    public void Delete(Task task)
    {
        dbContext.Tasks.Remove(task);
    }
}