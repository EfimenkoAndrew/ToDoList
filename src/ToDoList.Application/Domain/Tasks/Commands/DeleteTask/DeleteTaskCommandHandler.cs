using MediatR;
using ToDoList.Core.Common;
using ToDoList.Core.Domain.Tasks.Common;

namespace ToDoList.Application.Domain.Tasks.Commands.DeleteTask;

public class DeleteTaskCommandHandler(ITasksRepository tasksRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteTaskCommand>
{
    public async Task Handle(DeleteTaskCommand command, CancellationToken cancellationToken)
    {
        var task = await tasksRepository.FindOrDefaultAsync(command.Id, cancellationToken);
        if(task is not null)
        {
            tasksRepository.Delete(task);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}