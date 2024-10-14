using MediatR;
using ToDoList.Core.Common;
using ToDoList.Core.Domain.Users.Common;

namespace ToDoList.Application.Domain.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler(IUsersRepository usersRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await usersRepository.FindOrDefaultAsync(command.Id, cancellationToken);
        if (user is not null)
        {
            usersRepository.Delete(user);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
