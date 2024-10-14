using MediatR;

namespace ToDoList.Application.Domain.Tasks.Commands.DeleteTask;

public record DeleteTaskCommand(Guid Id, Guid UserId) : IRequest;