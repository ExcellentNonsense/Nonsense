﻿using FluentValidation;

namespace Nonsense.MvcApp.Areas.Admin.Features.Accounts {

    public class EditViewModelValidator : AbstractValidator<EditViewModel> {

        public EditViewModelValidator() {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Введите логин")
                .Length(3, 25).WithMessage("Должен быть между 3 и 25 символами");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Введите email")
                .Length(3, 50).WithMessage("Должен быть между 3 и 50 символами")
                .EmailAddress().WithMessage("Введите действительный email");

            RuleFor(x => x.Password)
                .Length(8, 128).WithMessage("Должен быть между 8 и 128 символами");

            RuleFor(x => x.ConfirmPassword)
                .Length(8, 128).WithMessage("Должен быть между 8 и 128 символами")
                .Equal(x => x.Password).WithMessage("Должен совпадать с паролем введенным в поле выше");
        }
    }
}
