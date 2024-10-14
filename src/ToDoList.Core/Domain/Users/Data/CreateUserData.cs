namespace ToDoList.Core.Domain.Users.Data;

public record CreateUserData(
    string FirstName,
    string LastName,
    string Email);
