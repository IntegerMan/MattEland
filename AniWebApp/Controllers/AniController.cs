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
            var entities = new AniEntities();

            // TODO: Grab this from the current user's profile
            const int ZipCode = 43035;

            var predictions = entities.ActiveWeatherPredictionsSelect(ZipCode).ToList();
            return View(predictions);
        }

        /// <summary>
        /// Gets the traffic incidents master view
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [Route(@"Ani\Traffic")]
        public ActionResult Traffic()
        {
            var entities = new AniEntities();

            var incidents = entities.ActiveTrafficIncidentInfoSelect().ToList();
            return View(incidents);
        }
    }
}