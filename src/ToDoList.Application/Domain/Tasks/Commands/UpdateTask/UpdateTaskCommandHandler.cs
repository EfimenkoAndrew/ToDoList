using MediatR;
using ToDoList.Core.Common;
using ToDoList.Core.Domain.Tasks.Common;
using ToDoList.Core.Domain.Tasks.Data;

namespace ToDoList.Application.Domain.Tasks.Commands.UpdateTask;

public class UpdateTaskCommandHandler(ITasksRepository tasksRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateTaskCommand>
{
    public async Task Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
    {
        var task = await tasksRepository.FindAsync(command.Id, cancellationToken);
        var data = new UpdateTaskData(
            command.Title,
            command.Description);
        task.Update(data);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
