

using System.ComponentModel.DataAnnotations;

namespace Nonsense.MvcApp.Areas.Admin.Features.Accounts {

    public class EditViewModel {

        public string Id { get; set; }

        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        public string ConfirmPassword { get; set; }
    }
}
