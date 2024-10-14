using MediatR;
using ToDoList.Application.Common;

namespace ToDoList.Application.Domain.Users.Queries.GetUsers;

public record GetUsersQuery(int PageNumber, int PageSize) : IRequest<PageResponse<UserDto[]>>;