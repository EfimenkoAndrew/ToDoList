using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Domain.Tasks.Queries.GetTaskDetails;
using ToDoList.Core.Exceptions;
using ToDoList.Persistence.ToDoListDb;
using Task = ToDoList.Core.Domain.Tasks.Models.Task;

namespace ToDoList.Infrastructure.Application.Domain.Tasks.GetTaskDetails;

public class GetTaskDetailsQueryHandler(ToDoListDbContext dbContext) : IRequestHandler<GetTaskDetailsQuery, TaskDetailsDto>
{
    public async Task<TaskDetailsDto> Handle(GetTaskDetailsQuery query, CancellationToken cancellationToken)
    {
        return await dbContext.Tasks
                .AsNoTracking()
                .Include(x => x.SharedWithUsers)
                .Where(x => x.Id == query.Id && x.UserId == query.UserId)
                .Where(x => x.SharedWithUsers.Any(u => u.UserId == query.UserId))
                .Select(x => new TaskDetailsDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                })
                .SingleOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException($"{nameof(Task)} with id: '{query.Id}' was not found.");
    }
}
