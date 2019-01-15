using Microsoft.AspNetCore.Mvc;

namespace Nonsense.MvcApp.Features.Profile {

    public class ProfileController : Controller {

        [HttpGet]
        public IActionResult Index() => View();
    }
}
