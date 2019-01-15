using Microsoft.AspNetCore.Mvc;

namespace Nonsense.MvcApp.Features.Home {

    public class HomeController : Controller {

        [HttpGet]
        public IActionResult Index() => View();
    }
}
