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
        public AppsController() : this(null)
        {
        }

        public AppsController(ApplicationRoleManager roleManager) : base(roleManager)
        {
        }

        /// <summary>
        /// Presents the main app list view.
        /// </summary>
        /// <returns>The main app list view.</returns>
        [HttpGet]
        [Route("Apps")]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Presents the main Seeking God page view.
        /// </summary>
        /// <returns>The Seeking God app view.</returns>
        [HttpGet]
        [Route(@"Apps/SeekingGod")]
        public ActionResult SeekingGod()
        {
            return View();
        }

        /// <summary>
        /// Presents the Seeking Jesus page view.
        /// </summary>
        /// <returns>The Seeking Jesus app view.</returns>
        [HttpGet]
        [Route(@"Apps/SeekingJesus")]
        public ActionResult SeekingJesus()
        {
            return View();
        }

        /// <summary>
        /// Presents the Life Lights page view.
        /// </summary>
        /// <returns>The Life Lights app view.</returns>
        [HttpGet]
        [Route(@"Apps/LifeLights")]
        public ActionResult LifeLights()
        {
            return View();
        }

    }
}