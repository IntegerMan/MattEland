using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AniWebApp.Models;
using AniWebApp.Models.Weather;

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

            var model = new WeatherHomeModel { ZipCode = zipCode };

            var latestRecord = this.Entities.LatestWeatherEntrySelect(zipCode).FirstOrDefault();
            if (latestRecord != null)
            {
                model.Conditions.Temperature = latestRecord.Temperature;
                model.Conditions.Sunrise = latestRecord.Sunrise;
                model.Conditions.Sunset = latestRecord.Sunset;
                model.Conditions.Description = latestRecord.Description;
                model.Conditions.Pressure = latestRecord.Pressure;
                model.Conditions.Humidity = latestRecord.Humidity;
                model.Conditions.Rising = latestRecord.Rising;
                model.Conditions.Visibility = latestRecord.Visibility;
                model.Conditions.WindChill = latestRecord.WindChill;
                model.Conditions.WindDirection = latestRecord.WindDirection;
                model.Conditions.WindSpeed = latestRecord.WindSpeed;
                model.Conditions.WeatherCode = latestRecord.WeatherCode;
                model.Conditions.WeatherCodeName = latestRecord.WeatherCodeName;
                model.Conditions.SeverityId = latestRecord.SeverityID;
                model.Conditions.IconClass = latestRecord.IconClass;
            }
            else
            {
                // Well, crap. No weather. This may be an out of service area. Indicate via a null model.
                model.Conditions = null;
            }

            var predictions = this.Entities.ActiveWeatherPredictionsSelect(zipCode, DateTime.Today);
            foreach (var prediction in predictions)
            {
                var forecast = new WeatherForecastModel
                {
                    Low = prediction.Low,
                    High = prediction.High,
                    SeverityId = prediction.SeverityID,
                    MinutesToDefrost = prediction.MinutesToDefrost,
                    WeatherCodeName = prediction.WeatherCodeName,
                    WeatherCode = prediction.WeatherCodeID,
                    ForecastDate = prediction.PredictionDateUTC,
                    IconClass = prediction.IconClass,
                    Description = prediction.Description
                };

                model.Forecasts.Add(forecast);
            }

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
            var predictions = this.Entities.WeatherFrostDataSelect().ToList();
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