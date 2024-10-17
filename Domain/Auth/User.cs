using System;
using DDDSample1.Domain.OperationRequestsAuth;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Auth;

public class User : Entity<UserId>, IAggregateRoot {

    public string Username { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public UserRole Role { get; set; }
    
    public User(string Username, string Password, string Email, UserRole Role){
        Id = new UserId(Guid.NewGuid());
        this.Username = Username;
        this.Password = Password;
        this.Email = Email;
        this.Role = Role;
    }
    public static User createFromDTO(UserDTO dto){
        return new User(dto.Username, dto.Email, dto.Password, (UserRole)dto.Role);
    }
}