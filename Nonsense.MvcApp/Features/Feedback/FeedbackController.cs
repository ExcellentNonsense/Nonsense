using Microsoft.AspNetCore.Mvc;

namespace Nonsense.MvcApp.Features.Feedback {

    public class FeedbackController : Controller {

        [HttpGet]
        public IActionResult Index() => View();
    }
}
