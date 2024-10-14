using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Core.Domain.Tasks.Models;

namespace ToDoList.Persistence.ToDoListDb.EntityConfigurations.Tasks;

public class TaskUserEntityTypeConfiguration : IEntityTypeConfiguration<TaskUser>
{
    public void Configure(EntityTypeBuilder<TaskUser> builder)
    {
        builder.HasKey(x => new
        {
            x.TaskId,
            x.UserId
        });
    }
}
