using MediatR;

namespace ToDoList.Application.Domain.Tasks.Queries.GetTaskDetails;

public record GetTaskDetailsQuery(Guid Id, Guid UserId) : IRequest<TaskDetailsDto>;