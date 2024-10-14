namespace ToDoList.Application.Domain.Users.Queries.GetUsers;

public record UserDto
{
    public Guid Id { get; init; }
    
    public string FirstName { get; init; }
    
    public string LastName { get; init; }
}