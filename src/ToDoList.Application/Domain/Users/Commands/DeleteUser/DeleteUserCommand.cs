using MediatR;

namespace ToDoList.Application.Domain.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid Id) : IRequest;
