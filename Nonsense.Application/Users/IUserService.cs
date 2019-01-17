using Nonsense.Application.Users.Dto;
using Nonsense.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nonsense.Application.Users {

    public interface IUserService {

        Task<BoundaryResponse<string>> CreateUser(User user);
        Task<BoundaryResponse<User>> GetUserById(string id);
        Task<BoundaryResponse<IEnumerable<User>>> GetAllUsers();
        Task<OperationResult> EditUser(User user);
        Task<OperationResult> DeleteUser(string id);
    }
}
