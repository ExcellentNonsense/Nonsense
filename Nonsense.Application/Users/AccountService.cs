using Nonsense.Application.Gateways.Repositories;
using Nonsense.Application.Users.Dto;
using Nonsense.Common;
using Nonsense.Common.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nonsense.Application.Users {

    public class AccountService : IAccountService {

        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository) {
            Guard.NotNull(accountRepository, nameof(accountRepository));

            _accountRepository = accountRepository;
        }

        public async Task<BoundaryResponse<string>> CreateAccount(Account account) {
            Guard.NotNull(account, nameof(account));

            var response = await _accountRepository.Create(account.UserName, account.Email, account.Password);

            return new BoundaryResponse<string>(response.Success, response.ErrorsList, response.Data);
        }

        public async Task<BoundaryResponse<Account>> GetAccountById(string id) {
            Guard.NotNullOrEmpty(id, nameof(id));

            var response = await _accountRepository.GetById(id);

            return new BoundaryResponse<Account>(response.Success, response.ErrorsList, response.Data);
        }

        public async Task<BoundaryResponse<IEnumerable<Account>>> GetAllAccounts() {
            var response = await _accountRepository.GetAll();

            return new BoundaryResponse<IEnumerable<Account>>(response.Success, response.ErrorsList, response.Data);
        }

        public async Task<OperationResult> EditAccount(Account account) {
            Guard.NotNull(account, nameof(account));

            var response = await _accountRepository.Update(account);

            return new OperationResult(response.Success, response.ErrorsList);
        }

        public async Task<OperationResult> DeleteAccount(string id) {
            Guard.NotNull(id, nameof(id));

            var response = await _accountRepository.Delete(id);

            return new OperationResult(response.Success, response.ErrorsList);
        }
    }
}
