using MediatR;
using ToDoList.Core.Common;
using ToDoList.Core.Domain.Tasks.Common;
using ToDoList.Core.Domain.Tasks.Data;
using Task = ToDoList.Core.Domain.Timers.Models.Task;

namespace ToDoList.Application.Domain.Tasks.Commands.CreateTask;

public class CreateTaskCommandHandler(
    ITasksRepository tasksRepository, 
    IUnitOfWork unitOfWork) : IRequestHandler<CreateTaskCommand, Guid>
{
    public async Task<Guid> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
    {
        var data = new CreateTaskData(
            command.Title,
            command.Description,
            command.DueDate,
            command.UserId);

        var task = Task.Create(data);
        tasksRepository.Add(task);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return task.Id;
    }
}