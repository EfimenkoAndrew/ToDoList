using MediatR;

namespace ToDoList.Application.Domain.Tasks.Commands.UnshareTask;

public record UnshareTaskCommand(Guid TaskId, Guid SharedWithUserId) : IRequest;
