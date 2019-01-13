using Microsoft.AspNetCore.Mvc;

namespace Nonsense.MvcApp.Features.Feedback {

    public class FeedbackController : Controller {

        public IActionResult Index() => View();
    }
}
