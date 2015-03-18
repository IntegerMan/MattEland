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

        [HttpGet]
        [Route(@"Ani\Traffic")]
        public ActionResult Traffic()
        {

            var utcNow = DateTime.UtcNow;

            var entities = new AniEntities();

            var eligibleIncidents = entities.TrafficIncidents.Where(i => !i.TI_EndTimeUTC.HasValue || i.TI_EndTimeUTC > utcNow);
            var incidents = eligibleIncidents.OrderBy(i => i.TI_StartTimeUTC).ThenBy(i => i.TI_EndTimeUTC).ToList();

            return View(incidents);
        }
    }
}