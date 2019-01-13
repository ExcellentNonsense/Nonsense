using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nonsense.MvcApp.Features.Home {
    public class HomeController : Controller {
        public IActionResult Index() => View();
    }
}
