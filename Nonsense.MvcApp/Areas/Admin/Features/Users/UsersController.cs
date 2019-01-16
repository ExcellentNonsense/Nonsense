using Microsoft.AspNetCore.Mvc;
using Nonsense.Application.Users;
using Nonsense.Application.Users.Requests;
using Nonsense.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nonsense.MvcApp.Areas.Admin.Features.Users {
    [Area("Admin")]
    public class UsersController : Controller {

        private readonly IUserService _userService;

        public UsersController(IUserService userService) {
            Guard.NotNull(userService, nameof(userService));

            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index() {
            var users = (await _userService.GetAllUsers()).Users;
            return View(users);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model) {
            Guard.NotNull(model, nameof(model));

            IActionResult result;

            if (ModelState.IsValid) {
                var response = await _userService.CreateUser(new CreateUserRequest(model.UserName, model.Email, model.Password));

                if (response.Success) {
                    result = RedirectToAction("Index");
                }
                else {
                    AddErrors(response.ErrorsList);
                    result = View(model);
                }
            }
            else {
                result = View(model);
            }

            return result;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id) {
            Guard.NotNullOrEmpty(id, nameof(id));

            IActionResult result;

            var response = await _userService.GetUserById(id);

            if (response.Success) {
                var model = new EditViewModel {
                    Id = response.User.Id,
                    UserName = response.User.UserName,
                    Email = response.User.Email
                };
                result = View(model);
            }
            else {
                AddErrors(response.ErrorsList);
                var users = (await _userService.GetAllUsers()).Users;
                result = View("Index", users);
            }

            return result;
        }

        private void AddErrors(IEnumerable<string> errors) {
            foreach (var error in errors) {
                ModelState.AddModelError(String.Empty, error);
            }
        }
    }
}
