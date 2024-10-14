using MediatR;
using ToDoList.Core.Common;
using ToDoList.Core.Domain.Tasks.Common;
using ToDoList.Core.Domain.Tasks.Models;
using ToDoList.Core.Domain.Users.Common;
using Task = System.Threading.Tasks.Task;

namespace ToDoList.Application.Domain.Tasks.Commands.UnshareTask;

public class UnshareTaskCommandHandler(
    ITasksRepository tasksRepository,
    IUsersRepository usersRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UnshareTaskCommand>
{
    public async Task Handle(UnshareTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await tasksRepository.FindAsync(request.TaskId, cancellationToken);
        var user = await usersRepository.FindAsync(request.SharedWithUserId, cancellationToken);

        var taskUser = TaskUser.Create(user.Id, task.Id);

        task.UnshareWithUser(taskUser);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
