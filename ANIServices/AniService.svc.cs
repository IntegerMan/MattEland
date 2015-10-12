using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MattEland.Ani.AniServices.Contracts;
using MattEland.Ani.AniServices.DataObjects;

namespace MattEland.Ani.AniServices
{
    /// <summary>
    /// A WCF Service for getting information on weather data
    /// </summary>
    public class AniService : IAniService
    {
        private readonly AniEntities _entities = new AniEntities();

        /// <summary>
        /// Gets the number of minutes estimated to scrape frost from a car for the specified zip code and date.
        /// This will error if out of the area of service or not for a time with recorded data present.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="apiKey">The API key.</param>
        /// <param name="zipCode">The zip code.</param>
        /// <param name="predictionDate">The prediction date.</param>
        /// <returns>The number of minutes required to scrape frost from a car in the morning.</returns>
        public double? GetFrostScrapeTimeInMinutes(string userName, string apiKey, int zipCode, DateTime predictionDate)
        {
            // Ensure we're just working with the date
            predictionDate = predictionDate.Date;

            var prediction = _entities.WeatherPredictions.Where(wp => wp.WP_ZipCode == zipCode).FirstOrDefault(wp => DbFunctions.TruncateTime(wp.WP_PredictionDateUTC) == predictionDate);

            return prediction?.WP_MinutesToDefrost;
        }

        /// <summary>
        /// Gets weather data for a specified zip code including current conditions and forecasts.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="apiKey">The API key.</param>
        /// <param name="zipCode">The zip code.</param>
        /// <returns>Weather data for the specified zip code.</returns>
        public WeatherData GetWeatherData(string userName, string apiKey, int zipCode)
        {
            // Build out basic model information
            var model = new WeatherData
            {
                ZipCode = zipCode,
                PressureUnits = "in",
                VisibilityUnits = "mi",
                TemperatureUnits = "F",
                WindUnits = "mph"
            };

            // Include Metadata from the zip code
            var zipData = _entities.ZipCodes.FirstOrDefault(z => z.ID == zipCode);
            if (zipData != null)
            {
                model.City = zipData.Name;
                model.Lat = zipData.Lat;
                model.Long = zipData.Lng;
            }

            var latestRecord = _entities.LatestWeatherEntrySelect(zipCode).FirstOrDefault();
            if (latestRecord != null)
            {
                model.Conditions = new WeatherConditionData
                {
                    WeatherDate = latestRecord.CreatedDateUTC,
                    Temperature = latestRecord.Temperature,
                    Sunrise = latestRecord.Sunrise,
                    Sunset = latestRecord.Sunset,
                    Description = latestRecord.Description,
                    Pressure = latestRecord.Pressure,
                    Humidity = latestRecord.Humidity,
                    Rising = latestRecord.Rising,
                    Visibility = latestRecord.Visibility,
                    WindChill = latestRecord.WindChill,
                    WindDirection = latestRecord.WindDirection,
                    WindCardinalDirection = WindDirectionHelper.GetCardinalDirection(latestRecord.WindDirection),
                    WindSpeed = latestRecord.WindSpeed,
                    WeatherCode = latestRecord.WeatherCode,
                    WeatherCodeName = latestRecord.WeatherCodeName,
                    SeverityId = latestRecord.SeverityID,
                    IconClass = latestRecord.IconClass
                };

            }
            else
            {
                // Well, crap. No weather. This may be an out of service area. Indicate via a null model.
                model.Conditions = null;
            }

            var predictions = _entities.ActiveWeatherPredictionsSelect(zipCode, DateTime.Today).ToList();
            if (predictions.Any())
            {
                model.Forecasts = new List<WeatherForecastData>(predictions.Count());
                foreach (var prediction in predictions)
                {
                    var forecast = new WeatherForecastData()
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
            }
            else
            {
                // No forecsats. Possibly an out of service area.
                model.Forecasts = null;
            }

            return model;
        }

        /*
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
        */
    }
}
