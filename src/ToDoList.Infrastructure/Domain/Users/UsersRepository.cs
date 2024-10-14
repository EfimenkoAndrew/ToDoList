using Microsoft.EntityFrameworkCore;
using ToDoList.Core.Domain.Users.Common;
using ToDoList.Core.Domain.Users.Models;
using ToDoList.Core.Exceptions;
using ToDoList.Persistence.ToDoListDb;

namespace ToDoList.Infrastructure.Domain.Users;

public class UsersRepository(ToDoListDbContext dbContext) : IUsersRepository
{
    public async Task<User> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext
            .Users
            .FirstOrDefaultAsync(x=> x.Id == id, cancellationToken)
            ?? throw new NotFoundException($"{nameof(User)} with id: '{id}' was not found.");
    }

    public async Task<User?> FindOrDefaultAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext
            .Users
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public void Add(User user)
    {
        dbContext.Users.Add(user);
    }

    public void Delete(User user)
    {
        dbContext.Users.Remove(user);
    }
}