namespace ToDoList.Core.Domain.Tasks.Data;

public record UpdateTaskData(
    string Title,
    string Description,
    DateTime DueDate);