namespace ToDoList.Api.Domain.Tasks.Requests;

public record CreateTaskRequest
{
    public string Title { get; init; }
    
    public string Description { get; init; }
    
    public DateTime DueDate { get; init; }
    
    public Guid CategoryId { get; init; }
    
    public Guid UserId { get; init; }
}