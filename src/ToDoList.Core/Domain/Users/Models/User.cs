using ToDoList.Core.Common;
using ToDoList.Core.Domain.Users.Data;

namespace ToDoList.Core.Domain.Users.Models;

public class User : Entity, IAggregateRoot
{
    private User() { }
    
    public Guid Id { get; private set; }
    
    public string FirstName { get; private set; }
    
    public string LastName { get; private set; }
    
    public string Email { get; private set; }
    
    private User(string firstName, string lastName, string email)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
    
    public static User Create(CreateUserData data)
    {
        return new User(data.FirstName, data.LastName, data.Email);
    }
    
    public void Update(UpdateUserData data)
    {
        FirstName = data.FirstName;
        LastName = data.LastName;
        Email = data.Email;
    }
}