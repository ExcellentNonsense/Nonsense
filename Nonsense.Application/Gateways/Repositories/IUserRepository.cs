using System.Threading.Tasks;

namespace Nonsense.Application.Gateways.Repositories {

    public interface IUserRepository {

        Task<GetUserByIdResponse> GetById(string id);
        Task<GetAllUsersResponse> GetAll();
        Task<CreateUserResponse> Create(string userName, string email, string password);
        Task<UpdateUserResponse> Update(string id, string userName, string email, string password);
        Task<DeleteUserResponse> Delete(string id);
    }
}
