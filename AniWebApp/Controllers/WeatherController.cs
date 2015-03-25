using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AniWebApp.Models;

namespace AniWebApp.Controllers
{
    /// <summary>
    /// The MVC weather controller
    /// </summary>
    [Route("Weather")]
    public class WeatherController : CustomController
    {


        [HttpGet]
        [Route(@"Weather")]
        public ActionResult Index()
        {
            return AreaWeather();
        }

        /// <summary>
        /// Gets the weather for a specific zip code.
        /// </summary>
        /// <param name="zipCode">The zip code</param>
        /// <returns>Redirects to the forecast view for this zip code</returns>
        [HttpGet]
        [Route(@"Weather/{zipCode}")]
        public ActionResult AreaWeather(int zipCode = 0)
        {
            if (zipCode <= 0)
            {
                zipCode = GetUserZipCode();
            }

            return RedirectToAction("Forecasts", "Weather", new { zipCode = zipCode });
        }

        /// <summary>
        /// Gets data for the weather forecasts page.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [Route(@"Weather/{zipCode}/Forecasts")]
        public ActionResult Forecasts(int zipCode = 0)
        {
            if (zipCode <= 0)
            {
                zipCode = GetUserZipCode();
            }

            var predictions = this.Entities.ActiveWeatherPredictionsSelect(zipCode, DateTime.Today).ToList();
            return View(predictions);
        }

        /// <summary>
        /// Gets data for the weather forecasts page.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [Route(@"Weather/Frost")]
        public ActionResult Frost()
        {
            List<WeatherFrostDataSelect_Result> predictions = this.Entities.WeatherFrostDataSelect().ToList();
            return View(predictions);
        }

        /// <summary>
        /// Shows a view allowing the user to enter a frost entry to the system.
        /// </summary>
        /// <returns>The view for entering a frost entry</returns>
        [HttpGet]
        [Route(@"Weather/Frost/AddEntry")]
        [Authorize(Users = "Batman, ani")]
        public ActionResult AddFrostEntry()
        {
            var model = new AddFrostRecordModel
            {
                RecordDate = DateTime.Today,
                ZipCode = GetUserZipCode(),
                ActualMinutes = 0.0
            };

            return View(model);
        }

        /// <summary>
        /// Adds the frost entry to the database.
        /// </summary>
        /// <param name="entry">The frost entry.</param>
        /// <returns>A view showing the new item or a view to re-enter the item.</returns>
        [HttpPost]
        [Route(@"Weather/Frost/AddEntry")]
        [Authorize(Users = "Batman, ani")]
        [ValidateAntiForgeryToken]
        public ActionResult AddFrostEntryPost(AddFrostRecordModel entry)
        {
            if (ModelState.IsValid)
            {
                var userId = GetUserId();

                var result = this.Entities.WeatherFrostResultsInsert(userId,
                    entry.RainedOvernight,
                    entry.SnowedOvernight,
                    entry.ActualMinutes,
                    entry.ZipCode,
                    entry.RecordDate.Date);

                // On success, go back to the list page
                if (result >= 1)
                {
                    return RedirectToAction("Frost");
                }
            }

            // We're not quite valid. Redirect to the view
            return View(entry);
        }
    }
}