using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AniWebApp.Controllers
{
    public class ErrorController : CustomController
    {

        /// <summary>
        /// Handles a not found / 404 request by directing to a 404 page.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        public ActionResult NotFound()
        {
            return View();
        }
    }
}