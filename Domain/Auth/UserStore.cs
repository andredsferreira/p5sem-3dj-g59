using System;
using System.Collections.Generic;
using DDDSample1.Domain.Auth;

namespace DDDSample1.Auth;

public class UserStore {
    
    public static List<User> Users = new List<User> {
            new User("admin", "password", "admin@Example.com", 0),
            new User("user", "password", "user@Example.com", (Domain.OperationRequestsAuth.UserRole)1),
            new User("test", "password", "test@Example.com", 0)
    };
}