using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nonsense.Web.Features.Home {
    public class HomeController : Controller {
        public IActionResult Index() => View();
    }
}
