using MediatR;
using ToDoList.Core.Common;
using ToDoList.Core.Domain.Tasks.Common;

namespace ToDoList.Application.Domain.Tasks.Commands.ShareTask;

public class ShareTaskCommandHandler(ITasksRepository tasksRepository, IUnitOfWork unitOfWork) : IRequestHandler<ShareTaskCommand>
{
    public async Task Handle(ShareTaskCommand command, CancellationToken cancellationToken)
    {
        var task = await tasksRepository.FindAsync(command.TaskId, cancellationToken);
        task.ShareWithUser(command.SharedWithUserId);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}