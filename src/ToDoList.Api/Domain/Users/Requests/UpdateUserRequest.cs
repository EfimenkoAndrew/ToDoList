namespace ToDoList.Api.Domain.Users.Requests;

public record UpdateUserRequest(
    string FirstName,
    string LastName,
    string Email);