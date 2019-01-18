using Nonsense.Application.Users.Dto;
using Nonsense.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nonsense.Application.Gateways.Repositories {

    public interface IAccountRepository {

        Task<BoundaryResponse<string>> Create(string userName, string email, string password);
        Task<BoundaryResponse<Account>> GetById(string id);
        Task<BoundaryResponse<IEnumerable<Account>>> GetAll();
        Task<OperationResult> Update(Account account);
        Task<OperationResult> Delete(string id);
    }
}
