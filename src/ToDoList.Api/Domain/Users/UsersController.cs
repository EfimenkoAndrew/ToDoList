using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Api.Common;
using ToDoList.Api.Constants;
using ToDoList.Api.Domain.Tasks.Requests;
using ToDoList.Api.Domain.Users.Requests;
using ToDoList.Application.Common;
using ToDoList.Application.Domain.Tasks.Commands.CreateTask;
using ToDoList.Application.Domain.Tasks.Commands.DeleteTask;
using ToDoList.Application.Domain.Tasks.Commands.UpdateTask;
using ToDoList.Application.Domain.Tasks.Queries.GetTaskDetails;
using ToDoList.Application.Domain.Tasks.Queries.GetTasks;
using ToDoList.Application.Domain.Users.Commands.CreateUser;
using ToDoList.Application.Domain.Users.Commands.DeleteUser;
using ToDoList.Application.Domain.Users.Commands.UpdateUser;
using ToDoList.Application.Domain.Users.Queries.GetUserDetails;
using ToDoList.Application.Domain.Users.Queries.GetUsers;

namespace ToDoList.Api.Domain.Users;

[Route(Routes.Users)]
public class UsersController(IMediator mediator) : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PageResponse<TaskDto[]>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetUsers(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetUsersQuery(pageNumber, pageSize);
        var tasks = await mediator.Send(query, cancellationToken);
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TaskDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskDetailsDto>> GetTask(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetUserDetailsQuery(id);
        var task = await mediator.Send(query, cancellationToken);
        return Ok(task);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreatedResponse<Guid>), StatusCodes.Status201Created)]
    public async Task<ActionResult<TaskDto>> CreateTask(
        [FromBody] [Required] CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand(request.FirstName, request.LastName, request.Email);
        var taskId = await mediator.Send(command, cancellationToken);
        return Created(taskId);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskDto>> UpdateTask(
        [FromRoute] Guid id, 
        [FromBody] [Required] UpdateUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateUserCommand(id, request.FirstName, request.LastName, request.Email);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> DeleteTask(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteUserCommand(id);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }
}