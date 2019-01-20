using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nonsense.Common.Utilities;
using Nonsense.Infrastructure.Identity;

namespace Nonsense.MvcApp.Infrastructure {

    public class CustomPasswordValidator : IPasswordValidator<ApplicationUser> {

        public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager,
            ApplicationUser user, string password) {

            Guard.NotNull(manager, nameof(manager));
            Guard.NotNull(user, nameof(user));
            Guard.NotNull(password, nameof(password));

            List<IdentityError> errors = new List<IdentityError>();

            if (password.Length > 128) {
                errors.Add(new IdentityError {
                    Code = "PasswordTooLong",
                    Description = "Password cannot be longer than 128 characters"
                });
            }

            if (!String.IsNullOrEmpty(user.UserName) && password.ToLower().Contains(user.UserName.ToLower())) {
                errors.Add(new IdentityError {
                    Code = "PasswordContainsUserName",
                    Description = "Password cannot contain username"
                });
            }

            var result = errors.Count == 0
                ? IdentityResult.Success
                : IdentityResult.Failed(errors.ToArray());

            return Task.FromResult(result);
        }
    }
}
