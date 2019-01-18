using Microsoft.AspNetCore.Mvc;
using Nonsense.Application.Users;
using Nonsense.Application.Users.Dto;
using Nonsense.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nonsense.MvcApp.Areas.Admin.Features.Accounts {
    [Area("Admin")]
    public class AccountsController : Controller {

        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService) {
            Guard.NotNull(accountService, nameof(accountService));

            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> Index() {
            var accounts = (await _accountService.GetAllAccounts()).Data;
            return View(accounts);
        }

        [HttpGet]
        public async Task<IActionResult> Display(string id) {
            Guard.NotNullOrEmpty(id, nameof(id));

            IActionResult result;

            var response = await _accountService.GetAccountById(id);

            if (response.Success) {
                var account = response.Data;

                var model = new DisplayViewModel {
                    Id = account.Id,
                    UserName = account.UserName,
                    Email = account.Email
                };

                result = View(model);
            }
            else {
                AddErrors(response.ErrorsList);
                var accounts = (await _accountService.GetAllAccounts()).Data;
                result = View("Index", accounts);
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
                var response = await _accountService.CreateAccount(new Account {
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

            var response = await _accountService.GetAccountById(id);

            if (response.Success) {
                var account = response.Data;

                var model = new EditViewModel {
                    Id = account.Id,
                    UserName = account.UserName,
                    Email = account.Email
                };

                result = View(model);
            }
            else {
                AddErrors(response.ErrorsList);
                var accounts = (await _accountService.GetAllAccounts()).Data;
                result = View("Index", accounts);
            }

            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model) {
            Guard.NotNull(model, nameof(model));

            IActionResult result;

            if (ModelState.IsValid) {
                var response = await _accountService.EditAccount(new Account {
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

            var response = await _accountService.DeleteAccount(id);

            if (response.Success) {
                result = RedirectToAction("Index");
            }
            else {
                AddErrors(response.ErrorsList);
                var accounts = (await _accountService.GetAllAccounts()).Data;
                result = View("Index", accounts);
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
