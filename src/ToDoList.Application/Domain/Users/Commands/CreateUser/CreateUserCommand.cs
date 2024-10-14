using MediatR;

namespace ToDoList.Application.Domain.Users.Commands.CreateUser;

public record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email) : IRequest<Guid>;
