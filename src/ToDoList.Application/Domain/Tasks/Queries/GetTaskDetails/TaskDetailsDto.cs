namespace ToDoList.Application.Domain.Tasks.Queries.GetTaskDetails;

public record TaskDetailsDto
{
    public Guid Id { get; init; }

    public string Title { get; init; }

    public string Description { get; init; }

    public DateTime CreatedAt { get; init; }
}
