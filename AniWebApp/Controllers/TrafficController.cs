using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ani.Core.Models.Traffic;
using AniWebApp.Models;

namespace AniWebApp.Controllers
{
    public class TrafficController : CustomController
    {

        public TrafficController() : this(null)
        {
        }

        public TrafficController(ApplicationRoleManager roleManager) : base(roleManager)
        {
        }

        /// <summary>
        /// The traffic root view
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [Route(@"Traffic")]
        public ActionResult Index()
        {
            var model = GetTrafficModel();

            return View(model);
        }

        /// <summary>
        /// Gets the model for traffic information.
        /// </summary>
        /// <returns>TrafficModel.</returns>
        private TrafficModel GetTrafficModel()
        {
            var model = new TrafficModel
            {
                Accidents = Entities.ActiveTrafficIncidentInfoSelect(true, false).ToList(),
                ConstructionEvents = Entities.ActiveTrafficIncidentInfoSelect(false, true).ToList()
            };

            return model;
        }

        /// <summary>
        /// Gets the traffic incidents master view
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [Route(@"Traffic/Accidents")]
        public ActionResult Accidents()
        {
            return View(GetTrafficModel().Accidents);
        }

        /// <summary>
        /// Gets the construction master view
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [Route(@"Traffic/Construction")]
        public ActionResult Construction()
        {
            return View(GetTrafficModel().ConstructionEvents);
        }
    }
}