using System.ComponentModel.DataAnnotations;

namespace Nonsense.MvcApp.Areas.Admin.Features.Users {

    public class CreateViewModel {

        [Display(Name = "Логин")]
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [Required]
        public string Password { get; set; }
    }
}
