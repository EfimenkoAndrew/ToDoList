using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Core.Common;
using ToDoList.Core.Domain.Tasks.Common;
using ToDoList.Core.Domain.Users.Common;
using ToDoList.Infrastructure.Common;
using ToDoList.Infrastructure.Domain.Tasks;
using ToDoList.Infrastructure.Domain.Users;
using ToDoList.Infrastructure.Exceptions;
using ToDoList.Infrastructure.Processing;

namespace ToDoList.Infrastructure;

public static class InfrastructureRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // mediatr
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITasksRepository, TasksRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();

        // exceptions
        services.AddSingleton<IExceptionToResponseMapper, ExceptionToResponseMapper>();
        services.AddSingleton<IExceptionToResponseDeveloperMapper, ExceptionToResponseDeveloperMapper>();
        services.AddTransient<ExceptionHandlerDeveloperMiddleware>();
        services.AddTransient<ExceptionHandlerMiddleware>();

        // processing
        services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
    }
}
