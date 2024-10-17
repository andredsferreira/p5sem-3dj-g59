using DDDSample1.Domain.Auth;
using DDDSample1.Infrastructure.Shared;

namespace DDDSample1.Infrastructure.Auth {

    public class UserRepository : BaseRepository<User, UserId>, IUserRepository {

        public UserRepository(DDDSample1DbContext context) : base(context.Users) {
            
        }

    }

}