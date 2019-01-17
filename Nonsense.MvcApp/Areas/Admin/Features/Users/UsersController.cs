using Microsoft.AspNetCore.Mvc;
using Nonsense.Application.Users;
using Nonsense.Application.Users.Dto;
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
            var users = (await _userService.GetAllUsers()).Data;
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Display(string id) {
            Guard.NotNullOrEmpty(id, nameof(id));

            IActionResult result;

            var response = await _userService.GetUserById(id);

            if (response.Success) {
                var user = response.Data;

                var model = new DisplayViewModel {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email
                };

                result = View(model);
            }
            else {
                AddErrors(response.ErrorsList);
                var users = (await _userService.GetAllUsers()).Data;
                result = View("Index", users);
            }

            return result;
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model) {
            Guard.NotNull(model, nameof(model));

            IActionResult result;

            if (ModelState.IsValid) {
                var response = await _userService.CreateUser(new User {
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = model.Password
                });

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
                var user = response.Data;

                var model = new EditViewModel {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email
                };

                result = View(model);
            }
            else {
                AddErrors(response.ErrorsList);
                var users = (await _userService.GetAllUsers()).Data;
                result = View("Index", users);
            }

            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model) {
            Guard.NotNull(model, nameof(model));

            IActionResult result;

            if (ModelState.IsValid) {
                var response = await _userService.EditUser(new User {
                    Id = model.Id,
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = model.Password
                });

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

        [HttpPost]
        public async Task<IActionResult> Delete(string id) {
            Guard.NotNullOrEmpty(id, nameof(id));

            IActionResult result;

            var response = await _userService.DeleteUser(id);

            if (response.Success) {
                result = RedirectToAction("Index");
            }
            else {
                AddErrors(response.ErrorsList);
                var users = (await _userService.GetAllUsers()).Data;
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
