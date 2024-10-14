using MediatR;
using ToDoList.Core.Common;
using ToDoList.Core.Domain.Users.Common;
using ToDoList.Core.Domain.Users.Data;

namespace ToDoList.Application.Domain.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler(IUsersRepository usersRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await usersRepository.FindAsync(command.Id, cancellationToken);
        var data = new UpdateUserData(
            command.FirstName,
            command.LastName,
            command.Email);
        user.Update(data);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}