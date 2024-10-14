namespace ToDoList.Core.Domain.Tasks.Data;

public record CreateTaskData(
    string Title,
    string Description,
    DateTime DueDate,
    Guid UserId);
