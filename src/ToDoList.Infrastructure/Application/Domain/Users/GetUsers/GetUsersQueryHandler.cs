using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Common;
using ToDoList.Application.Domain.Users.Queries.GetUsers;
using ToDoList.Persistence.ToDoListDb;

namespace ToDoList.Infrastructure.Application.Domain.Users.GetUsers;

public class GetUsersQueryHandler(ToDoListDbContext dbContext) : IRequestHandler<GetUsersQuery, PageResponse<UserDto[]>>
{
    public async Task<PageResponse<UserDto[]>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var sqlQuery = dbContext
            .Users
            .AsNoTracking();

        var take = query.PageSize;
        var skip = (query.PageNumber - 1) * query.PageSize;
        var total = await sqlQuery.CountAsync(cancellationToken);

        var users = await sqlQuery
            .Select(x => new UserDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
            })
            .Skip(skip)
            .Take(take)
            .ToArrayAsync(cancellationToken);

        return new PageResponse<UserDto[]>(total, users);
    }
}
