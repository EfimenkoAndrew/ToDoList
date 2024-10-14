using MediatR;
using ToDoList.Core.Common;
using ToDoList.Core.Domain.Tasks.Common;

namespace ToDoList.Application.Domain.Tasks.Commands.UnshareTask;

public class UnshareTaskCommandHandler(ITasksRepository tasksRepository, IUnitOfWork unitOfWork) : IRequestHandler<UnshareTaskCommand>
{
    public async Task Handle(UnshareTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await tasksRepository.FindAsync(request.TaskId, cancellationToken);
        task.UnshareWithUser(request.SharedWithUserId);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}