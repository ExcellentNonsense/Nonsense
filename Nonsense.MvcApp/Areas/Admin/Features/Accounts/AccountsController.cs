﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nonsense.Application.Users;
using Nonsense.Application.Users.Dto;
using Nonsense.Common.Utilities;
using Nonsense.MvcApp.Infrastructure;
using Nonsense.MvcApp.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nonsense.MvcApp.Areas.Admin.Features.Accounts {
    [Area("Admin")]
    public class AccountsController : Controller {

        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountsController(IAccountService accountService, IMapper mapper) {
            Guard.NotNull(accountService, nameof(accountService));
            Guard.NotNull(mapper, nameof(mapper));

            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1) {
            var accounts = await GetAccounts(page);
            return View(accounts);
        }

        [HttpGet]
        public async Task<IActionResult> Display(string id) {
            Guard.NotNullOrEmpty(id, nameof(id));

            IActionResult result;

            var response = await _accountService.GetAccountById(id);

            if (response.Success) {
                var account = response.Data;
                var model = _mapper.Map<DisplayViewModel>(account);
                result = View(model);
            }
            else {
                AddErrors(response.ErrorsList);
                var accounts = await GetAccounts();
                result = View("Index", accounts);
            }

            return result;
        }

        [HttpGet]
        public IActionResult Create(string id) => View();

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Create(CreateViewModel model) {
            IActionResult result;

            var account = _mapper.Map<Account>(model);
            var response = await _accountService.CreateAccount(account);

            if (response.Success) {
                result = RedirectToAction("Index");
            }
            else {
                AddErrors(response.ErrorsList);
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
                var model = _mapper.Map<EditViewModel>(account);
                result = View(model);
            }
            else {
                AddErrors(response.ErrorsList);
                var accounts = await GetAccounts();
                result = View("Index", accounts);
            }

            return result;
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Edit(EditViewModel model) {
            IActionResult result;

            var account = _mapper.Map<Account>(model);
            var response = await _accountService.EditAccount(account);

            if (response.Success) {
                result = RedirectToAction("Index");
            }
            else {
                AddErrors(response.ErrorsList);
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
                var accounts = await GetAccounts();
                result = View("Index", accounts);
            }

            return result;
        }

        private async Task<PaginatedCollection<Account>> GetAccounts(int page = 1) {
            var pageIndex = page - 1;
            int accountsPerPage = 20;

            var accountsCount = await _accountService.AccountsCount();
            var accounts = (await _accountService.GetAccounts(pageIndex * accountsPerPage, accountsPerPage)).Data;
            return new PaginatedCollection<Account>(accounts, page, accountsPerPage, accountsCount);
        }

        private void AddErrors(IEnumerable<string> errors) {
            foreach (var error in errors ?? Enumerable.Empty<string>()) {
                ModelState.AddModelError(String.Empty, error);
            }
        }
    }
}
