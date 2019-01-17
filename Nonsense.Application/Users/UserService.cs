using Nonsense.Application.Gateways.Repositories;
using Nonsense.Application.Users.Dto;
using Nonsense.Common;
using Nonsense.Common.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nonsense.Application.Users {

    public class UserService : IUserService {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) {
            Guard.NotNull(userRepository, nameof(userRepository));

            _userRepository = userRepository;
        }

        public async Task<BoundaryResponse<string>> CreateUser(User user) {
            Guard.NotNull(user, nameof(user));

            var response = await _userRepository.Create(user.UserName, user.Email, user.Password);

            return new BoundaryResponse<string>(response.Success, response.ErrorsList, response.Data);
        }

        public async Task<BoundaryResponse<User>> GetUserById(string id) {
            Guard.NotNullOrEmpty(id, nameof(id));

            var response = await _userRepository.GetById(id);

            return new BoundaryResponse<User>(response.Success, response.ErrorsList, response.Data);
        }

        public async Task<BoundaryResponse<IEnumerable<User>>> GetAllUsers() {
            var response = await _userRepository.GetAll();

            return new BoundaryResponse<IEnumerable<User>>(response.Success, response.ErrorsList, response.Data);
        }

        public async Task<OperationResult> EditUser(User user) {
            Guard.NotNull(user, nameof(user));

            var response = await _userRepository.Update(user.Id, user.UserName, user.Email, user.Password);

            return new OperationResult(response.Success, response.ErrorsList);
        }

        public async Task<OperationResult> DeleteUser(string id) {
            Guard.NotNull(id, nameof(id));

            var response = await _userRepository.Delete(id);

            return new OperationResult(response.Success, response.ErrorsList);
        }
    }
}
