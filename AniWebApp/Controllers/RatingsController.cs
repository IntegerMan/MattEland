﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AniWebApp.Controllers
{
    /// <summary>
    /// An MVC Controller governing ratings-related activities
    /// </summary>
    public class RatingsController : CustomController
    {

        public RatingsController() : this(null)
        {
        }

        public RatingsController(ApplicationRoleManager roleManager) : base(roleManager)
        {
        }

        /// <summary>
        /// Goes to the main index page for the ratings application.
        /// </summary>
        /// <returns>The view for the main ratings application.</returns>
        [HttpGet]
        [Route("Ratings")]
        public ActionResult Index()
        {
            return View();
        }
    }
}