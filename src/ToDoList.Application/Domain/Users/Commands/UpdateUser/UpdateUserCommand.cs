using MediatR;

namespace ToDoList.Application.Domain.Users.Commands.UpdateUser;

public record UpdateUserCommand(
    Guid Id,
    string FirstName, 
    string LastName,
    string Email) : IRequest;