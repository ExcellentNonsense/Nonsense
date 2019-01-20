

using System.ComponentModel.DataAnnotations;

namespace Nonsense.MvcApp.Areas.Admin.Features.Accounts {

    public class EditViewModel {

        public string Id { get; set; }

        [Required(ErrorMessage = "Введите логин")]
        [StringLength(25, ErrorMessage = "Должен быть между 3 и 25 символами", MinimumLength = 3)]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Введите email")]
        [StringLength(50, ErrorMessage = "Должен быть между 3 и 50 символами", MinimumLength = 3)]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(128, ErrorMessage = "Должен быть между 8 и 128 символами", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [StringLength(128, ErrorMessage = "Должен быть между 8 и 128 символами", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль должен совпадать с паролем из поля выше")]
        [Display(Name = "Подтвердите пароль")]
        public string ConfirmPassword { get; set; }
    }
}
