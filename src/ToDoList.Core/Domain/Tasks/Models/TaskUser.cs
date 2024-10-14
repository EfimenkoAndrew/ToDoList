using ToDoList.Core.Domain.Users.Models;

namespace ToDoList.Core.Domain.Tasks.Models;

public class TaskUser
{
    public Guid UserId { get; private init; }

    public User User { get; private init; }

    public Guid TaskId { get; private init; }

    public Task Task { get; private init; }

    private TaskUser()
    {
    }

    private TaskUser(Guid userId, Guid taskId)
    {
        UserId = userId;
        TaskId = taskId;
    }

    public static TaskUser Create(Guid userId, Guid taskId)
    {
        return new TaskUser(userId, taskId);
    }
}
