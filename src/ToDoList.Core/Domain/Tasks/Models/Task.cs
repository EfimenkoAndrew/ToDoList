using ToDoList.Core.Domain.Tasks.Data;
using ToDoList.Core.Domain.Users.Models;

namespace ToDoList.Core.Domain.Timers.Models;

public class Task
{
    private List<User> _users = new();
    
    public Guid Id { get; private set; }
    
    public string Title { get; private set; }
    
    public string Description { get; private set; }
    
    public DateTime CteatedAt { get; private set; }
    
    public Guid UserId { get; private set; }
    
    public User User { get; private set; }
    
    public IReadOnlyCollection<User> SharedWithUsers => _users.AsReadOnly();
    
    private Task(Guid id, string title, string description, Guid userId)
    {
        Id = id;
        Title = title;
        Description = description;
        UserId = userId;
        CteatedAt = DateTime.UtcNow;
    }
    
    public static Task Create(CreateTaskData data)
    {
        return new(Guid.NewGuid(), data.Title, data.Description, data.UserId);
    }
    
    public void ShareWithUser(User user)
    {
        _users.Add(user);
    }
    
    public void UnshareWithUser(User user)
    {
        _users.Remove(user);
    }

    public void Update(UpdateTaskData data)
    {
        
    }
}