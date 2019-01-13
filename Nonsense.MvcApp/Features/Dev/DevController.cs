using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nonsense.MvcApp.Features.Dev {
    public class DevController : Controller {
        public IActionResult SiteMap() => View();
    }
}
