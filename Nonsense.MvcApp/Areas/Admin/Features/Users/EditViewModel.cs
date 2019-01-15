

using System.ComponentModel.DataAnnotations;

namespace Nonsense.MvcApp.Areas.Admin.Features.Users {

    public class EditViewModel {

        public string Id { get; set; }

        [Display(Name = "Логин")]
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
