using MediatR;
using ToDoList.Core.Common;
using ToDoList.Core.Domain.Users.Common;
using ToDoList.Core.Domain.Users.Data;
using ToDoList.Core.Domain.Users.Models;

namespace ToDoList.Application.Domain.Users.Commands.CreateUser;

public class CreateUserCommandHandler(IUsersRepository usersRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var data = new CreateUserData(
            command.FirstName, 
            command.LastName, 
            command.Email);
        var user = User.Create(data);
        usersRepository.Add(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return user.Id;
    }
}