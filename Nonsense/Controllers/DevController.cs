using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nonsense.Controllers {
    public class DevController : Controller {
        public IActionResult SiteMap() => View();
    }
}
