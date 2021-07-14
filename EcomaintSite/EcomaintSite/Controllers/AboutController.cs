using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcomaintSite.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        [Authorize]
        public ActionResult Index() => View();
    }
}