using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Domain.Users.Queries.GetUserDetails;
using ToDoList.Core.Domain.Users.Models;
using ToDoList.Core.Exceptions;
using ToDoList.Persistence.ToDoListDb;

namespace ToDoList.Infrastructure.Application.Domain.Users.GetUserDeteils;

public class GetUserDetailsQueryHandler(ToDoListDbContext dbContext) : IRequestHandler<GetUserDetailsQuery, UserDetailDto>
{
    public async Task<UserDetailDto> Handle(GetUserDetailsQuery query, CancellationToken cancellationToken)
    {
        return await dbContext
            .Users
            .AsNoTracking()
            .Select(x => new UserDetailDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email
            })
            .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken)
            ?? throw new NotFoundException($"{nameof(User)} with id: '{query.Id}' was not found.");
    }
}