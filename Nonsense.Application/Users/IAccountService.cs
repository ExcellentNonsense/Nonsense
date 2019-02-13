using Nonsense.Application.Users.Dto;
using Nonsense.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nonsense.Application.Users {

    public interface IAccountService {

        Task<BoundaryResponse<string>> CreateAccount(Account account);
        Task<BoundaryResponse<Account>> GetAccountById(string id);
        Task<BoundaryResponse<IEnumerable<Account>>> GetAccounts(int skip, int take);
        Task<OperationResult> EditAccount(Account account);
        Task<OperationResult> DeleteAccount(string id);
        Task<int> AccountsCount();
    }
}
