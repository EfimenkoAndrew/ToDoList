using ToDoList.Core.Domain.Users.Models;

namespace ToDoList.Core.Domain.Users.Common;

public interface IUsersRepository
{
    Task<User> FindAsync(Guid id, CancellationToken cancellationToken = default);

    Task<User?> FindOrDefaultAsync(Guid id, CancellationToken cancellationToken = default);

    void Add(User user);

    void Delete(User user);
}
