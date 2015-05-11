using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using Ani.Core;
using Ani.Core.Helpers;
using Ani.Core.Models.Weather;
using Ani.Core.Services;

namespace AniWebApp.Controllers
{
    /// <summary>
    /// The MVC weather controller
    /// </summary>
    [Route("Weather")]
    public class WeatherController : CustomController
    {
        private readonly WeatherService _weatherService;

        public WeatherController() : this(null)
        {
        }

        public WeatherController(ApplicationRoleManager roleManager) : base(roleManager)
        {
            _weatherService = new WeatherService(this.Entities);
        }

        [HttpGet]
        [Route(@"Weather")]
        public ActionResult Home()
        {
            var zipCode = GetUserZipCode();

            return RedirectToAction("Index", "Weather", new {zipCode=zipCode});
        }

        /// <summary>
        /// Gets the weather for a specific zip code.
        /// </summary>
        /// <param name="zipCode">The zip code</param>
        /// <returns>Redirects to the forecast view for this zip code</returns>
        [HttpGet]
        [Route(@"Weather/{zipCode}")]
        public ActionResult Index(int zipCode = 0)
        {
            if (zipCode <= 0)
            {
                zipCode = GetUserZipCode();
            }

            var model = _weatherService.GetWeatherModel(zipCode);

            return View(model);
        }

        /// <summary>
        /// Gets the historical weather for a specific zip code.
        /// </summary>
        /// <param name="zipCode">The zip code</param>
        /// <returns>A view of weather history for this zip code</returns>
        [HttpGet]
        [Route(@"Weather/{zipCode}/History")]
        public ActionResult History(int zipCode = 0)
        {
            if (zipCode <= 0)
            {
                zipCode = GetUserZipCode();
            }

            var model = _weatherService.GetWeatherHistory(zipCode);

            return View(model);
        }

        /// <summary>
        /// Gets data for the weather forecasts page.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [Route(@"Weather/Frost")]
        public ActionResult Frost()
        {
            var model = _weatherService.GetFrostEntryData(this.User);

            return View(model);
        }

        /// <summary>
        /// Shows a view allowing the user to enter a frost entry to the system.
        /// </summary>
        /// <returns>The view for entering a frost entry</returns>
        [HttpGet]
        [Route(@"Weather/Frost/AddEntry")]
        [Authorize(Roles="Admin")]
        public ActionResult AddFrostEntry()
        {
            var model = WeatherService.BuildAddFrostEntryModel(this.GetUserZipCode());

            return View(model);
        }

        /// <summary>
        /// Adds the frost entry to the database.
        /// </summary>
        /// <param name="entry">The frost entry.</param>
        /// <returns>A view showing the new item or a view to re-enter the item.</returns>
        [HttpPost]
        [Route(@"Weather/Frost/AddEntry")]
        [Authorize(Roles="Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult AddFrostEntryPost(AddFrostRecordModel entry)
        {
            if (ModelState.IsValid)
            {
                var user = this.GetUserEntity();

                _weatherService.AddFrostEntry(user, entry);

                ShowSuccess("Frost Entry Added");

                return RedirectToAction("Frost");
            }

            // We're not quite valid. Redirect to the view
            return View(entry);
        }
    }
}