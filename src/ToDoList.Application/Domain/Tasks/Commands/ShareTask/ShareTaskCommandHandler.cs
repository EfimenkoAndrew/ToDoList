using MediatR;
using ToDoList.Core.Common;
using ToDoList.Core.Domain.Tasks.Common;
using ToDoList.Core.Domain.Tasks.Models;
using ToDoList.Core.Domain.Users.Common;
using Task = System.Threading.Tasks.Task;

namespace ToDoList.Application.Domain.Tasks.Commands.ShareTask;

public class ShareTaskCommandHandler(
    ITasksRepository tasksRepository,
    IUsersRepository usersRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<ShareTaskCommand>
{
    public async Task Handle(ShareTaskCommand command, CancellationToken cancellationToken)
    {
        var task = await tasksRepository.FindAsync(command.TaskId, cancellationToken);
        var user = await usersRepository.FindAsync(command.SharedWithUserId, cancellationToken);
        var taskUser = TaskUser.Create(user.Id, task.Id);
        task.ShareWithUser(taskUser);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
