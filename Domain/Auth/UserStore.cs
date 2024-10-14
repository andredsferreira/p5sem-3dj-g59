using System.Collections.Generic;

namespace DDDSample1.Auth;

public class UserStore {
    
    // Declare a public static list of User objects named 'Users'.
    // Being static, this list is shared across all instances of the UserStore class and accessible without creating an instance of the class.
    // This list is initialized with predefined user data.
    public static List<User> Users = new List<User> {
            new User { Id=1, Username = "admin", Password = "password", Email="admin@Example.com", Roles = new List<string> { "Admin", "User" } },
            new User { Id=2, Username = "user", Password = "password", Email="user@Example.com", Roles = new List<string> { "User" } },
            new User { Id=3, Username = "test", Password = "password", Email="test@Example.com", Roles = new List<string> { "Admin" } }
    };
}