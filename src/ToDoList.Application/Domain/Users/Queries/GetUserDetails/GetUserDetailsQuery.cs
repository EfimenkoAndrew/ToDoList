using MediatR;

namespace ToDoList.Application.Domain.Users.Queries.GetUserDetails;

public record GetUserDetailsQuery(Guid Id) : IRequest<UserDetailsDto>;
