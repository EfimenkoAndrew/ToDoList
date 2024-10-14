using MediatR;

namespace ToDoList.Application.Domain.Tasks.Commands.ShareTask;

public record ShareTaskCommand(
    Guid TaskId,
    Guid SharedWithUserId
    ) : IRequest;
