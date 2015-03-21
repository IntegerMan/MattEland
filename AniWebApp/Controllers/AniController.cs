using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AniWebApp.Models;

namespace AniWebApp.Controllers
{
    /// <summary>
    /// A Controller related to all ANI-related items
    /// </summary>
    public class AniController : Controller
    {
        private readonly AniEntities _entities = new AniEntities();

        /// <summary>
        /// Gets the main ANI view
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [Route(@"Ani")]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Gets the traffic incidents master view
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [Route(@"Ani/Traffic")]
        public ActionResult Traffic()
        {
            var incidents = _entities.ActiveTrafficIncidentInfoSelect().ToList();
            return View(incidents);
        }

    }
}