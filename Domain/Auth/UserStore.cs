using System.Collections.Generic;

namespace DDDSample1.Auth;

public class UserStore {
    
    public static List<User> Users = new List<User> {
            new User { Id=1, Username = "admin", Password = "password", Email="admin@Example.com", Roles = new List<string> { "Admin", "User" } },
            new User { Id=2, Username = "user", Password = "password", Email="user@Example.com", Roles = new List<string> { "User" } },
            new User { Id=3, Username = "test", Password = "password", Email="test@Example.com", Roles = new List<string> { "Admin" } }
    };
}