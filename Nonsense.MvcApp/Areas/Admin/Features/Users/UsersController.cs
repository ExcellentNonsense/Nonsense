using Microsoft.AspNetCore.Mvc;
using Nonsense.Application.Users.Interactors;
using Nonsense.Application.Users.Requests;
using Nonsense.Common.Utilities;
using Nonsense.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nonsense.MvcApp.Areas.Admin.Features.Users {
    [Area("Admin")]
    public class UsersController : Controller {

        private readonly ICreateUserInteractor _createUserInteractor;
        private readonly CreateUserPresenter _createUserPresenter;
        private readonly IGetAllUsersInteractor _getAllUsersInteractor;
        private readonly GetAllUsersPresenter _getAllUsersPresenter;
        private readonly IGetUserByIdInteractor _getUserByIdInteractor;
        private readonly GetUserByIdPresenter _getUserByIdPresenter;

        public UsersController(
            ICreateUserInteractor createUserInteractor, 
            CreateUserPresenter createUserPresenter,
            IGetAllUsersInteractor getAllUsersInteractor,
            GetAllUsersPresenter getAllUsersPresenter,
            IGetUserByIdInteractor getUserByIdInteractor,
            GetUserByIdPresenter getUserByIdPresenter) {

            Guard.NotNull(createUserInteractor, nameof(createUserInteractor));
            Guard.NotNull(createUserPresenter, nameof(createUserPresenter));
            Guard.NotNull(getAllUsersInteractor, nameof(getAllUsersInteractor));
            Guard.NotNull(getAllUsersPresenter, nameof(getAllUsersPresenter));
            Guard.NotNull(getUserByIdInteractor, nameof(getUserByIdInteractor));
            Guard.NotNull(getUserByIdPresenter, nameof(getUserByIdPresenter));

            _createUserInteractor = createUserInteractor;
            _createUserPresenter = createUserPresenter;
            _getAllUsersInteractor = getAllUsersInteractor;
            _getAllUsersPresenter = getAllUsersPresenter;
            _getUserByIdInteractor = getUserByIdInteractor;
            _getUserByIdPresenter = getUserByIdPresenter;
        }

        [HttpGet]
        public async Task<IActionResult> Index() {
            var users = await GetAllUsers();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model) {
            Guard.NotNull(model, nameof(model));

            IActionResult result;

            if (ModelState.IsValid) {
                await _createUserInteractor.Execute(
                    new CreateUserRequest(model.UserName, model.Email, model.Password),
                    _createUserPresenter);

                if (_createUserPresenter.Success) {
                    result = RedirectToAction("Index");
                }
                else {
                    AddErrors(_createUserPresenter.Errors);
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

            var user = await GetUserById(id);

            if (_getUserByIdPresenter.Success) {
                var model = new EditViewModel {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email
                };
                result = View(model);
            }
            else {
                AddErrors(_getUserByIdPresenter.Errors);
                result = View("Index", await GetAllUsers());
            }

            return result;
        }

        private async Task<User> GetUserById(string id) {
            await _getUserByIdInteractor.Execute(new GetUserByIdRequest(id), _getUserByIdPresenter);
            return _getUserByIdPresenter.User;
        }

        private async Task<IEnumerable<User>> GetAllUsers() {
            await _getAllUsersInteractor.Execute(_getAllUsersPresenter);
            return _getAllUsersPresenter.Users;
        }

        private void AddErrors(IEnumerable<string> errors) {
            foreach (var error in errors) {
                ModelState.AddModelError(String.Empty, error);
            }
        }
    }
}
