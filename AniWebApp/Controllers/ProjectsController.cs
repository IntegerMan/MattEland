using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ani.Core.Services;
using AniWebApp.Models;

namespace AniWebApp.Controllers
{
    /// <summary>
    /// A Controller related to all project-related items
    /// </summary>
    public class ProjectsController : CustomController
    {
        private readonly AniService _aniService;

        public ProjectsController() : this(null)
        {
        }

        public ProjectsController(ApplicationRoleManager roleManager) : base(roleManager)
        {
            _aniService = new AniService(this.Entities);
        }

        /// <summary>
        /// Gets the main projects list
        /// </summary>
        /// <returns>A view for projects.</returns>
        [HttpGet]
        [Route(@"Projects")]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Gets the ANI project overview
        /// </summary>
        /// <returns>A view for the ANI project.</returns>
        [HttpGet]
        [Route(@"Projects/ANI")]
        public ActionResult ANI()
        {
            return View();
        }

        /// <summary>
        /// Gets the Service Area view for ANI
        /// </summary>
        /// <returns>A view for the service area for ANI.</returns>
        [HttpGet]
        [Route(@"Projects/ANI/ServiceArea")]
        public ActionResult AniServiceArea()
        {
            var serviceZips = _aniService.GetZipCodesInService();

            return View(serviceZips);
        }

        /// <summary>
        /// Gets the PiMFD project overview
        /// </summary>
        /// <returns>A view for the PiMFD project.</returns>
        [HttpGet]
        [Route(@"Projects/PiMFD")]
        public ActionResult PiMFD()
        {
            return View();
        }

        /// <summary>
        /// Gets the Temporal Protocols project overview
        /// </summary>
        /// <returns>A view for the Temporal Protocols project.</returns>
        [HttpGet]
        [Route(@"Projects/TemporalProtocols")]
        public ActionResult TemporalProtocols()
        {
            return View();
        }

    }
}