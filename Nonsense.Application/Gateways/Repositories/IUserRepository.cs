using Nonsense.Application.Users.Dto;
using Nonsense.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nonsense.Application.Gateways.Repositories {

    public interface IUserRepository {

        Task<BoundaryResponse<string>> Create(string userName, string email, string password);
        Task<BoundaryResponse<User>> GetById(string id);
        Task<BoundaryResponse<IEnumerable<User>>> GetAll();
        Task<OperationResult> Update(string id, string userName, string email, string password);
        Task<OperationResult> Delete(string id);
    }
}
