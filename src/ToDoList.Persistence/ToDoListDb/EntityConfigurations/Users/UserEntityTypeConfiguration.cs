using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Core.Domain.Tasks.Models;
using ToDoList.Core.Domain.Users.Models;

namespace ToDoList.Persistence.ToDoListDb.EntityConfigurations.Users;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.FirstName)
            .HasMaxLength(250);

        builder
            .Property(x => x.LastName)
            .HasMaxLength(250);

        builder
            .Property(x => x.Email)
            .HasMaxLength(250);

        builder
            .HasMany<TaskUser>()
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
