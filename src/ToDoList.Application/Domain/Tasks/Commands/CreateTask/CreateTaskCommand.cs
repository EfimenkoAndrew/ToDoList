using MediatR;

namespace ToDoList.Application.Domain.Tasks.Commands.CreateTask;

public record CreateTaskCommand(Guid UserId, string Title, string Description, DateTime DueDate) : IRequest<Guid>;