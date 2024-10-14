using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ToDoList.Persistence.ToDoListDb;

public static class ToDoDbRegistration
{
    private const string ConnectionStringName = "ToDoListDb";
    
    public static void  AddToDoListDb(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringName)
                               ?? throw new AggregateException(
                                   $"Connection string: '{ConnectionStringName}' is not found in configurations.");


        services.AddDbContext<ToDoListDbContext>(options =>
        {
            options.UseNpgsql(
                connectionString,
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsHistoryTable(
                        ToDoListDbContext.TdlMigrationsHistoryTable,
                        ToDoListDbContext.TdlSchema);
                });
        });

        services.AddScoped<DbContext>(provider => provider.GetRequiredService<ToDoListDbContext>());
    }
}