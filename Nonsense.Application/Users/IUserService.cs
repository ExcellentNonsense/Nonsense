using Nonsense.Application.Users.Requests;
using Nonsense.Application.Users.Responses;
using System.Threading.Tasks;

namespace Nonsense.Application.Users {

    public interface IUserService {

        Task<CreateUserResponse> CreateUser(CreateUserRequest request);
        Task<GetAllUsersResponse> GetAllUsers();
        Task<GetUserByIdResponse> GetUserById(string id);
    }
}
