using MediatR;
using ToDoList.Core.Common;
using ToDoList.Core.Domain.Tasks.Common;

namespace ToDoList.Application.Domain.Tasks.Commands.DeleteTask;

public class DeleteTaskCommandHandler(ITasksRepository tasksRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteTaskCommand>
{
    public async Task Handle(DeleteTaskCommand command, CancellationToken cancellationToken)
    {
        var task = await tasksRepository.FindAsync(command.Id, cancellationToken);
        tasksRepository.Delete(task);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}