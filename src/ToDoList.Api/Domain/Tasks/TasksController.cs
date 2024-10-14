using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Api.Common;
using ToDoList.Api.Constants;
using ToDoList.Api.Domain.Tasks.Requests;
using ToDoList.Application.Common;
using ToDoList.Application.Domain.Tasks.Commands.CreateTask;
using ToDoList.Application.Domain.Tasks.Commands.DeleteTask;
using ToDoList.Application.Domain.Tasks.Commands.ShareTask;
using ToDoList.Application.Domain.Tasks.Commands.UnshareTask;
using ToDoList.Application.Domain.Tasks.Commands.UpdateTask;
using ToDoList.Application.Domain.Tasks.Queries.GetTaskDetails;
using ToDoList.Application.Domain.Tasks.Queries.GetTasks;
using ToDoList.Core.Domain.Tasks.Common;

namespace ToDoList.Api.Domain.Tasks;

[Route(Routes.Tasks)]
public class TasksController(IMediator mediator, ITasksRepository tasksRepository) : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PageResponse<TaskDto[]>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasks(
        [FromHeader][Required] Guid userId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetTasksQuery(pageNumber, pageSize, userId);
        var tasks = await mediator.Send(query, cancellationToken);
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TaskDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskDetailsDto>> GetTask(
        [FromHeader][Required] Guid userId,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetTaskDetailsQuery(id, userId);
        var task = await mediator.Send(query, cancellationToken);
        return Ok(task);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreatedResponse<Guid>), StatusCodes.Status201Created)]
    public async Task<ActionResult<TaskDto>> CreateTask(
        [FromHeader][Required] Guid userId,
        [FromBody][Required] CreateTaskRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateTaskCommand(userId, request.Title, request.Description, request.DueDate);
        var taskId = await mediator.Send(command, cancellationToken);
        return Created(taskId);
    }

    [HttpPost("{id}/share")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ShareTask(
        [FromHeader][Required] Guid userId,
        [FromRoute] Guid id,
        [FromBody][Required] ShareTaskRequest request,
        CancellationToken cancellationToken)
    {
        // this is the worst way to check if the user has access to the task
        // this should be done in the authorization handler
        var task = await tasksRepository.FindAsync(id, cancellationToken);

        if (task.UserId != userId && task.SharedWithUsers.All(x => x.UserId != userId))
        {
            return NotFound();
        }

        var command = new ShareTaskCommand(id, request.UserId);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpPost("{id}/unshare")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UnshareTask(
        [FromHeader][Required] Guid userId,
        [FromRoute] Guid id,
        [FromBody][Required] ShareTaskRequest request,
        CancellationToken cancellationToken)
    {
        // this is the worst way to check if the user has access to the task
        // this should be done in the authorization handler
        var task = await tasksRepository.FindAsync(id, cancellationToken);

        if (task.UserId != userId)
        {
            return NotFound();
        }

        var command = new UnshareTaskCommand(id, request.UserId);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskDto>> UpdateTask(
        [FromHeader][Required] Guid userId,
        [FromRoute] Guid id,
        [FromBody][Required] UpdateTaskRequest request,
        CancellationToken cancellationToken)
    {
        // this is the worst way to check if the user has access to the task
        // this should be done in the authorization handler
        var task = await tasksRepository.FindAsync(id, cancellationToken);

        if (task.UserId != userId && task.SharedWithUsers.All(x => x.UserId != userId))
        {
            return NotFound();
        }

        var command = new UpdateTaskCommand(id, userId, request.Title, request.Description);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> DeleteTask(
        [FromHeader][Required] Guid userId,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        // this is the worst way to check if the user has access to the task
        // this should be done in the authorization handler
        var task = await tasksRepository.FindAsync(id, cancellationToken);

        if (task.UserId != userId)
        {
            return NotFound();
        }

        var command = new DeleteTaskCommand(id, userId);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }
}
