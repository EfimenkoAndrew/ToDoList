namespace ToDoList.Application.Domain.Tasks.Queries.GetTasks;

public record TaskDto
{
    public Guid Id { get; init; }

    public string Title { get; init; }

    public string Description { get; init; }

    public DateTime CreatedAt { get; init; }
}
