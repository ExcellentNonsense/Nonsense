using Microsoft.AspNetCore.Mvc;

namespace Nonsense.MvcApp.Features.Profile {

    public class ProfileController : Controller {

        public IActionResult Index() => View();
    }
}
