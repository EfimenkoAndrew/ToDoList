using ToDoList.Core.Common;

namespace ToDoList.Core.Domain.Users.Models;

public class User : Entity, IAggregateRoot
{
    public Guid Id { get; private set; }
    
    public string FirstName { get; private set; }
    
    public string LastName { get; private set; }
    
    public string Email { get; private set; }
}