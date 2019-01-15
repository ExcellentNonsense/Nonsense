using System.Threading.Tasks;

namespace Nonsense.Application.Gateways.Repositories {

    public interface IUserRepository {

        Task<GetUserByIdResponse> GetUserById(string id);
        Task<GetAllUsersResponse> GetAllUsers();
        Task<CreateUserResponse> Create(string userName, string email, string password);
    }
}
