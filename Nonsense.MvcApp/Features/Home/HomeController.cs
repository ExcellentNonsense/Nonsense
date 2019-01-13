using Microsoft.AspNetCore.Mvc;

namespace Nonsense.MvcApp.Features.Home {

    public class HomeController : Controller {

        public IActionResult Index() => View();
    }
}
