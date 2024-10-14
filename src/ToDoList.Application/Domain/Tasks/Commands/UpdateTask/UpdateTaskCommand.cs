using MediatR;

namespace ToDoList.Application.Domain.Tasks.Commands.UpdateTask;

public record UpdateTaskCommand(Guid Id, Guid UserId, string Title, string Description)  : IRequest;