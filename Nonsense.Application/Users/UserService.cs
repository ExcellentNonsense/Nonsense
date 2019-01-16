using Nonsense.Application.Gateways.Repositories;
using Nonsense.Application.Users.Requests;
using Nonsense.Common.Utilities;
using System.Threading.Tasks;

namespace Nonsense.Application.Users {

    public class UserService : IUserService {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) {
            Guard.NotNull(userRepository, nameof(userRepository));

            _userRepository = userRepository;
        }

        public async Task<Responses.CreateUserResponse> CreateUser(CreateUserRequest request) {
            Guard.NotNull(request, nameof(request));

            var response = await _userRepository.Create(request.UserName, request.Email, request.Password);

            return new Responses.CreateUserResponse(response.Success, response.ErrorsList, response.Id);
        }

        public async Task<Responses.GetAllUsersResponse> GetAllUsers() {
            var response = await _userRepository.GetAllUsers();

            return new Responses.GetAllUsersResponse(response.Success, response.ErrorsList, response.Users);
        }

        public async Task<Responses.GetUserByIdResponse> GetUserById(string id) {
            Guard.NotNullOrEmpty(id, nameof(id));

            var response = await _userRepository.GetUserById(id);

            return new Responses.GetUserByIdResponse(response.Success, response.ErrorsList, response.User);
        }
    }
}
