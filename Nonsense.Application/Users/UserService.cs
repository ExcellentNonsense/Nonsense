using Nonsense.Application.Gateways.Repositories;
using Nonsense.Application.Users.Requests;
using Nonsense.Application.Users.Responses;
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
            var response = await _userRepository.GetAll();

            return new Responses.GetAllUsersResponse(response.Success, response.ErrorsList, response.Users);
        }

        public async Task<Responses.GetUserByIdResponse> GetUserById(string id) {
            Guard.NotNullOrEmpty(id, nameof(id));

            var response = await _userRepository.GetById(id);

            return new Responses.GetUserByIdResponse(response.Success, response.ErrorsList, response.User);
        }

        public async Task<EditUserResponse> EditUser(EditUserRequest request) {
            Guard.NotNull(request, nameof(request));

            var response = await _userRepository.Update(request.Id, request.UserName, request.Email, request.Password);

            return new EditUserResponse(response.Success, response.ErrorsList, response.Id);
        }

        public async Task<Responses.DeleteUserResponse> DeleteUser(string id) {
            Guard.NotNull(id, nameof(id));

            var response = await _userRepository.Delete(id);

            return new Responses.DeleteUserResponse(response.Success, response.ErrorsList, response.Id);
        }
    }
}
