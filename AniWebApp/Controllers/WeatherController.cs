using System;
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
            var predictions = this.Entities.WeatherFrostPredictionsVsActualsSelect().ToList();
            return View(predictions);
        }

        [HttpGet]
        [Route(@"Weather/Frost/AddEntry")]
        [Authorize]
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

        [HttpPost]
        [Route(@"Weather/Frost/AddEntry")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AddFrostEntry_Push(AddFrostRecordModel entry)
        {
            int userID = GetUserId();

            var result = this.Entities.WeatherFrostResultsInsert(userID,
                entry.RainedOvernight,
                entry.ActualMinutes,
                entry.ZipCode,
                entry.RecordDate.Date);

            // On success, go back to the list page
            if (result >= 1)
            {
                return RedirectToAction("Frost");
            }

            // We're not quite valid. Redirect to the view
            return View(entry);
        }
    }
}