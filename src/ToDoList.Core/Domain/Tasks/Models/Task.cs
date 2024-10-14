using ToDoList.Core.Common;
using ToDoList.Core.Domain.Tasks.Data;
using ToDoList.Core.Domain.Users.Models;

namespace ToDoList.Core.Domain.Timers.Models;

public class Task : Entity, IAggregateRoot
{
    // for ef core
    private Task()
    {
    }
    
    private List<TaskUser> _sharedWithUsers = new();
    
    public Guid Id { get; private set; }
    
    public string Title { get; private set; }
    
    public string? Description { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    
    public Guid UserId { get; private set; }
    
    public User User { get; private set; }
    
    public IReadOnlyCollection<TaskUser> SharedWithUsers => _sharedWithUsers.AsReadOnly();
    
    private Task(Guid id, string title, string description, Guid userId)
    {
        Id = id;
        Title = title;
        Description = description;
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
    }
    
    public static Task Create(CreateTaskData data)
    {
        return new(Guid.NewGuid(), data.Title, data.Description, data.UserId);
    }
    
    public void ShareWithUser(Guid userId)
    {
        if (_sharedWithUsers.Any(x => x.UserId == userId))
        {
            return;
        }
        
        _sharedWithUsers.Add(TaskUser.Create(userId, Id));
    }
    
    public void UnshareWithUser(Guid userId)
    {
        var user = _sharedWithUsers.SingleOrDefault(x => x.UserId == userId);
        if (user is null)
        {
            return;
        }
        
        _sharedWithUsers.Remove(user);
    }

    public void Update(UpdateTaskData data)
    {
        Title = data.Title;
        Description = data.Description;
    }
}