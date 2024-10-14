using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Common;
using ToDoList.Application.Domain.Tasks.Queries.GetTasks;
using ToDoList.Persistence.ToDoListDb;

namespace ToDoList.Infrastructure.Application.Domain.Tasks.GetTasks;

public class GetTasksQueryHandler(ToDoListDbContext dbContext) : IRequestHandler<GetTasksQuery, PageResponse<TaskDto[]>>
{
    public async Task<PageResponse<TaskDto[]>> Handle(GetTasksQuery query, CancellationToken cancellationToken)
    {
        var sqlQuery = dbContext.Tasks
            .AsNoTracking()
            .Where(x => x.UserId == query.UserId || x.SharedWithUsers.Any(u => u.UserId == query.UserId));

        var total = await sqlQuery.CountAsync(cancellationToken);
        var skip = (query.PageNumber - 1) * query.PageSize;

        var tasks = await sqlQuery
            .OrderBy(x => x.Title)
            .ThenBy(x => x.CreatedAt)
            .Skip(skip)
            .Take(query.PageSize)
            .Select(x => new TaskDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                CreatedAt = x.CreatedAt,
            })
            .ToArrayAsync(cancellationToken);

        return new PageResponse<TaskDto[]>(total, tasks);
    }
}
