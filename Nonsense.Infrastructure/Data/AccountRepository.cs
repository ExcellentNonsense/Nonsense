using Microsoft.AspNetCore.Identity;
using Nonsense.Application;
using Nonsense.Application.Gateways.Repositories;
using Nonsense.Application.Users.Dto;
using Nonsense.Common;
using Nonsense.Common.Utilities;
using Nonsense.Infrastructure.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nonsense.Infrastructure.Data {

    public sealed class AccountRepository : IAccountRepository {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserValidator<ApplicationUser> _userValidator;
        private readonly IPasswordValidator<ApplicationUser> _passwordValidator;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

        public AccountRepository(
            UserManager<ApplicationUser> userManager,
            IUserValidator<ApplicationUser> userValidator,
            IPasswordValidator<ApplicationUser> passwordValidator,
            IPasswordHasher<ApplicationUser> passwordHasher) {

            Guard.NotNull(userManager, nameof(userManager));
            Guard.NotNull(userValidator, nameof(userValidator));
            Guard.NotNull(passwordValidator, nameof(passwordValidator));
            Guard.NotNull(passwordHasher, nameof(passwordHasher));

            _userManager = userManager;
            _userValidator = userValidator;
            _passwordValidator = passwordValidator;
            _passwordHasher = passwordHasher;
        }

        public async Task<BoundaryResponse<string>> Create(string userName, string email, string password) {

            var user = new ApplicationUser {
                UserName = userName,
                Email = email
            };

            var creatingResult = await _userManager.CreateAsync(user, password);

            return new BoundaryResponse<string>(creatingResult.Succeeded, 
                creatingResult.Succeeded 
                    ? null 
                    : creatingResult.Errors.Select(e => e.Description),
                creatingResult.Succeeded
                    ? user.Id
                    : null);
        }

        public async Task<BoundaryResponse<Account>> GetById(string id) {
            Guard.NotNullOrEmpty(id, nameof(id));

            var user = await _userManager.FindByIdAsync(id);

            var userFound = (user != null);

            return new BoundaryResponse<Account>(userFound,
                userFound
                    ? null
                    : new string[] { Errors.UserNotFound },
                userFound
                    ? new Account {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email
                    }
                    : null);
        }

        public async Task<BoundaryResponse<IEnumerable<Account>>> GetAll() {
            return await Task.FromResult(new BoundaryResponse<IEnumerable<Account>>(true, null,
                _userManager.Users.Select(u => new Account {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email
                })));
        }

        public async Task<OperationResult> Update(Account account) {
            Guard.NotNull(account, nameof(account));

            var result = new OperationResult { Success = false };

            var user = await _userManager.FindByIdAsync(account.Id);

            if (user != null) {
                user.UserName = account.UserName;
                user.Email = account.Email;
                var userValidationResult = await _userValidator.ValidateAsync(_userManager, user);

                if (!userValidationResult.Succeeded) {
                    result.AddError(userValidationResult.Errors.Select(e => e.Description));
                }

                IdentityResult passwordValidationResult = null;

                if (!string.IsNullOrEmpty(account.Password)) {
                    passwordValidationResult = await _passwordValidator.ValidateAsync(_userManager, user, account.Password);

                    if (passwordValidationResult.Succeeded) {
                        user.PasswordHash = _passwordHasher.HashPassword(user, account.Password);
                    }
                    else {
                        result.AddError(passwordValidationResult.Errors.Select(e => e.Description));
                    }
                }

                if (userValidationResult.Succeeded
                    && (passwordValidationResult == null || passwordValidationResult.Succeeded)) {

                    var updatingResult = await _userManager.UpdateAsync(user);

                    if (updatingResult.Succeeded) {
                        result.Success = true;
                    }
                    else {
                        result.AddError(updatingResult.Errors.Select(e => e.Description));
                    }
                }
            }
            else {
                result.AddError(Errors.UserNotFound);
            }

            return result;
        }

        public async Task<OperationResult> Delete(string id) {
            var result = new OperationResult();

            var user = await _userManager.FindByIdAsync(id);

            if (user != null) {
                var deletingResult = await _userManager.DeleteAsync(user);

                if (!deletingResult.Succeeded) {
                    result.Success = false;
                    result.AddError(deletingResult.Errors.Select(e => e.Description));
                }
            }
            else {
                result.Success = false;
                result.AddError(Errors.UserNotFound);
            }

            return result;
        }

        private static class Errors {
            public static string UserNotFound => "User not found";
        }
    }
}
