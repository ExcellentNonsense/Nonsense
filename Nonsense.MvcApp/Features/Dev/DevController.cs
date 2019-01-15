using Microsoft.AspNetCore.Mvc;

namespace Nonsense.MvcApp.Features.Dev {

    public class DevController : Controller {

        [HttpGet]
        public IActionResult SiteMap() => View();
    }
}
