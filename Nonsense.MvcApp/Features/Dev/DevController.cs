using Microsoft.AspNetCore.Mvc;

namespace Nonsense.MvcApp.Features.Dev {

    public class DevController : Controller {

        public IActionResult SiteMap() => View();
    }
}
