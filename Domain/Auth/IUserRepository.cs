using DDDSample1.Auth;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Auth {

    public interface IUserRepository : IRepository<User, UserId> {
        
    }

}