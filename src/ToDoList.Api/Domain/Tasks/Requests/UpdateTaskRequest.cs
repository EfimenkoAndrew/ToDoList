namespace ToDoList.Api.Domain.Tasks.Requests;

public record UpdateTaskRequest
{
    public Guid Id { get; init; }

    public string Title { get; init; }

    public string Description { get; init; }

    public DateTime DueDate { get; init; }
}