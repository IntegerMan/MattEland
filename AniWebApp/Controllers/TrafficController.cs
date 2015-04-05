using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ani.Core.Models.Traffic;
using Ani.Core.Services;
using AniWebApp.Models;

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
            _trafficService = new TrafficService(this.Entities);
        }

        /// <summary>
        /// The traffic root view
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [Route(@"Traffic")]
        public ActionResult Index()
        {
            var model = _trafficService.GetTrafficModel();

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
            return View(_trafficService.GetTrafficModel().Accidents);
        }

        /// <summary>
        /// Gets the construction master view
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [Route(@"Traffic/Construction")]
        public ActionResult Construction()
        {
            return View(_trafficService.GetTrafficModel().ConstructionEvents);
        }
    }
}