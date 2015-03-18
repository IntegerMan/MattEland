using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AniWebApp.Controllers
{
    public class AniController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Forecasts()
        {
            return View();
        }

        /// <summary>
        /// Gets the traffic incidents master view
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [Route(@"Ani\Traffic")]
        public ActionResult Traffic()
        {

            var utcNow = DateTime.UtcNow;

            var entities = new AniEntities();

            List<ActiveTrafficIncidentInfoSelect_Result> incidents = entities.ActiveTrafficIncidentInfoSelect().ToList();

            return View(incidents);
        }
    }
}