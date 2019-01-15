using Microsoft.AspNetCore.Identity;
using Nonsense.Application.Gateways.Repositories;
using Nonsense.Common.Utilities;
using Nonsense.Domain.Entities;
using Nonsense.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nonsense.Infrastructure.Data {

    public sealed class UserRepository : IUserRepository {

        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager) {
            Guard.NotNull(userManager, nameof(userManager));

            _userManager = userManager;
        }

        public async Task<GetUserByIdResponse> GetUserById(string id) {
            var user = await _userManager.FindByIdAsync(id);

            return new GetUserByIdResponse(user != null,
                user != null
                    ? null
                    : new string[] { "User not found" },
                new User(user.Id, user.UserName, user.Email));
        }

        public async Task<GetAllUsersResponse> GetAllUsers() {
            return await Task.FromResult(new GetAllUsersResponse(true, null,
                _userManager.Users.Select(u => new User(u.Id, u.UserName, u.Email))));
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
                user.Id);
        }
    }
}
