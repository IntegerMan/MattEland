using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AniWebApp.Controllers
{
    /// <summary>
    /// A Controller related to all ANI-related items
    /// </summary>
    public class AniController : Controller
    {
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
        /// Gets data for the weather forecasts page.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [Route(@"Ani\Weather\Forecasts")]
        public ActionResult Forecasts()
        {
            var entities = new AniEntities();

            // TODO: Grab this from the current user's profile
            const int ZipCode = 43035;

            var predictions = entities.ActiveWeatherPredictionsSelect(ZipCode).ToList();
            return View(predictions);
        }

        /// <summary>
        /// Gets data for the weather forecasts page.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [Route(@"Ani\Weather\Frost")]
        public ActionResult Frost()
        {
            var entities = new AniEntities();

            var predictions = entities.FrostPredictionDataViews.ToList();
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