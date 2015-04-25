using System.Configuration;
using System.Web.Mvc;
using Ani.Core.Services;

namespace AniWebApp.Controllers
{
    public class TrafficController : CustomController
    {
        private readonly TrafficService _trafficService;

        public TrafficController() : this(null)
        {
        }

        public TrafficController(ApplicationRoleManager roleManager) : base(roleManager)
        {

            var apiKey = ConfigurationManager.AppSettings.Get("BingMapsKey");

            _trafficService = new TrafficService(this.Entities, apiKey);

        }

        /// <summary>
        /// The traffic root view
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [Route(@"Traffic")]
        public ActionResult Index()
        {
            var model = _trafficService.GetTrafficModel(this.GetUserZipCode());

            return View(model);
        }

        /// <summary>
        /// Gets the traffic incidents master view
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [Route(@"Traffic/Accidents")]
        public ActionResult Accidents()
        {
            return View(_trafficService.GetTrafficModel(this.GetUserZipCode()).Accidents);
        }

        /// <summary>
        /// Gets the construction master view
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [Route(@"Traffic/Construction")]
        public ActionResult Construction()
        {
            return View(_trafficService.GetTrafficModel(this.GetUserZipCode()).ConstructionEvents);
        }
    }
}