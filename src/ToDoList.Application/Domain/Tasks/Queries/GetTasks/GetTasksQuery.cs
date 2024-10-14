using MediatR;
using ToDoList.Application.Common;

namespace ToDoList.Application.Domain.Tasks.Queries.GetTasks;

public record GetTasksQuery(int PageNumber, int PageSize, Guid UserId) : IRequest<PageResponse<TaskDto[]>>;