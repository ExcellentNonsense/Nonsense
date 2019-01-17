using Microsoft.AspNetCore.Mvc;

namespace Nonsense.MvcApp.Features.Band {

    public class BandController : Controller {

        [HttpGet]
        public IActionResult Index() {
            var result = RedirectToAction(nameof(FlickrBand));

            if (User.Identity.IsAuthenticated) {
                result = RedirectToAction(nameof(MyBand));
            }

            return result;
        }

        [HttpGet]
        public IActionResult FlickrBand() => View();

        [HttpGet]
        public IActionResult MyBand() => View();
    }
}
