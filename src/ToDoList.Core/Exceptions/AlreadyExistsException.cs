namespace ToDoList.Core.Exceptions;

public class AlreadyExistsException(string message, object details) : DomainException(message)
{
    public object Details { get; } = details;
}
