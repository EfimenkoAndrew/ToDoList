using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Persistence.ToDoListDb;

public class ToDoListDbContext : DbContext
{
    internal const string TdlcSchema = "tdl";
    internal const string TdlMigrationsHistoryTable = "__TdlMigrationsHistory";
    
    public DbSet<Core.Domain.Timers.Models.Task> Tasks { get; set; }

    public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(TdlcSchema);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoListDbContext).Assembly);
    }
}