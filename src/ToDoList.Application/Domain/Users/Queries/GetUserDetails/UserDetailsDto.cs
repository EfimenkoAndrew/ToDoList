namespace ToDoList.Application.Domain.Users.Queries.GetUserDetails;

public record UserDetailsDto
{
    public Guid Id { get; init; }
    
    public string FirstName { get; init; }
    
    public string LastName { get; init; }
    
    public string Email { get; init; }
}