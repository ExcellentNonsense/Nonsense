using Microsoft.AspNetCore.Identity;
using Nonsense.Application.Gateways.Repositories;
using Nonsense.Application.Users.Dto;
using Nonsense.Common;
using Nonsense.Common.Utilities;
using Nonsense.Infrastructure.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Nonsense.Infrastructure.Data {

    public sealed class UserRepository : IUserRepository {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserValidator<ApplicationUser> _userValidator;
        private readonly IPasswordValidator<ApplicationUser> _passwordValidator;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

        public UserRepository(
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

        public async Task<GetUserByIdResponse> GetById(string id) {
            Guard.NotNullOrEmpty(id, nameof(id));

            var user = await _userManager.FindByIdAsync(id);

            var userFound = (user != null);

            return new GetUserByIdResponse(userFound,
                userFound
                    ? null
                    : new string[] { Errors.UserNotFound },
                userFound
                    ? new User {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email }
                    : null);
        }

        public async Task<GetAllUsersResponse> GetAll() {
            return await Task.FromResult(new GetAllUsersResponse(true, null,
                _userManager.Users.Select(u => new User {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email
                })));
        }

        public async Task<CreateUserResponse> Create(string userName, string email, string password) {

            var user = new ApplicationUser {
                UserName = userName,
                Email = email
            };

            var creatingResult = await _userManager.CreateAsync(user, password);

            return new CreateUserResponse(creatingResult.Succeeded, 
                creatingResult.Succeeded 
                    ? null 
                    : creatingResult.Errors.Select(e => e.Description),
                creatingResult.Succeeded
                    ? user.Id
                    : null);
        }

        public async Task<UpdateUserResponse> Update(string id, string userName, string email, string password) {
            Guard.NotNullOrEmpty(id, nameof(id));

            var user = await _userManager.FindByIdAsync(id);

            var result = new OperationResult { Success = false };

            if (user != null) {
                user.Email = email;
                user.UserName = userName;
                var emailValidationResult = await _userValidator.ValidateAsync(_userManager, user);

                if (!emailValidationResult.Succeeded) {
                    result.AddError(emailValidationResult.Errors.Select(e => e.Description));
                }

                IdentityResult passwordValidationResult = null;

                if (!string.IsNullOrEmpty(password)) {
                    passwordValidationResult = await _passwordValidator.ValidateAsync(_userManager, user, password);

                    if (passwordValidationResult.Succeeded) {
                        user.PasswordHash = _passwordHasher.HashPassword(user, password);
                    }
                    else {
                        result.AddError(passwordValidationResult.Errors.Select(e => e.Description));
                    }
                }

                if (emailValidationResult.Succeeded
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

            return new UpdateUserResponse(result.Success, result.ErrorsList, user.Id);
        }

        public async Task<DeleteUserResponse> Delete(string id) {
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

            return new DeleteUserResponse(result.Success, result.ErrorsList, user?.Id);
        }

        private static class Errors {
            public static string UserNotFound => "User not found";
        }
    }
}
