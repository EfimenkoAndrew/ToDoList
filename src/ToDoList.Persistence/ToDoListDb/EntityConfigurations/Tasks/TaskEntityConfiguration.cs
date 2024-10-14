using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = ToDoList.Core.Domain.Timers.Models.Task;

namespace ToDoList.Persistence.ToDoListDb.EntityConfigurations.Tasks;

public class TaskEntityConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Ignore(x => x.DomainEvents);

        builder.Property(x => x.Title)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder
            .Metadata
            .FindNavigation(nameof(Task.SharedWithUsers))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
        
        builder
            .HasMany(x => x.SharedWithUsers)
            .WithOne(x => x.Task)
            .HasForeignKey(x => x.TaskId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}