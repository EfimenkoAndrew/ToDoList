namespace ToDoList.Api.Domain.Tasks.Requests;

public record UpdateTaskRequest
{
    public string Title { get; init; }

    public string Description { get; init; }
}
