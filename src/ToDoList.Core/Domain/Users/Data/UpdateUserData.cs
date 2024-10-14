namespace ToDoList.Core.Domain.Users.Data;

public record UpdateUserData(
    string FirstName,
    string LastName,
    string Email);