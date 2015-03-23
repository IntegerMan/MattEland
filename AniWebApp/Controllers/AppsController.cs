using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AniWebApp.Controllers
{
    /// <summary>
    /// MVC Controller for Windows Phone application info pages
    /// </summary>
    public class AppsController : CustomController
    {
        [HttpGet]
        [Route("Apps")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route(@"Apps/SeekingGod")]
        public ActionResult SeekingGod()
        {
            return View();
        }

    }
}