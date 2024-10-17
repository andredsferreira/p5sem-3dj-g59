using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Auth {

    public class AuthService {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _repository;

        public AuthService(IUnitOfWork unitOfWork, IUserRepository repository) {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<UserDTO> CreateUser(UserDTO dto) {
            var user = User.createFromDTO(dto);
            await this._repository.AddAsync(user);
            await this._unitOfWork.CommitAsync();

            // TODO!
            return null;
        }

    }

}