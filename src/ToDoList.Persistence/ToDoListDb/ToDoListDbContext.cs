using Microsoft.EntityFrameworkCore;
using Task = ToDoList.Core.Domain.Tasks.Models.Task;

namespace ToDoList.Persistence.ToDoListDb;

public class ToDoListDbContext : DbContext
{
    internal const string TdlSchema = "tdl";
    internal const string TdlMigrationsHistoryTable = "__TdlMigrationsHistory";

    public DbSet<Task> Tasks { get; set; }

    public DbSet<Core.Domain.Users.Models.User> Users { get; set; }

    public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(TdlSchema);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoListDbContext).Assembly);
    }
}
