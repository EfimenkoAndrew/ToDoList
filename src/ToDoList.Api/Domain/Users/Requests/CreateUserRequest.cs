namespace ToDoList.Api.Domain.Users.Requests;

public record CreateUserRequest(
    string FirstName,
    string LastName,
    string Email);
