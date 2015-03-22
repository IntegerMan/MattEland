using System;
using System.Linq;
using System.Web.Mvc;
using AniWebApp.Models;
using Microsoft.AspNet.Identity;

namespace AniWebApp.Controllers
{
    /// <summary>
    /// The MVC weather controller
    /// </summary>
    [Route("Weather")]
    public class WeatherController : Controller
    {

        private readonly AniEntities _entities = new AniEntities();

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
                zipCode = this.GetUserZipCode();
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

            var predictions = _entities.ActiveWeatherPredictionsSelect(zipCode, DateTime.Today).ToList();
            return View(predictions);
        }

        private int GetUserZipCode()
        {
            User user = GetUserEntity();
            if (user != null)
            {
                return user.U_ZipCode;
            }

            return 43035;
        }

        private string GetUserAspNetId()
        {
            return this.User?.Identity?.GetUserId();
        }

        private User GetUserEntity()
        {
            var aspNetId = this.GetUserAspNetId();

            if (!string.IsNullOrWhiteSpace(aspNetId))
            {
                User user = _entities.Users.FirstOrDefault(u => u.U_ASPNET_ID == aspNetId);
                return user;
            }

            return null;
        }

        private int GetUserId()
        {
            User user = GetUserEntity();
            if (user != null)
            {
                return user.U_ID;
            }

            return 0;
        }

        /// <summary>
        /// Gets data for the weather forecasts page.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [Route(@"Weather/Frost")]
        public ActionResult Frost()
        {
            var predictions = _entities.WeatherFrostPredictionsVsActualsSelect().ToList();
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

            var result = _entities.WeatherFrostResultsInsert(userID,
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