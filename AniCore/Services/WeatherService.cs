using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using Ani.Core.Helpers;
using Ani.Core.Models.Weather;

namespace Ani.Core.Services
{
    public class WeatherService : ServiceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="entities">The database context for working with entity framework.</param>
        public WeatherService(Entities entities) : base(entities)
        {
        }

        /// <summary>
        /// Gets the latest weather result for the specified zip code.
        /// </summary>
        /// <param name="zipCode">The zip code.</param>
        /// <returns>The latest weather result or null if no data is available.</returns>
        public LatestWeatherEntrySelect_Result GetLatestWeatherForZipCode(int zipCode)
        {
            return Entities.LatestWeatherEntrySelect(zipCode).FirstOrDefault();
        }

        /// <summary>
        /// Gets the weather model for a specified zip code.
        /// This will never be null but can have null Conditions or Forecasts if no data is available.
        /// </summary>
        /// <param name="zipCode">The zip code.</param>
        /// <returns>
        /// A weather model for the specified zip code representing the latest weather and forecast info.
        /// </returns>
        public WeatherHomeModel GetWeatherModel(int zipCode)
        {
            var model = new WeatherHomeModel {ZipCode = zipCode};

            var latestRecord = GetLatestWeatherForZipCode(zipCode);
            if (latestRecord != null)
            {
                model.Conditions.WeatherDate = latestRecord.CreatedDateUTC;
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
                model.Conditions.WindCardinalDirection = WindDirectionHelper.GetCardinalDirection(latestRecord.WindDirection);
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

            int index = 0;

            // Start our line graph JSON
            var lowLineData = new StringBuilder("[");
            var highLineData = new StringBuilder("[");

            var predictions = Entities.ActiveWeatherPredictionsSelect(zipCode, DateTime.Today);
            foreach (var prediction in predictions)
            {
                // Build out object data for page binding
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

                // Build out JSON data for line graphs
                if (index > 0)
                {
                    lowLineData.Append(",");
                    highLineData.Append(",");
                }

                lowLineData.AppendFormat("{{\"x\": \"{0}\", \"y\": \"{1}\"}}", index, prediction.Low);
                highLineData.AppendFormat("{{\"x\": \"{0}\", \"y\": \"{1}\"}}", index, prediction.High);
                
                model.Forecasts.Add(forecast);

                index++;
            }

            // End our line graph JSON
            lowLineData.Append("]");
            model.ForecastLowLineData = lowLineData.ToString();

            highLineData.Append("]");
            model.ForecastHighLineData = highLineData.ToString();

            return model;
        }

        /// <summary>
        /// Adds a frost entry from the specified user
        /// </summary>
        /// <param name="user">The user. This user must have Admin access.</param>
        /// <param name="entry">The frost entry.</param>
        public void AddFrostEntry(User user, AddFrostRecordModel entry)
        {
            // Do our permission check
            if (!UserHasRole(user, "Admin"))
            {
                return; // TODO: Should I throw an exception here instead?
            }

            // Insert it into the database
            Entities.WeatherFrostResultsInsert(user.U_ID,
                entry.RainedOvernight,
                entry.SnowedOvernight,
                entry.ActualMinutes,
                entry.ZipCode,
                entry.RecordDate.Date);
        }

        /// <summary>
        /// Builds a new add frost entry model for use in creating a new frost entry.
        /// </summary>
        /// <param name="zipCode">The zip code.</param>
        /// <returns>The add frost entry model.</returns>
        public static AddFrostRecordModel BuildAddFrostEntryModel(int zipCode)
        {
            var model = new AddFrostRecordModel
            {
                RecordDate = DateTime.Today,
                ZipCode = zipCode,
                ActualMinutes = 0.0
            };
            return model;
        }

        /// <summary>
        /// Gets the frost entry data.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Frost entry data.</returns>
        public WeatherFrostListModel GetFrostEntryData(IPrincipal user)
        {
            var model = new WeatherFrostListModel
            {
                CanAddFrostEntry = user.IsInRole("Admin"),
                Items = Entities.WeatherFrostDataSelect().ToList()
            };
            return model;
        }

        /// <summary>
        /// Gets the weather history entries for the specified zip code.
        /// </summary>
        /// <param name="zipCode">The zip code.</param>
        /// <returns>A list of WeatherPredictions for the zip code.</returns>
        public IEnumerable<WeatherPrediction> GetWeatherHistory(int zipCode)
        {
            var predictions = Entities.WeatherPredictions.Where(p => p.WP_ZipCode == zipCode).OrderBy(p => p.WP_PredictionDateUTC);

            return predictions.ToList();
        }
    }
}