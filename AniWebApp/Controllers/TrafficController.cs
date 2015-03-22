using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AniWebApp.Controllers
{
    public class TrafficController : CustomController
    {
        /// <summary>
        /// The traffic root view
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [Route(@"Traffic")]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Gets the traffic incidents master view
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [Route(@"Traffic/Accidents")]
        public ActionResult Accidents()
        {
            var incidents = Entities.ActiveTrafficIncidentInfoSelect(true, false).ToList();
            return View(incidents);
        }

        /// <summary>
        /// Gets the construction master view
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [Route(@"Traffic/Construction")]
        public ActionResult Construction()
        {
            var incidents = Entities.ActiveTrafficIncidentInfoSelect(false, true).ToList();
            return View(incidents);
        }
    }
}